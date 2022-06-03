namespace WindowsFormsApp2
{
    partial class DatabaseForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.addElem = new System.Windows.Forms.Button();
            this.removeElem = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.addAllElem = new System.Windows.Forms.Button();
            this.removeAllElem = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ElementCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MolMasCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.elemCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.molMass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // addElem
            // 
            this.addElem.Location = new System.Drawing.Point(343, 266);
            this.addElem.Name = "addElem";
            this.addElem.Size = new System.Drawing.Size(30, 23);
            this.addElem.TabIndex = 5;
            this.addElem.Text = ">";
            this.addElem.UseVisualStyleBackColor = true;
            this.addElem.Click += new System.EventHandler(this.addElem_Click);
            // 
            // removeElem
            // 
            this.removeElem.Location = new System.Drawing.Point(343, 295);
            this.removeElem.Name = "removeElem";
            this.removeElem.Size = new System.Drawing.Size(30, 23);
            this.removeElem.TabIndex = 6;
            this.removeElem.Text = "<";
            this.removeElem.UseVisualStyleBackColor = true;
            this.removeElem.Click += new System.EventHandler(this.removeElem_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(626, 547);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 7;
            this.button4.Text = "Ok";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(545, 547);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 8;
            this.button5.Text = "Cancel";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 17);
            this.label1.TabIndex = 9;
            this.label1.Text = "DataBase Name:";
            // 
            // addAllElem
            // 
            this.addAllElem.Location = new System.Drawing.Point(343, 237);
            this.addAllElem.Name = "addAllElem";
            this.addAllElem.Size = new System.Drawing.Size(30, 23);
            this.addAllElem.TabIndex = 10;
            this.addAllElem.Text = ">>";
            this.addAllElem.UseVisualStyleBackColor = true;
            this.addAllElem.Click += new System.EventHandler(this.addAllElem_Click);
            // 
            // removeAllElem
            // 
            this.removeAllElem.Location = new System.Drawing.Point(343, 324);
            this.removeAllElem.Name = "removeAllElem";
            this.removeAllElem.Size = new System.Drawing.Size(30, 23);
            this.removeAllElem.TabIndex = 11;
            this.removeAllElem.Text = "<<";
            this.removeAllElem.UseVisualStyleBackColor = true;
            this.removeAllElem.Click += new System.EventHandler(this.removeAllElem_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.ElementCol,
            this.MolMasCol});
            this.dataGridView1.Location = new System.Drawing.Point(15, 61);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 25;
            this.dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView1.Size = new System.Drawing.Size(322, 480);
            this.dataGridView1.TabIndex = 12;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "№";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 40;
            // 
            // ElementCol
            // 
            this.ElementCol.HeaderText = "Elements";
            this.ElementCol.Name = "ElementCol";
            this.ElementCol.ReadOnly = true;
            this.ElementCol.Width = 150;
            // 
            // MolMasCol
            // 
            this.MolMasCol.HeaderText = "Molar Mass";
            this.MolMasCol.Name = "MolMasCol";
            this.MolMasCol.ReadOnly = true;
            this.MolMasCol.Width = 80;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.AllowUserToResizeColumns = false;
            this.dataGridView2.AllowUserToResizeRows = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column2,
            this.elemCol,
            this.molMass});
            this.dataGridView2.Location = new System.Drawing.Point(379, 61);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.RowHeadersWidth = 25;
            this.dataGridView2.Size = new System.Drawing.Size(322, 480);
            this.dataGridView2.TabIndex = 13;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "№";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 40;
            // 
            // elemCol
            // 
            this.elemCol.HeaderText = "Elements";
            this.elemCol.Name = "elemCol";
            this.elemCol.ReadOnly = true;
            this.elemCol.Width = 150;
            // 
            // molMass
            // 
            this.molMass.HeaderText = "Molar Mass";
            this.molMass.Name = "molMass";
            this.molMass.ReadOnly = true;
            this.molMass.Width = 80;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(103, 35);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 15;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 30;
            this.label2.Text = "Search Element:";
            // 
            // DatabaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(708, 576);
            this.ControlBox = false;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.removeAllElem);
            this.Controls.Add(this.addAllElem);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.removeElem);
            this.Controls.Add(this.addElem);
            this.MaximumSize = new System.Drawing.Size(724, 615);
            this.MinimumSize = new System.Drawing.Size(724, 615);
            this.Name = "DatabaseForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DataBase Elements Cut";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form2_FormClosing);
            this.Load += new System.EventHandler(this.Form2_Load);
            this.Shown += new System.EventHandler(this.DatabaseForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button addElem;
        private System.Windows.Forms.Button removeElem;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button addAllElem;
        private System.Windows.Forms.Button removeAllElem;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ElementCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn MolMasCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn elemCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn molMass;
    }
}