using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Threading;
using System.Globalization;
using System.Resources;

namespace JustJournal
{
	/// <summary>
	/// Summary description for Login.
	/// </summary>
	public class Login : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.GroupBox groupBox1;
		protected System.Windows.Forms.TextBox txtPassword;
		public System.Windows.Forms.TextBox txtUserName;
		private System.Windows.Forms.Button btnLogin;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.ComponentModel.IContainer components;

		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.Timer autoLoginTimer;
		protected System.Windows.Forms.NotifyIcon notify;
		private System.Windows.Forms.ContextMenu notifyMenu;
		private System.Windows.Forms.MenuItem menuItem8;
		private System.Windows.Forms.MenuItem menuItem10;
		private System.Windows.Forms.MenuItem menuItem11;
		private System.Windows.Forms.MenuItem menuItem9;
		private System.Windows.Forms.MenuItem menuItem12;
		private System.Windows.Forms.MenuItem mnuUseSSL;
		private System.Windows.Forms.MenuItem menuItem13;
		private System.Windows.Forms.MenuItem menuItem14;
		private System.Windows.Forms.MenuItem menuItem15;
		private System.Windows.Forms.MenuItem menuItem16;
		private System.Windows.Forms.MenuItem menuItem17;
		private System.Windows.Forms.MenuItem menuItem18;
		private ResourceManager rm;

		public Login()
		{
			RegistryKey rk = Registry.CurrentUser.CreateSubKey( "SOFTWARE\\JustJournal" );
			RegistryKey mk = rk.CreateSubKey( "Preferences" );
			string lang = (string)mk.GetValue("Language");
			if (lang != null) 
			{
				Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(lang);
				Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);
			}

			rm = new ResourceManager(typeof(Login));

			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

			rk.Close();
			mk.Close();
		}

		private void Login_Load(object sender, System.EventArgs e)
		{
			RegistryKey rk = Registry.CurrentUser.CreateSubKey( "SOFTWARE\\JustJournal" );
			string uname = (string)rk.GetValue( "username", "" );
			string pwd = (string)rk.GetValue( "password", "" );
			bool autoLog = ((string)rk.GetValue( "autologin", "no" )).Equals("yes");
			bool ssl = ((string)rk.GetValue( "usessl", "yes" )).Equals("yes");

			txtUserName.Text = uname;
			if( pwd.Equals("@") ) 
			{
				menuItem4.Checked = false;
				txtPassword.Text = "";
			}
			else
			{
				txtPassword.Text = pwd;
				menuItem4.Checked = true;
			}

			if ( ssl )
			{
				mnuUseSSL.Checked = true;
			}

			if( autoLog ) 
			{
				menuItem5.Checked = true;
				this.Enabled = false;
				autoLoginTimer.Start();
			}
			rk.Close();
		}

		private void autologinTimer_Tick(object sender, System.EventArgs e)
		{
			autoLoginTimer.Stop();
			if( menuItem5.Checked )
			{
				btnLogin_Click( sender, e );
			}
				
		}

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Login());
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Login));
			this.txtPassword = new System.Windows.Forms.TextBox();
			this.txtUserName = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.btnLogin = new System.Windows.Forms.Button();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.mnuUseSSL = new System.Windows.Forms.MenuItem();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.menuItem7 = new System.Windows.Forms.MenuItem();
			this.autoLoginTimer = new System.Windows.Forms.Timer(this.components);
			this.notify = new System.Windows.Forms.NotifyIcon(this.components);
			this.notifyMenu = new System.Windows.Forms.ContextMenu();
			this.menuItem8 = new System.Windows.Forms.MenuItem();
			this.menuItem9 = new System.Windows.Forms.MenuItem();
			this.menuItem10 = new System.Windows.Forms.MenuItem();
			this.menuItem13 = new System.Windows.Forms.MenuItem();
			this.menuItem12 = new System.Windows.Forms.MenuItem();
			this.menuItem11 = new System.Windows.Forms.MenuItem();
			this.menuItem14 = new System.Windows.Forms.MenuItem();
			this.menuItem15 = new System.Windows.Forms.MenuItem();
			this.menuItem16 = new System.Windows.Forms.MenuItem();
			this.menuItem17 = new System.Windows.Forms.MenuItem();
			this.menuItem18 = new System.Windows.Forms.MenuItem();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// txtPassword
			// 
			this.txtPassword.AccessibleDescription = resources.GetString("txtPassword.AccessibleDescription");
			this.txtPassword.AccessibleName = resources.GetString("txtPassword.AccessibleName");
			this.txtPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtPassword.Anchor")));
			this.txtPassword.AutoSize = ((bool)(resources.GetObject("txtPassword.AutoSize")));
			this.txtPassword.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtPassword.BackgroundImage")));
			this.txtPassword.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtPassword.Dock")));
			this.txtPassword.Enabled = ((bool)(resources.GetObject("txtPassword.Enabled")));
			this.txtPassword.Font = ((System.Drawing.Font)(resources.GetObject("txtPassword.Font")));
			this.txtPassword.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtPassword.ImeMode")));
			this.txtPassword.Location = ((System.Drawing.Point)(resources.GetObject("txtPassword.Location")));
			this.txtPassword.MaxLength = ((int)(resources.GetObject("txtPassword.MaxLength")));
			this.txtPassword.Multiline = ((bool)(resources.GetObject("txtPassword.Multiline")));
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.PasswordChar = ((char)(resources.GetObject("txtPassword.PasswordChar")));
			this.txtPassword.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtPassword.RightToLeft")));
			this.txtPassword.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtPassword.ScrollBars")));
			this.txtPassword.Size = ((System.Drawing.Size)(resources.GetObject("txtPassword.Size")));
			this.txtPassword.TabIndex = ((int)(resources.GetObject("txtPassword.TabIndex")));
			this.txtPassword.Text = resources.GetString("txtPassword.Text");
			this.txtPassword.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtPassword.TextAlign")));
			this.txtPassword.Visible = ((bool)(resources.GetObject("txtPassword.Visible")));
			this.txtPassword.WordWrap = ((bool)(resources.GetObject("txtPassword.WordWrap")));
			// 
			// txtUserName
			// 
			this.txtUserName.AccessibleDescription = resources.GetString("txtUserName.AccessibleDescription");
			this.txtUserName.AccessibleName = resources.GetString("txtUserName.AccessibleName");
			this.txtUserName.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtUserName.Anchor")));
			this.txtUserName.AutoSize = ((bool)(resources.GetObject("txtUserName.AutoSize")));
			this.txtUserName.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtUserName.BackgroundImage")));
			this.txtUserName.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
			this.txtUserName.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtUserName.Dock")));
			this.txtUserName.Enabled = ((bool)(resources.GetObject("txtUserName.Enabled")));
			this.txtUserName.Font = ((System.Drawing.Font)(resources.GetObject("txtUserName.Font")));
			this.txtUserName.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtUserName.ImeMode")));
			this.txtUserName.Location = ((System.Drawing.Point)(resources.GetObject("txtUserName.Location")));
			this.txtUserName.MaxLength = ((int)(resources.GetObject("txtUserName.MaxLength")));
			this.txtUserName.Multiline = ((bool)(resources.GetObject("txtUserName.Multiline")));
			this.txtUserName.Name = "txtUserName";
			this.txtUserName.PasswordChar = ((char)(resources.GetObject("txtUserName.PasswordChar")));
			this.txtUserName.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtUserName.RightToLeft")));
			this.txtUserName.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtUserName.ScrollBars")));
			this.txtUserName.Size = ((System.Drawing.Size)(resources.GetObject("txtUserName.Size")));
			this.txtUserName.TabIndex = ((int)(resources.GetObject("txtUserName.TabIndex")));
			this.txtUserName.Text = resources.GetString("txtUserName.Text");
			this.txtUserName.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtUserName.TextAlign")));
			this.txtUserName.Visible = ((bool)(resources.GetObject("txtUserName.Visible")));
			this.txtUserName.WordWrap = ((bool)(resources.GetObject("txtUserName.WordWrap")));
			// 
			// label1
			// 
			this.label1.AccessibleDescription = resources.GetString("label1.AccessibleDescription");
			this.label1.AccessibleName = resources.GetString("label1.AccessibleName");
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label1.Anchor")));
			this.label1.AutoSize = ((bool)(resources.GetObject("label1.AutoSize")));
			this.label1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label1.Dock")));
			this.label1.Enabled = ((bool)(resources.GetObject("label1.Enabled")));
			this.label1.Font = ((System.Drawing.Font)(resources.GetObject("label1.Font")));
			this.label1.ForeColor = System.Drawing.Color.MidnightBlue;
			this.label1.Image = ((System.Drawing.Image)(resources.GetObject("label1.Image")));
			this.label1.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label1.ImageAlign")));
			this.label1.ImageIndex = ((int)(resources.GetObject("label1.ImageIndex")));
			this.label1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label1.ImeMode")));
			this.label1.Location = ((System.Drawing.Point)(resources.GetObject("label1.Location")));
			this.label1.Name = "label1";
			this.label1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label1.RightToLeft")));
			this.label1.Size = ((System.Drawing.Size)(resources.GetObject("label1.Size")));
			this.label1.TabIndex = ((int)(resources.GetObject("label1.TabIndex")));
			this.label1.Text = resources.GetString("label1.Text");
			this.label1.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label1.TextAlign")));
			this.label1.Visible = ((bool)(resources.GetObject("label1.Visible")));
			// 
			// label2
			// 
			this.label2.AccessibleDescription = resources.GetString("label2.AccessibleDescription");
			this.label2.AccessibleName = resources.GetString("label2.AccessibleName");
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label2.Anchor")));
			this.label2.AutoSize = ((bool)(resources.GetObject("label2.AutoSize")));
			this.label2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label2.Dock")));
			this.label2.Enabled = ((bool)(resources.GetObject("label2.Enabled")));
			this.label2.Font = ((System.Drawing.Font)(resources.GetObject("label2.Font")));
			this.label2.ForeColor = System.Drawing.Color.MidnightBlue;
			this.label2.Image = ((System.Drawing.Image)(resources.GetObject("label2.Image")));
			this.label2.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label2.ImageAlign")));
			this.label2.ImageIndex = ((int)(resources.GetObject("label2.ImageIndex")));
			this.label2.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label2.ImeMode")));
			this.label2.Location = ((System.Drawing.Point)(resources.GetObject("label2.Location")));
			this.label2.Name = "label2";
			this.label2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label2.RightToLeft")));
			this.label2.Size = ((System.Drawing.Size)(resources.GetObject("label2.Size")));
			this.label2.TabIndex = ((int)(resources.GetObject("label2.TabIndex")));
			this.label2.Text = resources.GetString("label2.Text");
			this.label2.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label2.TextAlign")));
			this.label2.Visible = ((bool)(resources.GetObject("label2.Visible")));
			// 
			// groupBox1
			// 
			this.groupBox1.AccessibleDescription = resources.GetString("groupBox1.AccessibleDescription");
			this.groupBox1.AccessibleName = resources.GetString("groupBox1.AccessibleName");
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("groupBox1.Anchor")));
			this.groupBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("groupBox1.BackgroundImage")));
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.txtUserName);
			this.groupBox1.Controls.Add(this.txtPassword);
			this.groupBox1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("groupBox1.Dock")));
			this.groupBox1.Enabled = ((bool)(resources.GetObject("groupBox1.Enabled")));
			this.groupBox1.Font = ((System.Drawing.Font)(resources.GetObject("groupBox1.Font")));
			this.groupBox1.ForeColor = System.Drawing.SystemColors.WindowFrame;
			this.groupBox1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("groupBox1.ImeMode")));
			this.groupBox1.Location = ((System.Drawing.Point)(resources.GetObject("groupBox1.Location")));
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("groupBox1.RightToLeft")));
			this.groupBox1.Size = ((System.Drawing.Size)(resources.GetObject("groupBox1.Size")));
			this.groupBox1.TabIndex = ((int)(resources.GetObject("groupBox1.TabIndex")));
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = resources.GetString("groupBox1.Text");
			this.groupBox1.Visible = ((bool)(resources.GetObject("groupBox1.Visible")));
			// 
			// btnLogin
			// 
			this.btnLogin.AccessibleDescription = resources.GetString("btnLogin.AccessibleDescription");
			this.btnLogin.AccessibleName = resources.GetString("btnLogin.AccessibleName");
			this.btnLogin.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnLogin.Anchor")));
			this.btnLogin.BackColor = System.Drawing.Color.MidnightBlue;
			this.btnLogin.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnLogin.BackgroundImage")));
			this.btnLogin.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnLogin.Dock")));
			this.btnLogin.Enabled = ((bool)(resources.GetObject("btnLogin.Enabled")));
			this.btnLogin.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnLogin.FlatStyle")));
			this.btnLogin.Font = ((System.Drawing.Font)(resources.GetObject("btnLogin.Font")));
			this.btnLogin.ForeColor = System.Drawing.Color.White;
			this.btnLogin.Image = ((System.Drawing.Image)(resources.GetObject("btnLogin.Image")));
			this.btnLogin.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnLogin.ImageAlign")));
			this.btnLogin.ImageIndex = ((int)(resources.GetObject("btnLogin.ImageIndex")));
			this.btnLogin.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnLogin.ImeMode")));
			this.btnLogin.Location = ((System.Drawing.Point)(resources.GetObject("btnLogin.Location")));
			this.btnLogin.Name = "btnLogin";
			this.btnLogin.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnLogin.RightToLeft")));
			this.btnLogin.Size = ((System.Drawing.Size)(resources.GetObject("btnLogin.Size")));
			this.btnLogin.TabIndex = ((int)(resources.GetObject("btnLogin.TabIndex")));
			this.btnLogin.Text = resources.GetString("btnLogin.Text");
			this.btnLogin.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnLogin.TextAlign")));
			this.btnLogin.Visible = ((bool)(resources.GetObject("btnLogin.Visible")));
			this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
			// 
			// pictureBox1
			// 
			this.pictureBox1.AccessibleDescription = resources.GetString("pictureBox1.AccessibleDescription");
			this.pictureBox1.AccessibleName = resources.GetString("pictureBox1.AccessibleName");
			this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("pictureBox1.Anchor")));
			this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
			this.pictureBox1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("pictureBox1.Dock")));
			this.pictureBox1.Enabled = ((bool)(resources.GetObject("pictureBox1.Enabled")));
			this.pictureBox1.Font = ((System.Drawing.Font)(resources.GetObject("pictureBox1.Font")));
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("pictureBox1.ImeMode")));
			this.pictureBox1.Location = ((System.Drawing.Point)(resources.GetObject("pictureBox1.Location")));
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("pictureBox1.RightToLeft")));
			this.pictureBox1.Size = ((System.Drawing.Size)(resources.GetObject("pictureBox1.Size")));
			this.pictureBox1.SizeMode = ((System.Windows.Forms.PictureBoxSizeMode)(resources.GetObject("pictureBox1.SizeMode")));
			this.pictureBox1.TabIndex = ((int)(resources.GetObject("pictureBox1.TabIndex")));
			this.pictureBox1.TabStop = false;
			this.pictureBox1.Text = resources.GetString("pictureBox1.Text");
			this.pictureBox1.Visible = ((bool)(resources.GetObject("pictureBox1.Visible")));
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem1,
																					  this.menuItem3,
																					  this.menuItem6});
			this.mainMenu1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("mainMenu1.RightToLeft")));
			// 
			// menuItem1
			// 
			this.menuItem1.Enabled = ((bool)(resources.GetObject("menuItem1.Enabled")));
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem2});
			this.menuItem1.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("menuItem1.Shortcut")));
			this.menuItem1.ShowShortcut = ((bool)(resources.GetObject("menuItem1.ShowShortcut")));
			this.menuItem1.Text = resources.GetString("menuItem1.Text");
			this.menuItem1.Visible = ((bool)(resources.GetObject("menuItem1.Visible")));
			// 
			// menuItem2
			// 
			this.menuItem2.Enabled = ((bool)(resources.GetObject("menuItem2.Enabled")));
			this.menuItem2.Index = 0;
			this.menuItem2.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("menuItem2.Shortcut")));
			this.menuItem2.ShowShortcut = ((bool)(resources.GetObject("menuItem2.ShowShortcut")));
			this.menuItem2.Text = resources.GetString("menuItem2.Text");
			this.menuItem2.Visible = ((bool)(resources.GetObject("menuItem2.Visible")));
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Enabled = ((bool)(resources.GetObject("menuItem3.Enabled")));
			this.menuItem3.Index = 1;
			this.menuItem3.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem5,
																					  this.menuItem4,
																					  this.mnuUseSSL});
			this.menuItem3.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("menuItem3.Shortcut")));
			this.menuItem3.ShowShortcut = ((bool)(resources.GetObject("menuItem3.ShowShortcut")));
			this.menuItem3.Text = resources.GetString("menuItem3.Text");
			this.menuItem3.Visible = ((bool)(resources.GetObject("menuItem3.Visible")));
			// 
			// menuItem5
			// 
			this.menuItem5.Enabled = ((bool)(resources.GetObject("menuItem5.Enabled")));
			this.menuItem5.Index = 0;
			this.menuItem5.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("menuItem5.Shortcut")));
			this.menuItem5.ShowShortcut = ((bool)(resources.GetObject("menuItem5.ShowShortcut")));
			this.menuItem5.Text = resources.GetString("menuItem5.Text");
			this.menuItem5.Visible = ((bool)(resources.GetObject("menuItem5.Visible")));
			this.menuItem5.Click += new System.EventHandler(this.menuItem5_Click);
			// 
			// menuItem4
			// 
			this.menuItem4.Enabled = ((bool)(resources.GetObject("menuItem4.Enabled")));
			this.menuItem4.Index = 1;
			this.menuItem4.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("menuItem4.Shortcut")));
			this.menuItem4.ShowShortcut = ((bool)(resources.GetObject("menuItem4.ShowShortcut")));
			this.menuItem4.Text = resources.GetString("menuItem4.Text");
			this.menuItem4.Visible = ((bool)(resources.GetObject("menuItem4.Visible")));
			this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
			// 
			// mnuUseSSL
			// 
			this.mnuUseSSL.Checked = true;
			this.mnuUseSSL.Enabled = ((bool)(resources.GetObject("mnuUseSSL.Enabled")));
			this.mnuUseSSL.Index = 2;
			this.mnuUseSSL.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("mnuUseSSL.Shortcut")));
			this.mnuUseSSL.ShowShortcut = ((bool)(resources.GetObject("mnuUseSSL.ShowShortcut")));
			this.mnuUseSSL.Text = resources.GetString("mnuUseSSL.Text");
			this.mnuUseSSL.Visible = ((bool)(resources.GetObject("mnuUseSSL.Visible")));
			// 
			// menuItem6
			// 
			this.menuItem6.Enabled = ((bool)(resources.GetObject("menuItem6.Enabled")));
			this.menuItem6.Index = 2;
			this.menuItem6.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem7});
			this.menuItem6.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("menuItem6.Shortcut")));
			this.menuItem6.ShowShortcut = ((bool)(resources.GetObject("menuItem6.ShowShortcut")));
			this.menuItem6.Text = resources.GetString("menuItem6.Text");
			this.menuItem6.Visible = ((bool)(resources.GetObject("menuItem6.Visible")));
			// 
			// menuItem7
			// 
			this.menuItem7.Enabled = ((bool)(resources.GetObject("menuItem7.Enabled")));
			this.menuItem7.Index = 0;
			this.menuItem7.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("menuItem7.Shortcut")));
			this.menuItem7.ShowShortcut = ((bool)(resources.GetObject("menuItem7.ShowShortcut")));
			this.menuItem7.Text = resources.GetString("menuItem7.Text");
			this.menuItem7.Visible = ((bool)(resources.GetObject("menuItem7.Visible")));
			this.menuItem7.Click += new System.EventHandler(this.menuItem7_Click);
			// 
			// autoLoginTimer
			// 
			this.autoLoginTimer.Interval = 1500;
			this.autoLoginTimer.Tick += new System.EventHandler(this.autologinTimer_Tick);
			// 
			// notify
			// 
			this.notify.ContextMenu = this.notifyMenu;
			this.notify.Icon = ((System.Drawing.Icon)(resources.GetObject("notify.Icon")));
			this.notify.Text = resources.GetString("notify.Text");
			this.notify.Visible = ((bool)(resources.GetObject("notify.Visible")));
			this.notify.DoubleClick += new System.EventHandler(this.notify_DoubleClick);
			// 
			// notifyMenu
			// 
			this.notifyMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					   this.menuItem8,
																					   this.menuItem9,
																					   this.menuItem10,
																					   this.menuItem13,
																					   this.menuItem18,
																					   this.menuItem15,
																					   this.menuItem16,
																					   this.menuItem17,
																					   this.menuItem14,
																					   this.menuItem12,
																					   this.menuItem11});
			this.notifyMenu.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("notifyMenu.RightToLeft")));
			// 
			// menuItem8
			// 
			this.menuItem8.Enabled = ((bool)(resources.GetObject("menuItem8.Enabled")));
			this.menuItem8.Index = 0;
			this.menuItem8.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("menuItem8.Shortcut")));
			this.menuItem8.ShowShortcut = ((bool)(resources.GetObject("menuItem8.ShowShortcut")));
			this.menuItem8.Text = resources.GetString("menuItem8.Text");
			this.menuItem8.Visible = ((bool)(resources.GetObject("menuItem8.Visible")));
			this.menuItem8.Click += new System.EventHandler(this.menuItem8_Click);
			// 
			// menuItem9
			// 
			this.menuItem9.Enabled = ((bool)(resources.GetObject("menuItem9.Enabled")));
			this.menuItem9.Index = 1;
			this.menuItem9.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("menuItem9.Shortcut")));
			this.menuItem9.ShowShortcut = ((bool)(resources.GetObject("menuItem9.ShowShortcut")));
			this.menuItem9.Text = resources.GetString("menuItem9.Text");
			this.menuItem9.Visible = ((bool)(resources.GetObject("menuItem9.Visible")));
			// 
			// menuItem10
			// 
			this.menuItem10.Enabled = ((bool)(resources.GetObject("menuItem10.Enabled")));
			this.menuItem10.Index = 2;
			this.menuItem10.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("menuItem10.Shortcut")));
			this.menuItem10.ShowShortcut = ((bool)(resources.GetObject("menuItem10.ShowShortcut")));
			this.menuItem10.Text = resources.GetString("menuItem10.Text");
			this.menuItem10.Visible = ((bool)(resources.GetObject("menuItem10.Visible")));
			this.menuItem10.Click += new System.EventHandler(this.menuItem10_Click);
			// 
			// menuItem13
			// 
			this.menuItem13.Enabled = ((bool)(resources.GetObject("menuItem13.Enabled")));
			this.menuItem13.Index = 3;
			this.menuItem13.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("menuItem13.Shortcut")));
			this.menuItem13.ShowShortcut = ((bool)(resources.GetObject("menuItem13.ShowShortcut")));
			this.menuItem13.Text = resources.GetString("menuItem13.Text");
			this.menuItem13.Visible = ((bool)(resources.GetObject("menuItem13.Visible")));
			this.menuItem13.Click += new System.EventHandler(this.menuItem13_Click);
			// 
			// menuItem12
			// 
			this.menuItem12.Enabled = ((bool)(resources.GetObject("menuItem12.Enabled")));
			this.menuItem12.Index = 9;
			this.menuItem12.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("menuItem12.Shortcut")));
			this.menuItem12.ShowShortcut = ((bool)(resources.GetObject("menuItem12.ShowShortcut")));
			this.menuItem12.Text = resources.GetString("menuItem12.Text");
			this.menuItem12.Visible = ((bool)(resources.GetObject("menuItem12.Visible")));
			// 
			// menuItem11
			// 
			this.menuItem11.Enabled = ((bool)(resources.GetObject("menuItem11.Enabled")));
			this.menuItem11.Index = 10;
			this.menuItem11.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("menuItem11.Shortcut")));
			this.menuItem11.ShowShortcut = ((bool)(resources.GetObject("menuItem11.ShowShortcut")));
			this.menuItem11.Text = resources.GetString("menuItem11.Text");
			this.menuItem11.Visible = ((bool)(resources.GetObject("menuItem11.Visible")));
			this.menuItem11.Click += new System.EventHandler(this.menuItem11_Click);
			// 
			// menuItem14
			// 
			this.menuItem14.Enabled = ((bool)(resources.GetObject("menuItem14.Enabled")));
			this.menuItem14.Index = 8;
			this.menuItem14.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("menuItem14.Shortcut")));
			this.menuItem14.ShowShortcut = ((bool)(resources.GetObject("menuItem14.ShowShortcut")));
			this.menuItem14.Text = resources.GetString("menuItem14.Text");
			this.menuItem14.Visible = ((bool)(resources.GetObject("menuItem14.Visible")));
			this.menuItem14.Click += new System.EventHandler(this.menuItem14_Click);
			// 
			// menuItem15
			// 
			this.menuItem15.Enabled = ((bool)(resources.GetObject("menuItem15.Enabled")));
			this.menuItem15.Index = 5;
			this.menuItem15.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("menuItem15.Shortcut")));
			this.menuItem15.ShowShortcut = ((bool)(resources.GetObject("menuItem15.ShowShortcut")));
			this.menuItem15.Text = resources.GetString("menuItem15.Text");
			this.menuItem15.Visible = ((bool)(resources.GetObject("menuItem15.Visible")));
			this.menuItem15.Click += new System.EventHandler(this.menuItem15_Click);
			// 
			// menuItem16
			// 
			this.menuItem16.Enabled = ((bool)(resources.GetObject("menuItem16.Enabled")));
			this.menuItem16.Index = 6;
			this.menuItem16.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("menuItem16.Shortcut")));
			this.menuItem16.ShowShortcut = ((bool)(resources.GetObject("menuItem16.ShowShortcut")));
			this.menuItem16.Text = resources.GetString("menuItem16.Text");
			this.menuItem16.Visible = ((bool)(resources.GetObject("menuItem16.Visible")));
			this.menuItem16.Click += new System.EventHandler(this.menuItem16_Click);
			// 
			// menuItem17
			// 
			this.menuItem17.Enabled = ((bool)(resources.GetObject("menuItem17.Enabled")));
			this.menuItem17.Index = 7;
			this.menuItem17.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("menuItem17.Shortcut")));
			this.menuItem17.ShowShortcut = ((bool)(resources.GetObject("menuItem17.ShowShortcut")));
			this.menuItem17.Text = resources.GetString("menuItem17.Text");
			this.menuItem17.Visible = ((bool)(resources.GetObject("menuItem17.Visible")));
			this.menuItem17.Click += new System.EventHandler(this.menuItem17_Click);
			// 
			// menuItem18
			// 
			this.menuItem18.Enabled = ((bool)(resources.GetObject("menuItem18.Enabled")));
			this.menuItem18.Index = 4;
			this.menuItem18.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("menuItem18.Shortcut")));
			this.menuItem18.ShowShortcut = ((bool)(resources.GetObject("menuItem18.ShowShortcut")));
			this.menuItem18.Text = resources.GetString("menuItem18.Text");
			this.menuItem18.Visible = ((bool)(resources.GetObject("menuItem18.Visible")));
			// 
			// Login
			// 
			this.AcceptButton = this.btnLogin;
			this.AccessibleDescription = resources.GetString("$this.AccessibleDescription");
			this.AccessibleName = resources.GetString("$this.AccessibleName");
			this.AutoScaleBaseSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScaleBaseSize")));
			this.AutoScroll = ((bool)(resources.GetObject("$this.AutoScroll")));
			this.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMargin")));
			this.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMinSize")));
			this.BackColor = System.Drawing.SystemColors.Control;
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.ClientSize = ((System.Drawing.Size)(resources.GetObject("$this.ClientSize")));
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.btnLogin);
			this.Controls.Add(this.groupBox1);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.MaximizeBox = false;
			this.MaximumSize = ((System.Drawing.Size)(resources.GetObject("$this.MaximumSize")));
			this.Menu = this.mainMenu1;
			this.MinimumSize = ((System.Drawing.Size)(resources.GetObject("$this.MinimumSize")));
			this.Name = "Login";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.Load += new System.EventHandler(this.Login_Load);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void btnLogin_Click(object sender, System.EventArgs e)
		{	
			this.Enabled = false;
			this.Cursor = Cursors.WaitCursor;
			JustJournal.UserName = txtUserName.Text.Trim();
			JustJournal.Password = txtPassword.Text.Trim();

			if (mnuUseSSL.Checked)
			{
				JustJournal.useSSL = true;
				RegistryKey rk = Registry.CurrentUser.CreateSubKey( "SOFTWARE\\JustJournal" );
				rk.SetValue( "usessl", "yes" );
			}
			else 
			{
				JustJournal.useSSL = false;
			    RegistryKey rk = Registry.CurrentUser.CreateSubKey( "SOFTWARE\\JustJournal" );
			    rk.SetValue( "usessl", "no" );
		    }

			try 
			{
				if ( JustJournal.Login() )
				{
					this.Enabled = true;
					this.Cursor = Cursors.Default;
					this.Hide();
					RegistryKey rk = Registry.CurrentUser.CreateSubKey( "SOFTWARE\\JustJournal" );
					rk.SetValue( "username", txtUserName.Text );

					// save password
					if(!menuItem4.Checked)
					{
						rk.SetValue( "password", "@" );
					}
					else
					{
						if ((string)rk.GetValue( "password", "" ) == "@")
						{
							DialogResult x = MessageBox.Show("Password will be saved in the registry in clear text for your windows login.  This could be a security issue.  Are you sure?", "Save Password",
								MessageBoxButtons.YesNo, MessageBoxIcon.Question);

							if (x.Equals(DialogResult.Yes))
							{
								rk.SetValue( "password", txtPassword.Text );
							} 
							else 
							{
								rk.SetValue( "password", "@" );
							}
						}  
					
					}

					// auto login
					if (menuItem5.Checked)
					{
						rk.SetValue( "autologin", "yes" );				
					}
					else
					{
						rk.SetValue( "autologin", "no" );
					}

					rk.Close();
					notify.Visible = true;
				}
				else 
				{
					MessageBox.Show("Unable to login.  Make sure your username and password are correct.", "Authentication Error",
						MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

					this.Enabled = true;
					this.Cursor = Cursors.Default;
				}
			} 
			catch ( Exception ex ) 
			{
				MessageBox.Show( this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation );		
				this.Enabled = true;
				this.Cursor = Cursors.Default;
			}
		}

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			Application.Exit();
		}

		private void menuItem7_Click(object sender, System.EventArgs e)
		{
		    new AboutForm().Show();
		}

		private void menuItem5_Click(object sender, System.EventArgs e)
		{					
			if (menuItem5.Checked)
			{
				menuItem5.Checked = false;				
			}
			else
			{
				menuItem5.Checked = true;
			}
	    }

		private void menuItem4_Click(object sender, System.EventArgs e)
		{					
			if( menuItem4.Checked )
			{
                menuItem4.Checked = false;
			}
			else
			{
				menuItem4.Checked = true;
			}
		}

		private void menuItem8_Click(object sender, System.EventArgs e)
		{
			new PostForm().Show();
		}

		private void menuItem11_Click(object sender, System.EventArgs e)
		{
			Application.Exit();
		}

		private void menuItem10_Click(object sender, System.EventArgs e)
		{
		     new AboutForm().Show();
		}

		private void notify_DoubleClick(object sender, System.EventArgs e)
		{
			new PostForm().Show();
		}

		private void menuItem13_Click(object sender, System.EventArgs e)
		{
			new Options().Show();
		}

		private void menuItem15_Click(object sender, System.EventArgs e)
		{
			System.Diagnostics.Process.Start("http://www.justjournal.com/users/" + JustJournal.UserName);
		}

		private void menuItem16_Click(object sender, System.EventArgs e)
		{
			System.Diagnostics.Process.Start("http://www.justjournal.com/users/" + JustJournal.UserName + "/friends");
		}

		private void menuItem17_Click(object sender, System.EventArgs e)
		{
			System.Diagnostics.Process.Start("http://www.justjournal.com/users/" + JustJournal.UserName + "/calendar");
		}

		private void menuItem14_Click(object sender, System.EventArgs e)
		{
			System.Diagnostics.Process.Start("http://www.justjournal.com/");
		}

	}
}
