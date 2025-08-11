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
using System.Runtime.Remoting.Channels;

namespace Colimator_Control_GUI
{
    public partial class Form1 : Form
    {
        string Serial1PortName, Serial1BaudRate, Serial1DataBits, Serial1StopBits, Serial1Parity, dataIN1, dataOUT1;
        public static bool OpenIrisDW = false;
        public static bool CloseIrisDW = false;
        public static bool IrisUp = false;

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

            System.Windows.Forms.Button existingButton1 = buttonIrisOpen;
            System.Windows.Forms.Button existingButton2 = buttonIrisClose;
            System.Windows.Forms.Button existingButton3 = buttonVColOpen;
            System.Windows.Forms.Button existingButton4 = buttonVColClose;
            System.Windows.Forms.Button existingButton5 = buttonRotCW;
            System.Windows.Forms.Button existingButton6 = buttonRotCCW;
            // Remove button2 and button3 from the form
            this.Controls.Remove(existingButton1);
            this.Controls.Remove(existingButton2);
            this.Controls.Remove(existingButton3);
            this.Controls.Remove(existingButton4);
            this.Controls.Remove(existingButton5);
            this.Controls.Remove(existingButton6);
            CustomButton ButtonIrisOpen = new CustomButton();
            CustomButton ButtonIrisClose = new CustomButton();
            CustomButton ButtonVColOpen = new CustomButton();
            CustomButton ButtonVColClose = new CustomButton();
            CustomButton ButtonRotCW = new CustomButton();
            CustomButton ButtonRotCCW = new CustomButton();
            ButtonIrisOpen.Location = existingButton1.Location;
            ButtonIrisOpen.Size = existingButton1.Size;
            ButtonIrisOpen.Text = existingButton1.Text;
            ButtonIrisOpen.Font = existingButton1.Font;
            ButtonIrisOpen.MouseDown += buttonIrisOpen_MouseDown;
            ButtonIrisOpen.MouseUp += buttonIris_MouseUp;
            // ... Set any other properties you need ...
            this.Controls.Add(ButtonIrisOpen);
            ButtonIrisClose.Location = existingButton2.Location;
            ButtonIrisClose.Size = existingButton2.Size;
            ButtonIrisClose.Text = existingButton2.Text;
            ButtonIrisClose.Font = existingButton2.Font;
            ButtonIrisClose.MouseDown += buttonIrisClose_MouseDown;
            ButtonIrisClose.MouseUp += buttonIris_MouseUp;
            // ... Set any other properties you need ...
            this.Controls.Add(ButtonIrisClose);
            ButtonVColOpen.Location = existingButton3.Location;
            ButtonVColOpen.Size = existingButton3.Size;
            ButtonVColOpen.Text = existingButton3.Text;
            ButtonVColOpen.Font = existingButton3.Font;
            ButtonVColOpen.MouseDown += buttonVColOpen_MouseDown;
            ButtonVColOpen.MouseUp += buttonVCol_MouseUp;
            // ... Set any other properties you need ...
            this.Controls.Add(ButtonVColOpen);
            ButtonVColClose.Location = existingButton4.Location;
            ButtonVColClose.Size = existingButton4.Size;
            ButtonVColClose.Text = existingButton4.Text;
            ButtonVColClose.Font = existingButton4.Font;
            ButtonVColClose.MouseDown += buttonVColClose_MouseDown;
            ButtonVColClose.MouseUp += buttonVCol_MouseUp;
            // ... Set any other properties you need ...
            this.Controls.Add(ButtonVColClose);
            ButtonRotCW.Location = existingButton5.Location;
            ButtonRotCW.Size = existingButton5.Size;
            ButtonRotCW.Text = existingButton5.Text;
            ButtonRotCW.Font = existingButton5.Font;
            ButtonRotCW.MouseDown += buttonRotCW_MouseDown;
            ButtonRotCW.MouseUp += buttonRot_MouseUp;
            // ... Set any other properties you need ...
            this.Controls.Add(ButtonRotCW);
            ButtonRotCCW.Location = existingButton6.Location;
            ButtonRotCCW.Size = existingButton6.Size;
            ButtonRotCCW.Text = existingButton6.Text;
            ButtonRotCCW.Font = existingButton6.Font;
            ButtonRotCCW.MouseDown += buttonRotCW_MouseDown;
            ButtonRotCCW.MouseUp += buttonRot_MouseUp;
            // ... Set any other properties you need ...
            this.Controls.Add(ButtonRotCCW);

            CheckPortsNames();
            this.TopMost = true;
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

        // Change the access modifier of serialPort1 from private to internal static
        // Replace this line:
        // private SerialPort serialPort1;
        // With:
        internal static SerialPort serialPortT;
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
            try
            {
                dataIN1 = serialPort1.ReadLine();
                if (this.IsHandleCreated)
                {
                    this.Invoke(new EventHandler(ShowData1));
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error reading data from serial port: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowData1(object sender, EventArgs e)
        {
            // Separate DataIN in I= , S= , etc
            if (dataIN1.Contains("HS="))
            {
                textBoxP1.Text = getBetween(dataIN1, "HS=", 3);
            }
            if (dataIN1.Contains("LS="))
            {
                textBoxP2.Text = getBetween(dataIN1, "LS=", 3);
            }

            // textBoxSerial.Text = dataIN1;
            if (dataIN1.Contains("Ax"))
            {

            }
            else
            {

            }
        }

        public void buttonIrisOpen_MouseDown(object sender, MouseEventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                dataOUT1 = "IC5";
                serialPort1.WriteLine(dataOUT1);
            }
        }

        public void buttonIris_MouseUp(object sender, MouseEventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                dataOUT1 = "IC0";
                serialPort1.WriteLine(dataOUT1);
            }

        }

        public void buttonIrisClose_MouseDown(object sender, MouseEventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                dataOUT1 = "IC-5";
                serialPort1.WriteLine(dataOUT1);
            }
        }

        public void buttonVColOpen_MouseDown(object sender, MouseEventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                dataOUT1 = "VC5";
                serialPort1.WriteLine(dataOUT1);
            }
        }

        public void buttonVCol_MouseUp(object sender, MouseEventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                dataOUT1 = "VC0";
                serialPort1.WriteLine(dataOUT1);
            }
        }

        public void buttonVColClose_MouseDown(object sender, MouseEventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                dataOUT1 = "VC-5";
                serialPort1.WriteLine(dataOUT1);
            }
        }

        public void buttonRotCW_MouseDown(object sender, MouseEventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                dataOUT1 = "RO5";
                serialPort1.WriteLine(dataOUT1);
            }
        }

        public void buttonRot_MouseUp(object sender, MouseEventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                dataOUT1 = "RO0";
                serialPort1.WriteLine(dataOUT1);
            }
        }

        public void buttonRotCCW_MouseDown(object sender, MouseEventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                dataOUT1 = "RO-5";
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

        private void buttonM2_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                dataOUT1 = "IM2";
                serialPort1.WriteLine(dataOUT1);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                dataOUT1 = "SH" + textBoxP1.Text;
                serialPort1.WriteLine(dataOUT1);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                dataOUT1 = "SL" + textBoxP2.Text;
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
            if (serialPort1.IsOpen)
            {
                dataOUT1 = "M0";
                serialPort1.WriteLine(dataOUT1);
            }
        }

        private void buttonWriteM1_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                dataOUT1 = "M1";
                serialPort1.WriteLine(dataOUT1);
            }
        }

        private void buttonWriteM2_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                dataOUT1 = "M2";
                serialPort1.WriteLine(dataOUT1);
            }
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
 
  public class CustomButton : System.Windows.Forms.Button
    {
        const int WM_POINTERDOWN = 0x0246;
        const int WM_POINTERUP = 0x0247;

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            Form1 form = this.FindForm() as Form1;

            if (m.Msg == WM_POINTERDOWN)
            {
                // Differentiate between the two buttons
                if (this.Text == "Iris Open")
                {
                    // Handle the Iris Open button "buttonIrisOpen_MouseDown"
                    // FIX: Provide required parameters for buttonIrisOpen_MouseDown
                    if (form != null)
                    {
                        form.buttonIrisOpen_MouseDown(this, new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
                    }
                }
                if (this.Text == "Iris Close")
                {
                    // Handle the Iris Close button "buttonIrisClose_MouseDown"
                    if (form != null)
                    {
                        form.buttonIrisClose_MouseDown(this, new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
                    }
                }
                if (this.Text == "Coll Open")
                {
                    // Handle the Iris Open button "buttonIrisOpen_MouseDown"
                    // FIX: Provide required parameters for buttonIrisOpen_MouseDown
                    if (form != null)
                    {
                        form.buttonVColOpen_MouseDown(this, new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
                    }
                }
                if (this.Text == "Coll Close")
                {
                    // Handle the Iris Close button "buttonIrisClose_MouseDown"
                    if (form != null)
                    {
                        form.buttonVColClose_MouseDown(this, new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
                    }
                }
                if (this.Text == "Rot CW")
                {
                    // Handle the Iris Open button "buttonIrisOpen_MouseDown"
                    // FIX: Provide required parameters for buttonIrisOpen_MouseDown
                    if (form != null)
                    {
                        form.buttonRotCW_MouseDown(this, new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
                    }
                }
                if (this.Text == "Rot CCW")
                {
                    // Handle the Iris Close button "buttonIrisClose_MouseDown"
                    if (form != null)
                    {
                        form.buttonRotCCW_MouseDown(this, new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
                    }
                }
            }
            else if (m.Msg == WM_POINTERUP)
            {
                // Handle pointer up event
                if (form != null)
                {
                    form.buttonIris_MouseUp(this, new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
                    form.buttonVCol_MouseUp(this, new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
                    form.buttonRot_MouseUp(this, new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
                }
            }
        }
    }
}
