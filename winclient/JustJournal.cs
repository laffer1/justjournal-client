using System;
using System.Collections;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Reflection;

namespace JustJournal
{
	/// <summary>
	/// Summary description for JustJournal.
	/// </summary>
	public static class JustJournalCore
	{
        private const string Server = "www.justjournal.com";

        private static bool _loggedIn;
		private static string _userName;
		private static string _password;
		
        private static bool _enableSpellCheck;
		private static bool _autoSpellCheck;
		private static bool _enableItunes;
		private static bool _detectMusic;
		private static bool _useSsl = true;
		private static string _debug;
// ReSharper disable InconsistentNaming
		private static readonly ArrayList moods = new ArrayList(125);
// ReSharper restore InconsistentNaming
		private static bool _winampPaused;
		private static bool _winampStopped;

		/* Properties */

		public static ArrayList Moods
		{
			get
			{
				return moods;
			}
		}

		public static bool EnableSsl
		{
			get
			{
				return _useSsl;
			}
			set
			{
				_useSsl = value;
			}
		}

		public static string Debug
		{
			get
			{
				return _debug;
			}
			set
			{
				_debug = value;
			}
		}

		public static bool EnableSpellCheck
		{
			get
			{
				return _enableSpellCheck;
			}
			set
			{
				_enableSpellCheck = value;
			}
		}

		public static bool AutoSpellCheck
		{
			get
			{
				return _autoSpellCheck;
			}
			set
			{
				_autoSpellCheck = value;
			}
		}

		public static bool DetectItunes
		{
			get
			{
				return _enableItunes;
			}
			set
			{
				_enableItunes = value;
			}
		}

		public static bool EnableMusicDetection
		{
			get
			{
				return _detectMusic;
			}
			set
			{
				_detectMusic = value;
			}
		}

		public static string Version
		{
			get
			{
                return "JustJournal/" + AssemblyVersion() + " Win";
			}
		}

		public static bool LoggedIn
		{   
			get
			{
				return _loggedIn;
			}
			set
			{
				_loggedIn = value;
			}
		}

		public static string UserName
		{
			get
			{
				return _userName;
			}

			set
			{
				_userName = value;
			}
		}

		public static string Password
		{
			get
			{
				return _password;
			}

			set
			{
				_password = value;
			}
		}

		public static bool WinampPaused
		{
			get
			{
				return _winampPaused;
			}
			set
			{
				_winampPaused = value;
			}
		}
		
		public static bool WinampStopped
		{
			get
			{
				return _winampStopped;
			}
			set
			{
				_winampStopped = value;
			}
		}

		/* Methods */
		public static bool Login()
		{
			string uriString;
			WebClient client = new WebClient();	
			if (_useSsl)
                uriString = "https://";
			else
			    uriString = "http://";
			 
			uriString += Server + "/loginAccount";
			
			// Create a new NameValueCollection instance to hold some custom parameters to be posted to the URL.
			NameValueCollection myNameValueCollection = new NameValueCollection();

			// Add a user agent header in case the 
			// requested URI contains a query.
			client.Headers.Add("User-Agent", Version);

			myNameValueCollection.Add("username", _userName);            
			myNameValueCollection.Add("password", _password);
			myNameValueCollection.Add("password_hash", "");

			byte[] responseArray = client.UploadValues(uriString,"POST",myNameValueCollection);
			//Encoding.ASCII.GetString(responseArray)

			string resp = Encoding.ASCII.GetString(responseArray);
			_debug = resp;
			return resp.Length <= 0 || resp.IndexOf("JJ.LOGIN.OK", StringComparison.Ordinal) != -1;
		}

		public static void RetrieveMoods()
		{
            try
            {
                var client = new WebClient();
                var uriString = "http://" + Server + "/moodlist.h";

                client.Headers.Add("User-Agent", Version);
                byte[] responseArray = client.DownloadData(uriString);
                var resp = Encoding.UTF8.GetString(responseArray);
                var xdoc = new System.Xml.XmlDocument();
                xdoc.LoadXml(resp);

                var nl = xdoc.SelectNodes("model/moods/*");

                moods.Clear();  // clear the list
                System.Diagnostics.Debug.Assert(nl != null, "nl != null");
                for (var n = 0; n < nl.Count; n++)
                {
                    var xmlNode = nl.Item(n);
                    if (xmlNode != null)
                    {
                        var item = xmlNode.ChildNodes.Item(0);
                        if (item != null)
                        {
                            var node = xmlNode.ChildNodes.Item(2);
                            if (node != null)
                                moods.Add(new Mood(item.InnerText, node.InnerText));
                        }
                    }
                }
            }
            catch (System.IO.IOException)
            {
               new Alert("Error loading Moods from JJ Server. Please restart just journal and try again later.", "");
            }
		}

		// static class

	    private static String AssemblyVersion()
        {
            Assembly assembly = Assembly.GetCallingAssembly();
            AssemblyName assemblyname = assembly.GetName();
            Version assemblyver = assemblyname.Version;
            return assemblyver.ToString();
        }


	}
}
