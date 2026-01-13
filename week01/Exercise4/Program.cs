using System;

class Program
        {
            static void Main(string[] args)
    {
        List<int> numbers = new List<int>();

                Console.WriteLine("Enter a list of numbers, type 0 when finished.");
        while (true)
        {
            Console.Write("Enter number: ");
            int number = Convert.ToInt32(Console.ReadLine());

            if (number == 0)
            {
                break;
            }

            numbers.Add(number);
        }

        int sum = numbers.Sum();
        double average = numbers.Average();
        int max = numbers.Max();
        int smallestPositive = numbers.Where(n => n > 0).Min();
        List<int> sortedNumbers = numbers.OrderBy(n => n).ToList();

        Console.WriteLine($"The sum is: {sum}");
        Console.WriteLine($"The average is: {average}");
        Console.WriteLine($"The largest number is: {max}");
        Console.WriteLine($"The smallest positive number is: {smallestPositive}");
        Console.WriteLine($"The sorted list is: {string.Join(" ", sortedNumbers)}");
    }
}