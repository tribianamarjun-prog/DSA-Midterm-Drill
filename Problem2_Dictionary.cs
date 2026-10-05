using System;
using System.Collections.Generic;

namespace Problem2
{
    struct Student
    {
        public string StudentNumber;
        public string Name;
        public string Program;
        public int YearLevel;
    }

    class Program
    {
        static void Main()
        {
            Dictionary<string, Student> studentDictionary = new Dictionary<string, Student>();

            while (true)
            {
                Console.WriteLine("========================================");
                Console.WriteLine("        STUDENT LOOKUP USING DICTIONARY");
                Console.WriteLine("========================================");
                Console.WriteLine();
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. Search Student");
                Console.WriteLine("3. Display All Students");
                Console.WriteLine("4. Exit");
                Console.WriteLine();
                Console.Write("Enter choice: ");
                string choice = Console.ReadLine() ?? "";
                Console.WriteLine();

                switch (choice)
                {
                    case "1": AddStudent(studentDictionary); break;
                    case "2": SearchStudent(studentDictionary); break;
                    case "3": DisplayAll(studentDictionary); break;
                    case "4":
                        Console.WriteLine("Program exited.");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please enter 1-4.");
                        break;
                }
                Console.WriteLine();
            }
        }

        static void AddStudent(Dictionary<string, Student> studentDictionary)
        {
            Student s = new Student();
            s.StudentNumber = ReadText("Enter Student Number: ");

            if (studentDictionary.ContainsKey(s.StudentNumber))
            {
                Console.WriteLine("Student Number already exists. Duplicate not allowed.");
                return;
            }

            s.Name = ReadText("Enter Name: ");
            s.Program = ReadText("Enter Program: ");
            s.YearLevel = ReadYearLevel("Enter Year Level: ");

            studentDictionary.Add(s.StudentNumber, s);
            Console.WriteLine();
            Console.WriteLine("Student added successfully!");
        }

        static void SearchStudent(Dictionary<string, Student> studentDictionary)
        {
            string number = ReadText("Enter Student Number to search: ");
            Console.WriteLine();

            if (studentDictionary.TryGetValue(number, out Student s))
            {
                Console.WriteLine("Student Found!");
                PrintStudent(s);
            }
            else
            {
                Console.WriteLine("Student not found.");
            }
        }

        static void DisplayAll(Dictionary<string, Student> studentDictionary)
        {
            if (studentDictionary.Count == 0)
            {
                Console.WriteLine("No student records found.");
                return;
            }

            Console.WriteLine("========================================");
            Console.WriteLine("            STUDENT RECORDS");
            Console.WriteLine("========================================");

            int i = 0;
            foreach (KeyValuePair<string, Student> pair in studentDictionary)
            {
                PrintStudent(pair.Value);
                i++;
                if (i < studentDictionary.Count) Console.WriteLine();
            }
        }


        static void PrintStudent(Student s)
        {
            Console.WriteLine("Student Number: " + s.StudentNumber);
            Console.WriteLine("Name: " + s.Name);
            Console.WriteLine("Program: " + s.Program);
            Console.WriteLine("Year Level: " + s.YearLevel);
        }

        static string ReadText(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine() ?? "";
                if (!string.IsNullOrWhiteSpace(input))
                    return input.Trim();
                Console.WriteLine("Input cannot be empty.");
            }
        }

        static int ReadYearLevel(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int year) && year >= 1 && year <= 4)
                    return year;
                Console.WriteLine("Year Level must be 1, 2, 3, or 4.");
            }
        }
    }
}
