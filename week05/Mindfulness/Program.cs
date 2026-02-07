using System;
    using System;
using System.Threading;

public abstract class Activity
{
    protected string name;
    protected string description;
    protected int duration;

    public Activity(string name, string description)
    {
        this.name = name;
        this.description = description;
    }

    public void Start()
    {
        Console.WriteLine($"Welcome to the {name} activity!");
        Console.WriteLine(description);
        Console.Write("Enter the duration of the activity in seconds: ");
        duration = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Get ready to begin...");
        Pause(3);
        PerformActivity();
        End();
    }

    protected abstract void PerformActivity();

    protected void Pause(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write(i + " ");
            Thread.Sleep(1000);
            Console.Write("\b \b");
        }
    }

    protected void Spinner(int seconds)
    {
        string[] spinner = { "|", "/", "-", "\\" };
        int index = 0;
        DateTime startTime = DateTime.Now;
        DateTime endTime = startTime.AddSeconds(seconds);
        while (DateTime.Now < endTime)
        {
            Console.Write(spinner[index]);
            Thread.Sleep(100);
            Console.Write("\b \b");
            index = (index + 1) % spinner.Length;
        }
    }

    private void End()
    {
        Console.WriteLine("Great job!");
        Pause(2);
        Console.WriteLine($"You have completed the {name} activity for {duration} seconds.");
        Pause(3);
    }
}

public class BreathingActivity : Activity
{
    public BreathingActivity() : base("Breathing", "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.") { }

    protected override void PerformActivity()
    {
        DateTime startTime = DateTime.Now;
        DateTime endTime = startTime.AddSeconds(duration);
        while (DateTime.Now < endTime)
        {
            Console.WriteLine("Breathe in...");
            Pause(4);
            Console.WriteLine("Breathe out...");
            Pause(4);
        }
    }
}

public class ReflectionActivity : Activity
{
    private string[] prompts = { "Think of a time when you stood up for someone else.", "Think of a time when you did something really difficult.", "Think of a time when you helped someone in need.", "Think of a time when you did something truly selfless." };
    private string[] questions = { "Why was this experience meaningful to you?", "Have you ever done anything like this before?", "How did you get started?", "How did you feel when it was complete?", "What made this time different than other times when you were not as successful?", "What is your favorite thing about this experience?", "What could you learn from this experience that applies to other situations?", "What did you learn about yourself through this experience?", "How can you keep this experience in mind in the future?" };

    public ReflectionActivity() : base("Reflection", "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.") { }

    protected override void PerformActivity()
    {
        Random random = new Random();
        string prompt = prompts[random.Next(prompts.Length)];
        Console.WriteLine(prompt);
        DateTime startTime = DateTime.Now;
        DateTime endTime = startTime.AddSeconds(duration);
        while (DateTime.Now < endTime)
        {
            string question = questions[random.Next(questions.Length)];
            Console.WriteLine(question);
            Spinner(10);
        }
    }
}

public class ListingActivity : Activity
{
    private string[] prompts = { "Who are people that you appreciate?", "What are personal strengths of yours?", "Who are people that you have helped this week?", "When have you felt the Holy Ghost this month?", "Who are some of your personal heroes?" };

    public ListingActivity() : base("Listing", "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.") { }

    protected override void PerformActivity()
    {
        Random random = new Random();
        string prompt = prompts[random.Next(prompts.Length)];
        Console.WriteLine(prompt);
        Pause(5);
        Console.WriteLine("Start listing...");
        DateTime startTime = DateTime.Now;
        DateTime endTime = startTime.AddSeconds(duration);
        int count = 0;
        while (DateTime.Now < endTime)
        {
            string item = Console.ReadLine();
            if (!string.IsNullOrEmpty(item))
            {
                count++;
            }
        }
        Console.WriteLine($"You listed {count} items.");
    }
}

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Mindfulness Program");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Quit");
            Console.Write("Choose an activity: ");
            int choice = Convert.ToInt32(Console.ReadLine());
            Activity activity = null;
            switch (choice)
            {
                case 1:
                    activity = new BreathingActivity();
                    break;
                case 2:
                    activity = new ReflectionActivity();
                    break;
                case 3:
                    activity = new ListingActivity();
                    break;
                case 4:
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    continue;
            }
            activity.Start();
        }
    }
}


