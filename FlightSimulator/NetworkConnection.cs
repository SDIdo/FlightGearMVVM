using FlightSimulator.Model.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.ComponentModel;
using System.IO;

namespace FlightSimulator
{
    /// <summary>
    /// Class responsible for communication client and server wise
    /// </summary>
    public class NetworkConnection : ITelnetClient
    {
        static readonly string localHost = "localhost";
        static readonly string personalIp = "127.0.0.1";
        static readonly int tryPulse = 500;
        public static Mutex mutex = new Mutex();

        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Function uses an event to update about a property change
        /// </summary>
        /// <param name="str">name of the property</param>
        public void NotifyPropertyChanged(string str)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(str));
        }
        private string infoString;

        public string InfoString
        {
            get { return infoString; }
            set
            {
                infoString = value;
                NotifyPropertyChanged(infoString);
            }
        }

        private TcpClient myTcpClient;
        private TcpListener myTcpListener;
        public volatile bool stop = true;
        /// <summary>
        /// Connect opens program as server and looks for a server to reach
        /// </summary>
        /// <param name="ip">adress of ip</param>
        /// <param name="receivePort"></param>
        /// <param name="sendPort"></param>
        public void Connect(string ip, int receivePort, int sendPort)
        {
            if (!stop)
            {
                return;
            }
            stop = false;
            var ipNum = Dns.GetHostEntry(ip).AddressList[1];
            myTcpListener = new TcpListener(ipNum, receivePort); // set server.
            this.Start();

            string sendToIp = Properties.Settings.Default.FlightServerIP;
            if (sendToIp == personalIp)
            {
                sendToIp = localHost;
            }
            int sendToPort = Properties.Settings.Default.FlightCommandPort;
            myTcpClient = new TcpClient();
            {
                Thread connectThread = new Thread(() => //thread will try and reach a server
                {
                    while (!myTcpClient.Connected)
                    {
                        try
                        {
                            myTcpClient.Connect(sendToIp, sendToPort);
                            Thread.Sleep(tryPulse);
                        }
                        catch (Exception)
                        {
                            /** Keep trying */
                        }
                    }
                    /** Upon reaching here program has been conected */
                });
                connectThread.IsBackground = true;
                connectThread.Start();
            }
        }
        /// <summary>
        /// This function closes program as a server and a client.
        /// </summary>
        public void Disconnect()
        {
            if (stop)
            {
                return;
            }
            stop = true;
            this.myTcpClient.Close();
        }
        /// <summary>
        /// This function reads info from connected clients as long as not disconnected.
        /// </summary>
        public void Read()
        {
            try
            {
                myTcpListener.Start();
            }
            catch (Exception)
            {
                /** Could not open program as server */
            }

            TcpClient readTcpClient = myTcpListener.AcceptTcpClient();  //goes to sleep until interupt

            string information = "";

            using (var reader = new StreamReader(readTcpClient.GetStream(), Encoding.UTF8, true))
            {
                while (!stop)
                {
                    mutex.WaitOne();

                    information = reader.ReadLine();
                    this.InfoString = information;
                    mutex.ReleaseMutex();
                }
            }
            this.myTcpListener.Stop();
        }
        /// <summary>
        /// This function Writes a command to server
        /// </summary>
        /// <param name="command">command to server</param>
        public void Write(string command)
        {
            mutex.WaitOne();    //one command at a time
            NetworkStream writeStream = this.myTcpClient.GetStream();  //creates a network stream
            int byteCount = Encoding.ASCII.GetByteCount(command); //how many bytes
            byte[] sendData = new byte[byteCount];  //create a buffer

            sendData = Encoding.ASCII.GetBytes(command);   //puts the message in the buffer

            writeStream.Write(sendData, 0, sendData.Length); //network stream to transfer what's in buffer
            writeStream.Flush();    //cleans the buffer
            mutex.ReleaseMutex();
        }
        /// <summary>
        /// This function opens a server for reading from client on a new thread. 
        /// </summary>
        public void Start()
        {
            // Opens a thread which reads information from server.
            new Thread(() =>
           {
                   this.Read();
           }).Start();
        }
    }
}
