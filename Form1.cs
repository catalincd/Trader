using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace Trader
{
    public partial class Form1 : Form
    {
        public List<Skin> skins;
        public List<Skin> currentSkins;
        public int skinsCount = 15;
        public int counter = 0;
        public string currentName = "";
        public string currentCollection;
        public string currentCondition;
        public string currentStat = "";
        public string[] conditions = { "Factory New", "Minimal Wear", "Field-Tested", "Well-Worn", "Battle-Scarred" };
        public Form1()
        {
            InitializeComponent();
            skins = new List<Skin>();
            currentSkins = new List<Skin>();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            string colPath = Application.StartupPath + @"\collections.txt";
            string[] lines = File.ReadAllLines(colPath); 
            foreach(string q in lines)
            {
                comboBox1.Items.Add(q);
            }
            comboBox2.Items.Add("Factory New");
            comboBox2.Items.Add("Minimal Wear");
            comboBox2.Items.Add("Field Tested");
            comboBox2.Items.Add("Well Worn");
            comboBox2.Items.Add("Battle Scarred");

            string skinsPath = Application.StartupPath + @"\baseFinal.txt";
            string[] skinLines = File.ReadAllLines(skinsPath);
            foreach(string q in skinLines)
            {
                skins.Add(new Skin(q));
            }
        }
        private void SaveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            
        }
        private void ParseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Parser.parse2();
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            currentCollection = comboBox1.SelectedItem.ToString();
            currentCondition = comboBox2.SelectedItem.ToString();
            currentStat = checkBox1.Checked ? "StatTrak™ " : "";
            currentSkins.Clear();
            foreach(Skin q in skins)
            {
                if(q.collection == currentCollection)
                {
                    currentSkins.Add(q);
                }
            }
            skinsCount = currentSkins.Count * 5;
            counter = 0;
            progressBar1.Maximum = 100;
            progressBar1.Value = 0;
            label1.Text = "";
            getPrices();
        }

        void getPrices()
        {
            backgroundWorker1.RunWorkerAsync();
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            for (int i = 0; i < currentSkins.Count; i++)
            {
                for (int t = 0; t < 5; t++)
                {
                    if (currentSkins[i].allowsCondition(conditions[t]))
                    {
                        string name = currentSkins[i].getFullName(currentStat, conditions[t]);
                        float newPrice = Price.GetPriceFloat(name);
                        currentSkins[i].prices.prices[t] = newPrice;
                        currentName = name + " - " + newPrice + "\n";
                        worker.ReportProgress(((counter++) * 100 / skinsCount) + 1);
                        Thread.Sleep(3020);
                    }
                }
               
                //progressBar1.Value++;
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfg = new SaveFileDialog();
            sfg.Filter = "Text Files | *.txt";
            sfg.DefaultExt = "txt";
            if (sfg.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(sfg.FileName, label1.Text);
            }
        }

        private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Work completed");
            progressBar1.Value = 100;
        }

        private void BackgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            label1.Text += currentName;
        }
    }
}
