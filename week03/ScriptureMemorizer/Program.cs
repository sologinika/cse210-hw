using System;
namespace ScriptureMemorizer
{
    class Program
    {
        static void Main(string[] args)
        {
            // program written by Alagbaoso Solomon Ginikachukwu.
            // Exceeding requirements: Load scriptures from a file
            Scripture scripture = ScriptureLoader.LoadScripture("scriptures.txt");

            // Display the scripture and hide words until all are hidden
            scripture.DisplayAndHideWords();
        }
    }

 }
namespace ScriptureMemorizer
{
    public class Scripture
    {
        private Reference reference;
        private List<Word> words;

        public Scripture(Reference reference, string text)
        {
            this.reference = reference;
            words = new List<Word>();
            string[] wordArray = text.Split(' ');
            foreach (string word in wordArray)
            {
                words.Add(new Word(word));
            }
        }

        public void DisplayAndHideWords()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine(reference.ToString());
                foreach (Word word in words)
                {
                    Console.Write(word.ToString() + " ");
                }
                Console.WriteLine();

                Console.Write("Press enter to hide words, type 'quit' to exit: ");
                string input = Console.ReadLine();
                if (input.ToLower() == "quit")
                {
                    break;
                }

                HideWords();
                if (AllWordsHidden())
                {
                    Console.Clear();
                    Console.WriteLine(reference.ToString());
                    foreach (Word word in words)
                    {
                        Console.Write(word.ToString() + " ");
                    }
                    Console.WriteLine();
                    break;
                }
            }
        }

        private void HideWords()
        {
            Random random = new Random();
            for (int i = 0; i < 3; i++)
            {
                int index = random.Next(words.Count);
                words[index].Hide();
            }
        }

        private bool AllWordsHidden()
        {
            foreach (Word word in words)
            {
                if (!word.IsHidden())
                {
                    return false;
                }
            }
            return true;
        }
    }
}

namespace ScriptureMemorizer
{
    public class Reference
    {
        private string book;
        private int chapter;
        private int startVerse;
        private int? endVerse;

        public Reference(string book, int chapter, int startVerse)
        {
            this.book = book;
            this.chapter = chapter;
            this.startVerse = startVerse;
        }

        public Reference(string book, int chapter, int startVerse, int endVerse)
        {
            this.book = book;
            this.chapter = chapter;
            this.startVerse = startVerse;
            this.endVerse = endVerse;
        }

        public override string ToString()
        {
            if (endVerse.HasValue)
            {
                return $"{book} {chapter}:{startVerse}-{endVerse}";
            }
            else
            {
                return $"{book} {chapter}:{startVerse}";
            }
        }
    }
}

namespace ScriptureMemorizer
{
    public class Word
    {
        private string text;
        private bool hidden;

        public Word(string text)
        {
            this.text = text;
            hidden = false;
        }

        public void Hide()
        {
            hidden = true;
        }

        public bool IsHidden()
        {
            return hidden;
        }

        public override string ToString()
        {
            if (hidden)
            {
                return new string('_', text.Length);
            }
            else
            {
                return text;
            }
        }
    }
}

namespace ScriptureMemorizer
{
    public class ScriptureLoader
    {
        public static Scripture LoadScripture(string filename)
        {
            // Load a random scripture from the file
            string[] lines = File.ReadAllLines(filename);
            Random random = new Random();
            string line = lines[random.Next(lines.Length)];
            string[] parts = line.Split('|');
            Reference reference = ParseReference(parts[0]);
            string text = parts[1];
            return new Scripture(reference, text);
        }

        private static Reference ParseReference(string referenceString)
        {
            // Parse the reference string into a Reference object
            string[] parts = referenceString.Split(' ');
            string book = parts[0];
            string[] chapterVerse = parts[1].Split(':');
            int chapter = int.Parse(chapterVerse[0]);
            string[] verses = chapterVerse[1].Split('-');
            int startVerse = int.Parse(verses[0]);
            if (verses.Length > 1)
            {
                int endVerse = int.Parse(verses[1]);
                return new Reference(book, chapter, startVerse, endVerse);
            }
            else
            {
                return new Reference(book, chapter, startVerse);
            }
        }
    }
}
