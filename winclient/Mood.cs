namespace JustJournal
{
	/// <summary>
	/// Users's Mood
	/// </summary>
	public class Mood
	{
        public Mood() {}

        public Mood(string name, string id)
        {
            Id = id;
            Name = name;
        }

		public string Name { get; set; }
		
		public string Id { get; set; }
        
		public override string ToString()
		{
			return Id+ " - " + Name;
		}
	}
}
