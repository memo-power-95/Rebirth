using NPSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcuraIOT;


    public static class AlarmExtensionsV2
    {
        public static List<ErrorData> ErrorList { get; set; } = new List<ErrorData>();

        public static void ReportAlarm(this Alarm alarmClass, string Code, ErrorGroup Group, ErrorSubGroup SubGroup, ErrorType Type, string Content = null)
        {
            SDKKernal.ShowAlarm(Code, Content);
            
            if (!ErrorList.Exists(e => e.Code == Code))
            {
                string description = "";

                if (Content == null)
                {
                    var alarm = alarmClass.ArmUIList.Where(a => a.Code == Code);

                    if (alarm.Count() > 0)
                        description = Content ?? alarm.First().Content;
                }
                else
                    description = Content;
                ErrorList.Add(new ErrorData(Code, Group, SubGroup, Type, DateTime.UtcNow, description));
            }
        }

        public static void ReportClearAlarm(this Alarm alarmClass, string Code)
        {
            var alarms = alarmClass.ArmUIList.FindAll(a => a.Code == Code);

            foreach (var alarm in alarms)
            {
                bool hasAlarmsWithSameType = alarmClass.ArmUIList.Count(a => a.Type == alarm.Type) > 1;

                if (!hasAlarmsWithSameType)
                {
                    switch (alarm.Type)
                    {
                        case "W":
                            alarmClass._WarningNow = false;
                            break;
                        case "E":
                            alarmClass._ErrorNow = false;
                            break;
                        case "I":
                            alarmClass._InformNow = false;
                            break;
                    }
                }

                alarmClass.ArmUIList.Remove(alarm);
            }


        if (alarms.Count > 0)
            alarmClass.DoRefresh = true;

        var error = ErrorList.SingleOrDefault(e => e.Code == Code);

            if (error != null)
            {
                error.EndDatetime = DateTime.UtcNow;
                AcuraCloudServices.ReportErrorAsync(error.Code, error.Description, error.Group, error.SubGroup, error.Type, error.StartDatetime, error.EndDatetime);
                ErrorList.Remove(error);
            }
        }

        public static void ReportClearAllAlarm(this Alarm alarmClass)
        {
            foreach (var alarm in alarmClass.ArmUIList)
            {
                var error = ErrorList.SingleOrDefault(e => e.Code == alarm.Code);

                if (error != null)
                {
                    error.EndDatetime = DateTime.UtcNow;
                    AcuraCloudServices.ReportErrorAsync(error.Code, error.Description, error.Group, error.SubGroup, error.Type, error.StartDatetime, error.EndDatetime);
                    ErrorList.Remove(error);
                }
            }

            alarmClass.ClearAll();
        }
    }

    

