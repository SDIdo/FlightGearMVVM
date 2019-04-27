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
    public class NetworkConnection : ITelnetClient
    {
        public static Mutex mutex = new Mutex();

        public event PropertyChangedEventHandler PropertyChanged;

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
                Console.WriteLine("@@@@@@@@ This is being sent: " + value);
                infoString = value;
                NotifyPropertyChanged(infoString);
            }
        }

        private TcpClient myTcpClient;
        private TcpListener myTcpListener;
        public volatile bool stop = true;

        public void Connect(string ip, int infoPort, int commandPort)
        {
            if (!stop)
            {
                return;
            }
            stop = false;
            var ipNum = Dns.GetHostEntry(ip).AddressList[1];
            myTcpListener = new TcpListener(ipNum, infoPort); // set server.
            this.Start(); //

            string sendToIp = Properties.Settings.Default.FlightServerIP;
            if (sendToIp == "127.0.0.1")  // @TODO: later get host by address.
            {
                sendToIp = "localhost";
            }
            int sendToPort = Properties.Settings.Default.FlightCommandPort;
            myTcpClient = new TcpClient();

            new Thread(() =>
            {
                while (!myTcpClient.Connected)
                {
                    try
                    {
                        myTcpClient.Connect(sendToIp, sendToPort);
                        Thread.Sleep(500);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Was an attempt to connect\n");
                    }
                }
            }).Start();
            MessageBox.Show("connected");
        }

        /**
         * This function disconnects the open server for reading from client.
         */
        public void Disconnect()
        {
            if (stop)
            {
                return;
            }
            stop = true;
            this.myTcpClient.Close();
        }

        /**
         * This function reads info from connected clients as long as not disconnected.
         */
        public void Read()
        {

            try
            {
                myTcpListener.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.Read();
            }

            TcpClient readTcpClient = myTcpListener.AcceptTcpClient();  //goes to sleep until interupt
            MessageBox.Show("client accepted :)");
            //int index = 0;
            //int n = 0;
            //string remainder = "";
            //string backRemainder = "";
            string information = "";
            //bool isDataEnd = false;

            using (var reader = new StreamReader(readTcpClient.GetStream(), Encoding.UTF8, true))
            {
                while (!stop)
                {
                    mutex.WaitOne();

                    information = reader.ReadLine();
                    Console.WriteLine("************ " + information);
                    this.InfoString = information;
                    mutex.ReleaseMutex();
                }
            }
            Console.WriteLine("EXIT?");
            this.myTcpListener.Stop();
        }
        /**
         * This function connects to a server and writes a command to it.
         */
        public void Write(string command)
        {
            mutex.WaitOne();
            //MessageBox.Show("connect with ip:" + ip);
            //string ipName = Dns.GetHostByAddress(ip).HostName;
            NetworkStream writeStream = this.myTcpClient.GetStream();  //creates a network stream
            int byteCount = Encoding.ASCII.GetByteCount(command); //how many bytes
            byte[] sendData = new byte[byteCount];  //create a buffer

            sendData = Encoding.ASCII.GetBytes(command);   //puts the message in the buffer

            writeStream.Write(sendData, 0, sendData.Length); //network stream to transfer what's in buffer
            writeStream.Flush();
            mutex.ReleaseMutex();
        }

        /**
         * This function opens a server for reading from client on a new thread. 
         */
        public void Start()
        {
            // Opens a thread which reads information from server.
            new Thread(() =>
           {
                   //MessageBox.Show("starting thread!");
                   this.Read();
           }).Start();
        }
    }
}
