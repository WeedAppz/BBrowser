using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using System.Diagnostics;

namespace B
{
    public partial class okno : Form
    {
        private StringBuilder _pressedKeys = new StringBuilder();
        public List<ChromiumWebBrowser> name = new List<ChromiumWebBrowser>();


        public okno()
        {
            InitializeComponent();
            InitializeChromium();
        }

        public void InitializeChromium()
        {
            CefSettings settings = new CefSettings();
            Cef.Initialize(settings);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cef.Shutdown();
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int x;
            x = tabControl1.SelectedIndex;
            if (textBox1.Text != "")
            {
                if (textBox1.Text.IndexOf("http://") != -1)
                {
                    name[x].Load(textBox1.Text);
                }
                else
                {
                    name[x].Load("http://" + textBox1.Text);
                }
            }
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            tabControl1.TabPages.Add("1");
            tabControl1.SelectedIndex = tabControl1.TabPages.Count - 1;
            int number = name.Count();
            name.Add(new ChromiumWebBrowser("http://google.com"));
            this.Controls.Add(name[number]);
            name[number].Parent = tabControl1.SelectedTab;
            name[number].Dock = DockStyle.Fill;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int x;
            x = tabControl1.SelectedIndex + 1;
            name[x].Back();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int x;
            x = tabControl1.SelectedIndex + 1;
            name[x].Forward();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Add(Convert.ToString(tabControl1.TabPages.Count + 1));
            tabControl1.SelectedIndex = tabControl1.TabPages.Count - 1;
            int number = name.Count();
            name.Add(new ChromiumWebBrowser("http://google.com"));
            this.Controls.Add(name[number]);
            name[number].Parent = tabControl1.SelectedTab;
            name[number].Dock = DockStyle.Fill;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int x;
            x = tabControl1.SelectedIndex + 1;
            name[x].Load("https://www.google.pl/search?q=" + textBox2.Text);
            textBox2.Text = "";
           
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }
        private void timer1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            
            int x = name.Count - 1;
            name.RemoveAt(x);
            tabControl1.SelectedIndex = tabControl1.SelectedIndex - 1;
            tabControl1.TabPages.RemoveAt(tabControl1.TabPages.Count - 1);
            Process[] proc = Process.GetProcessesByName("CefSharp.BrowserSubprocess");
            proc[0].Kill();
        }
    }
}
