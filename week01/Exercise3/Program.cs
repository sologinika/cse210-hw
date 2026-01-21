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
             int guess = 8;
            int attempts = 0;

            while (guess != magicNumber)
            {
                Console.Write("What is your guess? ");
                guess = Convert.ToInt32(Console.ReadLine());
                attempts++;

                if (magicNumber < guess)
                {
                    Console.WriteLine("lower");
                }
                else if (magicNumber > guess)
                {
                    Console.WriteLine("Higher");
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

      