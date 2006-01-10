using System;
using System.Web;
using System.IO;
using System.Collections;
using System.Collections.Specialized;
using System.Windows.Forms;
using System.Net;
using Microsoft.Win32;
using System.Text.RegularExpressions;
using System.Text;
using System.Security.Cryptography;

namespace JustJournal
{
	/// <summary>
	/// Summary description for JustJournal.
	/// </summary>
	public class JustJournal
	{
        private const string server = "www.justjournal.com";
		private const string version = "JustJournal/1.2.2 Win";

		private static bool loggedIn = false;
		private static string userName;
		private static string password;
		
        private static bool enableSpellCheck = false;
		private static bool autoSpellCheck = false;
		private static bool enableItunes = false;
		private static bool detectMusic = false;
		private static bool useSSL = true;
		private static string debug;
		private static ArrayList moods = new ArrayList(125);
		private static bool winampPaused = false;
		private static bool winampStopped = false;
		private static bool outlook = false;

		/* Properties */
		public static bool Outlook
		{
			get
			{ 
				return outlook;
			}
			set
			{
				outlook = value;
			}
		}

		public static ArrayList Moods
		{
			get
			{
				return moods;
			}
			set
			{
				moods = value;
			}
		}

		public static bool EnableSSL
		{
			get
			{
				return useSSL;
			}
			set
			{
				useSSL = value;
			}
		}

		public static string Debug
		{
			get
			{
				return debug;
			}
			set
			{
				debug = value;
			}
		}

		public static bool EnableSpellCheck
		{
			get
			{
				return enableSpellCheck;
			}
			set
			{
				enableSpellCheck = value;
			}
		}

		public static bool AutoSpellCheck
		{
			get
			{
				return autoSpellCheck;
			}
			set
			{
				autoSpellCheck = value;
			}
		}

		public static bool DetectItunes
		{
			get
			{
				return enableItunes;
			}
			set
			{
				enableItunes = value;
			}
		}

		public static bool EnableMusicDetection
		{
			get
			{
				return detectMusic;
			}
			set
			{
				detectMusic = value;
			}
		}

		public static string Version
		{
			get
			{
                return version;
			}
		}

		public static bool LoggedIn
		{   
			get
			{
				return loggedIn;
			}
			set
			{
				loggedIn = value;
			}
		}

		public static string UserName
		{
			get
			{
				return userName;
			}

			set
			{
				userName = value;
			}
		}

		public static string Password
		{
			get
			{
				return password;
			}

			set
			{
				password = value;
			}
		}

		public static bool WinampPaused
		{
			get
			{
				return winampPaused;
			}
			set
			{
				winampPaused = value;
			}
		}
		
		public static bool WinampStopped
		{
			get
			{
				return winampStopped;
			}
			set
			{
				winampStopped = value;
			}
		}

			/* Methods */

			public static bool Login()
		{
			string uriString;
			WebClient client = new WebClient();	
			if (useSSL)
                uriString = "https://";
			else
			    uriString = "http://";
			 
			uriString += server + "/loginAccount";
			
			// Create a new NameValueCollection instance to hold some custom parameters to be posted to the URL.
			NameValueCollection myNameValueCollection = new NameValueCollection();

			// Add a user agent header in case the 
			// requested URI contains a query.
			client.Headers.Add("User-Agent", version);

			myNameValueCollection.Add("username", userName);            
			myNameValueCollection.Add("password", password);
			myNameValueCollection.Add("password_hash", "");

			byte[] responseArray = client.UploadValues(uriString,"POST",myNameValueCollection);
			//Encoding.ASCII.GetString(responseArray)

			string resp = Encoding.ASCII.GetString(responseArray);
			debug = resp.ToString();
			if ( resp.Length > 0 && resp.IndexOf("JJ.LOGIN.OK") == -1 ) 
				return false;
			else
				return true;
		}

		public static void RetrieveMoods()
		{
			WebClient client = new WebClient();
			string uriString = "http://" + server + "/moodlist.h";

		    client.Headers.Add("User-Agent", version);
			byte[] responseArray = client.DownloadData(uriString);
			string resp = Encoding.UTF8.GetString(responseArray);
			System.Xml.XmlDocument xdoc = new System.Xml.XmlDocument();
			xdoc.LoadXml(resp);

			System.Xml.XmlNodeList nl = xdoc.SelectNodes("model/moods/*");

			moods.Clear();  // clear the list
			for (int n = 0; n < nl.Count; n++)
			{
				moods.Add(new Mood(nl.Item(n).ChildNodes.Item(0).InnerText,nl.Item(n).ChildNodes.Item(2).InnerText));
			}
		}

		// static class
		private JustJournal()
		{
			
		}

	}
}
