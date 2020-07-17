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
        public Form1()
        {
            InitializeComponent();
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns.Add("Name", "Name");
            dataGridView1.Columns.Add("Rarity", "Rarity");
            dataGridView1.Columns.Add("Collection", "Collection");

            string path = Application.StartupPath + @"\base.txt";
            string[] lines = File.ReadAllLines(path);
            for (int i = 0; i < lines.Length; i++)
            {
                string[] words = lines[i].Split(',');
                dataGridView1.Rows.Add(words);
            }
        }


        private void SaveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            List<string> content = new List<string>();
            foreach(DataGridViewRow row in dataGridView1.Rows)
            {
                try
                {
                    string newString = "";
                    if (row.Cells == null)
                        continue;
                    if (row.Cells["Name"].Value != null)
                        newString += row.Cells["Name"].Value.ToString();
                    if (row.Cells["Rarity"].Value != null)
                        newString += "," + row.Cells["Rarity"].Value.ToString();
                    if(row.Cells["Collection"].Value != null)
                        newString += "," + row.Cells["Collection"].Value.ToString();
                    content.Add(newString);
                }
                catch(Exception xe)
                {
                    MessageBox.Show(xe.Message);
                }
            }
            string path = Application.StartupPath + @"\base.txt";
            File.WriteAllLines(path, content);
        }

        private void ParseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Parser.parse2();
            //FloatRange range = new FloatRange();
            //MessageBox.Show(JsonSerializer.Serialize<FloatRange>(range));
        }
    }
}
