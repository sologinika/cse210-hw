using System;
using System;
using System.Collections.Generic;

namespace FitnessTracker
{
    // =========================
    // Base Class
    // =========================
    public abstract class Activity
    {
        private DateTime _date;
        private int _minutes;

        protected Activity(DateTime date, int minutes)
        {
            _date = date;
            _minutes = minutes;
        }

        public int GetMinutes()
        {
            return _minutes;
        }

        public DateTime GetDate()
        {
            return _date;
        }

        // Abstract methods (declared, not implemented)
        public abstract double GetDistance(); // km
        public abstract double GetSpeed();    // kph
        public abstract double GetPace();     // min per km

        // Summary method (shared by all activities)
        public virtual string GetSummary()
        {
            return $"{_date:dd MMM yyyy} {GetType().Name} ({_minutes} min) - " +
                   $"Distance: {GetDistance():0.00} km, " +
                   $"Speed: {GetSpeed():0.00} kph, " +
                   $"Pace: {GetPace():0.00} min per km";
        }
    }

    // =========================
    // Running Class
    // =========================
    public class Running : Activity
    {
        private double _distance; // km

        public Running(DateTime date, int minutes, double distance)
            : base(date, minutes)
        {
            _distance = distance;
        }

        public override double GetDistance()
        {
            return _distance;
        }

        public override double GetSpeed()
        {
            return (GetDistance() / GetMinutes()) * 60;
        }

        public override double GetPace()
        {
            return GetMinutes() / GetDistance();
        }
    }

    // =========================
    // Cycling Class
    // =========================
    public class Cycling : Activity
    {
        private double _speed; // kph

        public Cycling(DateTime date, int minutes, double speed)
            : base(date, minutes)
        {
            _speed = speed;
        }

        public override double GetDistance()
        {
            return (_speed * GetMinutes()) / 60;
        }

        public override double GetSpeed()
        {
            return _speed;
        }

        public override double GetPace()
        {
            return 60 / _speed;
        }
    }

    // =========================
    // Swimming Class
    // =========================
    public class Swimming : Activity
    {
        private int _laps;

        public Swimming(DateTime date, int minutes, int laps)
            : base(date, minutes)
        {
            _laps = laps;
        }

        public override double GetDistance()
        {
            // Distance (km) = laps * 50 / 1000
            return (_laps * 50) / 1000.0;
        }

        public override double GetSpeed()
        {
            return (GetDistance() / GetMinutes()) * 60;
        }

        public override double GetPace()
        {
            return GetMinutes() / GetDistance();
        }
    }

    // =========================
    // Program.cs
    // =========================
    class Program
    {
        static void Main(string[] args)
        {
            List<Activity> activities = new List<Activity>();

            // Create activities
            activities.Add(new Running(new DateTime(2022, 11, 3), 30, 4.8));   // 4.8 km run
            activities.Add(new Cycling(new DateTime(2022, 11, 4), 45, 20.0));  // 20 kph cycling
            activities.Add(new Swimming(new DateTime(2022, 11, 5), 40, 30));   // 30 laps swimming

            // Display summaries
            foreach (Activity activity in activities)
            {
                Console.WriteLine(activity.GetSummary());
            }

            Console.ReadLine();
        }
    }
}
