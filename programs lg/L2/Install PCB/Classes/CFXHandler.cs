using CFX;
using CFX.InformationSystem.UnitValidation;
using CFX.Production;
using CFX.Production.Processing;
using CFX.ResourcePerformance;
using CFX.Sensor.Identification;
using CFX.Structures;
using CFX.Structures.JAG;
using CFX.Transport;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace Alpha._0.Classes
{
    public class CFXHandler
    {
        public bool UseConnectionIni = true;
        public bool SaveUnpubilshMessage = true;

        //Endpoint
        public AmqpCFXEndpoint Endpoint;
        public string Handle = "JAG"; //If UseConnectionIni = true, the value will be replaced
        private readonly Uri LocalUri = new Uri(string.Format("amqp://{0}:5671", System.Net.Dns.GetHostName()));

        //For Hardcoding Test (If UseConnectionIni = true, the following values can be ignored)
        private readonly string RemoteURL = "localhost:5672";
        private readonly string RemoteUserName = "jabil";
        private readonly string RemotePassword = "jabil";
        private readonly bool RemotePublishEnable = true;
        private readonly string RemotePublishAddress = "/exchange/amq.fanout";
        private readonly bool RemoteSubscribeEnable = true;
        private readonly string RemoteSubscribeAddress = "/queue/CFX";

        //Production Status
        private string ActiveRecipe = "recipe";
        private Guid WorkingTransactionID = Guid.Empty;
        private int WorkingUnitsCount = 0;
        private int WorkingStageIndex = 0;
        private Stage WorkingStage = null;
        private Guid FaultOccurrenceId = Guid.Empty;
        private bool FaultAcknowledgedDone = false;

        //File Path
        public static string ConnectionsPath = $"{System.IO.Directory.GetCurrentDirectory()}\\CFX_configs\\connections.ini";
        public static string StatusPath = $"{System.IO.Directory.GetCurrentDirectory()}\\CFX_configs\\status.ini";
        public static string UnpublishPath = $"{System.IO.Directory.GetCurrentDirectory()}\\CFX_configs\\unpublish";

        //Background
        private System.Timers.Timer BackgroundTimer;
        private bool StopRunTimer = false;
        private bool StopRunTimerDone = false;

        private Queue<CFXEnvelope> QueMessage;
        private bool PausePublish = false;

        private INIHelper iniStatus = new INIHelper(StatusPath);
        private DateTime OfflineDateTime
        {
            set { iniStatus.WriteIniFile("CFX", "OfflineDateTime", value.ToString()); }
            get { return DateTime.Parse(iniStatus.ReadIniFile("CFX", "OfflineDateTime", DateTime.Now.ToString())); }
        }
        private DateTime LatestOnlineTime
        {
            set { iniStatus.WriteIniFile("CFX", "LatestOnlineTime", value.ToString()); }
            get { return DateTime.Parse(iniStatus.ReadIniFile("CFX", "LatestOnlineTime", DateTime.Now.ToString())); }
        }
        public ResourceState LatestState
        {
            set { iniStatus.WriteIniFile("CFX", "LatestState", value.ToString()); }
            get { return (ResourceState)Enum.Parse(typeof(ResourceState), iniStatus.ReadIniFile("CFX", "LatestState", "NST_ShutdownAndStartup")); }
        }
        private DateTime LatestStateTime
        {
            set { iniStatus.WriteIniFile("CFX", "LatestStateTime", value.ToString()); }
            get { return DateTime.Parse(iniStatus.ReadIniFile("CFX", "LatestStateTime", DateTime.Now.ToString())); }
        }

        //Event
        public delegate void MessageReceivedHandler(Uri Uri, string Address, string Message);
        public event MessageReceivedHandler OnMessageReceived;

        public CFXHandler()
        {
            Endpoint = new AmqpCFXEndpoint();
            QueMessage = new Queue<CFXEnvelope>();
            StartRunBackgroundTimer();

            if (File.Exists(StatusPath))
            {
                //Abnormally stop the program
                if (OfflineDateTime != LatestOnlineTime)
                {
                    StationStateChanged(ResourceState.NST_ShutdownAndStartup, LatestOnlineTime);
                    OfflineDateTime = LatestOnlineTime;
                }
            }
            else
            {
                LatestState = ResourceState.NST_ShutdownAndStartup;
                DateTime Now = DateTime.Now;
                LatestStateTime = Now;
                LatestOnlineTime = Now;
                OfflineDateTime = Now;
            }
        }

        public void StartRunBackgroundTimer()
        {
            StopRunTimer = false;
            StopRunTimerDone = false;
            BackgroundTimer = new System.Timers.Timer();
            BackgroundTimer.Elapsed += BackgroundElapsed;
            BackgroundTimer.Interval = 100;
            BackgroundTimer.Enabled = true;
        }

        public void StopRunBackgroundTimer()
        {
            StopRunTimer = true;
            while (!StopRunTimerDone)
            {
                Thread.Sleep(1);
            }
        }

        private void BackgroundElapsed(object sender, ElapsedEventArgs e)
        {
            ((System.Timers.Timer)sender).Enabled = false;
            LatestOnlineTime = DateTime.Now;
            if (!PausePublish)
            {
                while (QueMessage.Count > 0)
                {
                    try
                    {
                        if (Endpoint.IsOpen)
                            Endpoint.Publish(QueMessage.Peek());
                        else
                            PublishMessageToFile(QueMessage.Peek());
                        QueMessage.Dequeue();
                    }
                    catch (Exception)
                    {
                        PublishMessageToFile(QueMessage.Peek());
                        QueMessage.Dequeue();
                    }
                    Thread.Sleep(1);
                }
                if (StopRunTimer)
                {
                    StopRunTimerDone = true;
                    BackgroundTimer.Enabled = false;
                }
            }
            ((System.Timers.Timer)sender).Enabled = true;
        }

        private void PublishMessageToFile(CFXEnvelope Message)
        {
            if (SaveUnpubilshMessage)
            {
                string FilePath = $"{UnpublishPath}\\{DateTime.Now.ToString("yyyyMMdd")}.cfxmsg";
                if (!Directory.Exists(Path.GetDirectoryName(FilePath)))
                    Directory.CreateDirectory(Path.GetDirectoryName(FilePath));
                if (!File.Exists(FilePath))
                {
                    StreamWriter sw = new StreamWriter(FilePath, false);
                    sw.Close();
                }
                File.AppendAllText(FilePath, Message.ToJson() + Environment.NewLine);
            }
        }

        private void PublishMessageFromFile()
        {
            PausePublish = true;
            if (!Directory.Exists(UnpublishPath) || !Endpoint.IsOpen) return;
            List<CFXEnvelope> MessageList = new List<CFXEnvelope>();
            DirectoryInfo DirInfo = new DirectoryInfo(UnpublishPath);
            FileInfo[] FileInfo = DirInfo.GetFiles("*.cfxmsg");
            foreach (FileInfo FileInformation in FileInfo)
            {
                string[] JSONMessages = System.IO.File.ReadAllLines(FileInformation.FullName);
                foreach (string JSONMessage in JSONMessages)
                {
                    if (JSONMessage != string.Empty)
                    {
                        CFXEnvelope Message = JsonConvert.DeserializeObject<CFXEnvelope>(JSONMessage);
                        MessageList.Add(Message);
                    }
                }
                FileInformation.Delete();
            }
            if (MessageList.Count > 0)
                Endpoint.PublishMany(MessageList);
            PausePublish = false;
        }

        public void OpenEndpoint()
        {
            CloseEndpoint();
            try
            {
                Endpoint = new AmqpCFXEndpoint();
                Endpoint.OnCFXMessageReceived += Endpoint_OnCFXMessageReceived;
                Endpoint.OnRequestReceived += Endpoint_OnRequestReceived;

                if (UseConnectionIni && File.Exists(ConnectionsPath))
                {
                    INIHelper Ini = new INIHelper(ConnectionsPath);
                    Handle = Ini.ReadIniFile("Endpoint", "Handle", Handle);
                    Endpoint.Open(Handle, LocalUri);
                    if (Endpoint.IsOpen)
                    {
                        int PublishIndex = 1;
                        while (Ini.ReadIniFile($"Publish{PublishIndex.ToString()}", "Url", string.Empty) != string.Empty)
                        {
                            if (Convert.ToBoolean(Ini.ReadIniFile($"Publish{PublishIndex.ToString()}", "Enable", "false")))
                            {
                                AmqpChannelAddress PublishChannelAddress = new AmqpChannelAddress()
                                {
                                    Uri = new Uri(string.Format("amqp://{0}:{1}@{2}",
                                    Ini.ReadIniFile($"Publish{PublishIndex.ToString()}", "UserName", RemoteUserName),
                                    Ini.ReadIniFile($"Publish{PublishIndex.ToString()}", "Password", RemotePassword),
                                    Ini.ReadIniFile($"Publish{PublishIndex.ToString()}", "Url", RemoteURL))),
                                    Address = Ini.ReadIniFile($"Publish{PublishIndex.ToString()}", "Address", RemotePublishAddress)
                                };
                                Endpoint.AddPublishChannel(PublishChannelAddress);
                            }
                            PublishIndex++;
                        }
                        int SubscribeIndex = 1;
                        while (Ini.ReadIniFile($"Subscribe{SubscribeIndex.ToString()}", "Url", string.Empty) != string.Empty)
                        {
                            if (Convert.ToBoolean(Ini.ReadIniFile($"Subscribe{SubscribeIndex.ToString()}", "Enable", "false")))
                            {
                                AmqpChannelAddress SubscribeChannelAddress = new AmqpChannelAddress()
                                {
                                    Uri = new Uri(string.Format("amqp://{0}:{1}@{2}",
                                    Ini.ReadIniFile($"Subscribe{SubscribeIndex.ToString()}", "UserName", RemoteUserName),
                                    Ini.ReadIniFile($"Subscribe{SubscribeIndex.ToString()}", "Password", "guest"),
                                    Ini.ReadIniFile($"Subscribe{SubscribeIndex.ToString()}", "Url", RemoteURL))),
                                    Address = Ini.ReadIniFile($"Subscribe{SubscribeIndex.ToString()}", "Address", RemoteSubscribeAddress)
                                };
                                Endpoint.AddSubscribeChannel(SubscribeChannelAddress);
                            }
                            SubscribeIndex++;
                        }
                    }
                }
                else
                {
                    Endpoint.Open(Handle, LocalUri);
                    if (Endpoint.IsOpen)
                    {
                        if (RemotePublishEnable)
                        {
                            AmqpChannelAddress PublishChannelAddress = new AmqpChannelAddress()
                            { Uri = new Uri(string.Format("amqp://{0}:{1}@{2}", RemoteUserName, RemotePassword, RemoteURL)), Address = RemotePublishAddress };
                            Endpoint.AddPublishChannel(PublishChannelAddress);
                        }
                        if (RemoteSubscribeEnable)
                        {
                            AmqpChannelAddress SubscribeChannelAddress = new AmqpChannelAddress()
                            { Uri = new Uri(string.Format("amqp://{0}:{1}@{2}", RemoteUserName, RemotePassword, RemoteURL)), Address = RemoteSubscribeAddress };
                            Endpoint.AddSubscribeChannel(SubscribeChannelAddress);
                        }
                    }
                }
                if (Endpoint.IsOpen)
                {
                    EndpointConnected();
                    Task.Factory.StartNew(PublishMessageFromFile);
                }
            }
            catch (Exception) { Console.WriteLine("failed to open endpoint"); }
        }

        public void CloseEndpoint()
        {
            if (Endpoint.IsOpen)
            {
                EndpointShuttingDown();
                Task.Factory.StartNew(() =>
                {
                    Thread.Sleep(3000);
                    Endpoint.OnCFXMessageReceived -= Endpoint_OnCFXMessageReceived;
                    Endpoint.OnRequestReceived -= Endpoint_OnRequestReceived;
                    Endpoint.Close();
                });
            }
        }

        public void Publish(CFXEnvelope Message)
        {
            QueMessage.Enqueue(Message);
        }

        private void Endpoint_OnCFXMessageReceived(AmqpChannelAddress Source, CFX.CFXEnvelope Message)
        {
            if (OnMessageReceived != null)
            {
                OnMessageReceived(Source.Uri, Source.Address, Message.ToJson());
            }
        }

        private CFXEnvelope Endpoint_OnRequestReceived(CFXEnvelope Request)
        {
            CFXMessage ResponseMessage = null;
            switch (Request.MessageName)
            {
                case "CFX.AreYouThereRequest":
                    ResponseMessage = AreYouThereResponse(Request);
                    break;
                case "CFX.GetEndpointInformationRequest":
                    ResponseMessage = GetEndpointInformationResponse(Request);
                    break;
                case "CFX.WhoIsThereRequest":
                    ResponseMessage = WhoIsThereResponse(Request);
                    break;
                case "CFX.Production.GetActiveRecipeRequest":
                    ResponseMessage = GetActiveRecipeResponse(Request);
                    break;
                case "CFX.InformationSystem.UnitValidation.ValidateUnitsRequest":
                    ResponseMessage = ValidateUnitsResponse(Request);
                    break;
                default:
                    ResponseMessage = NotSupportedResponse(Request);
                    break;
            }
            CFXEnvelope ResponseEnvelop = CFXEnvelope.FromCFXMessage(ResponseMessage);
            ResponseEnvelop.Source = Handle;
            ResponseEnvelop.Target = Request.Source;
            return ResponseEnvelop;
        }

        public void AddPublishChannel(string URL, string UserName, string Password, string TargetAddress)
        {
            if (Endpoint.IsOpen)
            {
                Uri uri = new Uri(string.Format("amqp://{0}:{1}@{2}", UserName, Password, URL));
                Endpoint.AddPublishChannel(uri, TargetAddress);
            }
        }

        public void AddSubscribeChannel(string URL, string UserName, string Password, string TargetAddress)
        {
            if (Endpoint.IsOpen)
            {
                Uri uri = new Uri(string.Format("amqp://{0}:{1}@{2}", UserName, Password, URL));
                Endpoint.AddSubscribeChannel(uri, TargetAddress);
            }
        }

        #region Publish Envelope
        private void EndpointConnected()
        {
            EndpointConnected EndpointConnected = new EndpointConnected()
            {
                CFXHandle = Handle,
                RequestNetworkUri = LocalUri.AbsoluteUri,
            };
            Publish(new CFXEnvelope(EndpointConnected));
        }

        private void EndpointShuttingDown()
        {
            EndpointShuttingDown EndpointShuttingDown = new EndpointShuttingDown() { CFXHandle = Handle };
            Publish(new CFXEnvelope(EndpointShuttingDown));
        }

        public void StationOnline()
        {
            StationOnline StationOnline = new StationOnline() { OfflineDuration = DateTime.Now - OfflineDateTime };
            Publish(new CFXEnvelope(StationOnline));
            LatestOnlineTime = DateTime.Now;
        }

        public void StationOffline()
        {
            StationOffline StationOffline = new StationOffline();
            Publish(new CFXEnvelope(StationOffline));
            OfflineDateTime = LatestOnlineTime = DateTime.Now;
        }

        public void StationStateChanged(ResourceState State, DateTime StateTime)
        {
            StationStateChanged StationStateChanged = new StationStateChanged()
            {
                OldState = LatestState,
                OldStateDuration = StateTime - LatestStateTime,
                NewState = State,
            };
            Publish(new CFXEnvelope(StationStateChanged));
            LatestState = State;
            LatestStateTime = StateTime;
        }

        public void RecipeActivated(string RecipeName)
        {
            ActiveRecipe = RecipeName;
            RecipeActivated RecipeActivated = new RecipeActivated() { RecipeName = RecipeName };
            Publish(new CFXEnvelope(RecipeActivated));
        }

        public void WorkStarted(string Identifier, int UnitCount = 1)
        {
            List<UnitPosition> Units = new List<UnitPosition>();
            if (UnitCount >= 1)
            {
                for (int i = 1; i <= UnitCount; i++)
                {
                    Units.Add(new UnitPosition() { PositionNumber = i });
                }
                WorkStarted WorkStarted = new WorkStarted()
                {
                    PrimaryIdentifier = Identifier,
                    Units = Units
                };
                Publish(new CFXEnvelope(WorkStarted));
                WorkingTransactionID = WorkStarted.TransactionID;
                WorkingUnitsCount = UnitCount;
                WorkingStageIndex = 1;
            }
        }

        public void WorkCompleted(string Identifier, List<int> FailedIndex = null, List<int> AbortedIndex = null)
        {
            if (WorkingTransactionID != Guid.Empty)
            {
                WorkCompleted WorkCompleted_Pass = new WorkCompleted()
                {
                    TransactionID = WorkingTransactionID,
                    PrimaryIdentifier = Identifier,
                    Result = WorkResult.Completed
                };
                WorkCompleted WorkCompleted_Failed = new WorkCompleted()
                {
                    TransactionID = WorkingTransactionID,
                    PrimaryIdentifier = Identifier,
                    Result = WorkResult.Failed
                };
                WorkCompleted WorkCompleted_Aborted = new WorkCompleted()
                {
                    TransactionID = WorkingTransactionID,
                    PrimaryIdentifier = Identifier,
                    Result = WorkResult.Aborted
                };
                for (int i = 1; i <= WorkingUnitsCount; i++)
                {
                    if (FailedIndex != null && FailedIndex.Contains(i))
                    {
                        WorkCompleted_Failed.Units.Add(new UnitPosition() { PositionNumber = i });
                    }
                    else if (AbortedIndex != null && AbortedIndex.Contains(i))
                    {
                        WorkCompleted_Aborted.Units.Add(new UnitPosition() { PositionNumber = i });
                    }
                    else
                    {
                        WorkCompleted_Pass.Units.Add(new UnitPosition() { PositionNumber = i });
                    }
                }
                if (WorkCompleted_Pass.UnitCount != 0) Publish(new CFXEnvelope(WorkCompleted_Pass));
                if (WorkCompleted_Failed.UnitCount != 0) Publish(new CFXEnvelope(WorkCompleted_Failed));
                if (WorkCompleted_Aborted.UnitCount != 0) Publish(new CFXEnvelope(WorkCompleted_Aborted));
                WorkingTransactionID = Guid.Empty;
            }
        }

        public void WorkStageStarted(string ProcessName)
        {
            if (WorkingTransactionID != Guid.Empty)
            {
                WorkingStage = new Stage() { StageName = ProcessName, StageSequence = WorkingStageIndex };
                WorkStageStarted WorkStageStarted = new WorkStageStarted()
                {
                    TransactionID = WorkingTransactionID,
                    Stage = WorkingStage,
                };
                Publish(new CFXEnvelope(WorkStageStarted));
            }
        }

        public void WorkStageCompleted()
        {
            if (WorkingTransactionID != Guid.Empty && WorkingStage != null)
            {
                WorkStageCompleted WorkStageCompleted = new WorkStageCompleted()
                {
                    TransactionID = WorkingTransactionID,
                    Stage = WorkingStage
                };
                Publish(new CFXEnvelope(WorkStageCompleted));
                WorkingStageIndex++;
                WorkingStage = null;
            }
        }

        public void UnitProcessed(JAGProcessData ProcessData, ProcessingResult Result)
        {
            UnitsProcessed UnitsProcessed = new UnitsProcessed()
            {
                TransactionId = Guid.NewGuid(),
                CommonProcessData = ProcessData,
                OverallResult = Result
            };
            Publish(new CFXEnvelope(UnitsProcessed));
        }

        public void FaultOccurred(Fault Fault)
        {
            if (FaultOccurrenceId == Guid.Empty)
            {
                Fault.TransactionID = WorkingTransactionID;
                FaultOccurred FaultOccurred = new FaultOccurred()
                {
                    Fault = Fault
                };
                Publish(new CFXEnvelope(FaultOccurred));
                FaultOccurrenceId = Fault.FaultOccurrenceId;
                FaultAcknowledgedDone = false;
            }
        }

        public void FaultAcknowledged(string LoginName)
        {
            if(FaultOccurrenceId != Guid.Empty && !FaultAcknowledgedDone)
            {
                FaultAcknowledged FaultAcknowledged = new FaultAcknowledged()
                {
                    Operator = new Operator()
                    {
                        LoginName = LoginName
                    },
                    FaultOccurrenceId = FaultOccurrenceId
                };
                Publish(new CFXEnvelope(FaultAcknowledged));
                FaultAcknowledgedDone = true;
            }
        }

        public void FaultCleared(string LoginName)
        {
            if (FaultOccurrenceId != Guid.Empty)
            {
                Publish(new CFXEnvelope(new FaultCleared()
                {
                    Operator = new Operator() { LoginName = LoginName },
                    FaultOccurrenceId = FaultOccurrenceId,
                    ClearedAt = DateTime.Now
                }));
                FaultOccurrenceId = Guid.Empty;
            }
        }
   
        #endregion

        #region Request & Response
        private CFXMessage NotSupportedResponse(CFXEnvelope Request)
        {
            CFXMessage ResponseMessage = new NotSupportedResponse
            {
                RequestResult = new RequestResult
                {
                    Result = StatusResult.Failed,
                    ResultCode = 0,
                    Message = string.Format("{0} request not implement", Request.MessageBody)
                }
            };
            return ResponseMessage;
        }

        public bool AreYouThereRequest(string RemoteUri, string TargetHandle)
        {
            CFXEnvelope Request = new CFXEnvelope(new AreYouThereRequest()
            { CFXHandle = this.Handle, })
            {
                Source = Handle,
                Target = TargetHandle,
            };
            try
            {
                CFXEnvelope Response = Endpoint.ExecuteRequest(RemoteUri, Request);
                if (Response.GetMessage<AreYouThereResponse>().Result.Result == StatusResult.Success)
                    return true;
            }
            catch (Exception) { }
            return false;
        }

        private CFXMessage AreYouThereResponse(CFXEnvelope Request)
        {
            CFXMessage Response = new AreYouThereResponse()
            {
                Result = new RequestResult
                {
                    Result = StatusResult.Success,
                    ResultCode = 0,
                    Message = "AreYouThereRequest response success"
                },
                CFXHandle = Handle,
                RequestNetworkUri = LocalUri.AbsoluteUri,
            };
            return Response;
        }

        public Endpoint GetEndpointInformationRequest(string RemoteUri, string TargetHandle)
        {
            CFXEnvelope Request = new CFXEnvelope(new GetEndpointInformationRequest()
            { CFXHandle = this.Handle, })
            {
                Source = Handle,
                Target = TargetHandle,
            };
            try
            {
                CFXEnvelope Response = Endpoint.ExecuteRequest(RemoteUri, Request);
                if (Response.GetMessage<GetEndpointInformationResponse>().Result.Result == StatusResult.Success)
                {
                    Endpoint info = Response.GetMessage<GetEndpointInformationResponse>().EndpointInformation;
                    return info;
                }
            }
            catch (Exception) { }
            return null;
        }

        private CFXMessage GetEndpointInformationResponse(CFXEnvelope Request)
        {
            CFXMessage Response = new GetEndpointInformationResponse()
            {
                Result = new RequestResult
                {
                    Result = StatusResult.Success,
                    ResultCode = 0,
                    Message = "GetEndpointInformationResponse response success"
                },
                EndpointInformation = new CFX.Structures.Endpoint
                {
                    CFXHandle = this.Handle,
                    CFXVersion = "None",
                    RequestNetworkUri = LocalUri.AbsoluteUri,
                    RequestTargetAddress = "None",
                    UniqueIdentifier = "None",
                    FriendlyName = "None",
                    Vendor = "JABIL EMS Automation",
                    ModelNumber = "None",
                    SerialNumber = "None",
                    SoftwareVersion = "v1.0.0.1",
                    FirmwareVersion = "None",
                    OperatingSystem = CFX.Structures.OperatingSystem.Windows10,
                    OperatingSystemPlatform = OperatingSystemPlatform.Platform64bit,
                    OperatingSystemVersion = System.Environment.OSVersion.VersionString,
                    NumberOfLanes = 1,
                }
            };
            return Response;
        }

        public string WhoIsThereRequest(string RemoteUri, string TargetHandle)
        {
            CFXEnvelope Request = new CFXEnvelope(new WhoIsThereRequest()
            { SupportedTopicQueryType = SupportedTopicQueryType.All })
            {
                Source = Handle,
                Target = TargetHandle,
            };
            try
            {
                CFXEnvelope Response = Endpoint.ExecuteRequest(RemoteUri, Request);
                if (Response.GetMessage<WhoIsThereResponse>().Result.Result == StatusResult.Success)
                {
                    string responseHandle = Response.GetMessage<WhoIsThereResponse>().CFXHandle;
                    return responseHandle;
                }
            }
            catch (Exception) { }
            return null;
        }

        private CFXMessage WhoIsThereResponse(CFXEnvelope Request)
        {
            CFXMessage Response = new WhoIsThereResponse()
            {
                Result = new RequestResult
                {
                    Result = StatusResult.Success,
                    ResultCode = 0,
                    Message = "WhoIsThereResponse response success"
                },
                CFXHandle = Handle,
                RequestNetworkUri = LocalUri.AbsoluteUri,
            };
            return Response;
        }

        public string GetActiveRecipeRequest(string RemoteUri, string TargetHandle)
        {
            CFXEnvelope Request = new CFXEnvelope(new GetActiveRecipeRequest())
            {
                Source = Handle,
                Target = TargetHandle,
            };
            try
            {
                CFXEnvelope Response = Endpoint.ExecuteRequest(RemoteUri, Request);
                if (Response.GetMessage<GetActiveRecipeResponse>().Result.Result == StatusResult.Success)
                {
                    string ActiveRecipe = Response.GetMessage<GetActiveRecipeResponse>().ActiveRecipeName;
                    return ActiveRecipe;
                }
            }
            catch (Exception) { }
            return string.Empty;
        }

        private CFXMessage GetActiveRecipeResponse(CFXEnvelope Request)
        {
            CFXMessage Response = new GetActiveRecipeResponse()
            {
                Result = new RequestResult
                {
                    Result = StatusResult.Success,
                    ResultCode = 0,
                    Message = "GetActiveRecipeResponse response success"
                },
                ActiveRecipeName = this.ActiveRecipe,
            };
            return Response;
        }

        public bool ValidateUnitsRequest(string RemoteUri, string TargetHandle, string SerialNumber)
        {
            CFXEnvelope Request = new CFXEnvelope(new ValidateUnitsRequest() { PrimaryIdentifier = SerialNumber })
            {
                Source = Handle,
                Target = TargetHandle,
            };
            try
            {
                CFXEnvelope Response = Endpoint.ExecuteRequest(RemoteUri, Request);
                return (Response.GetMessage<ValidateUnitsResponse>().PrimaryResult.Result == ValidationStatus.Passed);
            }
            catch (Exception) { }
            return false;
        }

        private CFXMessage ValidateUnitsResponse(CFXEnvelope Request)
        {
            CFXMessage Response = new ValidateUnitsResponse()
            {
                PrimaryResult = new ValidationResult
                {
                    Result = ValidationStatus.Passed,
                    Message = "ValidateUnitsResponse response success"
                },
            };
            return Response;
        }
        #endregion
    }
}
