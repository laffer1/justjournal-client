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
	/// Summary description for Options.
	/// </summary>
	public class Options : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.TabPage tabGeneral;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.RadioButton postRaw;
		private System.Windows.Forms.RadioButton postFormatted;
		private System.Windows.Forms.CheckBox chkUseSSL;
		private System.Windows.Forms.CheckBox chkSavePassword;
		private System.Windows.Forms.CheckBox chkAutoLogin;
		private System.Windows.Forms.TabPage tabIntegration;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.CheckBox chkUseWord;
		private System.Windows.Forms.CheckBox chkAutoSpell;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Options()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			RegistryKey rk = Registry.CurrentUser.CreateSubKey( "SOFTWARE\\JustJournal" );
			RegistryKey mk = rk.CreateSubKey( "Preferences" );
			bool autoLog = ((string)rk.GetValue( "autologin", "no" )).Equals("yes");
			bool ssl = ((string)rk.GetValue( "usessl", "yes" )).Equals("yes");
			string pwd = (string)rk.GetValue( "password", "" );
			string Format = (string)mk.GetValue( "Formatting", "Formatted" );

            bool useWord = ((string)rk.GetValue( "useword", "no" )).Equals("yes");
			bool autoSpell = ((string)rk.GetValue( "autospell", "no" )).Equals("yes");

			if (autoLog)
				chkAutoLogin.Checked = true;
			else
				chkAutoLogin.Checked = false;

			if (ssl)
				chkUseSSL.Checked = true;
			else
				chkUseSSL.Checked = false;

			if( pwd.Equals("@") || pwd.Equals("") ) 
				chkSavePassword.Checked = false;
			else
				chkSavePassword.Checked = true;

			if (Format.Equals("Formatted"))
				postFormatted.Checked = true;
			else
				postRaw.Checked = true;

			if (useWord)
				chkUseWord.Checked = true;
			else
				chkUseWord.Checked = false;

			if (autoSpell)
				chkAutoSpell.Checked = true;
			else 
				chkAutoSpell.Checked = false;

		}

		
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			this.SaveSettings();

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
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabGeneral = new System.Windows.Forms.TabPage();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.postRaw = new System.Windows.Forms.RadioButton();
			this.postFormatted = new System.Windows.Forms.RadioButton();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.chkUseSSL = new System.Windows.Forms.CheckBox();
			this.chkSavePassword = new System.Windows.Forms.CheckBox();
			this.chkAutoLogin = new System.Windows.Forms.CheckBox();
			this.btnClose = new System.Windows.Forms.Button();
			this.tabIntegration = new System.Windows.Forms.TabPage();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.chkUseWord = new System.Windows.Forms.CheckBox();
			this.chkAutoSpell = new System.Windows.Forms.CheckBox();
			this.tabControl1.SuspendLayout();
			this.tabGeneral.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.tabIntegration.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabGeneral);
			this.tabControl1.Controls.Add(this.tabIntegration);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.HotTrack = true;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Multiline = true;
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(432, 406);
			this.tabControl1.TabIndex = 0;
			// 
			// tabGeneral
			// 
			this.tabGeneral.Controls.Add(this.groupBox2);
			this.tabGeneral.Controls.Add(this.groupBox1);
			this.tabGeneral.Location = new System.Drawing.Point(4, 22);
			this.tabGeneral.Name = "tabGeneral";
			this.tabGeneral.Size = new System.Drawing.Size(424, 380);
			this.tabGeneral.TabIndex = 0;
			this.tabGeneral.Text = "General";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.postRaw);
			this.groupBox2.Controls.Add(this.postFormatted);
			this.groupBox2.Location = new System.Drawing.Point(8, 152);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(408, 88);
			this.groupBox2.TabIndex = 2;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Post Mode";
			// 
			// postRaw
			// 
			this.postRaw.Location = new System.Drawing.Point(24, 48);
			this.postRaw.Name = "postRaw";
			this.postRaw.TabIndex = 1;
			this.postRaw.Text = "Raw Text";
			// 
			// postFormatted
			// 
			this.postFormatted.Location = new System.Drawing.Point(24, 24);
			this.postFormatted.Name = "postFormatted";
			this.postFormatted.TabIndex = 0;
			this.postFormatted.Text = "Formatted Text";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.chkUseSSL);
			this.groupBox1.Controls.Add(this.chkSavePassword);
			this.groupBox1.Controls.Add(this.chkAutoLogin);
			this.groupBox1.Location = new System.Drawing.Point(8, 16);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(408, 125);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Login";
			// 
			// chkUseSSL
			// 
			this.chkUseSSL.Location = new System.Drawing.Point(24, 88);
			this.chkUseSSL.Name = "chkUseSSL";
			this.chkUseSSL.Size = new System.Drawing.Size(184, 24);
			this.chkUseSSL.TabIndex = 2;
			this.chkUseSSL.Text = "Use secure connection (SSL)";
			// 
			// chkSavePassword
			// 
			this.chkSavePassword.Location = new System.Drawing.Point(24, 56);
			this.chkSavePassword.Name = "chkSavePassword";
			this.chkSavePassword.Size = new System.Drawing.Size(184, 24);
			this.chkSavePassword.TabIndex = 1;
			this.chkSavePassword.Text = "Save Password";
			// 
			// chkAutoLogin
			// 
			this.chkAutoLogin.Location = new System.Drawing.Point(24, 24);
			this.chkAutoLogin.Name = "chkAutoLogin";
			this.chkAutoLogin.TabIndex = 0;
			this.chkAutoLogin.Text = "Auto Login";
			// 
			// btnClose
			// 
			this.btnClose.Location = new System.Drawing.Point(352, 376);
			this.btnClose.Name = "btnClose";
			this.btnClose.TabIndex = 1;
			this.btnClose.Text = "Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// tabIntegration
			// 
			this.tabIntegration.Controls.Add(this.groupBox3);
			this.tabIntegration.Location = new System.Drawing.Point(4, 22);
			this.tabIntegration.Name = "tabIntegration";
			this.tabIntegration.Size = new System.Drawing.Size(424, 380);
			this.tabIntegration.TabIndex = 1;
			this.tabIntegration.Text = "Integration";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.chkAutoSpell);
			this.groupBox3.Controls.Add(this.chkUseWord);
			this.groupBox3.Location = new System.Drawing.Point(8, 16);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(408, 96);
			this.groupBox3.TabIndex = 0;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Microsoft Office";
			// 
			// chkUseWord
			// 
			this.chkUseWord.Location = new System.Drawing.Point(24, 24);
			this.chkUseWord.Name = "chkUseWord";
			this.chkUseWord.Size = new System.Drawing.Size(240, 24);
			this.chkUseWord.TabIndex = 0;
			this.chkUseWord.Text = "Use Microsoft Word to spell check entries";
			// 
			// chkAutoSpell
			// 
			this.chkAutoSpell.Location = new System.Drawing.Point(24, 56);
			this.chkAutoSpell.Name = "chkAutoSpell";
			this.chkAutoSpell.Size = new System.Drawing.Size(256, 24);
			this.chkAutoSpell.TabIndex = 1;
			this.chkAutoSpell.Text = "Automatically check spelling before posting";
			// 
			// Options
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(432, 406);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.tabControl1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Options";
			this.Text = "Options";
			this.TopMost = true;
			this.tabControl1.ResumeLayout(false);
			this.tabGeneral.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.tabIntegration.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.SaveSettings();
			this.Close();
		}

		public void SaveSettings()
		{
			RegistryKey rk = Registry.CurrentUser.CreateSubKey( "SOFTWARE\\JustJournal" );
			RegistryKey mk = rk.CreateSubKey( "Preferences" );

			/*mk.SetValue( "ServerUrl", ServerURL );
			mk.SetValue( "FontFace", FontSetting.Name );
			mk.SetValue( "FontSize", FontSetting.Size );

			mk.SetValue( "ProxyURL", proxyURL.Text );
			mk.SetValue( "ProxyPort", proxyPort.Text );
			mk.SetValue( "ProxyPassword", (useLP.Checked ? proxyPassword.Text : string.Empty) );
			mk.SetValue( "ProxyLogin", (useLP.Checked ? proxyLogin.Text : string.Empty) );

			mk.SetValue( "AutoPlay", (AutoCheckMusic ? "Yes" : "No") );
			mk.SetValue( "StopCheck", (CheckStopped ? "Yes" : "No") );
			mk.SetValue( "PauseCheck", (CheckPaused ? "Yes" : "No") );
			mk.SetValue( "QueuePost", (PostToQueue ? "Yes" : "No") );
			mk.SetValue( "QueueSpeed", QueueSpeed.ToString() );
			mk.SetValue( "Outlook", (PostToOutlook ? "Yes" : "No") );
			mk.SetValue( "Word", (SpellCheck ? "Yes" : "No") );
			mk.SetValue( "Spellcheck", (AlwaysCheck ? "Yes" : "No") );
			*/
// TODO finish this
			mk.SetValue( "Formatting", (postFormatted.Checked ? "Formatted" : "Raw") );

			if (chkUseSSL.Checked)
			    rk.SetValue( "usessl", "yes" );
			else
				rk.SetValue("usessl", "no" );

			if ( chkSavePassword.Checked )
                rk.SetValue( "password", JustJournal.Password );
			else
			    rk.SetValue( "password", "@" );

			if ( chkAutoLogin.Checked )
			    rk.SetValue( "autologin", "yes" );
			else
				rk.SetValue( "autologin", "no" );

			if (chkUseWord.Checked)
			{
				JustJournal.EnableSpellCheck = true;
				rk.SetValue( "useword", "yes" );
			}
			else
			{
				JustJournal.EnableSpellCheck = false;
				rk.SetValue( "useword", "no" );
			}

			if (chkUseWord.Checked && chkAutoSpell.Checked)
			{
				JustJournal.AutoSpellCheck = true;
				rk.SetValue( "autospell", "yes" );
			}
			else
			{
				JustJournal.AutoSpellCheck = false;
				rk.SetValue( "autospell", "no" );
			}

			/*string old_lang = mk.GetValue( "Language", string.Empty ).ToString();
			string lang = cbLanguage.SelectedItem.ToString();
			foreach( object key in Languages.Keys )
				if( Languages[key].ToString().Equals( lang ) )
					mk.SetValue( "Language", key.ToString() );
				
			if( !old_lang.Equals( mk.GetValue( "Language", string.Empty ).ToString() ) )
				MessageBox.Show( rm.GetString( "message_change_lang" ) );*/
		}
	}

}
