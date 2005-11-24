using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Net;
using System.IO;
using System.Text;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace JustJournal
{
	/// <summary>
	/// Post Journal entries on JustJournal.com
	/// </summary>
	public class PostForm : System.Windows.Forms.Form
	{

		[DllImport("user32.dll",EntryPoint="FindWindow")]
		private static extern uint FindWindow(string _ClassName, string _WindowName);

		[DllImport("user32.dll")]
		private static extern uint GetWindowText ( uint hWnd, StringBuilder lpString, uint nMaxCount );

		private System.Windows.Forms.ToolBar toolBar1;
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
		private System.Windows.Forms.Label label6;
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
		private System.Windows.Forms.ToolBarButton tbNormal;
		private System.Windows.Forms.ToolBarButton sep1;
		private System.Windows.Forms.ToolBarButton tbFont;
		private System.Windows.Forms.ToolBarButton sep2;
		private System.Windows.Forms.ToolBarButton tbBold;
		private System.Windows.Forms.ToolBarButton tbItalic;
		private System.Windows.Forms.ToolBarButton tbUnderline;
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

			RegistryKey reg = Registry.CurrentUser.CreateSubKey( "SOFTWARE\\JustJournal\\Post" );
            cboSecurity.SelectedIndex = (int)reg.GetValue("security",0);
            cboLocation.SelectedIndex = (int)reg.GetValue("location",0);
			reg.Close();

			lblCurrentUser.Text = JustJournal.UserName;

			// If we need the moods, go get them.
			if ( JustJournal.moods.Count == 0)
				JustJournal.RetrieveMoods();

			cboMood.DataSource = JustJournal.moods;
			cboMood.DisplayMember = "Name";
			cboMood.ValueMember = "Id";
			cboMood.SelectedIndex = cboMood.FindStringExact("Not Specified",0);

			txtMusic.Text = detectMusic();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			RegistryKey reg = Registry.CurrentUser.CreateSubKey( "SOFTWARE\\JustJournal\\Post" );
			reg.SetValue("security", cboSecurity.SelectedIndex);
			reg.SetValue("location",cboLocation.SelectedIndex);
			reg.Close();

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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(PostForm));
			this.toolBar1 = new System.Windows.Forms.ToolBar();
			this.tbNormal = new System.Windows.Forms.ToolBarButton();
			this.sep1 = new System.Windows.Forms.ToolBarButton();
			this.tbFont = new System.Windows.Forms.ToolBarButton();
			this.sep2 = new System.Windows.Forms.ToolBarButton();
			this.tbBold = new System.Windows.Forms.ToolBarButton();
			this.tbItalic = new System.Windows.Forms.ToolBarButton();
			this.tbUnderline = new System.Windows.Forms.ToolBarButton();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
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
			this.label6 = new System.Windows.Forms.Label();
			this.btnMusicSense = new System.Windows.Forms.Button();
			this.lblCurrentUser = new System.Windows.Forms.Label();
			this.fontDlg = new System.Windows.Forms.FontDialog();
			this.colorDlg = new System.Windows.Forms.ColorDialog();
			this.openDlg = new System.Windows.Forms.OpenFileDialog();
			this.saveDlg = new System.Windows.Forms.SaveFileDialog();
			this.colorDlgBg = new System.Windows.Forms.ColorDialog();
			this.SuspendLayout();
			// 
			// toolBar1
			// 
			this.toolBar1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
			this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																						this.tbNormal,
																						this.sep1,
																						this.tbFont,
																						this.sep2,
																						this.tbBold,
																						this.tbItalic,
																						this.tbUnderline});
			this.toolBar1.DropDownArrows = true;
			this.toolBar1.ImageList = this.imageList1;
			this.toolBar1.Location = new System.Drawing.Point(0, 0);
			this.toolBar1.Name = "toolBar1";
			this.toolBar1.ShowToolTips = true;
			this.toolBar1.Size = new System.Drawing.Size(526, 28);
			this.toolBar1.TabIndex = 0;
			this.toolBar1.TextAlign = System.Windows.Forms.ToolBarTextAlign.Right;
			this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
			// 
			// tbNormal
			// 
			this.tbNormal.ImageIndex = 3;
			this.tbNormal.ToolTipText = "Normal Format";
			// 
			// sep1
			// 
			this.sep1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// tbFont
			// 
			this.tbFont.ImageIndex = 1;
			this.tbFont.ToolTipText = "Font";
			// 
			// sep2
			// 
			this.sep2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// tbBold
			// 
			this.tbBold.ImageIndex = 0;
			this.tbBold.ToolTipText = "Bold";
			// 
			// tbItalic
			// 
			this.tbItalic.ImageIndex = 2;
			this.tbItalic.ToolTipText = "Italic";
			// 
			// tbUnderline
			// 
			this.tbUnderline.ImageIndex = 4;
			this.tbUnderline.ToolTipText = "Underline";
			// 
			// imageList1
			// 
			this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth16Bit;
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
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
																					  this.mnuRawText});
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
			this.btnPost.ForeColor = System.Drawing.Color.White;
			this.btnPost.Location = new System.Drawing.Point(440, 312);
			this.btnPost.Name = "btnPost";
			this.btnPost.Size = new System.Drawing.Size(80, 24);
			this.btnPost.TabIndex = 7;
			this.btnPost.Text = "Post";
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
			this.cboSecurity.Location = new System.Drawing.Point(264, 312);
			this.cboSecurity.Name = "cboSecurity";
			this.cboSecurity.Size = new System.Drawing.Size(144, 21);
			this.cboSecurity.TabIndex = 6;
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
			this.cboLocation.Location = new System.Drawing.Point(376, 40);
			this.cboLocation.Name = "cboLocation";
			this.cboLocation.Size = new System.Drawing.Size(144, 21);
			this.cboLocation.TabIndex = 1;
			// 
			// cboMood
			// 
			this.cboMood.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboMood.Location = new System.Drawing.Point(56, 72);
			this.cboMood.Name = "cboMood";
			this.cboMood.Size = new System.Drawing.Size(144, 21);
			this.cboMood.TabIndex = 2;
			// 
			// txtSubject
			// 
			this.txtSubject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtSubject.Location = new System.Drawing.Point(56, 40);
			this.txtSubject.Name = "txtSubject";
			this.txtSubject.Size = new System.Drawing.Size(256, 20);
			this.txtSubject.TabIndex = 0;
			this.txtSubject.Text = "";
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.Location = new System.Drawing.Point(328, 40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(48, 24);
			this.label2.TabIndex = 7;
			this.label2.Text = "location";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(16, 72);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(40, 16);
			this.label3.TabIndex = 8;
			this.label3.Text = "mood";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 40);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(48, 16);
			this.label4.TabIndex = 9;
			this.label4.Text = "subject";
			// 
			// rtbBody
			// 
			this.rtbBody.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.rtbBody.AutoSize = true;
			this.rtbBody.ContextMenu = this.editBoxMenu;
			this.rtbBody.Location = new System.Drawing.Point(0, 104);
			this.rtbBody.Name = "rtbBody";
			this.rtbBody.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
			this.rtbBody.Size = new System.Drawing.Size(528, 200);
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
			this.txtMusic.Location = new System.Drawing.Point(256, 72);
			this.txtMusic.MaxLength = 100;
			this.txtMusic.Name = "txtMusic";
			this.txtMusic.Size = new System.Drawing.Size(192, 20);
			this.txtMusic.TabIndex = 3;
			this.txtMusic.Text = "";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(216, 72);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(40, 16);
			this.label6.TabIndex = 14;
			this.label6.Text = "music";
			// 
			// btnMusicSense
			// 
			this.btnMusicSense.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnMusicSense.Location = new System.Drawing.Point(456, 72);
			this.btnMusicSense.Name = "btnMusicSense";
			this.btnMusicSense.Size = new System.Drawing.Size(64, 23);
			this.btnMusicSense.TabIndex = 4;
			this.btnMusicSense.Text = "Sense";
			this.btnMusicSense.Click += new System.EventHandler(this.btnMusicSense_Click);
			// 
			// lblCurrentUser
			// 
			this.lblCurrentUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblCurrentUser.AutoSize = true;
			this.lblCurrentUser.Location = new System.Drawing.Point(16, 320);
			this.lblCurrentUser.Name = "lblCurrentUser";
			this.lblCurrentUser.Size = new System.Drawing.Size(69, 16);
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
			// PostForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(526, 343);
			this.Controls.Add(this.lblCurrentUser);
			this.Controls.Add(this.txtMusic);
			this.Controls.Add(this.txtSubject);
			this.Controls.Add(this.btnMusicSense);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.rtbBody);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.cboMood);
			this.Controls.Add(this.cboLocation);
			this.Controls.Add(this.cboSecurity);
			this.Controls.Add(this.btnPost);
			this.Controls.Add(this.toolBar1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Menu = this.mainMenu1;
			this.Name = "PostForm";
			this.Text = "JustJournal";
			this.ResumeLayout(false);

		}
		#endregion

		private void menuItem4_Click(object sender, System.EventArgs e)
		{
			// Close JJ
			Application.Exit();
		}

		private void btnPost_Click(object sender, System.EventArgs e)
		{
			this.Enabled = false;
			this.Cursor = Cursors.WaitCursor;

			WebClient client = new WebClient();
			string uriString;
            if (JustJournal.useSSL)
                uriString = "https://";
			else
			    uriString = "http://";		 
			uriString += "www.justjournal.com/updateJournal";
			
			// Create a new NameValueCollection instance to hold some custom parameters to be posted to the URL.
			NameValueCollection myNameValueCollection = new NameValueCollection();

			// Add a user agent header in case the 
			// requested URI contains a query.
			client.Headers.Add("User-Agent", JustJournal.Version);

			// Add necessary parameter/value pairs to the name/value container.
			
			myNameValueCollection.Add("user", JustJournal.UserName);            
			myNameValueCollection.Add("pass", JustJournal.Password);
			myNameValueCollection.Add("keeplogin", "NO");
			DateTime d1 = DateTime.Now;
            //myNameValueCollection.Add("date", "2005-09-30 16:18:26");
			myNameValueCollection.Add("date", d1.Year + "-" + d1.Month + "-" + d1.Day + " " + d1.Hour + ":" + d1.Minute + ":" + d1.Second);
			myNameValueCollection.Add("subject", txtSubject.Text);
			
			if (mnuFormattedText.Checked)
					myNameValueCollection.Add("body",RtfToHtml.Convert(rtbBody.Rtf,
						RtfToHtml.ToHexColor(rtbBody.BackColor.R,rtbBody.BackColor.G,
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
			myNameValueCollection.Add("music", txtMusic.Text );

			myNameValueCollection.Add("aformat", string.Empty);

			if (mEntryAllowComments.Checked)
				myNameValueCollection.Add("allow_comment", "checked");

			if (mEntryEmailComments.Checked)
				myNameValueCollection.Add("email_comment", "checked");

			// Upload the NameValueCollection.
			byte[] responseArray = client.UploadValues(uriString,"POST",myNameValueCollection);

			// Decode and display the response.
			//Console.WriteLine("\nResponse received was :\n{0}",Encoding.ASCII.GetString(responseArray));
			this.Cursor = Cursors.Default;
			this.Enabled = true;

			string resp = Encoding.ASCII.GetString(responseArray);
			if ( resp.Length > 0 && resp.IndexOf("JJ.LOGIN.OK") == -1 ) 
				this.Close();  // OK
			else 
			{          
				MessageBox.Show (Encoding.ASCII.GetString(responseArray), "Just Journal", 
					MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}

		private void menuItem5_Click(object sender, System.EventArgs e)
		{
			new AboutForm().Show();
		}

		private void mEntryAllowComments_Click(object sender, System.EventArgs e)
		{
		    if (mEntryAllowComments.Checked)
				mEntryAllowComments.Checked = false;
			else
				mEntryAllowComments.Checked = true;
		}

		private void mEntryEmailComments_Click(object sender, System.EventArgs e)
		{
			if (mEntryEmailComments.Checked)
				mEntryEmailComments.Checked = false;
			else 
				mEntryEmailComments.Checked = true;
		}

		private void menuItem7_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void menuItem9_Click(object sender, System.EventArgs e)
		{
			if (txtSubject.Focused)
				txtSubject.Cut();
		    else if (txtMusic.Focused)
				txtMusic.Cut();
			else if ( rtbBody.Focused)
				rtbBody.Cut();
		}

		private void menuItem10_Click(object sender, System.EventArgs e)
		{
			if (txtSubject.Focused)
				txtSubject.Copy();
			else if (txtMusic.Focused)
				txtMusic.Copy();
			else if ( rtbBody.Focused)
				rtbBody.Copy();
		}

		private void menuItem11_Click(object sender, System.EventArgs e)
		{
			if (txtSubject.Focused)
				txtSubject.Paste();
			else if (txtMusic.Focused)
				txtMusic.Paste();
			else if ( rtbBody.Focused)
				rtbBody.Paste();
		}

		private void btnMusicSense_Click(object sender, System.EventArgs e)
		{
		    try
		    {
		        txtMusic.Text = detectMusic();
		    }
		    catch( Exception ) //ignore any errors in here
		    {
			    txtMusic.Text = string.Empty;
		    } 
		}

		private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			if ( e.Button == tbNormal )
			    mnuNormal_Click( sender, e );
			else if ( e.Button == tbFont )
		        mnuFont_Click( sender, e );
		    else if ( e.Button == tbBold )
                mnuBold_Click( sender, e );
		    else if ( e.Button == tbItalic )
                mnuItalic_Click( sender, e );
			else if ( e.Button == tbUnderline )
                mnuUnderline_Click( sender, e );
		}

		private void mnuFont_Click(object sender, System.EventArgs e)
		{
			fontDlg.Font = rtbBody.SelectionFont;
			if( fontDlg.ShowDialog() == DialogResult.OK ) 
			{
				rtbBody.SelectionFont = fontDlg.Font;
			}
		}

		private void mnuNormal_Click(object sender, System.EventArgs e)
		{
			Font ft = rtbBody.SelectionFont;
			rtbBody.SelectionFont = new Font( ft.FontFamily, ft.Size, FontStyle.Regular );
			tbBold.Pushed = false;
			tbItalic.Pushed = false;
			tbUnderline.Pushed = false;
		}

		private void mnuBold_Click(object sender, System.EventArgs e)
		{
			if( mnuRawText.Checked ) 
			{
				int start = rtbBody.SelectionStart;
				int length = rtbBody.SelectionLength;
				rtbBody.Text = rtbBody.Text.Substring(0,start) +
					"<strong>" +	rtbBody.Text.Substring(start, length)
					+ "</strong>" + rtbBody.Text.Substring( start + length);
				rtbBody.SelectionStart = start + 3;
				rtbBody.SelectionLength = length;				
			}
			else
			{
				Font ft = rtbBody.SelectionFont;
				FontStyle fs = ( ft.Style ^ FontStyle.Bold );
				rtbBody.SelectionFont = new Font( ft.FontFamily, ft.Size, fs );			   
				tbBold.Pushed = !tbBold.Pushed; // swap button
			}
		}

		private void mnuItalic_Click(object sender, System.EventArgs e)
		{
			if( mnuRawText.Checked ) 
			{
				int start = rtbBody.SelectionStart;
				int length = rtbBody.SelectionLength;
				rtbBody.Text = rtbBody.Text.Substring(0,start) +
					"<em>" +	rtbBody.Text.Substring(start, length)
					+ "</em>" + rtbBody.Text.Substring( start + length);
				rtbBody.SelectionStart = start + 3;
				rtbBody.SelectionLength = length;				
			}
			else
			{
				Font ft = rtbBody.SelectionFont;
				FontStyle fs = ( ft.Style ^ FontStyle.Italic );
				rtbBody.SelectionFont = new Font( ft.FontFamily, ft.Size, fs );	
				tbItalic.Pushed = !tbItalic.Pushed;
			}
		}

		private void mnuUnderline_Click(object sender, System.EventArgs e)
		{
			if( mnuRawText.Checked ) 
			{
				int start = rtbBody.SelectionStart;
				int length = rtbBody.SelectionLength;
				rtbBody.Text = rtbBody.Text.Substring(0,start) +
					"<u>" +	rtbBody.Text.Substring(start, length)
					+ "</u>" + rtbBody.Text.Substring( start + length);
				rtbBody.SelectionStart = start + 3;
				rtbBody.SelectionLength = length;				
			}
			else
			{
				Font ft = rtbBody.SelectionFont;
				FontStyle fs = ( ft.Style ^ FontStyle.Underline );
				rtbBody.SelectionFont = new Font( ft.FontFamily, ft.Size, fs );	
				tbUnderline.Pushed = !tbUnderline.Pushed;
			}
		}

		private void mnuFormattedText_Click(object sender, System.EventArgs e)
		{
			mnuFormattedText.Checked = true;
			mnuRawText.Checked = false;
		}

		private void mnuRawText_Click(object sender, System.EventArgs e)
		{
			mnuRawText.Checked = true;
			mnuFormattedText.Checked = false;
		}

		private void menuItem14_Click(object sender, System.EventArgs e)
		{
			btnPost_Click( sender, e );
		}

		private void menuItem16_Click(object sender, System.EventArgs e)
		{
			try
			{
				if( openDlg.ShowDialog( this ).Equals( DialogResult.OK ) )
				{
					if( Path.GetExtension( openDlg.FileName ).ToLower().Equals( ".rtf" ) )
					{
						rtbBody.LoadFile( openDlg.FileName, RichTextBoxStreamType.RichText );
					} 
					else
					{
						rtbBody.LoadFile( openDlg.FileName, RichTextBoxStreamType.PlainText );
					}
				}
			}
			catch( Exception ex )
			{
				MessageBox.Show( this, ex.Message, "Could not open file", MessageBoxButtons.OK,
					MessageBoxIcon.Exclamation );
			}
		}

		private void menuItem17_Click(object sender, System.EventArgs e)
		{
			try
			{
				if( saveDlg.ShowDialog( this ).Equals( DialogResult.OK ) )
				{
					if( Path.GetExtension( saveDlg.FileName ).ToLower().Equals( ".rtf" ) )
					{
						rtbBody.SaveFile( saveDlg.FileName, RichTextBoxStreamType.RichText );
					}
					else
					{
						rtbBody.SaveFile( saveDlg.FileName, RichTextBoxStreamType.PlainText );
					}
				}
			}
			catch( Exception ex )
			{
				MessageBox.Show( this, ex.Message, "Could not save file", MessageBoxButtons.OK,
					MessageBoxIcon.Exclamation );
			}
		}

		private void menuItem4_Click_1(object sender, System.EventArgs e)
		{
			if (colorDlg.ShowDialog( this ).Equals( DialogResult.OK))
			{
				rtbBody.ForeColor = colorDlg.Color;
			}
		}

		private void menuItem19_Click(object sender, System.EventArgs e)
		{
			rtbBody.SelectAll();
		}

		private void menuItem20_Click(object sender, System.EventArgs e)
		{
			rtbBody.Undo();
		}

		private void menuItem21_Click(object sender, System.EventArgs e)
		{
			rtbBody.Redo();
		}

		private void menuItem24_Click(object sender, System.EventArgs e)
		{
			rtbBody.Cut();
		}

		private void menuItem25_Click(object sender, System.EventArgs e)
		{
			rtbBody.Copy();
		}

		private void menuItem26_Click(object sender, System.EventArgs e)
		{
			rtbBody.Paste();
		}

		private void menuItem27_Click(object sender, System.EventArgs e)
		{
		    rtbBody.SelectedRtf = string.Empty;
		}

		private string detectMusic()
		{
			try
			{
			uint player = 0;
			uint whandle = 0;
			int size = 0;
			string name = string.Empty;
			StringBuilder sb = new StringBuilder( 1024, 1024 );

			Hashtable mediaPlayers = new Hashtable();
			// Quintessential Player
			// MUST be first to prevent an exception
			mediaPlayers.Add( "PlayerCanvas", 1 );
			// Apollo
			mediaPlayers.Add( "Apollo - Main Window", 2 );
			// Winamp 2 and 5, and clones (e.g. foobar2000 with foo_winamp_spam)
			mediaPlayers.Add( "Winamp v1.x", 3 );
			// Winamp 3
			mediaPlayers.Add( "STUDIO", 4 );
			// Musicmatch Jukebox
			mediaPlayers.Add( "MMJB:MAINWND", 5 );
			// Sonique
			mediaPlayers.Add( "Sonique Window Class", 6 );
			// FreeAmp
			mediaPlayers.Add( "FreeAmp", 7 );
			// foobar2000
			mediaPlayers.Add( "FOOBAR2000_CLASS", 8 );
			mediaPlayers.Add( "{DA7CD0DE-1602-45e6-89A1-C2CA151E008E}", 8 );
			// Windows Media Player 6
			mediaPlayers.Add( "Media Player 2", 9 );
			// Windows Media Player 7 with blogging skin
			mediaPlayers.Add( "WMP Skin Host", 10 );
			// Windows CD Player
			mediaPlayers.Add( "MMFRAME_MAIN", 11 );
			// SysTrayPlayer (?)
			mediaPlayers.Add( "STPAppClass", 12 );

			foreach( string key in mediaPlayers.Keys )
			{
				whandle = FindWindow( key, null );
				if( whandle > 0 )
				{
					player = Convert.ToUInt32( mediaPlayers[key] );
					break;
				}
			}

			switch( player )
			{
				case 1: //QCD
					GetWindowText( whandle, sb, 1024 );
					name = sb.ToString();
					if( name.Length > 0 )
					{
						size = name.IndexOf( "]" ) + 2;
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
						name = name.Substring( 0, name.LastIndexOf( " - (" ) );
						name = name.Substring( 0, name.LastIndexOf( " - " ) );
					}
					break;
				case 3: //Winamp 2/5
				case 4: //Winamp 3
					GetWindowText( whandle, sb, 1024 );
					name = sb.ToString();
					if( name.Length > 0 )
					{
						int start = name.IndexOf( "." ) + 2;
						size = name.Length - start - 9;
						if( name.EndsWith( "[Paused]" ) )
						{
							//if( Logon.PrefWin.CheckPaused )
							//{
							//	size -= 9;
							//	name = name.Substring( start, size );
							//}
							//else
							//{
								name = string.Empty;
							//}
						}
						else if( name.EndsWith( "[Stopped]" ) )
						{
							//if( Logon.PrefWin.CheckStopped )
							//{
							//	size -= 10;
							//	name = name.Substring( start, size );
							//}
							//else
							//{
								name = string.Empty;
							//}
						}
						else
						{
							name = name.Substring( start, size );
						}
						if( player.Equals( 2 ) )
						{
							size = name.LastIndexOf( "(" ) - 1;
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
						size = name.IndexOf( "MUSICMATCH" );
						if( size < 0 )
						{
							size = name.IndexOf( "Musicmatch" );
						}
						if( size > 0 )
						{
							name = name.Substring( 0, size - 3 );
							//musicEdit.Text = name;
						}
					}
					break;
				case 6: //Sonique
					GetWindowText( whandle, sb, 1024 );
					name = sb.ToString();
					if( name.Length > 0 )
					{
						size = name.IndexOf( ".mp3" ) + 7;
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
						size = name.IndexOf( "[foobar" );
						if( size > 0 )
						{
							name = name.Substring( 0, size );
						}
						name = name.Trim( ' ' );
					}
					break;
				case 9: //Windows Media Player 6 (9 and 10 with blog plugin)
					RegistryKey rk = Registry.CurrentUser;
					rk = rk.CreateSubKey( "Software\\Microsoft\\MediaPlayer\\CurrentMetadata" );
					name = rk.GetValue( "Title", string.Empty ).ToString();
					//name = rk2.GetValue("Title") + " - " + rk2.GetValue("Album") + " - " + rk2.GetValue("Author");
					if( name.Length > 0 )
					{
						if (rk.GetValue("Album", string.Empty).ToString().Length > 0 )
							name += " - " + rk.GetValue("Album", string.Empty).ToString(); 

						if( rk.GetValue( "Author", string.Empty ).ToString().Length > 0 )
							name += " - " + rk.GetValue( "Author", string.Empty ).ToString();
					}
					else
					{
						name = rk.GetValue( "Author", string.Empty ).ToString();
					}
					rk.Close();
					name = name.Trim( ' ' );
					if( name.Equals( "-" ) )
					{
						GetWindowText( whandle, sb, 1024 );
						name = sb.ToString();
						if( name.Length > 0 )
						{
							size = name.LastIndexOf( "." );
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
							size = name.IndexOf( " - CD Player " );
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
				case 0:
				default:
					name = string.Empty;
					break;
			}

			return name;
		}
		catch
	    {
		    throw;
     	}				
			
		}

		private void menuItem28_Click(object sender, System.EventArgs e)
		{
			if (colorDlgBg.ShowDialog( this ).Equals( DialogResult.OK))
			{
				rtbBody.BackColor = colorDlgBg.Color;
			}
		}
	}
}
