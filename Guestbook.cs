using System.Text.Json;
using static System.Console;

namespace GuestbookApp
{
    public class Guestbook
    {
        // Lista för att hålla alla inlägg
        private List<Post> posts;

        // Sökväg till filen där gästboken sparas
        private readonly string filePath = "guestbook.json";

        // Konstruktor: laddar inlägg från fil om filen existerar
        public Guestbook()
        {
            posts = new List<Post>(); // skapa instans av list of post

            // Kontrollera om filen existerar och ladda inlägg
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath); // Läs innehållet från filen
                posts = JsonSerializer.Deserialize<List<Post>>(json)!; // Deserialisera JSON till lista med Post-objekt
            }
        }

        // Metod för att visa alla inlägg
        public void ShowPosts()
        {
            // Om det inte finns några inlägg, visa ett meddelande
            if (posts.Count == 0)
            {
                WriteLine("Inga inlägg hittades.");
            }
            else
            {
                // Visa varje inlägg med indexnummer
                int i = 0;
                foreach (Post post in posts)
                {
                    WriteLine($"[{i}] {post}");
                    i++;
                }
            }
        }

        // Lägg till ett nytt inlägg
        public bool AddPost(string? user, string? text)
        {
            // Kontrollera om användarnamn eller text är tomma
            if (string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(text))
            {
                WriteLine("Användarnamn och text kan inte vara tomma.");
                return false; // Returnera false om inmatningen är ogiltig
            }

            // Skapa nytt inlägg (Post-objekt) med användarnamn och text
            Post post = new()
            {
                User = user, // Tilldela användarnamn
                Text = text  // Tilldela text
            };

            // Lägg till inlägget i listan
            posts.Add(post);
            Marshal(); // Spara listan till fil
            return true; // Returnera true om inlägget lades till
        }

        // Ta bort ett inlägg via index
        public bool DeletePost(int index)
        {
            // Kontrollera om indexet är inom giltigt intervall
            if (index >= 0 && index < posts.Count)
            {
                posts.RemoveAt(index); // Ta bort inlägget
                Marshal(); // Spara listan till fil
                return true; // Returnera true om inlägget togs bort
            }
            else
            {
                WriteLine("Index utanför räckvidd."); // Meddela om index är ogiltigt
                return false; // Returnera false om index är ogiltigt
            }
        }

        // Spara listan med inlägg till fil i JSON-format
        private void Marshal()
        {
            var jsonString = JsonSerializer.Serialize(posts, new JsonSerializerOptions { WriteIndented = true }); // Serialisera listan till JSON-sträng, som indenteras med WriteIndented 
            File.WriteAllText(filePath, jsonString); // Skriv JSON-strängen till fil
        }

        // Metod för att returnera antal objekt i listan
        public int GetPostCount()
        {
            return posts.Count;
        }
    }
}
