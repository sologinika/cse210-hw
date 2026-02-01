
namespace JournalProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            Journal journal = new Journal();
            Entry anEntry = new Entry();
            anEntry.Display(anEntry);
            
            bool running = true;

            while (running)
            {
                Console.WriteLine("1. Write a new entry");
                Console.WriteLine("2. Display journal");
                Console.WriteLine("3. Save journal to file");
                Console.WriteLine("4. Load journal from file");
                Console.WriteLine("5. Quit");

                Console.Write("Choose an option: ");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        journal.AddEntry();
                        break;
                    case "2":
                        journal.DisplayEntries();
                        break;
                    case "3":
                        journal.SaveToFile();
                        break;
                    case "4":
                        journal.LoadFromFile();
                        break;
                    case "5":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please choose again.");
                        break;
                }
            }
        }
    }
}

namespace JournalProgram
{
    public class Journal
    {
        private List<Entry> entries = new List<Entry>();
        private List<string> prompts = new List<string>()
        {
            "Who was the most interesting person I interacted with today?",
            "What was the best part of my day?",
            "How did I see the hand of the Lord in my life today?",
            "What was the strongest emotion I felt today?",
            "If I had one thing I could do over today, what would it be?"
        };

        public void AddEntry()
        {
            string prompt = GetRandomPrompt();
            string response = GetResponse(prompt);
            string date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            Entry entry = new Entry(prompt, response, date);
            entries.Add(entry);
        }

        public void DisplayEntries()
        {
            foreach (Entry entry in entries)
            {
                Console.WriteLine(entry);
            }
        }

        public void SaveToFile()
        {
            Console.Write("Enter a filename: ");
            string filename = Console.ReadLine();
            using (StreamWriter writer = new StreamWriter(filename))
            {
                foreach (Entry entry in entries)
                {
                    writer.WriteLine($"{entry.Prompt}~|~{entry.Response}~|~{entry.Date}");
                }
            }
        }

        public void LoadFromFile()
        {
            Console.Write("Enter a filename: ");
            string filename = Console.ReadLine();
            entries.Clear();
            using (StreamReader reader = new StreamReader(filename))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(new string[] { "~|~" }, StringSplitOptions.None);
                    Entry entry = new Entry(parts[0], parts[1], parts[2]);
                    entries.Add(entry);
                }
            }
        }

        private string GetRandomPrompt()
        {
            Random random = new Random();
            return prompts[random.Next(prompts.Count)];
        }

        private string GetResponse(string prompt)
        {
            Console.WriteLine(prompt);
            return Console.ReadLine();
        }
    }
}


Entry.cs

using System;

namespace JournalProgram
{
    public class Entry
    {
        public string Prompt { get; set; }
        public string Response { get; set; }
        public string Date { get; set; }

        public Entry(string prompt, string response, string date)
        {
            Prompt = prompt;
            Response = response;
            Date = date;
        }

        public override string ToString()
        {
            return $"{Date}\n{Prompt}\n{Response}\n";
        }
    }
}
