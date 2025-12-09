using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

class Program
{
    static List<Student> Students = new();
    static List<Course> Courses = new();
    static List<Enroll> Enrolls = new();
    static string db = "db.json";

    static void Main()
    {
        Load();
        while (true)
        {
            Console.Clear();
            Console.WriteLine("1 Students\n2 Courses\n3 Enroll\n4 Save & Exit");
            var k = Console.ReadLine();
            if (k == "1") StudentMenu();
            else if (k == "2") CourseMenu();
            else if (k == "3") EnrollMenu();
            else if (k == "4") { Save(); return; }
        }
    }

    static void StudentMenu()
    {
        Console.Clear();
        Console.WriteLine("1 List\n2 Add\n3 Delete\n4 Back");
        var k = Console.ReadLine();

        if (k == "1")
        {
            foreach (var s in Students) Console.WriteLine($"{s.Id} {s.Name}");
            Pause();
        }
        else if (k == "2")
        {
            var s = new Student();
            Console.Write("Name: "); s.Name = Console.ReadLine();
            Students.Add(s);
        }
        else if (k == "3")
        {
            Console.Write("Id: ");
            var id = Console.ReadLine();
            Students.RemoveAll(x => x.Id.ToString() == id);
        }
    }

    static void CourseMenu()
    {
        Console.Clear();
        Console.WriteLine("1 List\n2 Add\n3 Delete\n4 Back");
        var k = Console.ReadLine();

        if (k == "1")
        {
            foreach (var c in Courses) Console.WriteLine($"{c.Id} {c.Title} {c.Price}");
            Pause();
        }
        else if (k == "2")
        {
            var c = new Course();
            Console.Write("Title: "); c.Title = Console.ReadLine();
            Console.Write("Price: "); decimal.TryParse(Console.ReadLine(), out c.Price);
            Courses.Add(c);
        }
        else if (k == "3")
        {
            Console.Write("Id: ");
            var id = Console.ReadLine();
            Courses.RemoveAll(x => x.Id.ToString() == id);
        }
    }

    static void EnrollMenu()
    {
        Console.Clear();
        Console.WriteLine("1 List\n2 Add\n3 Back");
        var k = Console.ReadLine();

        if (k == "1")
        {
            foreach (var e in Enrolls)
            {
                var s = Students.FirstOrDefault(x => x.Id == e.StudentId);
                var c = Courses.FirstOrDefault(x => x.Id == e.CourseId);
                Console.WriteLine($"{e.Id} {s?.Name} -> {c?.Title}");
            }
            Pause();
        }
        else if (k == "2")
        {
            Console.WriteLine("Students:");
            foreach (var s in Students) Console.WriteLine($"{s.Id} {s.Name}");

            Console.Write("Student Id: ");
            var sid = Console.ReadLine();

            Console.WriteLine("Courses:");
            foreach (var c in Courses) Console.WriteLine($"{c.Id} {c.Title}");

            Console.Write("Course Id: ");
            var cid = Console.ReadLine();

            Enrolls.Add(new Enroll
            {
                StudentId = Guid.Parse(sid),
                CourseId = Guid.Parse(cid)
            });
        }
    }

    static void Save()
    {
        var data = new { Students, Courses, Enrolls };
        File.WriteAllText(db, JsonSerializer.Serialize(data));
    }

    static void Load()
    {
        if (!File.Exists(db)) return;
        var json = File.ReadAllText(db);
        var obj = JsonSerializer.Deserialize<Temp>(json);
        Students = obj.Students;
        Courses = obj.Courses;
        Enrolls = obj.Enrolls;
    }

    static void Pause()
    {
        Console.WriteLine("Enter...");
        Console.ReadLine();
    }
}

class Temp
{
    public List<Student> Students { get; set; }
    public List<Course> Courses { get; set; }
    public List<Enroll> Enrolls { get; set; }
}
