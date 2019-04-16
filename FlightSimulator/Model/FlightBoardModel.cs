using FlightSimulator.Model.Interface;
using FlightSimulator.ViewModels;
using FlightSimulator.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FlightSimulator.Model
{
    public class FlightBoardModel : IFlightBoardModel
        
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        //TcpClient client = new TcpClient();
        //private FlightBoardViewModel flightBoardViewModel = new FlightBoardViewModel(this);

        static readonly string msg1 = "set controls/flight/rudder 1";

        ITelnetClient myTelnetClient;
        TcpClient clientty;     //Change to a 
        volatile Boolean stop;
        //The properties implementation
        private double lat;
        private double lon;

        public FlightBoardModel(ITelnetClient telnetClient)
        {
            myTelnetClient = telnetClient;
            stop = false;
        }
        
        public double Lat
        {
            get { return lat; }
            set
            {
                lat = value;
                NotifyPropertyChanged("Lat");
            }
        }
        public double Lon
        {
            get { return lon; }
            set
            {
                lon = value;
                NotifyPropertyChanged("Lon");
            }
        }

        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        //INotifyPropertyChanged implementation

        public void receive(string ip, int port)
        {
            var ipNum = Dns.GetHostEntry("localhost").AddressList[1];
            TcpListener server = new TcpListener(ipNum, 5400);

            TcpClient client = default(TcpClient);

            try
            {
                server.Start();
                Console.WriteLine("server waiting..");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.Read();
            }


            client = server.AcceptTcpClient();  //goes to sleep untill interupt
            MessageBox.Show("Client Accepted");
            //Thread.Sleep(160000); //wait 2 minutes before getting info -_-
            int index = 0;
            int n = 0;
            string remainder = "";
            string backRemainder = "";
            string information = "";
            bool isDataEnd = false;

            for (int i = 0; i < 2; ++i)
            {
                Thread.Sleep(100);
                byte[] receivedBuffer = new byte[256];

                NetworkStream streams = client.GetStream();

                n = streams.Read(receivedBuffer, 0, receivedBuffer.Length);



                Console.WriteLine("NOW!");
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
                Console.WriteLine("$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$");
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
                    //takeSamplesToTable(remainder);
                    //updateBindedValues();
                    Console.WriteLine("isDataEnd = True (then false)");
                    Console.WriteLine(remainder.ToString());

                    /** Split by commas */

                    string[] info = remainder.Split(',');
                    string lon = info[1];
                    string lat = info[2];
                    ///////////////////////

                    //MessageBox.Show("lon: " + lon + "lat: " + lat); //instead of this.. draw on the flightboard.

                    int numLon = 0;
                    int numLat = 0;

                    Int32.TryParse(lon, out numLon);
                    Int32.TryParse(lat, out numLat);


                    Lon = numLon;
                    Lat = numLat;   //Sending to be drawn..
                    MessageBox.Show("lon,lat after conversion: " + Lon + ", " + Lat);
                    remainder = "";
                    isDataEnd = false;
                }
            }
        }
        public void connect(string ip, int port)    //For sending. 
        {

            clientty = new TcpClient(ip, port); //we need a server ip and port number here.
            MessageBox.Show("Connection Established");


            /** Later move to send command func */
            string msg = msg1;
            //msg = (msg.Equals(msg0)) ? msg1 : msg0;
            //msg = message.Text; //Take whatever that is in the input

            //feedbackTXT.Text += "msg: " + msg + "\n";       //Write server respond on xaml feedback text
                                                            //Console.WriteLine("msg: " + msg);
            int byteCount = Encoding.ASCII.GetByteCount(msg + "\r\n"); //how many bytes
            byte[] sendData = new byte[byteCount];  //create a buffer

            sendData = Encoding.ASCII.GetBytes(msg + "\r\n");   //puts the message in the buffer
            //feedbackTXT.Text += "Encoded\n";
            NetworkStream stream = clientty.GetStream();  //creates a network stream

            stream.Write(sendData, 0, sendData.Length); //network stream to transfer what's in buffer
            //feedbackTXT.Text += "written to server and server said:\n";

            byte[] feedback = new byte[256];

            stream.Read(feedback, 0, 256);

            string fromServer = new string(Encoding.UTF8.GetChars(feedback));

            //feedbackTXT.Text += fromServer + "\n";


            //telnetClient.connect(ip, port); //scat for now later transfer to this telnet.
        }

        public void disconnect()
        {
            stop = true;
            myTelnetClient.disconnect();
        }

        //public void recieve()
        //{

        //}

        // Start getting info from FlightGear.
        //public void start()
        //{
        //    new Thread(delegate ()
        //    {
        //        while (!stop)
        //        {
        //            Lon = Double.Parse(telnetClient.read());    //get and parse the info from FlightGear
        //            Lat = Double.Parse(telnetClient.read());
        //            Thread.Sleep(100);  //How flightGear works in terms of time prashes
        //        }
        //    }).Start();
        //}
    }
}
