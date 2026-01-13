using System;


    class GuessMyNumber
{
    static void Main(string[] args)
    {
        bool playAgain = true;

        while (playAgain)
        {
            Random random = new Random();
            int magicNumber = random.Next(1, 101);
            int guess = 0;
            int attempts = 0;

            while (guess != magicNumber)
            {
                Console.Write("What is your guess? ");
                guess = Convert.ToInt32(Console.ReadLine());
                attempts++;

                if (guess < magicNumber)
                {
                    Console.WriteLine("Higher");
                }
                else if (guess > magicNumber)
                {
                    Console.WriteLine("Lower");
                }
                else
                {
                    Console.WriteLine($"You guessed it in {attempts} attempts!");
                }
            }

            Console.Write("Do you want to play again? (yes/no): ");
            string response = Console.ReadLine();
            playAgain = response.ToLower() == "yes";
        }
    }
}

      