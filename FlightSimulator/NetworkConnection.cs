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
            set {
                infoString = value;
                NotifyPropertyChanged(infoString);
                }
        }

        private TcpClient myTcpClient;
        private TcpListener myTcpListener;

        //private string myIP;
        //private int myInfoPort;
        //private int myCommandPort;
        public volatile bool stop = true;

        public void Connect(string ip, int infoPort, int commandPort)
        {
            if (!stop)
            {
                this.Disconnect();
            }

            //myIP = ip;
            //myInfoPort = infoPort;
            //myCommandPort = commandPort;
            var ipNum = Dns.GetHostEntry(ip).AddressList[1];
            myTcpListener = new TcpListener(ipNum, infoPort); // set server.
            myTcpClient = default(TcpClient);

            stop = false;
            this.Start();

            // now can continue to sending actions.

            //Console.WriteLine("now continuing with main!!");
            //this.Write("s");
        }

        /**
         * This function disconnects the open server for reading from client.
         */
        public void Disconnect()
        {
            stop = true;
            this.myTcpListener.Stop();
        }

        /**
         * This function reads info from connected clients as long as not disconnected.
         */
        public void Read()
        {

            try
            {
                myTcpListener.Start();
                MessageBox.Show("server waiting for client..");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.Read();
            }

            myTcpClient = myTcpListener.AcceptTcpClient();  //goes to sleep until interupt
            MessageBox.Show("client accepted :)");
            int index = 0;
            int n = 0;
            string remainder = "";
            string backRemainder = "";
            string information = "";
            bool isDataEnd = false;

            while (!stop)
            {
                byte[] receivedBuffer = new byte[256];

                NetworkStream streams = myTcpClient.GetStream();

                n = streams.Read(receivedBuffer, 0, receivedBuffer.Length);

                Console.WriteLine("NOW! in read (receive)");
                if (n < 0)
                {
                    Console.WriteLine("Problem with reading");
                }
                information = new string(Encoding.UTF8.GetChars(receivedBuffer));
                Console.WriteLine("THis is it!!!: " + information);

                if (backRemainder != "")
                {
                    remainder = backRemainder;
                    backRemainder = "";
                }

                index = information.IndexOf('\n');
                Console.WriteLine("index is: " + index);
                // if the line terminator was not found, append all of the information.
                if (index < 0)
                {
                    remainder += information;
                }
                else
                {
                    // appends the remainder of the information until the next line.
                    Console.WriteLine(information.Length);
                    remainder += information.Substring(0, index);
                    backRemainder = information.Substring(index + 1, information.Length - index - 1);
                    isDataEnd = true;
                }

                if (isDataEnd)
                {
                    Console.WriteLine("isDataEnd = True (then false)");
                    Console.WriteLine(remainder.ToString());

                    this.InfoString = remainder;

                    remainder = "";
                    isDataEnd = false;

                    /** Split by commas */

                    /*string[] infoArray = remainder.Split(',');*/
                    //string lon = infoArray[1];
                    //string lat = infoArray[2];
                    //MessageBox.Show("lon: " + lon + "lat: " + lat); //instead of this.. draw on the flightboard.
                    //int numLon = 0;
                    //int numLat = 0;
                    //Int32.TryParse(lon, out numLon);
                    //Int32.TryParse(lat, out numLat);
                    //double Lon = numLon;
                    //double Lat = numLat;   //Sending to be drawn..
                    //MessageBox.Show("lon,lat after conversion: " + Lon + ", " + Lat);

                }
            }
        }

        /**
         * This function connects to a server and writes a command to it.
         */
        public void Write(string command)
        {
            mutex.WaitOne();
            string ip = Properties.Settings.Default.FlightServerIP;
            int sendPort = Properties.Settings.Default.FlightCommandPort;
            //MessageBox.Show("connect with port:" + sendPort.ToString());
            //MessageBox.Show("connect with ip:" + ip);
            //string ipName = Dns.GetHostByAddress(ip).HostName;
            if (ip == "127.0.0.1")  // @TODO: later get host by address.
            {
                ip = "localhost";
            }

            myTcpClient = new TcpClient(ip, sendPort); // connect as client.

            //command = "set controls/flight/rudder 1";
            //msg = (msg.Equals(msg0)) ? msg1 : msg0;
            //msg = message.Text; //Take whatever that is in the input

            //feedbackTXT.Text += "msg: " + msg + "\n";       //Write server respond on xaml feedback text
            //Console.WriteLine("msg: " + msg);
            int byteCount = Encoding.ASCII.GetByteCount(command); //how many bytes
            byte[] sendData = new byte[byteCount];  //create a buffer

            sendData = Encoding.ASCII.GetBytes(command);   //puts the message in the buffer
            //feedbackTXT.Text += "Encoded\n";
            NetworkStream stream = myTcpClient.GetStream();  //creates a network stream

            stream.Write(sendData, 0, sendData.Length); //network stream to transfer what's in buffer
            //feedbackTXT.Text += "written to server and server said:\n";

            //byte[] feedback = new byte[256];

            //stream.Read(feedback, 0, 256);

            //string fromServer = new string(Encoding.UTF8.GetChars(feedback));
            mutex.ReleaseMutex();

            //feedbackTXT.Text += fromServer + "\n";
        }

        /**
         * This function opens a server for reading from client on a new thread. 
         */
        public void Start()
        {
            // Opens a thread which reads information from server.
            new Thread( ()=>
            {
                //MessageBox.Show("starting thread!");
                this.Read();
            }).Start();
        }
    }
}
