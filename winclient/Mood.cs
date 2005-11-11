using System;

namespace JustJournal
{
	/// <summary>
	/// Summary description for Mood.
	/// </summary>
	public class Mood
	{
		private string id;
		private string name;
    
		public Mood(string name, string id)
		{
	 	    this.id = id;
			this.name = name;
		}

		public string Name
		{
			get
			{
				return name;
			}
		}

		public string Id
		{
        
			get
			{
				return id ;
			}
		}

		public override string ToString()
		{
			return this.id+ " - " + this.name;
		}

	}
}
