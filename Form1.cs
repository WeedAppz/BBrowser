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
using CefSharp.WinForms.Internals;

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
                if (textBox1.Text.IndexOf("//") != -1)
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
            tabControl1.SelectedTab.Text = "       New tab       ";
            int number = name.Count();
            name.Add(new ChromiumWebBrowser("http://google.com"));
            this.Controls.Add(name[number]);
            name[number].Parent = tabControl1.SelectedTab;
            name[number].Dock = DockStyle.Fill;
            int x = tabControl1.SelectedIndex;
            name[x].TitleChanged += OnBrowserTitleChanged;
            name[x].AddressChanged += OnBrowserAddressChanged;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int x;
            x = tabControl1.SelectedIndex;
            name[x].Back();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int x;
            x = tabControl1.SelectedIndex;
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
            x = tabControl1.SelectedIndex;
            name[x].Load("https://www.google.pl/search?q=" + textBox2.Text);
            textBox2.Text = "";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int x = name.Count - 1;
            name.RemoveAt(x);
            tabControl1.SelectedIndex = tabControl1.SelectedIndex - 1;
            tabControl1.TabPages.RemoveAt(tabControl1.TabPages.Count - 1);
        }

        private void OnBrowserTitleChanged(object sender, TitleChangedEventArgs args)
        {
            int x = tabControl1.SelectedIndex;
            this.InvokeOnUiThreadIfRequired(() => Text = args.Title);
            tabControl1.SelectedTab.Text = "       " + this.Text + "       ";
        }

        private void OnBrowserAddressChanged(object sender, AddressChangedEventArgs args)
        {
            this.InvokeOnUiThreadIfRequired(() =>textBox1.Text = args.Address);
        }

        private void textbox1_KeyUp(object sender, KeyEventArgs e)
        {
            int x = tabControl1.SelectedIndex;
            if (e.KeyCode == Keys.Enter)
            {
                name[x].Load(textBox1.Text);
            }
        }

        private void textbox2_KeyUp(object sender, KeyEventArgs e)
        {
            int x = tabControl1.SelectedIndex;
            if (e.KeyCode == Keys.Enter)
            {
                name[x].Load("https://www.google.pl/search?q=" + textBox2.Text);
            }           
        }
    }
}
