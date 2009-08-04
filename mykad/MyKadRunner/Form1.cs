using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MyKadRunner
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            myKad.Reader reader = new myKad.Reader();
            int errorCode;
            errorCode = reader.init();
            if (errorCode == 0)
            {
                listBox1.Items.Add("Init OK");
            }
            errorCode = reader.readFile1();
            listBox1.Items.Add("Read 1:" + errorCode);
            reader.cleanUp();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }
    }
}
