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
        List<Skin> skins;
        List<Skin> currentSkins;
        string currentCollection;
        string currentCondition;
        string currentStat = "";
        string[] conditions = { "Factory New", "Minimal Wear", "Field-Tested", "Well-Worn", "Battle-Scarred" };
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
            progressBar1.Maximum = currentSkins.Count;
            progressBar1.Value = 0;
            label1.Text = "";
            getPrices();
        }

        void getPrices()
        {
            for(int i=0;i<currentSkins.Count;i++)
            {
                for(int t=0;t<5;t++)
                {
                    if(currentSkins[i].allowsCondition(conditions[t]))
                    {
                        string name = currentSkins[i].getFullName(currentStat, conditions[t]);
                        float newPrice = Price.GetPriceFloat(name);
                        currentSkins[i].prices.prices[t] = newPrice;
                        label1.Text += name + " - " + newPrice;
                        label1.Text += '\n';
                        Thread.Sleep(3020);
                    }
                }
                progressBar1.Value++;
            }
        }

        private void SplitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

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
    }
}
