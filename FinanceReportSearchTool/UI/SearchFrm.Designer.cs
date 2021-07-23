namespace FinanceReportSearchTool.UI
{
    partial class SearchFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchFrm));
            this.Menu = new System.Windows.Forms.MenuStrip();
            this.tmsearch = new System.Windows.Forms.ToolStripMenuItem();
            this.tmclose = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.dtend = new System.Windows.Forms.DateTimePicker();
            this.dtstart = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.txtsales = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtdep = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // Menu
            // 
            this.Menu.AutoSize = false;
            this.Menu.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmsearch,
            this.tmclose});
            this.Menu.Location = new System.Drawing.Point(0, 0);
            this.Menu.Name = "Menu";
            this.Menu.Size = new System.Drawing.Size(372, 24);
            this.Menu.TabIndex = 0;
            // 
            // tmsearch
            // 
            this.tmsearch.Name = "tmsearch";
            this.tmsearch.Size = new System.Drawing.Size(44, 20);
            this.tmsearch.Text = "查询";
            // 
            // tmclose
            // 
            this.tmclose.Name = "tmclose";
            this.tmclose.Size = new System.Drawing.Size(44, 20);
            this.tmclose.Text = "关闭";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(195, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "-";
            // 
            // dtend
            // 
            this.dtend.Location = new System.Drawing.Point(210, 51);
            this.dtend.Name = "dtend";
            this.dtend.Size = new System.Drawing.Size(117, 21);
            this.dtend.TabIndex = 9;
            // 
            // dtstart
            // 
            this.dtstart.Location = new System.Drawing.Point(75, 51);
            this.dtstart.Name = "dtstart";
            this.dtstart.Size = new System.Drawing.Size(117, 21);
            this.dtstart.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "日期:";
            // 
            // txtsales
            // 
            this.txtsales.Location = new System.Drawing.Point(75, 108);
            this.txtsales.Name = "txtsales";
            this.txtsales.Size = new System.Drawing.Size(117, 21);
            this.txtsales.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 11;
            this.label3.Text = "销售员:";
            // 
            // txtdep
            // 
            this.txtdep.Location = new System.Drawing.Point(75, 78);
            this.txtdep.Name = "txtdep";
            this.txtdep.Size = new System.Drawing.Size(252, 21);
            this.txtdep.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(36, 83);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 13;
            this.label4.Text = "部门:";
            // 
            // SearchFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 154);
            this.ControlBox = false;
            this.Controls.Add(this.txtdep);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtsales);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtend);
            this.Controls.Add(this.dtstart);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Menu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.Menu;
            this.Name = "SearchFrm";
            this.Text = "查询页";
            this.Menu.ResumeLayout(false);
            this.Menu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip Menu;
        private System.Windows.Forms.ToolStripMenuItem tmsearch;
        private System.Windows.Forms.ToolStripMenuItem tmclose;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtend;
        private System.Windows.Forms.DateTimePicker dtstart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtsales;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtdep;
        private System.Windows.Forms.Label label4;
    }
}