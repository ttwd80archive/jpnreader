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
            mykad.MyKad myKad = new mykad.MyKad();
            Boolean result = myKad.init();
            if (result != true)
            {
                listBox1.Items.Add("Init BAD");
                myKad.cleanup();
                listBox1.Items.Add("Clean Up");
                return;
            }
            listBox1.Items.Add("Init OK");
            myKad.cleanup();
            listBox1.Items.Add("Clean Up");


        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }
    }
}
