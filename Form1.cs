using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using UDP;

namespace Colimator_Control_GUI
{
    public partial class Form1 : Form
    {
#if !DEBUG
        bool DEBUG = false;
#else
        bool DEBUG = true;
#endif
        string Serial1PortName, Serial1BaudRate, Serial1DataBits, Serial1StopBits, Serial1Parity, dataIN1, dataOUT1;

        System.Windows.Forms.Timer t = null;

        // Lock for app path
        private static readonly object _lock = new object();
        private static string _appPath;
        public static string AppPath
        {
            get
            {
                lock (_lock)
                {
                    if (string.IsNullOrEmpty(_appPath))
                    {
                        _appPath = AppDomain.CurrentDomain.BaseDirectory;
                    }
                    return _appPath;
                }
            }
        }

        // Create an isntance of UDP with Serial Splitter
        SimpleUDP u = new SimpleUDP(45004);

        public Form1()
        {
            InitializeComponent();
            try
            {
                using (XmlTextReader configReader = new XmlTextReader(AppPath + "Config_Col.xml"))
                {
                    // Move to <Configuration>
                    while (configReader.Read())
                    {
                        if (configReader.NodeType == XmlNodeType.Element && configReader.Name == "Configuration")
                            break;
                    }

                    // Now read the configuration elements inside <Configuration>
                    while (configReader.Read())
                    {
                        if (configReader.NodeType == XmlNodeType.Element)
                        {
                            string elementName = configReader.Name;
                            string s1 = configReader.ReadElementContentAsString();

                            switch (elementName)
                            {
                                case "SerialPort1":
                                    Serial1PortName = getBetween(s1, "name=", 4);
                                    Serial1BaudRate = getBetween(s1, "baudrate=", 5);
                                    Serial1DataBits = getBetween(s1, "databits=", 1);
                                    Serial1StopBits = getBetween(s1, "stopbits=", 3);
                                    Serial1Parity = getBetween(s1, "parity=", 4);
                                    break;
                                default:
                                    break;
                            }
                        }
                        // Stop if we reach the end of <Configuration>
                        if (configReader.NodeType == XmlNodeType.EndElement && configReader.Name == "Configuration")
                            break;
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            CheckPortsNames();
            this.TopMost = true;
            StartTimer();
        }

        private void StartTimer()
        {
            t = new System.Windows.Forms.Timer();
            t.Interval = 500;
            t.Tick += new EventHandler(t_Tick);
            t.Enabled = true;
        }

        // Make f_Tick async
        private async void t_Tick(object sender, EventArgs e)
        {
            await ReadUPD_Data(e); // Await the async method
        }

        // Make ReadUPD_Data async
        private async Task ReadUPD_Data(EventArgs e)
        {
            string s = u.Read();
            if (string.IsNullOrEmpty(s))
                return;

            textBoxUDP.Text = s;

            switch (s)
            {
                case "Normal":
                    // Code for Normal Field of View
                    break;
                case "Mag1":
                    // Code for Mag1 Field of View
                    break;
                case "Mag2":
                    // Code for Mag2 Field of View
                    break;
                default:
                    // Optionally log unknown command
                    break;
            }
        }

        void CheckPortsNames()
        {
            string[] ports = SerialPort.GetPortNames();
            for (int i = 0; i < ports.Length; i++)
            {
                if (Serial1PortName == ports[i])
                {
                    OpenSerial1();
                }
            }
        }

        public async void OpenSerial1()     // Serial Port para la comunicacion con el Software Digirad
        {
            serialPort1.PortName = Serial1PortName;
            serialPort1.BaudRate = int.Parse(Serial1BaudRate);  // 19200  Valid values are 110, 300, 1200, 2400, 4800, 9600, 19200, 38400, 57600, or 115200.
            serialPort1.DataBits = int.Parse(Serial1DataBits);
            serialPort1.StopBits = (StopBits)Enum.Parse(typeof(StopBits), Serial1StopBits);
            serialPort1.Parity = (Parity)Enum.Parse(typeof(Parity), Serial1Parity);
            serialPort1.Encoding = Encoding.GetEncoding("iso-8859-1");
            // Encoding = Encoding.GetEncoding("Windows-1252");
            serialPort1.Open();
            serialPort1.DtrEnable = false;
            await Task.Delay(50);
            serialPort1.DtrEnable = true;
            await Task.Delay(100);
        }

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            dataIN1 = serialPort1.ReadLine();
            this.Invoke(new EventHandler(ShowData1));
        }

        private void ShowData1(object sender, EventArgs e)
        {
            // if (DEBUG) DisplayData(1, dataIN1);
            textBoxSerial.Text = dataIN1;
            if (dataIN1.Contains("Ax"))
            {

            }
            else
            {

            }
        }

        private void buttonIrisOpen_MouseDown(object sender, MouseEventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                dataOUT1 = "IC5";
                serialPort1.WriteLine(dataOUT1);
            }
        }

        private void buttonIrisOpen_MouseUp(object sender, MouseEventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                dataOUT1 = "IC0";
                serialPort1.WriteLine(dataOUT1);
            }

        }

        private void buttonIrisClose_MouseDown(object sender, MouseEventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                dataOUT1 = "IC-5";
                serialPort1.WriteLine(dataOUT1);
            }
        }

        private void buttonIrisClose_MouseUp(object sender, MouseEventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                dataOUT1 = "IC0";
                serialPort1.WriteLine(dataOUT1);
            }
        }


        private void buttonVColsOpen_MouseDown(object sender, MouseEventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                dataOUT1 = "K+";
                serialPort1.WriteLine(dataOUT1);
            }
        }

        private void buttonVColOpen_MouseUp(object sender, MouseEventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                dataOUT1 = "K+";
                serialPort1.WriteLine(dataOUT1);
            }
        }

        private void buttonVColsClose_MouseDown(object sender, MouseEventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                dataOUT1 = "K+";
                serialPort1.WriteLine(dataOUT1);
            }
        }

        private void buttonVColClose_MouseUp(object sender, MouseEventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                dataOUT1 = "K+";
                serialPort1.WriteLine(dataOUT1);
            }
        }

        private void buttonRotCW_MouseDown(object sender, MouseEventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                dataOUT1 = "K+";
                serialPort1.WriteLine(dataOUT1);
            }
        }

        private void buttonRotCW_MouseUp(object sender, MouseEventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                dataOUT1 = "K+";
                serialPort1.WriteLine(dataOUT1);
            }
        }

        private void buttonRotCCW_MouseDown(object sender, MouseEventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                dataOUT1 = "K+";
                serialPort1.WriteLine(dataOUT1);
            }
        }

        private void buttonRotCCW_MouseUp(object sender, MouseEventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                dataOUT1 = "K+";
                serialPort1.WriteLine(dataOUT1);
            }
        }

        private void buttonM0_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                dataOUT1 = "IM0";
                serialPort1.WriteLine(dataOUT1);
            }
        }

        private void buttonM1_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                dataOUT1 = "IM1";
                serialPort1.WriteLine(dataOUT1);
            }
        }

        private void buttonCE_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                dataOUT1 = "CE";
                serialPort1.WriteLine(dataOUT1);
            }

        }

        private void buttonM2_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                dataOUT1 = "IM2";
                serialPort1.WriteLine(dataOUT1);
            }
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            serialPort1.DtrEnable = false;
            Thread.Sleep(50);
            serialPort1.DtrEnable = true;
            Thread.Sleep(100);
            serialPort1.DtrEnable = false;
        }

        private void buttonWriteM0_Click(object sender, EventArgs e)
        {

        }

        private void buttonWriteM1_Click(object sender, EventArgs e)
        {

        }

        private void buttonWriteM2_Click(object sender, EventArgs e)
        {

        }

        public static string getBetween(string strSource, string strStart, int largo)
        {
            if (strSource.Contains(strStart))
            {
                int Start, End;
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = Start + largo;
                return strSource.Substring((Start + 1), End - Start);
            }

            return "";
        }
    }
}
