using System;

class Program

{
    static void Main(string[] args)
    {
        Console.Write("What is your grade percentage: ");
        string answer = Console.ReadLine();
        int percent = int.Parse(answer);

        string letter = "";
        if (percent >= 90)
        {
            letter = "A";
        }
        else if (percent >= 80)
        {
            letter = "B";
        }
        else if (percent >= 70)
        {
            letter = "C";
        }
        else if (percent >= 60)
        {
            letter = "D";
        }
        else
        {
            letter = "F";
        }

        string sign = "";
        if (letter != "F")
        {
            int lastDigit = percent % 10;
            if (lastDigit >= 7)
            {
                sign = "+";
            }
            else if (lastDigit < 3)
            {
                sign = "-";
            }
        }

        // Handle exceptional cases
        if (letter == "A" && sign == "+")
        {
            sign = ""; // No A+
        }
        if (letter == "F")
        {
            sign = ""; // No F+ or F-
        }

        Console.WriteLine($"Your letter grade is: {letter}{sign}");

        if (percent >= 70)
        {
            Console.WriteLine("Congratulations, you passed the course!");
        }
        else
        {
            Console.WriteLine("Don't worry, try again next time!");
        }
    }
}