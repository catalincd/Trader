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

namespace Trader
{
    public partial class Form1 : Form
    {
        string[] skins = null;
        string currentCollection;
        string currentCondition;
        public Form1()
        {
            InitializeComponent();
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
            skins = File.ReadAllLines(skinsPath);
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


        }
    }
}
