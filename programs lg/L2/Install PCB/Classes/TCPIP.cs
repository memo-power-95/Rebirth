using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Collections.Generic;

/// <summary>
/// Implements a TCP/IP communication
/// </summary>
public class TCPIP
{
    // State object for reading client data asynchronously  
    public class StateObject
    {
        // Client clientSocket.  
        public Socket workSocket = null;
        // Size of receive buffer.  
        public const int BufferSize = 1024;
        // Receive buffer.  
        public byte[] buffer = new byte[BufferSize];
        // Received data string.  
        public StringBuilder sb = new StringBuilder();
    }

   

    /// <summary>
    /// Gets the data received from the client
    /// </summary>
    public string listenerData { get; set; }
    /// <summary>
    /// Gets a value that indicates whether the remote device sent data
    /// </summary>
    public bool listenerDataReceived { get; set; }

    public Socket clientSocket;
    public IPAddress IPAddress;
    /// <summary>
    /// Gets the IP of the remote device
    /// </summary>
    public string clientIP { get; set; }
    /// <summary>
    /// Gets the port of the remote device
    /// </summary>
    public int clientPort { get; set; }

    /// <summary>
    /// Connects to the remote device
    /// </summary>
    /// 
    public TCPIP()
    {
        clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        IPAddress = IPAddress.Parse("192.168.0.1");
    }
    public bool fnConnectClient()
    {
        try
        {
            if (clientSocket != null)
                if(!clientSocket.Connected)
                    clientSocket.Connect("192.168.0.1", 23);
            return true;
        }
        catch (Exception ex)
        {
            //ex.StackTrace.ToString();
            

            Console.WriteLine(ex.Message);
        }
        return false;
    }

    /// <summary>
    /// Connects to the device asynchronously
    /// </summary>
    public async Task<bool> fnConnectClientAsync()
    {
        return await Task.Run(() => { return fnConnectClient(); }).ConfigureAwait(false);
    }

    /// <summary>
    /// Sends data asynchronously to the remote device and receives the response of the device
    /// </summary>
    /// <param name="data">Data to send</param>
    public string fnClientSendReceive(string dataToSend, int receiveLength)
    {
        return fnClientSend(dataToSend) ? fnClientReceive(receiveLength) : "";
    }

    /// <summary>
    /// Sends data asynchronously to the remote device and receives the response of the device
    /// </summary>
    /// <param name="data">Data to send</param>
    public async Task<string> fnClientSendReceiveAsync(string dataToSend, int receiveLength)
    {
        return await Task.Run(() => { return fnClientSendReceive(dataToSend, receiveLength); }).ConfigureAwait(false);
    }

    /// <summary>
    /// Sends data to the remote device
    /// </summary>
    /// <param name="data">Data to send</param>
    public bool fnClientSend(string dataToSend)
    {
        try
        {
            byte[] byteData = Encoding.ASCII.GetBytes(dataToSend);
           if(clientSocket.Connected)
            {
                clientSocket?.Send(byteData);
                return true;
            }
            
        }
        catch (Exception ex)
        {
            fnClientDisconnect();
            
            fnConnectClient();
            Console.WriteLine(ex.Message);
        }

        return false;
    }

    /// <summary>
    /// Sends data asynchronously to the remote device
    /// </summary>
    /// <param name="data">Data to send</param>
    public async Task<bool> fnClientSendAsync(string dataToSend)
    {
        return await Task.Run(() => { return fnClientSend(dataToSend); }).ConfigureAwait(false);
    }


    /// <summary>
    /// Receives the response of the remote device
    /// </summary>
    public string fnClientReceive(int length)
    {
        try
        {
            List<byte> bytes = new List<byte>();
            byte[] buffer = new byte[length];
            clientSocket?.Receive(buffer);
            return Encoding.ASCII.GetString(buffer);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return "";
    }

    /// <summary>
    /// Receives the response of the remote device asynchronously
    /// </summary>
    public async Task<string> ReceiveAsync(int length)
    {
        return await Task.Run(() => { return fnClientReceive(length); }).ConfigureAwait(false);
    }

    /// <summary>
    /// Closes the connection
    /// </summary>
    /// <returns>True if the connection closed successfully</returns>
    public void fnClientDisconnect()
    {
        if (clientSocket != null)
        {
            if (clientSocket.Connected)
            {
                //clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Disconnect(true);
                clientSocket.Close();
                clientSocket = null;
                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            }
            else
            {

                clientSocket = null;
                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            }
        }
        else
        {

            clientSocket = null;
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        }

        
    }


    /// <summary>
    /// Closes the connection
    /// </summary>
    /// <returns>True if the connection closed successfully</returns>
    public async Task fnClientDisconnectAsync()
    {
        await Task.Run(() => { fnClientDisconnect(); }).ConfigureAwait(false);
    }

    /// <summary>
    /// Starts listening for incoming connections
    /// </summary>
    public void fnStartListening(string listenerIP, int listenerPort)
    {
        IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Parse(listenerIP), listenerPort);
        Socket listener = new Socket(AddressFamily.InterNetwork,
            SocketType.Stream, ProtocolType.Tcp);

        try
        {
            listener.Bind(localEndPoint);
            listener.Listen(100);
            listener.BeginAccept(new AsyncCallback(AcceptCallback), listener);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    public void AcceptCallback(IAsyncResult ar)
    {
        // Get the clientSocket that handles the client request.  
        Socket listener = (Socket)ar.AsyncState;
        Socket handler = listener.EndAccept(ar);
        StateObject state = new StateObject();
        state.workSocket = handler;

        // Begin receiving data
        handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallback), state);
        // Listen for more connections
        listener.BeginAccept(new AsyncCallback(AcceptCallback), listener);
    }

    public void ReadCallback(IAsyncResult ar)
    {
        // Retrieve the state object and the handler clientSocket from the asynchronous state object.  
        StateObject state = (StateObject)ar.AsyncState;
        Socket handler = state.workSocket;

        // Read data from the client clientSocket.   
        int bytesRead = handler.EndReceive(ar);

        if (bytesRead > 0)
        {
            listenerDataReceived = true;
            listenerData = Encoding.ASCII.GetString(state.buffer, 0, bytesRead);
            // Echo the data back to the client.  
            Send(handler, listenerData);
        }
    }

    /// <summary>
    /// Sends data to the a clientSocket
    /// </summary>
    /// <param name="handler">Socket where to send the data</param>
    /// <param name="data">Data to send</param>
    private void Send(Socket handler, string data)
    {
        byte[] byteData = Encoding.ASCII.GetBytes(data);
        handler.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), handler);
    }

    private void SendCallback(IAsyncResult ar)
    {
        try
        {
            // Retrieve the clientSocket from the state object.  
            Socket handler = (Socket)ar.AsyncState;

            // Complete sending the data to the remote device.  
            int bytesSent = handler.EndSend(ar);
            //handler.Shutdown(SocketShutdown.Both);
            handler.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }
}
