using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;


namespace WindowsFormsApp2
{
    public partial class DatabaseForm : Form
    {
        Form1 MainForm;
        bool firstShow;
        public DatabaseForm()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            for (int index = 0; index < MainForm.specimensArray.Length; index++)
            {
                MainForm.specimensArray[index].Using = true;
            }

            MainForm.Enabled = true;
            this.Hide();
        }

        private void addElem_Click(object sender, EventArgs e)
        {
            for (int index = 0; index < dataGridView1.RowCount; index++)
            {
                if (dataGridView1.Rows[index].Selected)
                {
                    if (!MainForm.specimensArray[index].Using)
                    {
                        dataGridView2.RowCount++;
                        dataGridView2.Rows[dataGridView2.RowCount - 1].Cells[0].Value = dataGridView2.RowCount;
                        dataGridView2.Rows[dataGridView2.RowCount - 1].Cells[1].Value = dataGridView1.Rows[index].Cells[1].Value;
                        dataGridView2.Rows[dataGridView2.RowCount - 1].Cells[2].Value = dataGridView1.Rows[index].Cells[2].Value;
                        MainForm.specimensArray[index].Using = true;
                    }
                }
            }
        }

        private void removeAllElem_Click(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear();
        }

        private void removeElem_Click(object sender, EventArgs e)
        {
            if (dataGridView2.RowCount == 0)
            {
                return;
            }
            dataGridView2.Rows.RemoveAt(dataGridView2.CurrentRow.Index);
        }

        private void addAllElem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                int index = dataGridView2.Rows.Add(r.Clone() as DataGridViewRow);
                foreach (DataGridViewCell o in r.Cells)
                {
                    dataGridView2.Rows[index].Cells[o.ColumnIndex].Value = o.Value;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MainForm.UpdateDataGridView();
            MainForm.saveToolStripMenuItem1.Enabled = true;
            MainForm.Enabled = true;
            this.Hide();
        }



        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    dataGridView1.Rows[i].Selected = false;
                    for (int j = 0; j < dataGridView1.ColumnCount; j++)
                        if (dataGridView1.Rows[i].Cells[j].Value != null)
                            if (dataGridView1.Rows[i].Cells[j].Value.ToString() == (textBox1.Text.ToUpper()))
                            {
                                dataGridView1.Rows[i].Selected = true;
                                dataGridView1.FirstDisplayedScrollingRowIndex = i;
                                break;
                            }
                }
            }
            else
            {
                dataGridView1.FirstDisplayedScrollingRowIndex = 0;
            }
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                //MainForm.Enabled = true;
            }
            catch { return; }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            MainForm = (Form1)Application.OpenForms["Form1"];
            firstShow = true;
        }

        private void DatabaseForm_Shown(object sender, EventArgs e)
        {
            for (int index = 0; index < MainForm.specimensArray.Length; index++)
            {
                dataGridView1.RowCount = index + 1;
                dataGridView1.Rows[index].Cells[0].Value = index + 1;
                dataGridView1[0, index].Style.BackColor = Color.LightGray;
                dataGridView1.Rows[index].Cells[1].Value = MainForm.specimensArray[index].Name; //заполняем таблицу
                if (MainForm.specimensArray[index].molecular_mass != 0)
                    dataGridView1.Rows[index].Cells[2].Value = MainForm.specimensArray[index].molecular_mass; //заполняем таблицу 
                if (!firstShow)
                {
                    if (MainForm.specimensArray[index].Using)
                    {
                        dataGridView2.RowCount = index + 1;
                        dataGridView2.Rows[index].Cells[0].Value = index + 1;
                        dataGridView2[0, index].Style.BackColor = Color.LightGray;
                        dataGridView2.Rows[index].Cells[1].Value = MainForm.specimensArray[index].Name; //заполняем таблицу
                        if (MainForm.specimensArray[index].molecular_mass != 0)
                            dataGridView2.Rows[index].Cells[2].Value = MainForm.specimensArray[index].molecular_mass.ToString(); //заполняем таблицу 
                    }
                }
                MainForm.specimensArray[index].Using = false;
            }
            label1.Text += " " + Path.GetFileName(MainForm.database_name);
            firstShow = false;
        }
    }
}
