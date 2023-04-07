
namespace FluxTool_CleanerSystem
{
    partial class ConfigureForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigureForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBoxWaterFillTimeout = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBoxWaterHeatingTimeout = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnParameterSave = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.label24 = new System.Windows.Forms.Label();
            this.txtBoxCylinderFwdBwdTimeout = new System.Windows.Forms.TextBox();
            this.txtBoxDoorOpenCloseTimeout = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnHeatingTempSave = new System.Windows.Forms.Button();
            this.txtBoxHeatingTemp = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtBoxWaterFillTimeout);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtBoxWaterHeatingTimeout);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnParameterSave);
            this.groupBox1.Controls.Add(this.label24);
            this.groupBox1.Controls.Add(this.txtBoxCylinderFwdBwdTimeout);
            this.groupBox1.Controls.Add(this.txtBoxDoorOpenCloseTimeout);
            this.groupBox1.Controls.Add(this.label27);
            this.groupBox1.Controls.Add(this.label26);
            this.groupBox1.Controls.Add(this.label25);
            this.groupBox1.Font = new System.Drawing.Font("Nirmala UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Navy;
            this.groupBox1.Location = new System.Drawing.Point(117, 145);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.groupBox1.Size = new System.Drawing.Size(550, 432);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "< Time out >";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Navy;
            this.label4.Location = new System.Drawing.Point(495, 255);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 25);
            this.label4.TabIndex = 47;
            this.label4.Text = "Sec";
            // 
            // txtBoxWaterFillTimeout
            // 
            this.txtBoxWaterFillTimeout.BackColor = System.Drawing.SystemColors.Control;
            this.txtBoxWaterFillTimeout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.txtBoxWaterFillTimeout.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxWaterFillTimeout.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtBoxWaterFillTimeout.Location = new System.Drawing.Point(333, 250);
            this.txtBoxWaterFillTimeout.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.txtBoxWaterFillTimeout.Name = "txtBoxWaterFillTimeout";
            this.txtBoxWaterFillTimeout.ReadOnly = true;
            this.txtBoxWaterFillTimeout.Size = new System.Drawing.Size(152, 30);
            this.txtBoxWaterFillTimeout.TabIndex = 46;
            this.txtBoxWaterFillTimeout.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtBoxWaterFillTimeout.Click += new System.EventHandler(this.txtBoxDoorOpenCloseTimeout_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Navy;
            this.label6.Location = new System.Drawing.Point(26, 250);
            this.label6.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(172, 25);
            this.label6.TabIndex = 45;
            this.label6.Text = "Water fill time out";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.Location = new System.Drawing.Point(495, 205);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 25);
            this.label1.TabIndex = 44;
            this.label1.Text = "Sec";
            // 
            // txtBoxWaterHeatingTimeout
            // 
            this.txtBoxWaterHeatingTimeout.BackColor = System.Drawing.SystemColors.Control;
            this.txtBoxWaterHeatingTimeout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.txtBoxWaterHeatingTimeout.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxWaterHeatingTimeout.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtBoxWaterHeatingTimeout.Location = new System.Drawing.Point(333, 200);
            this.txtBoxWaterHeatingTimeout.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.txtBoxWaterHeatingTimeout.Name = "txtBoxWaterHeatingTimeout";
            this.txtBoxWaterHeatingTimeout.ReadOnly = true;
            this.txtBoxWaterHeatingTimeout.Size = new System.Drawing.Size(152, 30);
            this.txtBoxWaterHeatingTimeout.TabIndex = 43;
            this.txtBoxWaterHeatingTimeout.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtBoxWaterHeatingTimeout.Click += new System.EventHandler(this.txtBoxDoorOpenCloseTimeout_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Navy;
            this.label2.Location = new System.Drawing.Point(26, 200);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(217, 25);
            this.label2.TabIndex = 42;
            this.label2.Text = "Water heating time out";
            // 
            // btnParameterSave
            // 
            this.btnParameterSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnParameterSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnParameterSave.FlatAppearance.BorderSize = 0;
            this.btnParameterSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnParameterSave.Font = new System.Drawing.Font("Nirmala UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnParameterSave.ForeColor = System.Drawing.Color.Navy;
            this.btnParameterSave.ImageIndex = 0;
            this.btnParameterSave.ImageList = this.imageList1;
            this.btnParameterSave.Location = new System.Drawing.Point(414, 369);
            this.btnParameterSave.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnParameterSave.Name = "btnParameterSave";
            this.btnParameterSave.Size = new System.Drawing.Size(126, 51);
            this.btnParameterSave.TabIndex = 41;
            this.btnParameterSave.Text = "Save";
            this.btnParameterSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnParameterSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnParameterSave.UseVisualStyleBackColor = true;
            this.btnParameterSave.Click += new System.EventHandler(this.btnParameterSave_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Save.png");
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.ForeColor = System.Drawing.Color.Navy;
            this.label24.Location = new System.Drawing.Point(495, 105);
            this.label24.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(42, 25);
            this.label24.TabIndex = 33;
            this.label24.Text = "Sec";
            // 
            // txtBoxCylinderFwdBwdTimeout
            // 
            this.txtBoxCylinderFwdBwdTimeout.BackColor = System.Drawing.SystemColors.Control;
            this.txtBoxCylinderFwdBwdTimeout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.txtBoxCylinderFwdBwdTimeout.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxCylinderFwdBwdTimeout.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtBoxCylinderFwdBwdTimeout.Location = new System.Drawing.Point(333, 100);
            this.txtBoxCylinderFwdBwdTimeout.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.txtBoxCylinderFwdBwdTimeout.Name = "txtBoxCylinderFwdBwdTimeout";
            this.txtBoxCylinderFwdBwdTimeout.ReadOnly = true;
            this.txtBoxCylinderFwdBwdTimeout.Size = new System.Drawing.Size(152, 30);
            this.txtBoxCylinderFwdBwdTimeout.TabIndex = 31;
            this.txtBoxCylinderFwdBwdTimeout.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtBoxCylinderFwdBwdTimeout.Click += new System.EventHandler(this.txtBoxDoorOpenCloseTimeout_Click);
            // 
            // txtBoxDoorOpenCloseTimeout
            // 
            this.txtBoxDoorOpenCloseTimeout.BackColor = System.Drawing.SystemColors.Control;
            this.txtBoxDoorOpenCloseTimeout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.txtBoxDoorOpenCloseTimeout.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxDoorOpenCloseTimeout.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtBoxDoorOpenCloseTimeout.Location = new System.Drawing.Point(333, 50);
            this.txtBoxDoorOpenCloseTimeout.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.txtBoxDoorOpenCloseTimeout.Name = "txtBoxDoorOpenCloseTimeout";
            this.txtBoxDoorOpenCloseTimeout.ReadOnly = true;
            this.txtBoxDoorOpenCloseTimeout.Size = new System.Drawing.Size(152, 30);
            this.txtBoxDoorOpenCloseTimeout.TabIndex = 30;
            this.txtBoxDoorOpenCloseTimeout.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtBoxDoorOpenCloseTimeout.Click += new System.EventHandler(this.txtBoxDoorOpenCloseTimeout_Click);
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.ForeColor = System.Drawing.Color.Navy;
            this.label27.Location = new System.Drawing.Point(26, 50);
            this.label27.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(296, 25);
            this.label27.TabIndex = 22;
            this.label27.Text = "Cover door open/close time out";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.ForeColor = System.Drawing.Color.Navy;
            this.label26.Location = new System.Drawing.Point(495, 55);
            this.label26.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(42, 25);
            this.label26.TabIndex = 24;
            this.label26.Text = "Sec";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.ForeColor = System.Drawing.Color.Navy;
            this.label25.Location = new System.Drawing.Point(26, 100);
            this.label25.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(252, 25);
            this.label25.TabIndex = 25;
            this.label25.Text = "Cylinder fwd/bwd time out";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnHeatingTempSave);
            this.groupBox2.Controls.Add(this.txtBoxHeatingTemp);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Font = new System.Drawing.Font("Nirmala UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.Navy;
            this.groupBox2.Location = new System.Drawing.Point(677, 145);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.groupBox2.Size = new System.Drawing.Size(378, 278);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "< Water heating >";
            // 
            // btnHeatingTempSave
            // 
            this.btnHeatingTempSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnHeatingTempSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHeatingTempSave.FlatAppearance.BorderSize = 0;
            this.btnHeatingTempSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnHeatingTempSave.Font = new System.Drawing.Font("Nirmala UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHeatingTempSave.ForeColor = System.Drawing.Color.Navy;
            this.btnHeatingTempSave.ImageIndex = 0;
            this.btnHeatingTempSave.ImageList = this.imageList1;
            this.btnHeatingTempSave.Location = new System.Drawing.Point(239, 215);
            this.btnHeatingTempSave.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnHeatingTempSave.Name = "btnHeatingTempSave";
            this.btnHeatingTempSave.Size = new System.Drawing.Size(126, 51);
            this.btnHeatingTempSave.TabIndex = 41;
            this.btnHeatingTempSave.Text = "Save";
            this.btnHeatingTempSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnHeatingTempSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnHeatingTempSave.UseVisualStyleBackColor = true;
            this.btnHeatingTempSave.Click += new System.EventHandler(this.btnHeatingTempSave_Click);
            // 
            // txtBoxHeatingTemp
            // 
            this.txtBoxHeatingTemp.BackColor = System.Drawing.SystemColors.Control;
            this.txtBoxHeatingTemp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.txtBoxHeatingTemp.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxHeatingTemp.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtBoxHeatingTemp.Location = new System.Drawing.Point(171, 50);
            this.txtBoxHeatingTemp.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.txtBoxHeatingTemp.Name = "txtBoxHeatingTemp";
            this.txtBoxHeatingTemp.ReadOnly = true;
            this.txtBoxHeatingTemp.Size = new System.Drawing.Size(152, 30);
            this.txtBoxHeatingTemp.TabIndex = 30;
            this.txtBoxHeatingTemp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtBoxHeatingTemp.Click += new System.EventHandler(this.txtBoxDoorOpenCloseTimeout_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Navy;
            this.label3.Location = new System.Drawing.Point(27, 50);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(134, 25);
            this.label3.TabIndex = 22;
            this.label3.Text = "Heating temp";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Navy;
            this.label5.Location = new System.Drawing.Point(333, 55);
            this.label5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(24, 25);
            this.label5.TabIndex = 24;
            this.label5.Text = "C";
            // 
            // ConfigureForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1172, 824);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.Name = "ConfigureForm";
            this.Text = "Configure";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConfigureForm_FormClosing);
            this.Load += new System.EventHandler(this.ConfigureForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnParameterSave;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox txtBoxCylinderFwdBwdTimeout;
        private System.Windows.Forms.TextBox txtBoxDoorOpenCloseTimeout;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnHeatingTempSave;
        private System.Windows.Forms.TextBox txtBoxHeatingTemp;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBoxWaterHeatingTimeout;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBoxWaterFillTimeout;
        private System.Windows.Forms.Label label6;
    }
}