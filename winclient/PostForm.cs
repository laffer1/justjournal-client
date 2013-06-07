using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Text;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using iTunesLib;

namespace JustJournal
{
	/// <summary>
	/// Post Journal entries on JustJournal.com
	/// </summary>
	public class PostForm : Form
	{

		[DllImport("user32.dll",EntryPoint="FindWindow")]
// ReSharper disable InconsistentNaming
		private static extern uint FindWindow(string _ClassName, string _WindowName);
// ReSharper restore InconsistentNaming

		[DllImport(dllName: "user32.dll", CharSet = CharSet.Auto)]
        private static extern uint GetWindowText(System.UInt64 hWnd, StringBuilder lpString, uint nMaxCount);
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.Button btnPost;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtSubject;
		private System.Windows.Forms.ComboBox cboSecurity;
		private System.Windows.Forms.ComboBox cboLocation;
		private System.Windows.Forms.ComboBox cboMood;
		private System.Windows.Forms.RichTextBox rtbBody;
		private System.Windows.Forms.TextBox txtMusic;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.MenuItem mEntryAllowComments;
		private System.Windows.Forms.MenuItem mEntryEmailComments;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.MenuItem menuItem8;
		private System.Windows.Forms.MenuItem menuItem9;
		private System.Windows.Forms.MenuItem menuItem10;
		private System.Windows.Forms.MenuItem menuItem11;
		private System.Windows.Forms.Button btnMusicSense;
        private System.Windows.Forms.Label lblCurrentUser;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.MenuItem menuItem12;
		private System.Windows.Forms.MenuItem mnuFormattedText;
		private System.Windows.Forms.MenuItem mnuRawText;
		private System.Windows.Forms.MenuItem menuItem13;
		private System.Windows.Forms.MenuItem menuItem15;
		private System.Windows.Forms.MenuItem mnuFont;
		private System.Windows.Forms.MenuItem mnuNormal;
		private System.Windows.Forms.MenuItem mnuBold;
		private System.Windows.Forms.MenuItem mnuItalic;
		private System.Windows.Forms.MenuItem mnuUnderline;
		private System.Windows.Forms.MenuItem menuItem14;
		private System.Windows.Forms.FontDialog fontDlg;
		private System.Windows.Forms.MenuItem menuItem16;
		private System.Windows.Forms.MenuItem menuItem17;
		private System.Windows.Forms.MenuItem menuItem18;
		private System.Windows.Forms.OpenFileDialog openDlg;
		private System.Windows.Forms.SaveFileDialog saveDlg;
		private System.Windows.Forms.ColorDialog colorDlg;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem menuItem19;
		private System.Windows.Forms.MenuItem menuItem20;
		private System.Windows.Forms.MenuItem menuItem21;
		private System.Windows.Forms.MenuItem menuItem22;
		private System.Windows.Forms.MenuItem menuItem23;
		private System.Windows.Forms.ContextMenu editBoxMenu;
		private System.Windows.Forms.MenuItem menuItem24;
		private System.Windows.Forms.MenuItem menuItem25;
		private System.Windows.Forms.MenuItem menuItem26;
		private System.Windows.Forms.MenuItem menuItem27;
		private System.Windows.Forms.MenuItem menuItem28;
		private System.Windows.Forms.ColorDialog colorDlgBg;
		private System.Windows.Forms.MenuItem menuItem30;
		private System.Windows.Forms.MenuItem menuItem31;
        private ToolStrip toolStrip1;
        private ToolStripButton toolStripButton1;
        private ToolStripButton toolStripButton2;
        private ToolStripButton toolStripButton3;
        private ToolStripButton toolStripButton4;
        private ToolStripButton toolStripButton5;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripButton toolStripButton6;
        private ToolStripButton toolStripButton7;
        private PictureBox pictureBox1;
        private ToolStripContainer toolStripContainer1;
        private ToolStrip toolStrip2;
        private ToolStrip toolStrip3;
        private ToolStripButton toolStripButton9;
        private ToolStripButton toolStripButton10;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton toolStripButton11;
        private ToolStripButton toolStripButton12;
        private ToolStripButton toolStripButton13;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton toolStripButton14;
        private ToolStripButton toolStripButton15;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripButton tsbAbout;
        private TextBox txtTags;
        private Label lblTags;
		private System.ComponentModel.IContainer components;

		public PostForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

			if (!JustJournalCore.EnableSpellCheck)
				this.menuItem31.Enabled = false; // turn off spell check

			RegistryKey reg = Registry.CurrentUser.CreateSubKey( "SOFTWARE\\JustJournal\\Post" );
		    if (reg != null)
		    {
		        cboSecurity.SelectedIndex = (int)reg.GetValue("security",0);
		        cboLocation.SelectedIndex = (int)reg.GetValue("location",0);
		        reg.Close();
		    }

		    lblCurrentUser.Text = JustJournalCore.UserName;

			// If we need the moods, go get them.
			if ( JustJournalCore.Moods.Count == 0)
				JustJournalCore.RetrieveMoods();

			cboMood.DataSource = JustJournalCore.Moods;
			cboMood.DisplayMember = "Name";
			cboMood.ValueMember = "Id";
			cboMood.SelectedIndex = cboMood.FindStringExact("Not Specified",0);

            if (JustJournalCore.EnableMusicDetection)
                txtMusic.Text = DetectMusic();
            else
            {
                txtMusic.Text = "";
                txtMusic.Enabled = false;
                btnMusicSense.Enabled = false;
            }
		}


		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			RegistryKey reg = Registry.CurrentUser.CreateSubKey( "SOFTWARE\\JustJournal\\Post" );
		    if (reg != null)
		    {
		        reg.SetValue("security", cboSecurity.SelectedIndex);
		        reg.SetValue("location",cboLocation.SelectedIndex);
		        reg.Close();
		    }

		    if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PostForm));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem14 = new System.Windows.Forms.MenuItem();
            this.menuItem18 = new System.Windows.Forms.MenuItem();
            this.menuItem16 = new System.Windows.Forms.MenuItem();
            this.menuItem17 = new System.Windows.Forms.MenuItem();
            this.menuItem8 = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem20 = new System.Windows.Forms.MenuItem();
            this.menuItem21 = new System.Windows.Forms.MenuItem();
            this.menuItem22 = new System.Windows.Forms.MenuItem();
            this.menuItem9 = new System.Windows.Forms.MenuItem();
            this.menuItem10 = new System.Windows.Forms.MenuItem();
            this.menuItem11 = new System.Windows.Forms.MenuItem();
            this.menuItem23 = new System.Windows.Forms.MenuItem();
            this.menuItem19 = new System.Windows.Forms.MenuItem();
            this.menuItem13 = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItem28 = new System.Windows.Forms.MenuItem();
            this.mnuFont = new System.Windows.Forms.MenuItem();
            this.menuItem15 = new System.Windows.Forms.MenuItem();
            this.mnuNormal = new System.Windows.Forms.MenuItem();
            this.mnuBold = new System.Windows.Forms.MenuItem();
            this.mnuItalic = new System.Windows.Forms.MenuItem();
            this.mnuUnderline = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.mEntryAllowComments = new System.Windows.Forms.MenuItem();
            this.mEntryEmailComments = new System.Windows.Forms.MenuItem();
            this.menuItem12 = new System.Windows.Forms.MenuItem();
            this.mnuFormattedText = new System.Windows.Forms.MenuItem();
            this.mnuRawText = new System.Windows.Forms.MenuItem();
            this.menuItem30 = new System.Windows.Forms.MenuItem();
            this.menuItem31 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.btnPost = new System.Windows.Forms.Button();
            this.cboSecurity = new System.Windows.Forms.ComboBox();
            this.cboLocation = new System.Windows.Forms.ComboBox();
            this.cboMood = new System.Windows.Forms.ComboBox();
            this.txtSubject = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.rtbBody = new System.Windows.Forms.RichTextBox();
            this.editBoxMenu = new System.Windows.Forms.ContextMenu();
            this.menuItem24 = new System.Windows.Forms.MenuItem();
            this.menuItem25 = new System.Windows.Forms.MenuItem();
            this.menuItem26 = new System.Windows.Forms.MenuItem();
            this.menuItem27 = new System.Windows.Forms.MenuItem();
            this.txtMusic = new System.Windows.Forms.TextBox();
            this.btnMusicSense = new System.Windows.Forms.Button();
            this.lblCurrentUser = new System.Windows.Forms.Label();
            this.fontDlg = new System.Windows.Forms.FontDialog();
            this.colorDlg = new System.Windows.Forms.ColorDialog();
            this.openDlg = new System.Windows.Forms.OpenFileDialog();
            this.saveDlg = new System.Windows.Forms.SaveFileDialog();
            this.colorDlgBg = new System.Windows.Forms.ColorDialog();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton7 = new System.Windows.Forms.ToolStripButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton9 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton10 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton11 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton12 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton13 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton14 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton15 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbAbout = new System.Windows.Forms.ToolStripButton();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.txtTags = new System.Windows.Forms.TextBox();
            this.lblTags = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            this.imageList1.Images.SetKeyName(2, "");
            this.imageList1.Images.SetKeyName(3, "");
            this.imageList1.Images.SetKeyName(4, "");
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.menuItem2,
            this.menuItem13,
            this.menuItem6,
            this.menuItem3});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem14,
            this.menuItem18,
            this.menuItem16,
            this.menuItem17,
            this.menuItem8,
            this.menuItem7});
            this.menuItem1.Text = "&File";
            // 
            // menuItem14
            // 
            this.menuItem14.Index = 0;
            this.menuItem14.Text = "&Post";
            this.menuItem14.Click += new System.EventHandler(this.menuItem14_Click);
            // 
            // menuItem18
            // 
            this.menuItem18.Index = 1;
            this.menuItem18.Text = "-";
            // 
            // menuItem16
            // 
            this.menuItem16.Index = 2;
            this.menuItem16.Text = "&Load Draft...";
            this.menuItem16.Click += new System.EventHandler(this.menuItem16_Click);
            // 
            // menuItem17
            // 
            this.menuItem17.Index = 3;
            this.menuItem17.Text = "&Save Draft...";
            this.menuItem17.Click += new System.EventHandler(this.menuItem17_Click);
            // 
            // menuItem8
            // 
            this.menuItem8.Index = 4;
            this.menuItem8.Text = "-";
            // 
            // menuItem7
            // 
            this.menuItem7.Index = 5;
            this.menuItem7.Text = "&Close";
            this.menuItem7.Click += new System.EventHandler(this.menuItem7_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 1;
            this.menuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem20,
            this.menuItem21,
            this.menuItem22,
            this.menuItem9,
            this.menuItem10,
            this.menuItem11,
            this.menuItem23,
            this.menuItem19});
            this.menuItem2.Text = "&Edit";
            // 
            // menuItem20
            // 
            this.menuItem20.Index = 0;
            this.menuItem20.Text = "Undo";
            this.menuItem20.Click += new System.EventHandler(this.menuItem20_Click);
            // 
            // menuItem21
            // 
            this.menuItem21.Index = 1;
            this.menuItem21.Text = "Redo";
            this.menuItem21.Click += new System.EventHandler(this.menuItem21_Click);
            // 
            // menuItem22
            // 
            this.menuItem22.Index = 2;
            this.menuItem22.Text = "-";
            // 
            // menuItem9
            // 
            this.menuItem9.Index = 3;
            this.menuItem9.Text = "Cu&t";
            this.menuItem9.Click += new System.EventHandler(this.menuItem9_Click);
            // 
            // menuItem10
            // 
            this.menuItem10.Index = 4;
            this.menuItem10.Text = "&Copy";
            this.menuItem10.Click += new System.EventHandler(this.menuItem10_Click);
            // 
            // menuItem11
            // 
            this.menuItem11.Index = 5;
            this.menuItem11.Text = "&Paste";
            this.menuItem11.Click += new System.EventHandler(this.menuItem11_Click);
            // 
            // menuItem23
            // 
            this.menuItem23.Index = 6;
            this.menuItem23.Text = "-";
            // 
            // menuItem19
            // 
            this.menuItem19.Index = 7;
            this.menuItem19.Text = "Select All";
            this.menuItem19.Click += new System.EventHandler(this.menuItem19_Click);
            // 
            // menuItem13
            // 
            this.menuItem13.Index = 2;
            this.menuItem13.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem4,
            this.menuItem28,
            this.mnuFont,
            this.menuItem15,
            this.mnuNormal,
            this.mnuBold,
            this.mnuItalic,
            this.mnuUnderline});
            this.menuItem13.Text = "F&ormat";
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 0;
            this.menuItem4.Text = "&Color...";
            this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click_1);
            // 
            // menuItem28
            // 
            this.menuItem28.Index = 1;
            this.menuItem28.Text = "&Background Color";
            this.menuItem28.Click += new System.EventHandler(this.menuItem28_Click);
            // 
            // mnuFont
            // 
            this.mnuFont.Index = 2;
            this.mnuFont.Text = "&Font...";
            this.mnuFont.Click += new System.EventHandler(this.mnuFont_Click);
            // 
            // menuItem15
            // 
            this.menuItem15.Index = 3;
            this.menuItem15.Text = "-";
            // 
            // mnuNormal
            // 
            this.mnuNormal.Index = 4;
            this.mnuNormal.Shortcut = System.Windows.Forms.Shortcut.CtrlN;
            this.mnuNormal.Text = "&Normal";
            this.mnuNormal.Click += new System.EventHandler(this.mnuNormal_Click);
            // 
            // mnuBold
            // 
            this.mnuBold.Index = 5;
            this.mnuBold.Shortcut = System.Windows.Forms.Shortcut.CtrlB;
            this.mnuBold.Text = "&Bold";
            this.mnuBold.Click += new System.EventHandler(this.mnuBold_Click);
            // 
            // mnuItalic
            // 
            this.mnuItalic.Index = 6;
            this.mnuItalic.Shortcut = System.Windows.Forms.Shortcut.CtrlI;
            this.mnuItalic.Text = "&Italic";
            this.mnuItalic.Click += new System.EventHandler(this.mnuItalic_Click);
            // 
            // mnuUnderline
            // 
            this.mnuUnderline.Index = 7;
            this.mnuUnderline.Shortcut = System.Windows.Forms.Shortcut.CtrlU;
            this.mnuUnderline.Text = "&Underline";
            this.mnuUnderline.Click += new System.EventHandler(this.mnuUnderline_Click);
            // 
            // menuItem6
            // 
            this.menuItem6.Index = 3;
            this.menuItem6.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mEntryAllowComments,
            this.mEntryEmailComments,
            this.menuItem12,
            this.mnuFormattedText,
            this.mnuRawText,
            this.menuItem30,
            this.menuItem31});
            this.menuItem6.Text = "E&ntry";
            // 
            // mEntryAllowComments
            // 
            this.mEntryAllowComments.Checked = true;
            this.mEntryAllowComments.Index = 0;
            this.mEntryAllowComments.Text = "&Allow Comments";
            this.mEntryAllowComments.Click += new System.EventHandler(this.mEntryAllowComments_Click);
            // 
            // mEntryEmailComments
            // 
            this.mEntryEmailComments.Checked = true;
            this.mEntryEmailComments.Index = 1;
            this.mEntryEmailComments.Text = "&E-mail Comments";
            this.mEntryEmailComments.Click += new System.EventHandler(this.mEntryEmailComments_Click);
            // 
            // menuItem12
            // 
            this.menuItem12.Index = 2;
            this.menuItem12.Text = "-";
            // 
            // mnuFormattedText
            // 
            this.mnuFormattedText.Checked = true;
            this.mnuFormattedText.Index = 3;
            this.mnuFormattedText.RadioCheck = true;
            this.mnuFormattedText.Text = "&Formatted Text";
            this.mnuFormattedText.Click += new System.EventHandler(this.mnuFormattedText_Click);
            // 
            // mnuRawText
            // 
            this.mnuRawText.Index = 4;
            this.mnuRawText.RadioCheck = true;
            this.mnuRawText.Text = "&Raw Text";
            this.mnuRawText.Click += new System.EventHandler(this.mnuRawText_Click);
            // 
            // menuItem30
            // 
            this.menuItem30.Index = 5;
            this.menuItem30.Text = "-";
            // 
            // menuItem31
            // 
            this.menuItem31.Index = 6;
            this.menuItem31.Shortcut = System.Windows.Forms.Shortcut.F7;
            this.menuItem31.Text = "&Check Spelling";
            this.menuItem31.Click += new System.EventHandler(this.menuItem31_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 4;
            this.menuItem3.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem5});
            this.menuItem3.Text = "&Help";
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 0;
            this.menuItem5.Text = "&About";
            this.menuItem5.Click += new System.EventHandler(this.menuItem5_Click);
            // 
            // btnPost
            // 
            this.btnPost.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPost.BackColor = System.Drawing.Color.MidnightBlue;
            this.btnPost.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnPost.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPost.ForeColor = System.Drawing.Color.White;
            this.btnPost.Location = new System.Drawing.Point(554, 450);
            this.btnPost.Name = "btnPost";
            this.btnPost.Size = new System.Drawing.Size(80, 24);
            this.btnPost.TabIndex = 8;
            this.btnPost.Text = "Post";
            this.btnPost.UseVisualStyleBackColor = false;
            this.btnPost.Click += new System.EventHandler(this.btnPost_Click);
            // 
            // cboSecurity
            // 
            this.cboSecurity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cboSecurity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSecurity.Items.AddRange(new object[] {
            "public",
            "friends",
            "private"});
            this.cboSecurity.Location = new System.Drawing.Point(378, 450);
            this.cboSecurity.Name = "cboSecurity";
            this.cboSecurity.Size = new System.Drawing.Size(144, 21);
            this.cboSecurity.TabIndex = 7;
            // 
            // cboLocation
            // 
            this.cboLocation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLocation.Items.AddRange(new object[] {
            "Not Specified",
            "Home",
            "Other",
            "School",
            "Work"});
            this.cboLocation.Location = new System.Drawing.Point(491, 57);
            this.cboLocation.Name = "cboLocation";
            this.cboLocation.Size = new System.Drawing.Size(144, 21);
            this.cboLocation.TabIndex = 1;
            // 
            // cboMood
            // 
            this.cboMood.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMood.Location = new System.Drawing.Point(57, 88);
            this.cboMood.Name = "cboMood";
            this.cboMood.Size = new System.Drawing.Size(144, 21);
            this.cboMood.TabIndex = 2;
            // 
            // txtSubject
            // 
            this.txtSubject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSubject.Location = new System.Drawing.Point(57, 57);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Size = new System.Drawing.Size(370, 20);
            this.txtSubject.TabIndex = 0;
            this.txtSubject.TextChanged += new System.EventHandler(this.txtSubject_TextChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Location = new System.Drawing.Point(435, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 18);
            this.label2.TabIndex = 7;
            this.label2.Text = "Location:";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(17, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 16);
            this.label3.TabIndex = 8;
            this.label3.Text = "Mood:";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(9, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 16);
            this.label4.TabIndex = 9;
            this.label4.Text = "Subject:";
            // 
            // rtbBody
            // 
            this.rtbBody.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbBody.AutoSize = true;
            this.rtbBody.ContextMenu = this.editBoxMenu;
            this.rtbBody.Location = new System.Drawing.Point(0, 117);
            this.rtbBody.Name = "rtbBody";
            this.rtbBody.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.rtbBody.Size = new System.Drawing.Size(642, 296);
            this.rtbBody.TabIndex = 5;
            this.rtbBody.Text = "";
            // 
            // editBoxMenu
            // 
            this.editBoxMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem24,
            this.menuItem25,
            this.menuItem26,
            this.menuItem27});
            // 
            // menuItem24
            // 
            this.menuItem24.Index = 0;
            this.menuItem24.Text = "C&ut";
            this.menuItem24.Click += new System.EventHandler(this.menuItem24_Click);
            // 
            // menuItem25
            // 
            this.menuItem25.Index = 1;
            this.menuItem25.Text = "&Copy";
            this.menuItem25.Click += new System.EventHandler(this.menuItem25_Click);
            // 
            // menuItem26
            // 
            this.menuItem26.Index = 2;
            this.menuItem26.Text = "&Paste";
            this.menuItem26.Click += new System.EventHandler(this.menuItem26_Click);
            // 
            // menuItem27
            // 
            this.menuItem27.Index = 3;
            this.menuItem27.Text = "&Delete";
            this.menuItem27.Click += new System.EventHandler(this.menuItem27_Click);
            // 
            // txtMusic
            // 
            this.txtMusic.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMusic.Location = new System.Drawing.Point(217, 88);
            this.txtMusic.MaxLength = 100;
            this.txtMusic.Name = "txtMusic";
            this.txtMusic.Size = new System.Drawing.Size(346, 20);
            this.txtMusic.TabIndex = 3;
            // 
            // btnMusicSense
            // 
            this.btnMusicSense.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMusicSense.BackColor = System.Drawing.SystemColors.Control;
            this.btnMusicSense.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnMusicSense.Location = new System.Drawing.Point(571, 88);
            this.btnMusicSense.Name = "btnMusicSense";
            this.btnMusicSense.Size = new System.Drawing.Size(64, 19);
            this.btnMusicSense.TabIndex = 4;
            this.btnMusicSense.Text = "Music";
            this.btnMusicSense.UseVisualStyleBackColor = false;
            this.btnMusicSense.Click += new System.EventHandler(this.btnMusicSense_Click);
            // 
            // lblCurrentUser
            // 
            this.lblCurrentUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCurrentUser.AutoSize = true;
            this.lblCurrentUser.Location = new System.Drawing.Point(39, 461);
            this.lblCurrentUser.Name = "lblCurrentUser";
            this.lblCurrentUser.Size = new System.Drawing.Size(66, 13);
            this.lblCurrentUser.TabIndex = 16;
            this.lblCurrentUser.Text = "Current User";
            // 
            // openDlg
            // 
            this.openDlg.DefaultExt = "rtf";
            this.openDlg.Filter = "RTF Files (*.rtf)|*.rtf|Text Files(*.txt)|*.txt|All Files(*.*)|*.*";
            this.openDlg.Title = "Load Draft";
            // 
            // saveDlg
            // 
            this.saveDlg.DefaultExt = "rtf";
            this.saveDlg.Filter = "RTF Files (*.rtf)|*.rtf|Text Files(*.txt)|*.txt|All Files(*.*)|*.*";
            this.saveDlg.Title = "Save Draft";
            // 
            // toolStrip1
            // 
            this.toolStrip1.AllowItemReorder = true;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton3,
            this.toolStripButton4,
            this.toolStripButton5,
            this.toolStripSeparator3,
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripButton6,
            this.toolStripButton7});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 23);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(640, 23);
            this.toolStrip1.Stretch = true;
            this.toolStrip1.TabIndex = 17;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.CheckOnClick = true;
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(23, 20);
            this.toolStripButton3.Text = "Bold";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.CheckOnClick = true;
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(23, 20);
            this.toolStripButton4.Text = "Italic";
            this.toolStripButton4.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.CheckOnClick = true;
            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton5.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton5.Image")));
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(23, 20);
            this.toolStripButton5.Text = "Underline";
            this.toolStripButton5.Click += new System.EventHandler(this.toolStripButton5_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 23);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 20);
            this.toolStripButton1.Text = "Normal";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 20);
            this.toolStripButton2.Text = "Font";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton6.Image")));
            this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.Size = new System.Drawing.Size(56, 20);
            this.toolStripButton6.Text = "Color";
            this.toolStripButton6.Click += new System.EventHandler(this.toolStripButton6_Click);
            // 
            // toolStripButton7
            // 
            this.toolStripButton7.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton7.Image")));
            this.toolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton7.Name = "toolStripButton7";
            this.toolStripButton7.Size = new System.Drawing.Size(91, 20);
            this.toolStripButton7.Text = "Background";
            this.toolStripButton7.Click += new System.EventHandler(this.toolStripButton7_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(14, 449);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(27, 30);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 18;
            this.pictureBox1.TabStop = false;
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(640, 4);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Top;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.MaximumSize = new System.Drawing.Size(0, 50);
            this.toolStripContainer1.MinimumSize = new System.Drawing.Size(0, 25);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(640, 50);
            this.toolStripContainer1.TabIndex = 19;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip2);
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip3);
            // 
            // toolStrip3
            // 
            this.toolStrip3.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton9,
            this.toolStripButton10,
            this.toolStripSeparator1,
            this.toolStripButton11,
            this.toolStripButton12,
            this.toolStripButton13,
            this.toolStripSeparator2,
            this.toolStripButton14,
            this.toolStripButton15,
            this.toolStripSeparator4,
            this.tsbAbout});
            this.toolStrip3.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip3.Location = new System.Drawing.Point(0, 0);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new System.Drawing.Size(640, 23);
            this.toolStrip3.Stretch = true;
            this.toolStrip3.TabIndex = 19;
            // 
            // toolStripButton9
            // 
            this.toolStripButton9.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton9.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton9.Image")));
            this.toolStripButton9.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton9.Name = "toolStripButton9";
            this.toolStripButton9.Size = new System.Drawing.Size(23, 20);
            this.toolStripButton9.Text = "Open";
            this.toolStripButton9.Click += new System.EventHandler(this.toolStripButton9_Click);
            // 
            // toolStripButton10
            // 
            this.toolStripButton10.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton10.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton10.Image")));
            this.toolStripButton10.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton10.Name = "toolStripButton10";
            this.toolStripButton10.Size = new System.Drawing.Size(23, 20);
            this.toolStripButton10.Text = "Save";
            this.toolStripButton10.Click += new System.EventHandler(this.toolStripButton10_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 23);
            // 
            // toolStripButton11
            // 
            this.toolStripButton11.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton11.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton11.Image")));
            this.toolStripButton11.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton11.Name = "toolStripButton11";
            this.toolStripButton11.Size = new System.Drawing.Size(23, 20);
            this.toolStripButton11.Text = "Cut";
            this.toolStripButton11.Click += new System.EventHandler(this.toolStripButton11_Click);
            // 
            // toolStripButton12
            // 
            this.toolStripButton12.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton12.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton12.Image")));
            this.toolStripButton12.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton12.Name = "toolStripButton12";
            this.toolStripButton12.Size = new System.Drawing.Size(23, 20);
            this.toolStripButton12.Text = "Copy";
            this.toolStripButton12.Click += new System.EventHandler(this.toolStripButton12_Click);
            // 
            // toolStripButton13
            // 
            this.toolStripButton13.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton13.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton13.Image")));
            this.toolStripButton13.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton13.Name = "toolStripButton13";
            this.toolStripButton13.Size = new System.Drawing.Size(23, 20);
            this.toolStripButton13.Text = "Paste";
            this.toolStripButton13.Click += new System.EventHandler(this.toolStripButton13_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 23);
            // 
            // toolStripButton14
            // 
            this.toolStripButton14.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton14.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton14.Image")));
            this.toolStripButton14.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton14.Name = "toolStripButton14";
            this.toolStripButton14.Size = new System.Drawing.Size(23, 20);
            this.toolStripButton14.Text = "Undo";
            this.toolStripButton14.Click += new System.EventHandler(this.toolStripButton14_Click);
            // 
            // toolStripButton15
            // 
            this.toolStripButton15.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton15.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton15.Image")));
            this.toolStripButton15.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton15.Name = "toolStripButton15";
            this.toolStripButton15.Size = new System.Drawing.Size(23, 20);
            this.toolStripButton15.Text = "Redo";
            this.toolStripButton15.Click += new System.EventHandler(this.toolStripButton15_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 23);
            // 
            // tsbAbout
            // 
            this.tsbAbout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAbout.Image = ((System.Drawing.Image)(resources.GetObject("tsbAbout.Image")));
            this.tsbAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAbout.Name = "tsbAbout";
            this.tsbAbout.Size = new System.Drawing.Size(23, 20);
            this.tsbAbout.Text = "About";
            this.tsbAbout.Click += new System.EventHandler(this.tsbAbout_Click);
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip2.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip2.Location = new System.Drawing.Point(149, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(1, 0);
            this.toolStrip2.TabIndex = 18;
            // 
            // txtTags
            // 
            this.txtTags.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTags.Location = new System.Drawing.Point(49, 419);
            this.txtTags.Name = "txtTags";
            this.txtTags.Size = new System.Drawing.Size(586, 20);
            this.txtTags.TabIndex = 6;
            // 
            // lblTags
            // 
            this.lblTags.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTags.AutoSize = true;
            this.lblTags.Location = new System.Drawing.Point(9, 421);
            this.lblTags.Name = "lblTags";
            this.lblTags.Size = new System.Drawing.Size(34, 13);
            this.lblTags.TabIndex = 21;
            this.lblTags.Text = "Tags:";
            // 
            // PostForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(640, 481);
            this.Controls.Add(this.lblTags);
            this.Controls.Add(this.txtTags);
            this.Controls.Add(this.toolStripContainer1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblCurrentUser);
            this.Controls.Add(this.txtSubject);
            this.Controls.Add(this.txtMusic);
            this.Controls.Add(this.rtbBody);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboMood);
            this.Controls.Add(this.cboLocation);
            this.Controls.Add(this.cboSecurity);
            this.Controls.Add(this.btnPost);
            this.Controls.Add(this.btnMusicSense);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Menu = this.mainMenu1;
            this.Name = "PostForm";
            this.Text = "<no subject> - JustJournal";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion


		private void txtSubject_TextChanged(object sender, EventArgs e)
		{
			if( txtSubject.Text.Length > 0 ) 
			{
				Text = string.Format("{0} - " + "JustJournal", txtSubject.Text);
			}
			else
			{
				Text = @"<no subject> - JustJournal";
			}
		}

	    private void btnPost_Click(object sender, EventArgs e)
		{
			if (JustJournalCore.AutoSpellCheck)
			{
				if( SpellingCheck() )
				{
					if( MessageBox.Show( this,  
						@"A spelling error was found.  Do you wish to post anyway?", @"Spelling Error",MessageBoxButtons.YesNo ) == DialogResult.No )
					{
						return;
					}
				}
			}

			this.Enabled = false;
			this.Cursor = Cursors.WaitCursor;
			
	     	WebClient client = new WebClient();
			string uriString;
            if (JustJournalCore.EnableSsl)
                uriString = "https://";
			else
			    uriString = "http://";		 
			uriString += "www.justjournal.com/updateJournal";
			
			// Create a new NameValueCollection instance to hold some custom parameters to be posted to the URL.
			NameValueCollection myNameValueCollection = new NameValueCollection();

			// Add a user agent header in case the 
			// requested URI contains a query.
			client.Headers.Add("User-Agent", JustJournalCore.Version);

			// Add necessary parameter/value pairs to the name/value container.
			
			myNameValueCollection.Add("user", JustJournalCore.UserName);            
			myNameValueCollection.Add("pass", JustJournalCore.Password);
			myNameValueCollection.Add("keeplogin", "NO");
			DateTime d1 = DateTime.Now;
            //myNameValueCollection.Add("date", "2005-09-30 16:18:26");
			myNameValueCollection.Add("date", d1.Year + "-" + d1.Month + "-" + d1.Day + " " + d1.Hour + ":" + d1.Minute + ":" + d1.Second);
			myNameValueCollection.Add("subject", txtSubject.Text);
   
			if (mnuFormattedText.Checked)
                myNameValueCollection.Add("body", RtfToHtml.Convert(rtbBody.Rtf,
                    RtfToHtml.ToHexColor(rtbBody.BackColor.R, rtbBody.BackColor.G,
                        rtbBody.BackColor.B)));
			else
			    myNameValueCollection.Add("body", rtbBody.Text);

			switch (cboSecurity.Text)
			{
                case "public":
					myNameValueCollection.Add("security","2");
					break;
				case "friends":
					myNameValueCollection.Add("security","1");
					break;
				case "private":
					myNameValueCollection.Add("security","0");
					break;
				default:
					myNameValueCollection.Add("security","0");
					break;
			}

			switch (cboLocation.Text)
			{
                case "Home":
                    myNameValueCollection.Add("location", "1");
					break;
				case "Other":
                    myNameValueCollection.Add("location", "5");
					break;
			    case "Work":
                    myNameValueCollection.Add("location", "2");
					break;
				case "School":
                    myNameValueCollection.Add("location", "3");
					break;
				default:
					// not specified case
					myNameValueCollection.Add("location", "0");
					break;
			}

			myNameValueCollection.Add("mood", cboMood.SelectedValue.ToString());
			myNameValueCollection.Add("music", txtMusic.Text);
            myNameValueCollection.Add("tags", txtTags.Text);

			myNameValueCollection.Add("aformat", string.Empty);

			if (mEntryAllowComments.Checked)
				myNameValueCollection.Add("allow_comment", "checked");

			if (mEntryEmailComments.Checked)
				myNameValueCollection.Add("email_comment", "checked");

			// Upload the NameValueCollection.
			var responseArray = client.UploadValues(uriString,"POST",myNameValueCollection);

			// Decode and display the response.
			//Console.WriteLine("\nResponse received was :\n{0}",Encoding.ASCII.GetString(responseArray));
			Cursor = Cursors.Default;
			Enabled = true;

			var resp = Encoding.ASCII.GetString(responseArray);
            if (resp.Length > 0 && resp.IndexOf("JJ.LOGIN.OK", StringComparison.Ordinal) == -1)
            {
// ReSharper disable ObjectCreationAsStatement
                new Alert(string.Format("Message \"{0}\" Posted.", txtSubject.Text), "");
// ReSharper restore ObjectCreationAsStatement
                Close();  // OK
            }
            else
            {
                MessageBox.Show(Encoding.ASCII.GetString(responseArray), @"Just Journal",
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
		}

		private void menuItem5_Click(object sender, EventArgs e)
		{
			new AboutForm().Show();
		}

		private void mEntryAllowComments_Click(object sender, EventArgs e)
		{
		    if (mEntryAllowComments.Checked)
				mEntryAllowComments.Checked = false;
			else
				mEntryAllowComments.Checked = true;
		}

		private void mEntryEmailComments_Click(object sender, EventArgs e)
		{
			if (mEntryEmailComments.Checked)
				mEntryEmailComments.Checked = false;
			else 
				mEntryEmailComments.Checked = true;
		}

		private void menuItem7_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void menuItem9_Click(object sender, EventArgs e)
		{
			if (txtSubject.Focused)
				txtSubject.Cut();
		    else if (txtMusic.Focused)
				txtMusic.Cut();
			else if ( rtbBody.Focused)
				rtbBody.Cut();
		}

		private void menuItem10_Click(object sender, EventArgs e)
		{
			if (txtSubject.Focused)
				txtSubject.Copy();
			else if (txtMusic.Focused)
				txtMusic.Copy();
			else if ( rtbBody.Focused)
				rtbBody.Copy();
		}

		private void menuItem11_Click(object sender, EventArgs e)
		{
			if (txtSubject.Focused)
				txtSubject.Paste();
			else if (txtMusic.Focused)
				txtMusic.Paste();
			else if ( rtbBody.Focused)
				rtbBody.Paste();
		}

		private void btnMusicSense_Click(object sender, EventArgs e)
		{
		    try
		    {
		        txtMusic.Text = DetectMusic();
		    }
		    catch( Exception ) //ignore any errors in here
		    {
			    txtMusic.Text = string.Empty;
		    } 
		}

		private void mnuFont_Click(object sender, EventArgs e)
		{
			fontDlg.Font = rtbBody.SelectionFont;
			if( fontDlg.ShowDialog() == DialogResult.OK ) 
			{
				rtbBody.SelectionFont = fontDlg.Font;
			}
		}

		private void mnuNormal_Click(object sender, EventArgs e)
		{
			var ft = rtbBody.SelectionFont;
			rtbBody.SelectionFont = new Font( ft.FontFamily, ft.Size, FontStyle.Regular );
            // bold, italic, underline off
            toolStripButton3.Checked = false;
            toolStripButton4.Checked = false;
            toolStripButton5.Checked = false;
		}

		private void mnuBold_Click(object sender, EventArgs e)
		{
			if( mnuRawText.Checked ) 
			{
				int start = rtbBody.SelectionStart;
				int length = rtbBody.SelectionLength;
				rtbBody.Text = rtbBody.Text.Substring(0,start) +
					@"<strong>" +	rtbBody.Text.Substring(start, length)
					+ @"</strong>" + rtbBody.Text.Substring( start + length);
				rtbBody.SelectionStart = start + 3;
				rtbBody.SelectionLength = length;				
			}
			else
			{
				Font ft = rtbBody.SelectionFont;
				FontStyle fs = ( ft.Style ^ FontStyle.Bold );
				rtbBody.SelectionFont = new Font( ft.FontFamily, ft.Size, fs );
                //toolStripButton3.Pressed = !toolStripButton3.Pressed; // swap button
			}
		}

		private void mnuItalic_Click(object sender, EventArgs e)
		{
			if( mnuRawText.Checked ) 
			{
				int start = rtbBody.SelectionStart;
				int length = rtbBody.SelectionLength;
				rtbBody.Text = rtbBody.Text.Substring(0,start) +
					@"<em>" +	rtbBody.Text.Substring(start, length)
					+ @"</em>" + rtbBody.Text.Substring( start + length);
				rtbBody.SelectionStart = start + 3;
				rtbBody.SelectionLength = length;				
			}
			else
			{
				Font ft = rtbBody.SelectionFont;
				FontStyle fs = ( ft.Style ^ FontStyle.Italic );
				rtbBody.SelectionFont = new Font( ft.FontFamily, ft.Size, fs );
                //toolStripButton4.Pressed = !toolStripButton4.Pressed;
			}
		}

		private void mnuUnderline_Click(object sender, EventArgs e)
		{
			if( mnuRawText.Checked ) 
			{
				int start = rtbBody.SelectionStart;
				int length = rtbBody.SelectionLength;
				rtbBody.Text = rtbBody.Text.Substring(0,start) +
					@"<u>" +	rtbBody.Text.Substring(start, length)
					+ @"</u>" + rtbBody.Text.Substring( start + length);
				rtbBody.SelectionStart = start + 3;
				rtbBody.SelectionLength = length;				
			}
			else
			{
				Font ft = rtbBody.SelectionFont;
				FontStyle fs = ( ft.Style ^ FontStyle.Underline );
				rtbBody.SelectionFont = new Font( ft.FontFamily, ft.Size, fs );
                //toolStripButton5.Pressed = !toolStripButton5.Pressed;
			}
		}

		private void mnuFormattedText_Click(object sender, EventArgs e)
		{
			mnuFormattedText.Checked = true;
			mnuRawText.Checked = false;
		}

		private void mnuRawText_Click(object sender, EventArgs e)
		{
			mnuRawText.Checked = true;
			mnuFormattedText.Checked = false;
		}

		private void menuItem14_Click(object sender, EventArgs e)
		{
			btnPost_Click( sender, e );
		}

		private void menuItem16_Click(object sender, EventArgs e)
		{
			try
			{
				if( openDlg.ShowDialog( this ).Equals( DialogResult.OK ) )
				{
				    rtbBody.LoadFile(openDlg.FileName,
				                     String.Compare(Path.GetExtension(openDlg.FileName), ".rtf",
				                                    StringComparison.OrdinalIgnoreCase) == 0
				                         ? RichTextBoxStreamType.RichText
				                         : RichTextBoxStreamType.PlainText);
				}
			}
			catch( Exception ex )
			{
				MessageBox.Show( this, ex.Message, @"Could not open file", MessageBoxButtons.OK,
					MessageBoxIcon.Exclamation );
			}
		}

		private void menuItem17_Click(object sender, EventArgs e)
		{
			try
			{
			    if (!saveDlg.ShowDialog(this).Equals(DialogResult.OK)) return;
			    var extension = Path.GetExtension(saveDlg.FileName);
			    rtbBody.SaveFile(saveDlg.FileName,
			                     extension != null && extension.ToLower().Equals(".rtf")
			                         ? RichTextBoxStreamType.RichText
			                         : RichTextBoxStreamType.PlainText);
			}
			catch( Exception ex )
			{
				MessageBox.Show( this, ex.Message, @"Could not save file", MessageBoxButtons.OK,
					MessageBoxIcon.Exclamation );
			}
		}

		private void menuItem4_Click_1(object sender, EventArgs e)
		{
			if (colorDlg.ShowDialog( this ).Equals( DialogResult.OK))
			{
				rtbBody.ForeColor = colorDlg.Color;
			}
		}

		private void menuItem19_Click(object sender, EventArgs e)
		{
			rtbBody.SelectAll();
		}

		private void menuItem20_Click(object sender, EventArgs e)
		{
			rtbBody.Undo();
		}

		private void menuItem21_Click(object sender, EventArgs e)
		{
			rtbBody.Redo();
		}

		private void menuItem24_Click(object sender, EventArgs e)
		{
			rtbBody.Cut();
		}

		private void menuItem25_Click(object sender, EventArgs e)
		{
			rtbBody.Copy();
		}

		private void menuItem26_Click(object sender, EventArgs e)
		{
			rtbBody.Paste();
		}

		private void menuItem27_Click(object sender, EventArgs e)
		{
		    rtbBody.SelectedRtf = string.Empty;
		}

		private static string DetectMusic()
		{
			// skip if we have it disabled.
            if (!JustJournalCore.EnableMusicDetection)
				return "";

			if (JustJournalCore.DetectItunes)
			{
				// iTunes Detection
				try 
				{
					string name = string.Empty;
				    IiTunes iTunesApp = new iTunesAppClass();

					if (iTunesApp.CurrentTrack.Name != null )
					{
						name += iTunesApp.CurrentTrack.Name;

						if (iTunesApp.CurrentTrack.Album != null)
						{
							name += " - ";
							name += iTunesApp.CurrentTrack.Album;
						}

						if (iTunesApp.CurrentTrack.Artist != null)
						{
							name += " - ";
							name += iTunesApp.CurrentTrack.Artist;
						}
					
					}
					return name;
				} 
				catch
				{
				    //MessageBox.Show("iTunes Error",e.Message,
				    //	MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}

			// other clients
		    {
		        uint player = 0;
		        uint whandle = 0;
		        int size;
		        var name = string.Empty;
		        var sb = new StringBuilder( 1024, 1024 );

		        var mediaPlayers = new Hashtable
		            {
		                {"PlayerCanvas", 1},
		                {"Apollo - Main Window", 2},
		                {"Winamp v1.x", 3},
		                {"STUDIO", 4},
		                {"MMJB:MAINWND", 5},
		                {"Sonique Window Class", 6},
		                {"FreeAmp", 7},
		                {"FOOBAR2000_CLASS", 8},
		                {"{DA7CD0DE-1602-45e6-89A1-C2CA151E008E}", 8},
		                {"Media Player 2", 9},
		                {"WMP Skin Host", 10},
		                {"MMFRAME_MAIN", 11},
		                {"STPAppClass", 12}
		            };
		        // Quintessential Player
		        // MUST be first to prevent an exception
		        // Apollo
		        // Winamp 2 and 5, and clones (e.g. foobar2000 with foo_winamp_spam)
		        // Winamp 3
		        // Musicmatch Jukebox
		        // Sonique
		        // FreeAmp
		        // foobar2000
		        // Windows Media Player 6
		        // Windows Media Player 7 with blogging skin
		        // Windows CD Player
		        // SysTrayPlayer (?)

		        foreach( string key in mediaPlayers.Keys )
		        {
		            whandle = FindWindow( key, null );
		            if (whandle <= 0) continue;
		            player = Convert.ToUInt32( mediaPlayers[key] );
		            break;
		        }

		        switch( player )
		        {
		            case 1: //QCD
		                GetWindowText( whandle, sb, 1024 );
		                name = sb.ToString();
		                if( name.Length > 0 )
		                {
		                    size = name.IndexOf("]", StringComparison.Ordinal) + 2;
		                    if( size > 0 )
		                    {
		                        name = name.Substring( size, name.Length - size );
		                    }
		                }
		                break;
		            case 2: //Apollo
		                GetWindowText( whandle, sb, 1024 );
		                name = sb.ToString();
		                if( name.Length > 0 )
		                {
		                    name = name.TrimStart( "0123456789 ".ToCharArray() );
		                    name = name.Substring( 0, name.LastIndexOf(" - (", StringComparison.Ordinal) );
		                    name = name.Substring( 0, name.LastIndexOf(" - ", StringComparison.Ordinal) );
		                }
		                break;
		            case 3: //Winamp 2/5
		            case 4: //Winamp 3
		                GetWindowText( whandle, sb, 1024 );
		                name = sb.ToString();
		                if( name.Length > 0 )
		                {
		                    int start = name.IndexOf(".", StringComparison.Ordinal) + 2;
		                    size = name.Length - start - 9;
		                    if( name.EndsWith( "[Paused]" ) )
		                    {
		                        if( JustJournalCore.WinampPaused )
		                        {
		                            size -= 9;
		                            name = name.Substring( start, size );
		                        }
		                        else
		                        {
		                            name = string.Empty;
		                        }
		                    }
		                    else if( name.EndsWith( "[Stopped]" ) )
		                    {
		                        if( JustJournalCore.WinampStopped )
		                        {
		                            size -= 10;
		                            name = name.Substring( start, size );
		                        }
		                        else
		                        {
		                            name = string.Empty;
		                        }
		                    }
		                    else
		                    {
		                        name = name.Substring( start, size );
		                    }
		                    if( player.Equals( 2 ) )
		                    {
		                        size = name.LastIndexOf("(", StringComparison.Ordinal) - 1;
		                        if( size > 0 )
		                        {
		                            name = name.Substring( 0, size );
		                        }
		                    }
		                    name = name.Trim( ' ' );
		                }
		                break;
		            case 5: //Musicmatch Jukebox
		                GetWindowText( whandle, sb, 1024 );
		                name = sb.ToString();
		                if( name.Length > 0 )
		                {
		                    size = name.IndexOf("MUSICMATCH", StringComparison.Ordinal);
		                    if( size < 0 )
		                    {
		                        size = name.IndexOf("Musicmatch", StringComparison.Ordinal);
		                    }
		                    if( size > 0 )
		                    {
		                        name = name.Substring( 0, size - 3 );
		                    }
		                }
		                break;
		            case 6: //Sonique
		                GetWindowText( whandle, sb, 1024 );
		                name = sb.ToString();
		                if( name.Length > 0 )
		                {
		                    size = name.IndexOf(".mp3", StringComparison.Ordinal) + 7;
		                    name = name.Substring( 0, size);
		                }
		                break;
		            case 7: //FreeAmp
		                GetWindowText( whandle, sb, 1024 );
		                if( name.Length > 0 )
		                {
		                    name = sb.ToString();
		                    name = name.Substring( 9, name.Length );
		                }
		                break;
		            case 8: //foobar2000
		                GetWindowText( whandle, sb, 1024 );
		                name = sb.ToString();
		                if( name.Length > 0 )
		                {
		                    size = name.IndexOf("[foobar", StringComparison.Ordinal);
		                    if( size > 0 )
		                    {
		                        name = name.Substring( 0, size );
		                    }
		                    name = name.Trim( ' ' );
		                }
		                break;
		            case 9: //Windows Media Player 6 (9 and 10 with blog plugin)
		                var rk = Registry.CurrentUser;
		                rk = rk.CreateSubKey( "Software\\Microsoft\\MediaPlayer\\CurrentMetadata" );
		                if (rk != null)
		                {
		                    name = rk.GetValue( "Title", string.Empty ).ToString();
					
		                    if( name.Length > 0 )
		                    {
		                        if (rk.GetValue("Album", string.Empty).ToString().Length > 0 )
		                            name += " - " + rk.GetValue("Album", string.Empty); 

		                        if( rk.GetValue( "Author", string.Empty ).ToString().Length > 0 )
		                            name += " - " + rk.GetValue( "Author", string.Empty );
		                    }
		                    else
		                    {
		                        name = rk.GetValue( "Author", string.Empty ).ToString();
		                    }
		                    rk.Close();
		                }
		                name = name.Trim( ' ' );
		                if( name.Equals( "-" ) )
		                {
		                    GetWindowText( whandle, sb, 1024 );
		                    name = sb.ToString();
		                    if( name.Length > 0 )
		                    {
		                        size = name.LastIndexOf(".", StringComparison.Ordinal);
		                        name = name.Substring( 0, size );
		                    }
		                }
		                break;
		            case 10: //Windows Media Player 7 with skin
		                GetWindowText( whandle, sb, 1024 );
		                name = sb.ToString();
		                break;
		            case 11: //Windows CD Player
		                GetWindowText( whandle, sb, 1024 );
		                name = sb.ToString();
		                if( name.Length > 0 )
		                {
		                    if( name.Equals( "Please insert an audio compact disc" ) )
		                    {
		                        name = string.Empty;
		                    }
		                    else
		                    {
		                        size = name.IndexOf(" - CD Player ", StringComparison.Ordinal);
		                        name = name.Substring( 0, size );
		                    }
		                }
		                break;
		            case 12: //SysTrayPlayer (?)
		                GetWindowText( whandle, sb, 1024 );
		                name = sb.ToString();
		                if( name.Length > 0 && name.StartsWith( "STP: " ) )
		                {
		                    name = name.Substring( 5, name.Length - 5 );
		                }
		                break;
		            default:
		                name = string.Empty;
		                break;
		        }

		        return name;
		    }
		}

		private void menuItem28_Click(object sender, EventArgs e)
		{
			if (colorDlgBg.ShowDialog( this ).Equals( DialogResult.OK))
			{
				rtbBody.BackColor = colorDlgBg.Color;
			}
		}

		private bool SpellingCheck()
		{
			var errors = false;			
			Object nul = null;
			
			try
			{
                Microsoft.Office.Interop.Word.Application chk = new Microsoft.Office.Interop.Word.ApplicationClass();
				var cur = 0;
                var word = new StringBuilder();
				while( cur < rtbBody.Text.Length ) 
				{
					if( char.IsLetter( rtbBody.Text[cur] ) || rtbBody.Text[cur].Equals( '\'' ) )
					{
						word.Append( rtbBody.Text[cur] );
					}
					else
					{
						if( word.Length != 0 ) 
						{		
							if( !chk.CheckSpelling( word.ToString(), ref nul, ref nul, ref nul, ref nul, ref nul,
								ref nul, ref nul, ref nul, ref nul, ref nul, ref nul, ref nul ) )
							{
								rtbBody.Select( cur - word.Length, word.Length );
								var ft = new Font( rtbBody.SelectionFont.Name, rtbBody.SelectionFont.Size, FontStyle.Strikeout );
								rtbBody.SelectionFont = ft;
								errors = true;
							}
                            word.Remove(0, word.Length); // reset
						}
					}
					cur++;
				}
				rtbBody.Select( 0, 0 );
				chk.Quit(ref nul, ref nul, ref nul);
			}
			catch( Exception x )
			{
				MessageBox.Show( x.Message );
			}
			
			return errors;
		}

		private void menuItem31_Click(object sender, EventArgs e)
		{
			SpellingCheck();
		}

        private void toolStripButton1_Click(object sender, EventArgs e)
        {    
            mnuNormal_Click(sender, e);      
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            mnuFont_Click(sender, e);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            mnuBold_Click(sender, e);
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            mnuItalic_Click(sender, e);
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            mnuUnderline_Click(sender, e);
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            if (colorDlg.ShowDialog(this).Equals(DialogResult.OK))
            {
                rtbBody.ForeColor = colorDlg.Color;
            }
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            if (colorDlgBg.ShowDialog(this).Equals(DialogResult.OK))
            {
                rtbBody.BackColor = colorDlgBg.Color;
            }
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            rtbBody.Cut();
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            rtbBody.Copy();
        }

        private void toolStripButton13_Click(object sender, EventArgs e)
        {
            rtbBody.Paste();
        }

        private void toolStripButton14_Click(object sender, EventArgs e)
        {
            rtbBody.Undo();
        }

        private void toolStripButton15_Click(object sender, EventArgs e)
        {
            rtbBody.Redo();
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            try
            {
                if (saveDlg.ShowDialog(this).Equals(DialogResult.OK))
                {
                    var extension = Path.GetExtension(saveDlg.FileName);
                    if (extension != null && extension.ToLower().Equals(".rtf"))
                    {
                        rtbBody.SaveFile(saveDlg.FileName, RichTextBoxStreamType.RichText);
                    }
                    else
                    {
                        rtbBody.SaveFile(saveDlg.FileName, RichTextBoxStreamType.PlainText);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Could not save file", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            try
            {
                if (openDlg.ShowDialog(this).Equals(DialogResult.OK))
                {
                    rtbBody.LoadFile(openDlg.FileName,
                                     String.CompareOrdinal(Path.GetExtension(openDlg.FileName), ".rtf") == 0
                                         ? RichTextBoxStreamType.RichText
                                         : RichTextBoxStreamType.PlainText);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, @"Could not open file", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
        }

        private void tsbAbout_Click(object sender, EventArgs e)
        {
            new AboutForm().Show();
        }
	}
}
