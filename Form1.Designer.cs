namespace frmsaccheck
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblstatus = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lklinfo = new System.Windows.Forms.LinkLabel();
            this.btnsacmeans = new System.Windows.Forms.Button();
            this.btndeactive = new System.Windows.Forms.Button();
            this.btnactive = new System.Windows.Forms.Button();
            this.txtcmd = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblstatus
            // 
            this.lblstatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblstatus.AutoSize = true;
            this.lblstatus.Font = new System.Drawing.Font("Segoe UI Light", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblstatus.ForeColor = System.Drawing.Color.White;
            this.lblstatus.Location = new System.Drawing.Point(115, 27);
            this.lblstatus.Name = "lblstatus";
            this.lblstatus.Size = new System.Drawing.Size(49, 20);
            this.lblstatus.TabIndex = 0;
            this.lblstatus.Text = "label1";
            this.lblstatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.groupBox1.Controls.Add(this.lklinfo);
            this.groupBox1.Controls.Add(this.btnsacmeans);
            this.groupBox1.Controls.Add(this.btndeactive);
            this.groupBox1.Controls.Add(this.btnactive);
            this.groupBox1.Controls.Add(this.txtcmd);
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.groupBox1.Location = new System.Drawing.Point(2, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(459, 156);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // lklinfo
            // 
            this.lklinfo.AutoSize = true;
            this.lklinfo.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.lklinfo.Location = new System.Drawing.Point(10, 115);
            this.lklinfo.Name = "lklinfo";
            this.lklinfo.Size = new System.Drawing.Size(0, 13);
            this.lklinfo.TabIndex = 4;
            this.lklinfo.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.lklinfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lklinfo_LinkClicked);
            // 
            // btnsacmeans
            // 
            this.btnsacmeans.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnsacmeans.FlatAppearance.BorderSize = 0;
            this.btnsacmeans.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnsacmeans.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnsacmeans.ForeColor = System.Drawing.Color.White;
            this.btnsacmeans.Location = new System.Drawing.Point(345, 49);
            this.btnsacmeans.Name = "btnsacmeans";
            this.btnsacmeans.Size = new System.Drawing.Size(103, 49);
            this.btnsacmeans.TabIndex = 3;
            this.btnsacmeans.Text = "What\'s S.A.C";
            this.btnsacmeans.UseVisualStyleBackColor = false;
            this.btnsacmeans.Click += new System.EventHandler(this.btnsacmeans_Click);
            this.btnsacmeans.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.btnsacmeans.MouseLeave += new System.EventHandler(this.Button_MouseLeave);
            // 
            // btndeactive
            // 
            this.btndeactive.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btndeactive.Enabled = false;
            this.btndeactive.FlatAppearance.BorderSize = 0;
            this.btndeactive.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btndeactive.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btndeactive.ForeColor = System.Drawing.Color.White;
            this.btndeactive.Location = new System.Drawing.Point(172, 49);
            this.btndeactive.Name = "btndeactive";
            this.btndeactive.Size = new System.Drawing.Size(103, 49);
            this.btndeactive.TabIndex = 2;
            this.btndeactive.Text = "Deactive";
            this.btndeactive.UseVisualStyleBackColor = false;
            this.btndeactive.Click += new System.EventHandler(this.btndeactive_Click);
            this.btndeactive.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.btndeactive.MouseLeave += new System.EventHandler(this.Button_MouseLeave);
            // 
            // btnactive
            // 
            this.btnactive.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(90)))), ((int)(((byte)(158)))));
            this.btnactive.Enabled = false;
            this.btnactive.FlatAppearance.BorderSize = 0;
            this.btnactive.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnactive.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnactive.ForeColor = System.Drawing.Color.White;
            this.btnactive.Location = new System.Drawing.Point(10, 49);
            this.btnactive.Name = "btnactive";
            this.btnactive.Size = new System.Drawing.Size(103, 49);
            this.btnactive.TabIndex = 1;
            this.btnactive.Text = "Active";
            this.btnactive.UseVisualStyleBackColor = false;
            this.btnactive.Click += new System.EventHandler(this.btnactive_Click);
            this.btnactive.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.btnactive.MouseLeave += new System.EventHandler(this.Button_MouseLeave);
            // 
            // txtcmd
            // 
            this.txtcmd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.txtcmd.Enabled = false;
            this.txtcmd.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcmd.ForeColor = System.Drawing.Color.Lime;
            this.txtcmd.Location = new System.Drawing.Point(6, 12);
            this.txtcmd.Multiline = true;
            this.txtcmd.Name = "txtcmd";
            this.txtcmd.Size = new System.Drawing.Size(442, 31);
            this.txtcmd.TabIndex = 0;
            this.txtcmd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.groupBox2.Controls.Add(this.lblstatus);
            this.groupBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.groupBox2.Location = new System.Drawing.Point(2, 162);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(459, 56);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(462, 218);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblstatus;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btndeactive;
        private System.Windows.Forms.Button btnactive;
        private System.Windows.Forms.TextBox txtcmd;
        private System.Windows.Forms.Button btnsacmeans;
        private System.Windows.Forms.LinkLabel lklinfo;
    }
}

