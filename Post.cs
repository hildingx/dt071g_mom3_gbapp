namespace GuestbookApp
{
    public class Post
    {
        // Egenskap för användarnamn
        public string? User { get; set; }

        // Egenskap för inläggstext
        public string? Text { get; set; }

        // Överskriver ToString-metoden för att returnera en formaterad sträng med inläggsinformation
        public override string ToString()
        {
            // Returnerar en sträng som beskriver inlägget med användarnamn och text
            return $"Inlägg av {User}: {Text}";
        }
    }
}