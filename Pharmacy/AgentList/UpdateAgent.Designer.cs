namespace Pharmacy.AgentList
{
    partial class UpdateAgent
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateAgent));
            this.bunifuElipse1 = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.bunifuDragControl1 = new Bunifu.Framework.UI.BunifuDragControl(this.components);
            this.agentlistcards = new Bunifu.Framework.UI.BunifuCards();
            this.addAgentId = new System.Windows.Forms.Label();
            this.bunifuImageButton2 = new Bunifu.Framework.UI.BunifuImageButton();
            this.bunifuFlatButton1 = new Bunifu.Framework.UI.BunifuFlatButton();
            this.addPhoneNumber = new WindowsFormsControlLibrary1.BunifuCustomTextbox();
            this.addCompanyName = new WindowsFormsControlLibrary1.BunifuCustomTextbox();
            this.addAgentName = new WindowsFormsControlLibrary1.BunifuCustomTextbox();
            this.bunifuCustomLabel72 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.bunifuCustomLabel71 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.bunifuCustomLabel70 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.bunifuCustomLabel69 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.agentlistcards.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton2)).BeginInit();
            this.SuspendLayout();
            // 
            // bunifuElipse1
            // 
            this.bunifuElipse1.ElipseRadius = 5;
            this.bunifuElipse1.TargetControl = this;
            // 
            // bunifuDragControl1
            // 
            this.bunifuDragControl1.Fixed = true;
            this.bunifuDragControl1.Horizontal = true;
            this.bunifuDragControl1.TargetControl = this.agentlistcards;
            this.bunifuDragControl1.Vertical = true;
            // 
            // agentlistcards
            // 
            this.agentlistcards.BackColor = System.Drawing.Color.White;
            this.agentlistcards.BorderRadius = 5;
            this.agentlistcards.BottomSahddow = true;
            this.agentlistcards.color = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.agentlistcards.Controls.Add(this.addAgentId);
            this.agentlistcards.Controls.Add(this.bunifuImageButton2);
            this.agentlistcards.Controls.Add(this.bunifuFlatButton1);
            this.agentlistcards.Controls.Add(this.addPhoneNumber);
            this.agentlistcards.Controls.Add(this.addCompanyName);
            this.agentlistcards.Controls.Add(this.addAgentName);
            this.agentlistcards.Controls.Add(this.bunifuCustomLabel72);
            this.agentlistcards.Controls.Add(this.bunifuCustomLabel71);
            this.agentlistcards.Controls.Add(this.bunifuCustomLabel70);
            this.agentlistcards.Controls.Add(this.bunifuCustomLabel69);
            this.agentlistcards.Dock = System.Windows.Forms.DockStyle.Top;
            this.agentlistcards.LeftSahddow = false;
            this.agentlistcards.Location = new System.Drawing.Point(0, 0);
            this.agentlistcards.Name = "agentlistcards";
            this.agentlistcards.RightSahddow = true;
            this.agentlistcards.ShadowDepth = 20;
            this.agentlistcards.Size = new System.Drawing.Size(522, 423);
            this.agentlistcards.TabIndex = 7;
            // 
            // addAgentId
            // 
            this.addAgentId.AutoSize = true;
            this.addAgentId.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addAgentId.Location = new System.Drawing.Point(244, 95);
            this.addAgentId.Name = "addAgentId";
            this.addAgentId.Size = new System.Drawing.Size(29, 20);
            this.addAgentId.TabIndex = 29;
            this.addAgentId.Text = "00";
            // 
            // bunifuImageButton2
            // 
            this.bunifuImageButton2.BackColor = System.Drawing.Color.White;
            this.bunifuImageButton2.Image = global::Pharmacy.Properties.Resources.cross_black;
            this.bunifuImageButton2.ImageActive = global::Pharmacy.Properties.Resources.cross_black;
            this.bunifuImageButton2.Location = new System.Drawing.Point(456, 29);
            this.bunifuImageButton2.Name = "bunifuImageButton2";
            this.bunifuImageButton2.Size = new System.Drawing.Size(25, 17);
            this.bunifuImageButton2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.bunifuImageButton2.TabIndex = 28;
            this.bunifuImageButton2.TabStop = false;
            this.bunifuImageButton2.Zoom = 20;
            this.bunifuImageButton2.Click += new System.EventHandler(this.bunifuImageButton2_Click);
            // 
            // bunifuFlatButton1
            // 
            this.bunifuFlatButton1.Activecolor = System.Drawing.Color.Gray;
            this.bunifuFlatButton1.BackColor = System.Drawing.Color.Gray;
            this.bunifuFlatButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bunifuFlatButton1.BorderRadius = 7;
            this.bunifuFlatButton1.ButtonText = "Save";
            this.bunifuFlatButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bunifuFlatButton1.DisabledColor = System.Drawing.Color.Gray;
            this.bunifuFlatButton1.Iconcolor = System.Drawing.Color.Transparent;
            this.bunifuFlatButton1.Iconimage = ((System.Drawing.Image)(resources.GetObject("bunifuFlatButton1.Iconimage")));
            this.bunifuFlatButton1.Iconimage_right = null;
            this.bunifuFlatButton1.Iconimage_right_Selected = null;
            this.bunifuFlatButton1.Iconimage_Selected = null;
            this.bunifuFlatButton1.IconMarginLeft = 0;
            this.bunifuFlatButton1.IconMarginRight = 0;
            this.bunifuFlatButton1.IconRightVisible = true;
            this.bunifuFlatButton1.IconRightZoom = 0D;
            this.bunifuFlatButton1.IconVisible = true;
            this.bunifuFlatButton1.IconZoom = 90D;
            this.bunifuFlatButton1.IsTab = false;
            this.bunifuFlatButton1.Location = new System.Drawing.Point(353, 329);
            this.bunifuFlatButton1.Name = "bunifuFlatButton1";
            this.bunifuFlatButton1.Normalcolor = System.Drawing.Color.Gray;
            this.bunifuFlatButton1.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.bunifuFlatButton1.OnHoverTextColor = System.Drawing.Color.White;
            this.bunifuFlatButton1.selected = false;
            this.bunifuFlatButton1.Size = new System.Drawing.Size(97, 43);
            this.bunifuFlatButton1.TabIndex = 11;
            this.bunifuFlatButton1.Text = "Save";
            this.bunifuFlatButton1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bunifuFlatButton1.Textcolor = System.Drawing.Color.White;
            this.bunifuFlatButton1.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuFlatButton1.Click += new System.EventHandler(this.bunifuFlatButton1_Click);
            // 
            // addPhoneNumber
            // 
            this.addPhoneNumber.BorderColor = System.Drawing.Color.SeaGreen;
            this.addPhoneNumber.Location = new System.Drawing.Point(243, 235);
            this.addPhoneNumber.Multiline = true;
            this.addPhoneNumber.Name = "addPhoneNumber";
            this.addPhoneNumber.Size = new System.Drawing.Size(171, 30);
            this.addPhoneNumber.TabIndex = 10;
            // 
            // addCompanyName
            // 
            this.addCompanyName.BorderColor = System.Drawing.Color.SeaGreen;
            this.addCompanyName.Location = new System.Drawing.Point(244, 185);
            this.addCompanyName.Multiline = true;
            this.addCompanyName.Name = "addCompanyName";
            this.addCompanyName.Size = new System.Drawing.Size(171, 30);
            this.addCompanyName.TabIndex = 9;
            // 
            // addAgentName
            // 
            this.addAgentName.BorderColor = System.Drawing.Color.SeaGreen;
            this.addAgentName.Location = new System.Drawing.Point(244, 136);
            this.addAgentName.Multiline = true;
            this.addAgentName.Name = "addAgentName";
            this.addAgentName.Size = new System.Drawing.Size(171, 30);
            this.addAgentName.TabIndex = 8;
            // 
            // bunifuCustomLabel72
            // 
            this.bunifuCustomLabel72.AutoSize = true;
            this.bunifuCustomLabel72.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuCustomLabel72.Location = new System.Drawing.Point(131, 236);
            this.bunifuCustomLabel72.Name = "bunifuCustomLabel72";
            this.bunifuCustomLabel72.Size = new System.Drawing.Size(101, 17);
            this.bunifuCustomLabel72.TabIndex = 7;
            this.bunifuCustomLabel72.Text = "Phone Number";
            // 
            // bunifuCustomLabel71
            // 
            this.bunifuCustomLabel71.AutoSize = true;
            this.bunifuCustomLabel71.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuCustomLabel71.Location = new System.Drawing.Point(131, 189);
            this.bunifuCustomLabel71.Name = "bunifuCustomLabel71";
            this.bunifuCustomLabel71.Size = new System.Drawing.Size(106, 17);
            this.bunifuCustomLabel71.TabIndex = 5;
            this.bunifuCustomLabel71.Text = "Company Name";
            // 
            // bunifuCustomLabel70
            // 
            this.bunifuCustomLabel70.AutoSize = true;
            this.bunifuCustomLabel70.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuCustomLabel70.Location = new System.Drawing.Point(131, 142);
            this.bunifuCustomLabel70.Name = "bunifuCustomLabel70";
            this.bunifuCustomLabel70.Size = new System.Drawing.Size(85, 17);
            this.bunifuCustomLabel70.TabIndex = 3;
            this.bunifuCustomLabel70.Text = "Agent Name";
            // 
            // bunifuCustomLabel69
            // 
            this.bunifuCustomLabel69.AutoSize = true;
            this.bunifuCustomLabel69.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuCustomLabel69.Location = new System.Drawing.Point(130, 95);
            this.bunifuCustomLabel69.Name = "bunifuCustomLabel69";
            this.bunifuCustomLabel69.Size = new System.Drawing.Size(63, 17);
            this.bunifuCustomLabel69.TabIndex = 1;
            this.bunifuCustomLabel69.Text = "Agent ID";
            // 
            // UpdateAgent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(522, 458);
            this.Controls.Add(this.agentlistcards);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "UpdateAgent";
            this.Text = "UpdateAgent";
            this.Load += new System.EventHandler(this.UpdateAgent_Load);
            this.agentlistcards.ResumeLayout(false);
            this.agentlistcards.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Bunifu.Framework.UI.BunifuElipse bunifuElipse1;
        private Bunifu.Framework.UI.BunifuDragControl bunifuDragControl1;
        private Bunifu.Framework.UI.BunifuCards agentlistcards;
        private Bunifu.Framework.UI.BunifuImageButton bunifuImageButton2;
        private Bunifu.Framework.UI.BunifuFlatButton bunifuFlatButton1;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel72;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel71;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel70;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel69;
        public WindowsFormsControlLibrary1.BunifuCustomTextbox addPhoneNumber;
        public WindowsFormsControlLibrary1.BunifuCustomTextbox addCompanyName;
        public WindowsFormsControlLibrary1.BunifuCustomTextbox addAgentName;
        public System.Windows.Forms.Label addAgentId;
    }
}