using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
// using UDP;

namespace Colimator_Control_GUI
{
    public partial class Form1 : Form
    {
#if !DEBUG
        bool DEBUG = false;
#else
        bool DEBUG = true;
#endif

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
        // SimpleUDP u = new SimpleUDP(45004);

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonIrisOpen_Click(object sender, EventArgs e)
        {

        }

        private void buttonIrisClose_Click(object sender, EventArgs e)
        {

        }

        private void buttonVColOpen_Click(object sender, EventArgs e)
        {

        }

        private void buttonVColClose_Click(object sender, EventArgs e)
        {

        }

        private void buttonRotCW_Click(object sender, EventArgs e)
        {

        }

        private void buttonRotCCW_Click(object sender, EventArgs e)
        {

        }

        private void buttonM0_Click(object sender, EventArgs e)
        {

        }

        private void buttonM1_Click(object sender, EventArgs e)
        {

        }

        private void buttonM2_Click(object sender, EventArgs e)
        {

        }

        private void buttonReset_Click(object sender, EventArgs e)
        {

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
    }
}
