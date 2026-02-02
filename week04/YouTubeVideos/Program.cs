using System;
using System.Collections.Generic;

// Comment class
public class Comment
{
    private string name;
    private string text;

    public Comment(string name, string text)
    {
        this.name = name;
        this.text = text;
    }

    public string GetName()
    {
        return name;
    }

    public string GetText()
    {
        return text;
    }
}

// Video class
public class Video
{
    private string title;
    private string author;
    private int length; // in seconds
    private List<Comment> comments;

    public Video(string title, string author, int length)
    {
        this.title = title;
        this.author = author;
        this.length = length;
        this.comments = new List<Comment>();
    }

    public void AddComment(Comment comment)
    {
        comments.Add(comment);
    }

    public int GetCommentCount()
    {
        return comments.Count;
    }

    public string GetTitle()
    {
        return title;
    }

    public string GetAuthor()
    {
        return author;
    }

    public int GetLength()
    {
        return length;
    }

    public List<Comment> GetComments()
    {
        return comments;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Create videos
        Video video1 = new Video("Video 1", "Author 1", 360);
        video1.AddComment(new Comment("John Doe", "Great video!"));
        video1.AddComment(new Comment("Jane Smith", "Love it!"));
        video1.AddComment(new Comment("Bob Johnson", "Good stuff!"));

        Video video2 = new Video("Video 2", "Author 2", 420);
        video2.AddComment(new Comment("Alice Brown", "Awesome!"));
        video2.AddComment(new Comment("Mike Davis", "Nice work!"));
        video2.AddComment(new Comment("Emily Chen", "Good job!"));

        Video video3 = new Video("Video 3", "Author 3", 300);
        video3.AddComment(new Comment("David Lee", "Excellent!"));
        video3.AddComment(new Comment("Sarah Kim", "Well done!"));
        video3.AddComment(new Comment("Kevin White", "Good video!"));

        // Add videos to list
        List<Video> videos = new List<Video> { video1, video2, video3 };

        // Display video information
        foreach (var video in videos)
        {
            Console.WriteLine($"Title: {video.GetTitle()}");
            Console.WriteLine($"Author: {video.GetAuthor()}");
            Console.WriteLine($"Length: {video.GetLength()} seconds");
            Console.WriteLine($"Comments: {video.GetCommentCount()}");
            Console.WriteLine("Comments:");
            foreach (var comment in video.GetComments())
            {
                Console.WriteLine($"  {comment.GetName()}: {comment.GetText()}");
            }
            Console.WriteLine();
        }
    }
}