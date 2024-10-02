using static System.Console; // För att kunna använda Console-metoder utan att skriva "Console."

namespace GuestbookApp
{
    class Program
    {
        static void Main ()
        {
            // Skapar ny instans av klassen guestbook
            Guestbook guestbook = new();

            // Starta huvudloopen
            while (true)
            {
                Clear(); // Rensa konsolen
                WriteLine("Alex Gästbook\n");
                WriteLine("1. Lägg till inlägg");
                WriteLine("2. Ta bort inlägg");
                WriteLine("X. Avsluta\n");

                WriteLine("Tidigare inlägg:");
                guestbook.ShowPosts(); // Metod för att visa alla inlägg
                
                WriteLine("\nVälj ett alternativ: ");
                string? input = ReadLine()?.ToLower(); // Läs in användarens input och omvandla till gemener

                // Hantera användarens val via switch-sats
                switch (input)
                {
                    case "1":
                        Clear();
                        string? user; // Variabel för användarnamn
                        string? post; // Variabel för inläggstext
                        
                        // Be om användarnamn och kontrollera att det inte är tomt
                        Write("Ange ditt namn: ");
                        while (string.IsNullOrWhiteSpace(user = ReadLine()))
                        {
                            Clear();
                            Write("Du måste ange ett namn.\n");
                            Write("Ange ditt namn: ");
                        }

                        // Be om inlägg och kontrollera att det inte är tomt
                        Write("Skriv ditt inlägg: ");
                        while (string.IsNullOrWhiteSpace(post = ReadLine()))
                        {
                            Clear();
                            Write("Du måste ange ett inlägg.\n");
                            Write("Skriv inlägg: ");
                        }
                        
                        // Lägg till inlägget i gästboken (om metoden returnerar true från variablerna)
                        if (guestbook.AddPost(user, post))
                        {
                            WriteLine("\nInlägget har lagts till!");
                        }
                        
                        WriteLine("Tryck på valfri tangent för att fortsätta...");
                        ReadKey();
                        break;

                    case "2":
                        Clear();
                        
                        // Kontrollera om det finns inlägg att ta bort
                        if (guestbook.GetPostCount() == 0)
                        {
                            WriteLine("Det finns inga inlägg att ta bort.");
                            WriteLine("Tryck på valfri tangent för att fortsätta...");
                            ReadKey();
                            break;
                        }

                        guestbook.ShowPosts(); // Visa inläggen för att välja borttagning
                        WriteLine("\nAnge index för det inlägg du vill ta bort:");

                        // Kontrollera om input är ett giltigt index (siffra)
                        if (int.TryParse(ReadLine(), out int index))
                        {
                            try
                            {
                                if (guestbook.DeletePost(index)) // Ta bort inlägget (om metoden returnerar true)
                                {
                                    WriteLine("\nInlägget har tagits bort.");
                                }
                            }
                            catch (Exception ex) // Fånga alla undantag som inträffar under borttagningen
                            {
                                WriteLine($"\nEtt fel inträffade: {ex.Message}");
                            }
                        }
                        else
                        {
                            // Visa felmeddelande om inmatningen inte är en giltig siffra
                            WriteLine("\nOgiltigt index.");
                        }

                        WriteLine("Tryck på valfri tangent för att fortsätta...");
                        ReadKey();
                        break;

                    case "x":
                        // Avsluta programmet
                        Environment.Exit(0);
                        break;

                    default:
                        // Visa felmeddelande om användaren gjort ett ogiltigt val
                        WriteLine("Ogiltigt val.");
                        WriteLine("Tryck på valfri tangent för att fortsätta...");
                        ReadKey();
                        break;
                }
            }
        }
    }
}
