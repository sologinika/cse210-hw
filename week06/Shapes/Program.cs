using System;
using System.Collections.Generic;

public abstract class Shape
{
    public string Color { get; set; }

    public Shape(string color)
    {
        Color = color;
    }

    public abstract double GetArea();
}

public class Square : Shape
{
    private double _side;

    public Square(string color, double side) : base(color)
    {
        _side = side;
    }

    public override double GetArea()
    {
        return _side * _side;
    }
}

public class Rectangle : Shape
{
    private double _width;
    private double _height;

    public Rectangle(string color, double width, double height) : base(color)
    {
        _width = width;
        _height = height;
    }

    public override double GetArea()
    {
        return _width * _height;
    }
}

public class Circle : Shape   // ✅ made public
{
    private double _radius;

    public Circle(string color, double radius) : base(color)
    {
        _radius = radius;
    }

    public override double GetArea()
    {
        return Math.PI * _radius * _radius;
    }
}

public class Triangle : Shape
{
    private double _base;    // ✅ added
    private double _height;  // ✅ added

    public Triangle(string color, double baseLength, double height) : base(color)
    {
        _base = baseLength;
        _height = height;
    }

    public override double GetArea()
    {
        return 0.5 * _base * _height;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Square square = new Square("Red", 4);
        Console.WriteLine($"Square Color: {square.Color}, Area: {square.GetArea()}");

        Rectangle rectangle = new Rectangle("Blue", 3, 5);
        Console.WriteLine($"Rectangle Color: {rectangle.Color}, Area: {rectangle.GetArea()}");

        Circle circle = new Circle("Green", 2);
        Console.WriteLine($"Circle Color: {circle.Color}, Area: {circle.GetArea()}");

        Triangle triangle = new Triangle("Pink", 4, 6);
        Console.WriteLine($"Triangle Color: {triangle.Color}, Area: {triangle.GetArea()}");

        List<Shape> shapes = new List<Shape>();
        shapes.Add(new Square("Yellow", 3));
        shapes.Add(new Rectangle("Purple", 2, 6));
        shapes.Add(new Circle("Orange", 1));
        shapes.Add(new Triangle("Brown", 5, 2));

        foreach (Shape shape in shapes)
        {
            Console.WriteLine($"Shape Color: {shape.Color}, Area: {shape.GetArea()}");
        }
    }
}
