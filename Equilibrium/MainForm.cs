using System;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public string sep = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
        public Speciement[] specimensArray = new Speciement[0]; //создание массива химических соединений
        public Atom[] atomArray = new Atom[0]; //создание массива атомов
        DatabaseForm DataBaseForm = new DatabaseForm();
        string[,] dgv3 = new string[2000, 3];
        string[,] dgv7 = new string[2000, 3];
        string[,] dgv9 = new string[2000, 3];
        string[,] dgv11 = new string[2000, 3];

        EquilOut allEquilOut = new EquilOut();
        EquilOut gasEquilOut = new EquilOut();
        EquilOut liqEquilOut = new EquilOut();
        EquilOut solidEquilOut = new EquilOut();
        public string database_name;
        public Form1()
        {
            InitializeComponent();
        }

        int BoolToInt(bool boolean)
        {
            if (boolean) return 1;
            else return 0;
        }

        public void UpdateDataGridView()
        {
            dataGridView1.Rows.Clear();
            for (int i = 0; i <= specimensArray.Length - 1; i++)
            {
                if (specimensArray[i].Using)
                {
                    dataGridView1.RowCount++;
                    dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[0].Value = specimensArray[i].Name;
                    dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[1].Value = "0";
                    dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[2].Value = "0";
                    dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[3].Value = "0";
                }
            }

            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            textBox4.Enabled = true;
            textBox5.Enabled = true;
            textBox8.Enabled = true;
            textBox9.Enabled = true;
            textBox10.Enabled = true;
            textBox11.Enabled = true;
            textBox12.Enabled = true;

            radioButton1.Enabled = true;
            radioButton2.Enabled = true;
            radioButton6.Enabled = true;
            radioButton7.Enabled = true;
            radioButton8.Enabled = true;
            radioButton9.Enabled = true;

            button2.Enabled = true;
            button3.Enabled = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DataBaseForm.Owner = this;

            StreamReader streamReader = new StreamReader(new FileStream(@"bin\AtomicWeights.dat", FileMode.Open)); //создание потокового читателя
            string readStr = "";
            int index = 0;
            while (streamReader.Peek() >= 0)
            {
                Array.Resize(ref atomArray, index + 1);
                atomArray[index] = new Atom();
                readStr = streamReader.ReadLine(); //читаем строку из файла
                atomArray[index].Name = readStr.Substring(0, readStr.IndexOf("|")).ToUpper(); //пишем название атома
                string temp =
                    readStr.Substring(readStr.IndexOf("|") + 1, readStr.Length - readStr.IndexOf("|") - 1).Replace(".", sep).Replace(",", sep);
                atomArray[index].Weihgt = Convert.ToDouble(temp);
                index++;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double temp = 0;
            dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[1].Value = textBox1.Text;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                temp += double.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString().Replace(".", sep).Replace(",", sep));
            }
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Cells[2].Value =
                    (double.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString().Replace(".", sep).Replace(",", sep)) / temp * 100).ToString();
            }
            temp = 0;

            for (int index = 0; index < dataGridView1.RowCount; index++)
            {
                for (int index2 = 0; index2 < specimensArray.Length; index2++)
                {
                    if (dataGridView1.Rows[index].Cells[0].Value.ToString() == specimensArray[index2].Name)
                    {
                        temp += specimensArray[index2].molecular_mass * Double.Parse(dataGridView1.Rows[index].Cells[1].Value.ToString().Replace(".", sep).Replace(",", sep));
                    }
                }
            }

            for (int index = 0; index < dataGridView1.RowCount; index++)
            {
                for (int index2 = 0; index2 < specimensArray.Length; index2++)
                {
                    if (dataGridView1.Rows[index].Cells[0].Value.ToString() == specimensArray[index2].Name)
                    {
                        double elemW = specimensArray[index2].molecular_mass * Double.Parse(dataGridView1.Rows[index].Cells[1].Value.ToString().Replace(".", sep).Replace(",", sep));
                        dataGridView1.Rows[index].Cells[3].Value = (elemW / temp * 100).ToString();
                    }
                }
            }
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button2_Click(sender, e);
            }
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton8.Checked)
            {
                textBox6.Enabled = true;
                textBox7.Enabled = true;
            }
            else
            {
                textBox6.Enabled = false;
                textBox7.Enabled = false;
            }
        }

        private void textBox10_TextChanged(object sender, EventArgs e) // наводим красоту текстбоксов
        {
            double K;
            if (textBox10.Text != "")
            {
                if (Double.TryParse(textBox10.Text.Replace(".", sep).Replace(",", sep), out K))
                {
                    textBox10.BackColor = Color.FromArgb(255, 255, 255);
                    textBox11.Text = (K + 273.15).ToString();
                }
                else textBox10.BackColor = Color.FromArgb(255, 0, 0);
            }
            else textBox10.BackColor = Color.FromArgb(255, 0, 0);
        }

        private void textBox7_TextChanged(object sender, EventArgs e) // наводим красоту текстбоксов
        {
            double K;
            if (textBox7.Text != "")
            {
                if (Double.TryParse(textBox7.Text.Replace(".", sep).Replace(",", sep), out K))
                {
                    textBox7.BackColor = Color.FromArgb(255, 255, 255);
                    textBox6.Text = (K + 273.15).ToString();
                }
                else textBox7.BackColor = Color.FromArgb(255, 0, 0);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e) // наводим красоту текстбоксов
        {
            double cur_part;
            textBox1.BackColor = Color.FromArgb(255, 0, 0);
            if (Double.TryParse(textBox1.Text.Replace(".", sep).Replace(",", sep), out cur_part))
            {
                textBox1.BackColor = Color.FromArgb(255, 255, 255);
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            dataGridView2.Left = 5;
            dataGridView2.Width = tabPage4.Width - 5;
            double tH1 = tabPage4.Height;
            dataGridView2.Height = (int)Math.Round(tH1 / 3) - 3;
            dataGridView3.Left = 5;
            dataGridView3.Width = tabPage4.Width - 5;
            dataGridView3.Top = dataGridView2.Top + dataGridView2.Height + 4;
            dataGridView3.Height = tabPage4.Height - dataGridView3.Top - 5;

            dataGridView8.Left = 5;
            dataGridView8.Width = tabPage5.Width - 5;
            double tH2 = tabPage5.Height;
            dataGridView8.Height = (int)Math.Round(tH2 / 3) - 3;
            dataGridView7.Left = 5;
            dataGridView7.Width = tabPage5.Width - 5;
            dataGridView7.Top = dataGridView8.Top + dataGridView8.Height + 4;
            dataGridView7.Height = tabPage5.Height - dataGridView7.Top - 5;

            dataGridView10.Left = 5;
            dataGridView10.Width = tabPage6.Width - 5;
            double tH3 = tabPage6.Height;
            dataGridView10.Height = (int)Math.Round(tH3 / 3) - 3;
            dataGridView9.Left = 5;
            dataGridView9.Width = tabPage6.Width - 5;
            dataGridView9.Top = dataGridView10.Top + dataGridView10.Height + 4;
            dataGridView9.Height = tabPage6.Height - dataGridView9.Top - 5;

            dataGridView12.Left = 5;
            dataGridView12.Width = tabPage7.Width - 5;
            double tH4 = tabPage7.Height;
            dataGridView12.Height = (int)Math.Round(tH4 / 3) - 3;
            dataGridView11.Left = 5;
            dataGridView11.Width = tabPage7.Width - 5;
            dataGridView11.Top = dataGridView12.Top + dataGridView12.Height + 4;
            dataGridView11.Height = tabPage7.Height - dataGridView11.Top - 5;

            int sel = tabControl2.SelectedIndex;
            tabControl2.SelectedIndex = 0;
            tabControl2.SelectedIndex = 1;
            tabControl2.SelectedIndex = 2;
            tabControl2.SelectedIndex = 3;
            tabControl2.SelectedIndex = sel;
        }

        private void makeEquilINPFile()
        {
            FileStream fstream = new FileStream(@"bin\equil.inp", FileMode.Create);//создаем поток файла

            int index = tabControl1.SelectedIndex;
            string specimens;
            switch (index)
            {
                case 0:
                    string temp = "";
                    if (radioButton1.Checked) temp += "P";
                    if (radioButton2.Checked) temp += "V";
                    if (radioButton8.Checked) temp += "T";
                    if (radioButton9.Checked) temp += "H";
                    temp += "\n";
                    byte[] tempArr = System.Text.Encoding.Default.GetBytes(temp);
                    fstream.Write(tempArr, 0, tempArr.Length);
                    if (radioButton9.Checked)
                    {
                        temp = "TEMP " + textBox11.Text.Replace(",", ".") + "\nTEST " + textBox11.Text.Replace(",", ".") + "\n";
                    }
                    if (radioButton8.Checked)
                    {
                        temp = "TEMP " + textBox6.Text.Replace(",", ".") + "\nTEST " + textBox11.Text.Replace(",", ".") + "\n";
                    }
                    tempArr = System.Text.Encoding.Default.GetBytes(temp);
                    fstream.Write(tempArr, 0, tempArr.Length);

                    temp = "PRES " + textBox9.Text.Replace(",", ".") + "\nPEST " + textBox9.Text.Replace(",", ".") + "\n";
                    tempArr = System.Text.Encoding.Default.GetBytes(temp);
                    fstream.Write(tempArr, 0, tempArr.Length);

                    for (int i = 0; i < dataGridView1.RowCount; i++)
                    {
                        string tempName = dataGridView1.Rows[i].Cells[0].Value.ToString().ToUpper();
                        if (tempName.Contains(" "))
                            tempName = tempName.Substring(0, tempName.IndexOf(" "));
                        if (dataGridView1.Rows[i].Cells[1].Value.ToString().Replace(",", ".") != "0")
                        {
                            specimens = "REAC " + tempName + " " + dataGridView1.Rows[i].Cells[1].Value.ToString().Replace(",", ".") + "\n";
                            tempArr = System.Text.Encoding.Default.GetBytes(specimens);
                            fstream.Write(tempArr, 0, tempArr.Length);
                        }
                    }
                    temp = "END\n";
                    tempArr = System.Text.Encoding.Default.GetBytes(temp);
                    fstream.Write(tempArr, 0, tempArr.Length);
                    break;
            }
            fstream.Close();//закрываем файл
        }
        private void makeSurfINPFile()
        {
            FileStream fstream = new FileStream(@"bin\surf.inp", FileMode.Create);//создаем поток файла
            string temp = "BULK\n";
            byte[] tempArr = System.Text.Encoding.Default.GetBytes(temp);
            fstream.Write(tempArr, 0, tempArr.Length);

            if (radioButton3.Checked)
                temp = "!C(GR) / 1.0 /\n";
            if (radioButton4.Checked)
                temp = "C(GR) / 1.0 /\n";
            tempArr = System.Text.Encoding.Default.GetBytes(temp);
            fstream.Write(tempArr, 0, tempArr.Length);

            temp = "END";
            tempArr = System.Text.Encoding.Default.GetBytes(temp);
            fstream.Write(tempArr, 0, tempArr.Length);

            fstream.Close();//закрываем файл
        }

        private void makeChemINPFile()
        {
            FileStream fstream = new FileStream(@"bin\chem.inp", FileMode.Create);//создаем поток файла
            string temp = "ELEM\n";
            byte[] tempArr = System.Text.Encoding.Default.GetBytes(temp);
            fstream.Write(tempArr, 0, tempArr.Length);

            for (int index = 0; index < specimensArray.Length; index++)
            {
                if (specimensArray[index].Using)
                {
                    string tempASF = specimensArray[index].AtomicSF;
                    if (!tempASF.Contains("WARNING"))
                        while (tempASF.Contains("/"))
                        {
                            string temp2 = tempASF.Substring(0, tempASF.IndexOf("/"));
                            tempASF = tempASF.Remove(0, tempASF.IndexOf("/") + 1);
                            int count = int.Parse(tempASF.Substring(0, tempASF.IndexOf("/")));
                            tempASF = tempASF.Remove(0, tempASF.IndexOf("/") + 1);
                            for (int i = 0; i < atomArray.Length; i++)
                            {
                                if (temp2 == atomArray[i].Name)
                                {
                                    atomArray[i].ChemUse = true;
                                }
                            }
                        }
                }
            }

            for (int index = 0; index < atomArray.Length; index++)
            {
                if (atomArray[index].ChemUse)
                {
                    temp = atomArray[index].Name + "\n";
                    tempArr = System.Text.Encoding.Default.GetBytes(temp);
                    fstream.Write(tempArr, 0, tempArr.Length);
                }
            }

            temp = "END\n\n";
            tempArr = System.Text.Encoding.Default.GetBytes(temp);
            fstream.Write(tempArr, 0, tempArr.Length);

            temp = "SPECIES\n";
            tempArr = System.Text.Encoding.Default.GetBytes(temp);
            fstream.Write(tempArr, 0, tempArr.Length);

            for (int index = 0; index < dataGridView1.RowCount; index++)
            {
                temp = dataGridView1.Rows[index].Cells[0].Value.ToString() + "\n";
                if (temp.Contains("C(GR)"))
                {
                    continue;
                }
                tempArr = System.Text.Encoding.Default.GetBytes(temp);
                fstream.Write(tempArr, 0, tempArr.Length);
            }

            temp = "END";
            tempArr = System.Text.Encoding.Default.GetBytes(temp);
            fstream.Write(tempArr, 0, tempArr.Length);

            fstream.Close();//закрываем файл
        }

        private void button3_Click(object sender, EventArgs e)
        {
            makeEquilINPFile();
            makeChemINPFile();
            makeSurfINPFile();

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.InitialDirectory = Directory.GetCurrentDirectory() + @"\userdata";
                    string filename = Application.StartupPath + "/bin/therm.dat";
                    FileStream fstream = new FileStream(filename, FileMode.Create);//создаем поток файла

                    string temp = "THERMO ALL\n300.000  1000.000  5000.000\n";
                    byte[] tempArr = System.Text.Encoding.Default.GetBytes(temp);
                    fstream.Write(tempArr, 0, tempArr.Length);

                    for (int index = 0; index < specimensArray.Length; index++)
                    {
                        if (specimensArray[index].Using)
                        {
                            temp = specimensArray[index].string1 + "\n";
                            tempArr = System.Text.Encoding.Default.GetBytes(temp);
                            fstream.Write(tempArr, 0, tempArr.Length);

                            temp = specimensArray[index].string2 + "\n";
                            tempArr = System.Text.Encoding.Default.GetBytes(temp);
                            fstream.Write(tempArr, 0, tempArr.Length);

                            temp = specimensArray[index].string3 + "\n";
                            tempArr = System.Text.Encoding.Default.GetBytes(temp);
                            fstream.Write(tempArr, 0, tempArr.Length);

                            temp = specimensArray[index].string4 + "\n";
                            tempArr = System.Text.Encoding.Default.GetBytes(temp);
                            fstream.Write(tempArr, 0, tempArr.Length);
                        }
                    }
                    fstream.Close();
                
            }

            Process proc = new System.Diagnostics.Process();//объявление стороннего процеса
            proc.StartInfo.UseShellExecute = false;//игнорировать расширение файла
            proc.StartInfo.FileName = "cmd.exe";
            proc.StartInfo.Arguments = @"/C cd " + Application.StartupPath + "\\bin & ToPr0.bat";
            
            proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            proc.StartInfo.CreateNoWindow = true;
            proc.Start();
            proc.WaitForExit();
            bool All = false;
            bool Gas = false;
            bool Liquid = false;
            bool Solid = false;
            bool Mol = false;

            dataGridView2.Rows.Clear();
            dataGridView3.Rows.Clear();
            dataGridView7.Rows.Clear();
            dataGridView8.Rows.Clear();
            dataGridView9.Rows.Clear();
            dataGridView10.Rows.Clear();
            dataGridView11.Rows.Clear();
            dataGridView12.Rows.Clear();

            StreamReader streamReader = new StreamReader(new FileStream(@"bin\equil.out", FileMode.Open)); //создание потокового читателя
            while (streamReader.Peek() >= 0)
            {

                string readStr = streamReader.ReadLine();
                //if (readStr == "") continue;
                if (readStr.Contains("ERROR") || readStr.Contains("Error") || readStr.Contains("error"))
                {
                    MessageBox.Show( //если есть какая-то ошибка, выводим ее на экран
                        readStr,
                        "Warning!!!",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.DefaultDesktopOnly);
                    streamReader.Close();
                    return;
                }
                if (readStr.Contains("MIXTURE"))
                {
                    All = true;
                    continue;
                }
                if (readStr.Contains("GAS PHASE"))
                {
                    Gas = true;
                    continue;
                }
                if (readStr.Contains("BULK PHASE #  1"))
                {
                    Liquid = true;
                    continue;
                }
                if (readStr.Contains("BULK PHASE #  2"))
                {
                    Solid = true;
                    continue;
                }
                if (All)
                {
                    if (Mol)
                    {
                        if (readStr != "")
                        {
                            dataGridView3.RowCount++;                           
                            int len = readStr.Length;
                            dataGridView3.Rows[dataGridView3.RowCount - 1].Cells[0].Value = 
                                readStr.Substring(0, 21).Replace(" ", "");
                            dgv3[dataGridView3.RowCount - 1, 0] = readStr.Substring(0, 21).Replace(" ", "");

                            Array.Resize(ref allEquilOut.molarInlet, allEquilOut.molarInlet.Length + 1);
                            Array.Resize(ref allEquilOut.molarOutlet, allEquilOut.molarOutlet.Length + 1);
                            Array.Resize(ref allEquilOut.massInlet, allEquilOut.massInlet.Length + 1);
                            Array.Resize(ref allEquilOut.massOutlet, allEquilOut.massOutlet.Length + 1);

                            allEquilOut.molarInlet[allEquilOut.molarInlet.Length-1] =
                                Double.Parse(readStr.Substring(21, 20).Replace(" ", "").Replace(".", sep).Replace(",", sep));
                            dgv3[dataGridView3.RowCount - 1, 1] = readStr.Substring(21, 20).Replace(" ", "");

                            if (Double.TryParse(readStr.Substring(41, len - 41).Replace(" ", "").Replace(".", sep).Replace(",", sep), out double temp))
                            {
                                allEquilOut.molarOutlet[allEquilOut.molarOutlet.Length - 1] =
                                    Double.Parse(readStr.Substring(41, len - 41).Replace(" ", "").Replace(".", sep).Replace(",", sep));
                                dgv3[dataGridView3.RowCount - 1, 2] = readStr.Substring(41, len - 41).Replace(" ", "");
                            }
                            else
                            {
                                dataGridView3.Rows[dataGridView3.RowCount - 1].Cells[2].Value = 0;
                                dgv3[dataGridView3.RowCount - 1, 2] = "0";
                            }
                                    
                            dataGridView3.Sort(dataGridView3.Columns[1], System.ComponentModel.ListSortDirection.Descending);
                        }
                        else
                        {
                            All = false;
                            Mol = false;
                            continue;
                        }
                    }
                    if (readStr.Contains("Mol Fractions"))
                    {
                        Mol = true;
                        continue;
                    }
                    if (readStr != "" && !Mol)
                    {
                        dataGridView2.RowCount++;
                        int len = readStr.Length;
                        dataGridView2.Rows[dataGridView2.RowCount - 1].Cells[0].Value = readStr.Substring(0, 21).Replace(" ","");
                        dataGridView2.Rows[dataGridView2.RowCount - 1].Cells[1].Value = readStr.Substring(21, 20).Replace(" ", "");
                        dataGridView2.Rows[dataGridView2.RowCount - 1].Cells[2].Value = readStr.Substring(41, len - 41).Replace(" ", "");
                    }
                }
                if (Gas)
                {
                    if (Mol)
                    {
                        if (readStr != "")
                        {
                            dataGridView7.RowCount++;
                            int len = readStr.Length;
                            dataGridView7.Rows[dataGridView7.RowCount - 1].Cells[0].Value = 
                                readStr.Substring(0, 21).Replace(" ", "");

                            Array.Resize(ref gasEquilOut.molarInlet, gasEquilOut.molarInlet.Length + 1);
                            Array.Resize(ref gasEquilOut.molarOutlet, gasEquilOut.molarOutlet.Length + 1);
                            Array.Resize(ref gasEquilOut.massInlet, gasEquilOut.massInlet.Length + 1);
                            Array.Resize(ref gasEquilOut.massOutlet, gasEquilOut.massOutlet.Length + 1);

                            dgv7[dataGridView7.RowCount - 1, 0] = readStr.Substring(0, 21).Replace(" ", "");
                            dataGridView7.Rows[dataGridView7.RowCount - 1].Cells[1].Value =
                                Double.Parse(readStr.Substring(21, 20).Replace(" ", "").Replace(".", sep).Replace(",", sep)) *100; ;
                            dgv7[dataGridView7.RowCount - 1, 1] = readStr.Substring(21, 20).Replace(" ", "");
                        if (Double.TryParse(readStr.Substring(41, len - 41).Replace(" ", "").Replace(".", sep).Replace(",", sep), out double temp))
                        {
                            dataGridView7.Rows[dataGridView7.RowCount - 1].Cells[2].Value =
                                Double.Parse(readStr.Substring(41, len - 41).Replace(" ", "").Replace(".", sep).Replace(",", sep)) * 100;
                            dgv7[dataGridView7.RowCount - 1, 2] = readStr.Substring(41, len - 41).Replace(" ", "");
                        }
                        else
                        {
                            dataGridView7.Rows[dataGridView7.RowCount - 1].Cells[2].Value = 0;
                            dgv7[dataGridView7.RowCount - 1, 2] = "0";
                        }
                        dataGridView7.Sort(dataGridView7.Columns[1], System.ComponentModel.ListSortDirection.Descending);
                        }
                        else
                        {
                            Gas = false;
                            Mol = false;
                            continue;
                        }
                    }
                    if (readStr.Contains("Mol Fractions"))
                    {
                        Mol = true;
                        continue;
                    }
                    if (readStr != "" && !Mol)
                    {
                        dataGridView8.RowCount++;
                        int len = readStr.Length;
                        dataGridView8.Rows[dataGridView8.RowCount - 1].Cells[0].Value = readStr.Substring(0, 21).Replace(" ", "");
                        dataGridView8.Rows[dataGridView8.RowCount - 1].Cells[1].Value = readStr.Substring(21, 20).Replace(" ", "");
                        dataGridView8.Rows[dataGridView8.RowCount - 1].Cells[2].Value = readStr.Substring(41, len - 41).Replace(" ", "");
                    }
                }
                if (Liquid)
                {
                    if (Mol)
                    {
                        if (readStr.Replace(" ", "") != "")
                        {
                            dataGridView9.RowCount++;
                            int len = readStr.Length;

                            Array.Resize(ref liqEquilOut.molarInlet, liqEquilOut.molarInlet.Length + 1);
                            Array.Resize(ref liqEquilOut.molarOutlet, liqEquilOut.molarOutlet.Length + 1);
                            Array.Resize(ref liqEquilOut.massInlet, liqEquilOut.massInlet.Length + 1);
                            Array.Resize(ref liqEquilOut.massOutlet, liqEquilOut.massOutlet.Length + 1);

                            dataGridView9.Rows[dataGridView9.RowCount - 1].Cells[0].Value = readStr.Substring(0, 21).Replace(" ", "");
                            dgv9[dataGridView9.RowCount - 1, 0] = readStr.Substring(0, 21).Replace(" ", "");
                            dataGridView9.Rows[dataGridView9.RowCount - 1].Cells[1].Value =
                                Double.Parse(readStr.Substring(21, 20).Replace(" ", "").Replace(".", sep).Replace(",", sep)) * 100; ;
                            dgv9[dataGridView9.RowCount - 1, 1] = readStr.Substring(21, 20).Replace(" ", "");
                            if (Double.TryParse(readStr.Substring(41, len - 41).Replace(" ", "").Replace(".", sep).Replace(",", sep), out double temp))
                            {
                                dataGridView9.Rows[dataGridView9.RowCount - 1].Cells[2].Value =
                                    Double.Parse(readStr.Substring(41, len - 41).Replace(" ", "").Replace(".", sep).Replace(",", sep)) * 100;
                                dgv7[dataGridView9.RowCount - 1, 2] = readStr.Substring(41, len - 41).Replace(" ", "");
                            }
                            else
                            {
                                dataGridView9.Rows[dataGridView9.RowCount - 1].Cells[2].Value = 0;
                                dgv7[dataGridView9.RowCount - 1, 2] = "0";
                            }
                        dataGridView9.Sort(dataGridView9.Columns[1], System.ComponentModel.ListSortDirection.Descending);
                        }
                        else
                        {
                            Liquid = false;
                            Mol = false;
                            continue;
                        }
                    }
                    if (readStr.Contains("Mol Fractions"))
                    {
                        Mol = true;
                        continue;
                    }
                    if (readStr != "" && !Mol)
                    {
                        dataGridView10.RowCount++;
                        int len = readStr.Length;
                        dataGridView10.Rows[dataGridView10.RowCount - 1].Cells[0].Value = readStr.Substring(0, 21).Replace(" ", "");
                        dataGridView10.Rows[dataGridView10.RowCount - 1].Cells[1].Value = readStr.Substring(21, 20).Replace(" ", "");
                        dataGridView10.Rows[dataGridView10.RowCount - 1].Cells[2].Value = readStr.Substring(41, len - 41).Replace(" ", "");
                    }
                }
                if (Solid)
                {
                    if (Mol)
                    {
                        if (readStr != "")
                        {
                            dataGridView11.RowCount++;
                            int len = readStr.Length;

                            Array.Resize(ref solidEquilOut.molarInlet, solidEquilOut.molarInlet.Length + 1);
                            Array.Resize(ref solidEquilOut.molarOutlet, solidEquilOut.molarOutlet.Length + 1);
                            Array.Resize(ref solidEquilOut.massInlet, solidEquilOut.massInlet.Length + 1);
                            Array.Resize(ref solidEquilOut.massOutlet, solidEquilOut.massOutlet.Length + 1);

                            dataGridView11.Rows[dataGridView11.RowCount - 1].Cells[0].Value = readStr.Substring(0, 21).Replace(" ", "");
                            dgv11[dataGridView11.RowCount - 1, 0] = readStr.Substring(0, 21).Replace(" ", "");
                            dataGridView11.Rows[dataGridView11.RowCount - 1].Cells[1].Value =
                                Double.Parse(readStr.Substring(21, 20).Replace(" ", "").Replace(".", sep).Replace(",", sep)) * 100; ;
                            dgv11[dataGridView11.RowCount - 1, 1] = readStr.Substring(21, 20).Replace(" ", "");
                            if (Double.TryParse(readStr.Substring(41, len - 41).Replace(" ", "").Replace(".", sep).Replace(",", sep), out double temp))
                            {
                                dataGridView11.Rows[dataGridView11.RowCount - 1].Cells[2].Value =
                                    Double.Parse(readStr.Substring(41, len - 41).Replace(" ", "").Replace(".", sep).Replace(",", sep)) * 100;
                                dgv7[dataGridView11.RowCount - 1, 2] = readStr.Substring(41, len - 41).Replace(" ", "");
                            }
                            else
                            {
                                dataGridView11.Rows[dataGridView11.RowCount - 1].Cells[2].Value = 0;
                                dgv7[dataGridView11.RowCount - 1, 2] = "0";
                            }
                        dataGridView11.Sort(dataGridView11.Columns[1], System.ComponentModel.ListSortDirection.Descending);
                        }
                        else
                        {
                            Solid = false;
                            Mol = false;
                            continue;
                        }
                    }
                    if (readStr.Contains("Mol Fractions"))
                    {
                        Mol = true;
                        continue;
                    }
                    if (readStr != "" && !Mol)
                    {
                        dataGridView12.RowCount++;
                        int len = readStr.Length;
                        dataGridView12.Rows[dataGridView12.RowCount - 1].Cells[0].Value = readStr.Substring(0, 21).Replace(" ", "");
                        dataGridView12.Rows[dataGridView12.RowCount - 1].Cells[1].Value = readStr.Substring(21, 20).Replace(" ", "");
                        dataGridView12.Rows[dataGridView12.RowCount - 1].Cells[2].Value = readStr.Substring(41, len - 41).Replace(" ", "");
                    }
                }
            }
            streamReader.Close();
            toolStripStatusLabel2.Text = "Done!";
        }

        private void aboutProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm AboutForm = new AboutForm();
            AboutForm.Owner = this;
            AboutForm.Show();
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.SelectAll();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            AboutForm AboutForm = new AboutForm();
            DatabaseForm DatabaseForm = new DatabaseForm();

            AboutForm.Close();
            DatabaseForm.Close();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void openDataBase(string database_name)
        {
            StreamReader streamReader = new StreamReader(new FileStream(database_name, FileMode.Open)); //создание потокового читателя
            string readStr = "";
            int lineIndex = 0;
            string LowT, HighT, CommonT;
            int index = 0;
            try
            {
                while (streamReader.Peek() >= 0)
                {
                    readStr = streamReader.ReadLine(); //читаем строку из файла
                    lineIndex++;
                    if (readStr.Length >= 80)
                    {
                        if (index == 2187)
                        {
                            Thread.Sleep(1);
                        }
                        string str = readStr.Substring(79, 1); //проверяем символ в конце строки
                        if (str == "1")
                        {
                            Array.Resize(ref specimensArray, index + 1);
                            specimensArray[index] = new Speciement();
                            specimensArray[index].string1 = readStr;

                            specimensArray[index].Name = readStr.Substring(0, readStr.IndexOf(" ")).ToUpper(); //читаем из строки имя элемента
                            specimensArray[index].AtomicSF = readStr.Substring(24, 20); // читаем формулу элемента
                            specimensArray[index].AtomicSF =
                                specimensArray[index].AtomicSF.Replace("0", "").Replace(".", "").Replace(" ", "").ToUpper(); // убираем лишнее

                            string tmp = Regex.Replace(specimensArray[index].AtomicSF, @"(?<a>[0-9])(?<b>[^0-9\s])", @"${a}/${b}");
                            specimensArray[index].AtomicSF = Regex.Replace(tmp, @"(?<a>[^0-9\s])(?<b>[0-9])", @"${a}/${b}");
                            specimensArray[index].AtomicSF = specimensArray[index].AtomicSF + "/";
                            specimensArray[index].Phase = readStr.Substring(44, 1).ToUpper(); // читаем фазу элемента

                            LowT = readStr.Substring(45, 10).Replace(" ", "").Replace(".", sep).Replace(",", sep); //читаем нижнюю темпрературу
                            HighT = readStr.Substring(55, 10).Replace(" ", "").Replace(".", sep).Replace(",", sep); //читаем верхнюю темпрературу
                            CommonT = readStr.Substring(65, 10).Replace(" ", "").Replace(".", sep).Replace(",", sep); //читаем темпрературу точки сшивки

                            if (LowT != "")
                                specimensArray[index].LowT = double.Parse(LowT); //записываем температуру в массив
                            if (HighT != "")
                                specimensArray[index].HighT = double.Parse(HighT); //записываем температуру в массив
                            if (CommonT != "")
                                specimensArray[index].CommonT = double.Parse(CommonT); //записываем температуру в массив                           
                            string tempASF = specimensArray[index].AtomicSF;
                            if (!tempASF.Contains("WARNING"))
                                while (tempASF.Contains("/"))
                                {
                                    string temp = tempASF.Substring(0, tempASF.IndexOf("/"));
                                    tempASF = tempASF.Remove(0, tempASF.IndexOf("/") + 1);
                                    int count = int.Parse(tempASF.Substring(0, tempASF.IndexOf("/")));
                                    tempASF = tempASF.Remove(0, tempASF.IndexOf("/") + 1);
                                    for (int i = 0; i < atomArray.Length; i++)
                                    {
                                        if (temp == atomArray[i].Name)
                                        {
                                            specimensArray[index].molecular_mass += atomArray[i].Weihgt * count;
                                        }
                                    }
                                }
                            specimensArray[index].Using = true;
                        }
                        if (str == "2") //строка с коэффициентами
                        {
                            specimensArray[index].string2 = readStr;
                        }
                        if (str == "3") //строка с коэффициентами
                        {
                            specimensArray[index].string3 = readStr;
                        }
                        if (str == "4") //строка с коэффициентами
                        {
                            specimensArray[index].string4 = readStr;
                            index++;
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show( //если загружаемая база некорректна выводим ошибку
                "Database was created incorrectly, the error is in the line: " + lineIndex.ToString(),
                "ERROR!",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);
                return;
            }
            finally
            {
                streamReader.Close();
            }
            streamReader.Close();
            UpdateDataGridView();
            cutforProUsersOnlyToolStripMenuItem.Enabled = true;
            saveToolStripMenuItem.Enabled = true;
            label21.Text = "Opened database: " + Path.GetFileName(database_name); ;
        
                
        }

        private void openToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Directory.GetCurrentDirectory() + @"database";
                if (openFileDialog.ShowDialog() == DialogResult.OK) //открываем диалоговое окно выбора файла
                {
                    database_name = openFileDialog.FileName; //создание переменной пути и имени файла
                    openDataBase(database_name);
                }
            }
            for (int index = 1; index < specimensArray.Length; index++)
            {
                if (specimensArray[index].Name == specimensArray[index - 1].Name)
                {
                    MessageBox.Show( //если загружаемая база имеет повторяющиеся элементы выводим предупреждение
                        "There are duplicate items in the database, use this database on your own risk",
                        "Warning!!!",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.DefaultDesktopOnly);
                    return;
                }
            }
        }

        private void cutforProUsersOnlyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DatabaseForm DatabaseForm = new DatabaseForm();
            this.Enabled = false;
            DatabaseForm.Show();
        }

        private void saveToolStripMenuItem1_Click(object sender, EventArgs e) //сохраняем пользовательскую базу данных
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.InitialDirectory = Directory.GetCurrentDirectory() + @"\userdata";
                saveFileDialog.DefaultExt = ".dat";
                saveFileDialog.Filter = "Database files(*.dat)|*.dat|All files(*.*)|*.*";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filename = saveFileDialog.FileName;
                    FileStream fstream = new FileStream(filename, FileMode.Create);//создаем поток файла

                    string temp = "THERMO ALL\n300.000  1000.000  5000.000\n";
                    byte[] tempArr = System.Text.Encoding.Default.GetBytes(temp);
                    fstream.Write(tempArr, 0, tempArr.Length);

                    for (int index = 0; index < specimensArray.Length; index++)
                    {
                        if (specimensArray[index].Using)
                        {
                            temp = specimensArray[index].string1 + "\n";
                            tempArr = System.Text.Encoding.Default.GetBytes(temp);
                            fstream.Write(tempArr, 0, tempArr.Length);

                            temp = specimensArray[index].string2 + "\n";
                            tempArr = System.Text.Encoding.Default.GetBytes(temp);
                            fstream.Write(tempArr, 0, tempArr.Length);

                            temp = specimensArray[index].string3 + "\n";
                            tempArr = System.Text.Encoding.Default.GetBytes(temp);
                            fstream.Write(tempArr, 0, tempArr.Length);

                            temp = specimensArray[index].string4 + "\n";
                            tempArr = System.Text.Encoding.Default.GetBytes(temp);
                            fstream.Write(tempArr, 0, tempArr.Length);
                        }
                    }
                    fstream.Close();
                }
            }

        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {
            if (textBox12.Text != "")
            {
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    dataGridView1.Rows[i].Selected = false;
                    for (int j = 0; j < dataGridView1.ColumnCount; j++)
                        if (dataGridView1.Rows[i].Cells[j].Value != null)
                            if (dataGridView1.Rows[i].Cells[j].Value.ToString() == (textBox12.Text.ToUpper()))
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

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dataGridView1.Rows[dataGridView1.CurrentRow.Index].Selected = true;
        }

        private void tabControl2_Selecting(object sender, TabControlCancelEventArgs e)
        {
            this.Form1_Resize(sender, e);
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            
            double threshold;
            if (Double.TryParse(textBox8.Text.Replace(".", sep).Replace(",", sep), out threshold))
            {
                textBox8.BackColor = Color.FromArgb(255, 255, 255);
                dataGridView3.Rows.Clear();
                for (int Rows = 0; Rows < dgv3.Length/3; Rows++)
                {
                    if (dgv3[Rows, 0] != null)
                    {
                        if (Double.Parse(dgv3[Rows, 2].Replace(".", sep).Replace(",", sep)) *100 > threshold)
                        {
                            dataGridView3.RowCount++;
                            dataGridView3.Rows[dataGridView3.RowCount-1].Cells[0].Value = dgv3[Rows, 0];
                            dataGridView3.Rows[dataGridView3.RowCount - 1].Cells[1].Value = 
                                Double.Parse(dgv3[Rows, 1].Replace(".", sep).Replace(",", sep)) * 100;
                            dataGridView3.Rows[dataGridView3.RowCount - 1].Cells[2].Value = 
                                Double.Parse(dgv3[Rows, 2].Replace(".", sep).Replace(",", sep)) * 100;
                        }
                    }        
                }

                dataGridView7.Rows.Clear();
                for (int Rows = 0; Rows < dgv3.Length / 3; Rows++)
                {
                    if (dgv7[Rows, 0] != null)
                    {
                        if (Double.Parse(dgv7[Rows, 2].Replace(".", sep).Replace(",", sep)) * 100 > threshold)
                        {
                            dataGridView7.RowCount++;
                            dataGridView7.Rows[dataGridView7.RowCount - 1].Cells[0].Value = dgv7[Rows, 0];
                            dataGridView7.Rows[dataGridView7.RowCount - 1].Cells[1].Value =
                                Double.Parse(dgv7[Rows, 1].Replace(".", sep).Replace(",", sep)) * 100;
                            dataGridView7.Rows[dataGridView7.RowCount - 1].Cells[2].Value =
                                Double.Parse(dgv7[Rows, 2].Replace(".", sep).Replace(",", sep)) * 100;
                        }
                    }
                }

                dataGridView9.Rows.Clear();
                for (int Rows = 0; Rows < dgv9.Length / 3; Rows++)
                {
                    if (dgv9[Rows, 0] != null)
                    {
                        if (Double.Parse(dgv9[Rows, 2].Replace(".", sep).Replace(",", sep)) * 100 > threshold)
                        {
                            dataGridView9.RowCount++;
                            dataGridView9.Rows[dataGridView9.RowCount - 1].Cells[0].Value = dgv9[Rows, 0];
                            dataGridView9.Rows[dataGridView9.RowCount - 1].Cells[1].Value =
                                Double.Parse(dgv9[Rows, 1].Replace(".", sep).Replace(",", sep)) * 100;
                            dataGridView9.Rows[dataGridView9.RowCount - 1].Cells[2].Value =
                                Double.Parse(dgv9[Rows, 2].Replace(".", sep).Replace(",", sep)) * 100;
                        }
                    }
                }

                dataGridView11.Rows.Clear();
                for (int Rows = 0; Rows < dgv11.Length / 3; Rows++)
                {
                    if (dgv11[Rows, 0] != null)
                    {
                        if (Double.Parse(dgv11[Rows, 2].Replace(".", sep).Replace(",", sep)) * 100 > threshold)
                        {
                            dataGridView11.RowCount++;
                            dataGridView11.Rows[dataGridView11.RowCount - 1].Cells[0].Value = dgv11[Rows, 0];
                            dataGridView11.Rows[dataGridView11.RowCount - 1].Cells[1].Value =
                                Double.Parse(dgv11[Rows, 1].Replace(".", sep).Replace(",", sep)) * 100;
                            dataGridView11.Rows[dataGridView11.RowCount - 1].Cells[2].Value =
                                Double.Parse(dgv11[Rows, 2].Replace(".", sep).Replace(",", sep)) * 100;
                        }
                    }
                }
            }
            else
            {
                textBox8.BackColor = Color.FromArgb(255, 0, 0);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.InitialDirectory =  @"\userdata";
                saveFileDialog.DefaultExt = ".dtb";
                saveFileDialog.Filter = "User progress files(*.upf)|*.upf|All files(*.*)|*.*";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filename = saveFileDialog.FileName;
                    FileStream fstream = new FileStream(filename, FileMode.Create);//создаем поток файла

                    string temp = $"DB Name: [{Path.GetFileName(database_name)}]\n";
                    byte[] tempArr = System.Text.Encoding.Default.GetBytes(temp);
                    fstream.Write(tempArr, 0, tempArr.Length);

                    temp = radioButton1.Checked.ToString() + "\n";
                    temp += radioButton2.Checked.ToString() + "\n";
                    temp += radioButton3.Checked.ToString() + "\n";
                    temp += radioButton4.Checked.ToString() + "\n";
                    temp += radioButton6.Checked.ToString() + "\n";
                    temp += radioButton7.Checked.ToString() + "\n";
                    temp += radioButton8.Checked.ToString() + "\n";
                    temp += radioButton9.Checked.ToString() + "\n";
                    tempArr = System.Text.Encoding.Default.GetBytes(temp);
                    fstream.Write(tempArr, 0, tempArr.Length);

                    temp = textBox9.Text + "\n";
                    temp += textBox10.Text + "\n";
                    temp += textBox11.Text + "\n";
                    temp += textBox7.Text + "\n";
                    temp += textBox6.Text + "\n";
                    temp += textBox8.Text + "\n";
                    tempArr = System.Text.Encoding.Default.GetBytes(temp);
                    fstream.Write(tempArr, 0, tempArr.Length);



                    for (int index = 0; index < specimensArray.Length-1; index++)
                    {
                        temp = "{" + specimensArray[index].Name + "}\n";
                        temp += dataGridView1.Rows[index].Cells[1].Value.ToString() + "\n";
                        tempArr = System.Text.Encoding.Default.GetBytes(temp);
                        fstream.Write(tempArr, 0, tempArr.Length);
                    }
                    
                    fstream.Close();
                }
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                string[] SpeciesName = new string[0];
                double[] SpeciesUnnormalizedValue = new double[0];
                openFileDialog.InitialDirectory = @"userdata";
                openFileDialog.DefaultExt = ".dtb";
                openFileDialog.Filter = "User progress files(*.upf)|*.upf|All files(*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filename = openFileDialog.FileName;
                    StreamReader streamReader = new StreamReader(new FileStream(filename, FileMode.Open));
                    string tempLine = streamReader.ReadLine();
                    database_name = @".\database\" + tempLine.Substring(tempLine.IndexOf("[") + 1, tempLine.Length - tempLine.IndexOf("[") - 2);
                    openDataBase(database_name);
                    tempLine = streamReader.ReadLine();
                    radioButton1.Checked = bool.Parse(tempLine);
                    tempLine = streamReader.ReadLine();
                    radioButton2.Checked = bool.Parse(tempLine);
                    tempLine = streamReader.ReadLine();
                    radioButton3.Checked = bool.Parse(tempLine);
                    tempLine = streamReader.ReadLine();
                    radioButton4.Checked = bool.Parse(tempLine);
                    tempLine = streamReader.ReadLine();
                    radioButton6.Checked = bool.Parse(tempLine);
                    tempLine = streamReader.ReadLine();
                    radioButton7.Checked = bool.Parse(tempLine);
                    tempLine = streamReader.ReadLine();
                    radioButton8.Checked = bool.Parse(tempLine);
                    tempLine = streamReader.ReadLine();
                    radioButton9.Checked = bool.Parse(tempLine);

                    tempLine = streamReader.ReadLine();
                    textBox9.Text = tempLine;
                    tempLine = streamReader.ReadLine();
                    textBox10.Text = tempLine;
                    tempLine = streamReader.ReadLine();
                    textBox11.Text = tempLine;
                    tempLine = streamReader.ReadLine();
                    textBox7.Text = tempLine;
                    tempLine = streamReader.ReadLine();
                    textBox6.Text = tempLine;
                    tempLine = streamReader.ReadLine();
                    textBox8.Text = tempLine;


                    while (streamReader.Peek() >= 0)
                    {
                        tempLine = streamReader.ReadLine();
                        if (tempLine.Contains("{"))
                        {
                            Array.Resize(ref SpeciesName, SpeciesName.Length + 1);
                            Array.Resize(ref SpeciesUnnormalizedValue, SpeciesUnnormalizedValue.Length + 1);

                            SpeciesName[SpeciesName.Length - 1] = tempLine.Substring(tempLine.IndexOf("{")+1, tempLine.Length - 2);
                            tempLine = streamReader.ReadLine();
                            SpeciesUnnormalizedValue[SpeciesUnnormalizedValue.Length - 1] = Double.Parse(tempLine.Replace(".",sep).Replace(",", sep));
                        }
                    }

                    for (int index = 0; index < SpeciesName.Length-1; index++)
                    {
                        for (int gridIndex = 0; gridIndex < dataGridView1.RowCount -1; gridIndex++)
                        {
                            if (dataGridView1.Rows[gridIndex].Cells[0].Value.ToString() == SpeciesName[index])
                            {
                                dataGridView1.Rows[gridIndex].Cells[1].Value = SpeciesUnnormalizedValue[index].ToString();
                            }
                        }
                    }
                    textBox1.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value.ToString();
                    button2_Click(sender, e);
                    streamReader.Close();
                }
            }
        }
    }
}
