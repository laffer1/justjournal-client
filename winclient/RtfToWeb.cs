using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Net;
using System.Collections;
using System.Windows.Forms;
using System.IO;
using System.Web;

namespace JustJournal
{
	/// <summary>
	/// Summary description for RtfToWeb.
	/// </summary>
	public class RtfToWeb
	{
		private bool css = true;
		private bool xhtml = true;
		private string bgcolor = "transparent";

		public RtfToWeb()
		{
			
		}

		private string ToCssFontSize( int size )
		{
			switch( size )
			{
				case 1:
				case 2:
				case 3:
				case 4:
				case 5:
					return "xx-small";
				case 6:
				case 7:
					return "x-small";
				case 8:
				case 9:
				case 10:
					return "small";
				case 11:
				case 12:
					return "medium";
				case 13:
				case 14:
					return "large";
				case 15:
				case 16:
				case 17:
				case 18:
					return "x-large";
				default:
					return "xx-large";
				
			}
		}
	
		private string ToHexColor( int red, int green, int blue )
		{
			return "#" + System.Convert.ToString( red, 16 ).PadLeft( 2, '0' )
				+ System.Convert.ToString( green, 16 ).PadLeft( 2, '0' )
				+ System.Convert.ToString( blue, 16 ).PadLeft( 2, '0' );
		}

		private void writeRtf( string rtf )
		{
			#region write RTF string to file on desktop
			StreamWriter sw = File.CreateText( Environment.GetFolderPath( Environment.SpecialFolder.Desktop ) + "\\debug.txt" );
			sw.Write( rtf );
			sw.Close();
			#endregion
		}

		private void writeHtml( string result )
		{
			#region Write finished output to desktop for debugging
			StreamWriter sw2 = File.CreateText( Environment.GetFolderPath( Environment.SpecialFolder.Desktop ) + "\\debugdone.txt" );
			sw2.Write( result );
			sw2.Close();
			#endregion
		}

		/// <summary>
		/// Converts RTF text to HTML acceptable for the JJ Server.
		///    IMPORTANT NOTE:
		///	     This does NOT do a pure HTML conversion - many of the RTF codes
		///	     that are inappropriate for JJ are ignored.
		/// </summary>
		/// <param name="rtf">The RTF string to convert.</param>
		/// <returns>A string containing the HTML equivalent of the RTF.</returns>
		public string Convert( string rtf )
		{
			try
			{
				writeRtf(rtf);  // debug
				Hashtable fontTable = new Hashtable();
				Hashtable colorTable = new Hashtable();

				string result = string.Empty;
				string tg = string.Empty;
				string sz = "medium";
				string ft = "Arial Unicode MS";
				
				bool tag = false;
				int brace = 0;
				bool rdfonts = false;
				int ftlevel = 0;
				string deffont = string.Empty;
				bool rdcolor = false;
				ArrayList al = new ArrayList();

				int cur = 0;

				while( cur < rtf.Length )
				{
					if( !rtf[cur].Equals( '\n' ) ) //skip linefeeds
					{
						if( ( rtf[cur].Equals( ' ' ) || rtf[cur].Equals( '\r' ) || rtf[cur].Equals( '\\' ) ) && tag || rdcolor )
						{
							tag = false;
							
							if( tg.StartsWith( "deff" ) )
							{
								deffont = tg.Substring( 3, tg.Length - 3 );
							}
							else if( tg.StartsWith( "f" ) ) //could be font or fontsize
							{
								if( tg.Equals( "fonttbl" ) )
								{
									rdfonts = true;
									ftlevel = brace - 1;
								}
								else if( rdfonts ) 
								{
									string ftname = string.Empty;

									while( !rtf[cur].Equals( ' ' ) )
										cur++;

									cur++;

									while( !(rtf[cur].Equals( ';' )) )
										ftname += rtf[cur++];

									fontTable.Add( tg, ftname );

									if( tg.Equals( deffont ) )
										ft = fontTable[deffont].ToString();
								}
								else
								{
									if( TagCounts.Span ) 
									{
										result += "</span>";
										TagCounts.Span = false;
									}
									if( tg.StartsWith( "fs" ) )
									{
										sz = ToCssFontSize( System.Convert.ToInt32( tg.Substring( 2 ) ) / 2 );
									}
									else if( fontTable.ContainsKey( tg ) )
									{
										ft = fontTable[tg].ToString();
									}
									else if( fontTable.ContainsKey( deffont ) )
									{
										ft = fontTable[deffont].ToString();
									}
									TagCounts.Span = true;
									result += "<span style=\"font-family:" + ft + "; font-size:" + sz + "\">";
								}
							}
							else if( rdcolor )
							{
								string strcolor = string.Empty;
								int colorindex = 0;
								while( !(rtf[cur].Equals( '}' )) )
								{
									while( !(rtf[cur].Equals( ';' )) )
									{
										strcolor += rtf[cur++];
									}
									if( strcolor.Length > 0 )
									{
										int red = 0, green = 0, blue = 0;
										strcolor = strcolor.TrimStart( '\\' );
										string[] colors = strcolor.Split( '\\' );
										foreach( string s in colors )
										{
											if( s.StartsWith( "red" ) )
											{
												red = System.Convert.ToInt32( s.Substring( 3 ) );
											}
											else if( s.StartsWith( "green" ) )
											{
												green = System.Convert.ToInt32( s.Substring( 5 ) );
											}
											else if( s.StartsWith( "blue" ) )
											{
												blue = System.Convert.ToInt32( s.Substring( 4 ) );
											}
										}
										colorTable.Add( colorindex++, ToHexColor( red, green, blue ) );
									}
									else
									{
										colorTable.Add( colorindex++, string.Empty );
									}
									cur++;
								}
								rdcolor = false;
							}
							else if( tg.StartsWith( "highlight" ) )
							{
								string color = colorTable[System.Convert.ToInt32( tg.Substring( 9 ) )].ToString();
								if( color.Length > 0 )
								{
									result += "<span style=\"background-color:" + color + ";\">";
									TagCounts.Span = true;
								}
							}
							else if( tg.StartsWith( "cf" ) )
							{
								string color = colorTable[System.Convert.ToInt32( tg.Substring( 2 ) )].ToString();
								if( color.Length > 0 )
								{
									result += "<span style=\"color:" + color + ";\">";
									TagCounts.Span = true;
								}
							}
								//								else if( tg.StartsWith( "'" ) )
								//								{
								//									utext = true;
								//									al.Add( System.Convert.ToByte( tg.Substring( 1 ), 16 ) );
								//								}
							else
							{
								switch( tg )
								{
									case "par":
										if( TagCounts.Span )
										{
											result += "</span>";
											TagCounts.Span = false;
										}
										result += "<br />"; // newline/paragraph
										break;
									case "ul":
										result += "<u>";
										break;
									case "ulnone":
									case "ul0":
										result += "</u>";
										break;
									case "b":
										result += "<strong>";
										break;
									case "b0":
										result += "</strong>";
										break;
									case "i":
										result += "<em>";
										break;
									case "i0":
										result += "</em>";
										break;
									case "strike":
										result += "<s>";
										break;
									case "strike0":
										result += "</s>";
										break;
									case "colortbl":
										rdcolor = true;
										break;
									case "line":
										result += "\n";
										break;
									case "emdash":
										result += "&mdash;";
										break;
									case "endash":
										result += "&ndash;";
										break;
									case "lquote":
										result += "&lsquo;";
										break;
									case "rquote":
										result += "&rsquo;";
										break;
									case "ldblquote":
										result += "&ldquo;";
										break;
									case "rdblquote":
										result += "&rdquo;";
										break;
									default:
										break;
								}
							}
							if( rtf[cur].Equals( '\\' ) ) // start another tag
							{
								tag = true;
								tg = string.Empty;
							}
						}
						else if( rtf[cur].Equals( '{' ) )
						{
							brace++;
						}
						else if( rtf[cur].Equals( '}' ) )
						{
							brace--;
							if( brace < ftlevel )
							{
								rdfonts = false;
							}
						}
						else if( rtf[cur].Equals( '\\' ) )
						{
							tg = string.Empty;
							tag = true;
						}
						else //only interested in text
						{
							if( !tag && !char.IsControl( rtf[cur] ) )
							{
								result += rtf[cur];
							}
							else
							{
								switch( rtf[cur] )
								{
									case '<':
										tg += "&lt;";
										break;
									case '>':
										tg += "&gt;";
										break;
									case '&':
										tg += "&amp;";
										break;
									case '\"':
										tg += "&quot;";
										break;
									default:
										tg += rtf[cur];
										break;
								}
							}
						}
					}
					cur++;
				}

				result = result.TrimEnd( "\r\n".ToCharArray() );
				while( TagCounts.Font )
				{
					result += "</font>";
					TagCounts.Font = false;
				}
				while( TagCounts.Span )
				{
					result += "</span>";
					TagCounts.Span = false;
				}
				//Regex re = new Regex( @"\<font[^>]+\>[ \n\r]*\</font\>", RegexOptions.Compiled );
				//result = re.Replace( result, string.Empty );

			   writeHtml(result);  // debug

				if( result.Length > 0 )
				{
					if (bgcolor == "#ffffff")
						bgcolor="transparent";
					result = "<p style=\"background-color:" + bgcolor + "\">" + result + "</p>";
					return result;
				}
			}
			catch( Exception ex )
			{
				MessageBox.Show( ex.Message + "\n" + ex.StackTrace, "Exception in " + ex.Source );
			}
			throw new Exception( "Error converting post to HTML." );
		}
	}
}
