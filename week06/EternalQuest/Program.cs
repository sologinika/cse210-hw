using System;
using System.Collections.Generic;
using System.IO;

/*
CREATIVITY / EXCEEDING REQUIREMENTS:
-----------------------------------
1. Added LEVELING SYSTEM:
   - User gains XP with points
   - Levels increase every 1000 points
2. STREAK BONUS SYSTEM:
   - Completing goals consecutively gives bonus points
3. BONUS MULTIPLIER:
   - Higher level gives higher score multiplier
4. GAMIFICATION:
   - Rewards scaling with progress
   - Motivation system

This exceeds the base Eternal Quest requirements.
*/

#region Base Class
public abstract class Goal
{
    private string _name;
    private string _description;
    private int _points;

    public string Name => _name;
    public string Description => _description;
    public int Points => _points;

    protected Goal(string name, string description, int points)
    {
        _name = name;
        _description = description;
        _points = points;
    }

    public abstract int RecordEvent();
    public abstract bool IsComplete();
    public abstract string GetStatus();
    public abstract string GetSaveData();
}
#endregion

#region Simple Goal
public class SimpleGoal : Goal
{
    private bool _isComplete;

    public SimpleGoal(string name, string description, int points, bool isComplete = false)
        : base(name, description, points)
    {
        _isComplete = isComplete;
    }

    public override int RecordEvent()
    {
        if (!_isComplete)
        {
            _isComplete = true;
            return Points;
        }
        return 0;
    }

    public override bool IsComplete() => _isComplete;

    public override string GetStatus()
    {
        return $"{(_isComplete ? "[X]" : "[ ]")} {Name} - {Description}";
    }

    public override string GetSaveData()
    {
        return $"Simple|{Name}|{Description}|{Points}|{_isComplete}";
    }
}
#endregion

#region Eternal Goal
public class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points)
        : base(name, description, points) { }

    public override int RecordEvent()
    {
        return Points;
    }

    public override bool IsComplete() => false;

    public override string GetStatus()
    {
        return $"[∞] {Name} - {Description}";
    }

    public override string GetSaveData()
    {
        return $"Eternal|{Name}|{Description}|{Points}";
    }
}
#endregion

#region Checklist Goal
public class ChecklistGoal : Goal
{
    private int _currentCount;
    private int _targetCount;
    private int _bonus;

    public ChecklistGoal(string name, string description, int points, int target, int bonus, int current = 0)
        : base(name, description, points)
    {
        _targetCount = target;
        _bonus = bonus;
        _currentCount = current;
    }

    public override int RecordEvent()
    {
        if (_currentCount < _targetCount)
        {
            _currentCount++;
            if (_currentCount == _targetCount)
                return Points + _bonus;
            return Points;
        }
        return 0;
    }

    public override bool IsComplete() => _currentCount >= _targetCount;

    public override string GetStatus()
    {
        return $"{(IsComplete() ? "[X]" : "[ ]")} {Name} - {Description} (Completed {_currentCount}/{_targetCount})";
    }

    public override string GetSaveData()
    {
        return $"Checklist|{Name}|{Description}|{Points}|{_targetCount}|{_bonus}|{_currentCount}";
    }
}
#endregion

#region Goal Manager
public class GoalManager
{
    private List<Goal> _goals = new List<Goal>();
    private int _score = 0;
    private int _level = 1;
    private int _streak = 0;

    public void Start()
    {
        while (true)
        {
            Console.Clear();
            DisplayStats();
            Console.WriteLine("\nEternal Quest Menu:");
            Console.WriteLine("1. Create New Goal");
            Console.WriteLine("2. List Goals");
            Console.WriteLine("3. Record Event");
            Console.WriteLine("4. Save");
            Console.WriteLine("5. Load");
            Console.WriteLine("6. Exit");
            Console.Write("Choose: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": CreateGoal(); break;
                case "2": ListGoals(); break;
                case "3": RecordEvent(); break;
                case "4": Save(); break;
                case "5": Load(); break;
                case "6": return;
            }
        }
    }

    private void DisplayStats()
    {
        Console.WriteLine($"Score: {_score} | Level: {_level} | Streak: {_streak}");
    }

    private void CreateGoal()
    {
        Console.WriteLine("\n1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");
        Console.Write("Select type: ");
        string type = Console.ReadLine();

        Console.Write("Name: ");
        string name = Console.ReadLine();
        Console.Write("Description: ");
        string desc = Console.ReadLine();
        Console.Write("Points: ");
        int points = int.Parse(Console.ReadLine());

        if (type == "1")
            _goals.Add(new SimpleGoal(name, desc, points));
        else if (type == "2")
            _goals.Add(new EternalGoal(name, desc, points));
        else if (type == "3")
        {
            Console.Write("Target Count: ");
            int target = int.Parse(Console.ReadLine());
            Console.Write("Bonus: ");
            int bonus = int.Parse(Console.ReadLine());
            _goals.Add(new ChecklistGoal(name, desc, points, target, bonus));
        }
    }

    private void ListGoals()
    {
        Console.WriteLine("\nGoals:");
        for (int i = 0; i < _goals.Count; i++)
            Console.WriteLine($"{i + 1}. {_goals[i].GetStatus()}");

        Console.ReadLine();
    }

    private void RecordEvent()
    {
        ListGoals();
        Console.Write("Select goal number: ");
        int index = int.Parse(Console.ReadLine()) - 1;

        int earned = _goals[index].RecordEvent();

        if (earned > 0)
        {
            _streak++;
            int multiplier = 1 + (_level / 5);
            int streakBonus = _streak * 10;
            int total = (earned * multiplier) + streakBonus;

            _score += total;
            LevelCheck();

            Console.WriteLine($"Earned {total} points! (Base {earned}, Multiplier x{multiplier}, Streak Bonus {streakBonus})");
        }
        else
        {
            _streak = 0;
            Console.WriteLine("No points earned.");
        }

        Console.ReadLine();
    }

    private void LevelCheck()
    {
        _level = (_score / 1000) + 1;
    }

    private void Save()
    {
        using (StreamWriter sw = new StreamWriter("goals.txt"))
        {
            sw.WriteLine($"{_score}|{_level}|{_streak}");
            foreach (var g in _goals)
                sw.WriteLine(g.GetSaveData());
        }
    }

    private void Load()
    {
        if (!File.Exists("goals.txt")) return;

        string[] lines = File.ReadAllLines("goals.txt");
        _goals.Clear();

        string[] stats = lines[0].Split('|');
        _score = int.Parse(stats[0]);
        _level = int.Parse(stats[1]);
        _streak = int.Parse(stats[2]);

        for (int i = 1; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split('|');
            if (parts[0] == "Simple")
                _goals.Add(new SimpleGoal(parts[1], parts[2], int.Parse(parts[3]), bool.Parse(parts[4])));
            else if (parts[0] == "Eternal")
                _goals.Add(new EternalGoal(parts[1], parts[2], int.Parse(parts[3])));
            else if (parts[0] == "Checklist")
                _goals.Add(new ChecklistGoal(parts[1], parts[2], int.Parse(parts[3]),
                    int.Parse(parts[4]), int.Parse(parts[5]), int.Parse(parts[6])));
        }
    }
}
#endregion

#region Program
class Program
{
    static void Main(string[] args)
    {
        GoalManager manager = new GoalManager();
        manager.Start();
    }
}
#endregion
