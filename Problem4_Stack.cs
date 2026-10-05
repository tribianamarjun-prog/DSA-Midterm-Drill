using System;
using System.Collections.Generic;

namespace Problem4
{
    struct Student
    {
        public string StudentNumber;
        public string Name;
        public string Program;
        public int YearLevel;
    }

    struct Operation
    {
        public string Action;      
        public string StudentName;

        public override string ToString()
        {
            return Action + " " + StudentName;
        }
    }

    class Program
    {
        const int MaxStudents = 10;

        static void Main()
        {
            Student[] students = new Student[MaxStudents];
            int studentCount = 0;
            Stack<Operation> operationHistory = new Stack<Operation>();

            while (true)
            {
                Console.WriteLine("========================================");
                Console.WriteLine("   STUDENT RECORDS WITH OPERATION HISTORY");
                Console.WriteLine("========================================");
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. Display All Students");
                Console.WriteLine("3. Search Student");
                Console.WriteLine("4. Update Student");
                Console.WriteLine("5. Delete Student");
                Console.WriteLine("6. Exit");
                Console.WriteLine("7. View Operation History");
                Console.WriteLine("8. View Last Operation");
                Console.WriteLine("9. Remove Last Operation");
                Console.WriteLine();
                Console.Write("Enter choice: ");
                string choice = Console.ReadLine() ?? "";
                Console.WriteLine();

                switch (choice)
                {
                    case "1": AddStudent(students, ref studentCount, operationHistory); break;
                    case "2": DisplayAll(students, studentCount); break;
                    case "3": SearchStudent(students, studentCount); break;
                    case "4": UpdateStudent(students, studentCount, operationHistory); break;
                    case "5": DeleteStudent(students, ref studentCount, operationHistory); break;
                    case "6":
                        Console.WriteLine("Program exited.");
                        return;
                    case "7": ViewHistory(operationHistory); break;
                    case "8": ViewLastOperation(operationHistory); break;
                    case "9": RemoveLastOperation(operationHistory); break;
                    default:
                        Console.WriteLine("Invalid choice. Please enter 1-9.");
                        break;
                }
                Console.WriteLine();
            }
        }

        static void RecordOperation(Stack<Operation> history, string action, string studentName)
        {
            Operation op = new Operation();
            op.Action = action;
            op.StudentName = studentName;
            history.Push(op);  
        }

        static void ViewHistory(Stack<Operation> history)
        {
            if (history.Count == 0)
            {
                Console.WriteLine("No operations recorded.");
                return;
            }

            Console.WriteLine("OPERATION HISTORY (most recent first)");
            int number = 1;
            
            foreach (Operation op in history)
            {
                Console.WriteLine(number + ". " + op);
                number++;
            }
        }

        static void ViewLastOperation(Stack<Operation> history)
        {
            if (history.Count == 0)
            {
                Console.WriteLine("No operations recorded.");
                return;
            }

            Console.WriteLine("Most recent operation: " + history.Peek());   // Peek = look, don't remove
        }

        static void RemoveLastOperation(Stack<Operation> history)
        {
            if (history.Count == 0)
            {
                Console.WriteLine("No operations recorded.");
                return;
            }

            Operation removed = history.Pop();   
            Console.WriteLine("Removed operation: " + removed);
        }


        static void AddStudent(Student[] students, ref int count, Stack<Operation> history)
        {
            if (count >= MaxStudents)
            {
                Console.WriteLine("Cannot add student. The record is full (maximum of 10 students).");
                return;
            }

            Student s = new Student();
            s.StudentNumber = ReadText("Enter Student Number: ");

            if (FindIndex(students, count, s.StudentNumber) != -1)
            {
                Console.WriteLine("Student Number already exists.");
                return;
            }

            s.Name = ReadText("Enter Name: ");
            s.Program = ReadText("Enter Program: ");
            s.YearLevel = ReadYearLevel("Enter Year Level: ");

            students[count] = s;
            count++;

            RecordOperation(history, "Added", s.Name);
            Console.WriteLine();
            Console.WriteLine("Student added successfully!");
        }

        static void DisplayAll(Student[] students, int count)
        {
            if (count == 0)
            {
                Console.WriteLine("No student records found.");
                return;
            }

            Console.WriteLine("========================================");
            Console.WriteLine("            STUDENT RECORDS");
            Console.WriteLine("========================================");
            for (int i = 0; i < count; i++)
            {
                PrintStudent(students[i]);
                if (i < count - 1) Console.WriteLine();
            }
        }

        static void SearchStudent(Student[] students, int count)
        {
            string number = ReadText("Enter Student Number to search: ");
            int index = FindIndex(students, count, number);

            Console.WriteLine();
            if (index == -1)
            {
                Console.WriteLine("Student not found.");
                return;
            }

            Console.WriteLine("Student Found!");
            PrintStudent(students[index]);
        }

        static void UpdateStudent(Student[] students, int count, Stack<Operation> history)
        {
            string number = ReadText("Enter Student Number to update: ");
            int index = FindIndex(students, count, number);

            if (index == -1)
            {
                Console.WriteLine("Student not found.");
                return;
            }

            Console.WriteLine("Enter the new details (Student Number stays the same).");
            Student s = students[index];
            s.Name = ReadText("Enter New Name: ");
            s.Program = ReadText("Enter New Program: ");
            s.YearLevel = ReadYearLevel("Enter New Year Level: ");
            students[index] = s;

            RecordOperation(history, "Updated", s.Name);
            Console.WriteLine();
            Console.WriteLine("Student updated successfully!");
        }

        static void DeleteStudent(Student[] students, ref int count, Stack<Operation> history)
        {
            string number = ReadText("Enter Student Number to delete: ");
            int index = FindIndex(students, count, number);

            if (index == -1)
            {
                Console.WriteLine("Student not found.");
                return;
            }

            string deletedName = students[index].Name;

            for (int i = index; i < count - 1; i++)
                students[i] = students[i + 1];

            students[count - 1] = new Student();
            count--;

            RecordOperation(history, "Deleted", deletedName);
            Console.WriteLine();
            Console.WriteLine("Student deleted successfully!");
        }


        static int FindIndex(Student[] students, int count, string number)
        {
            for (int i = 0; i < count; i++)
                if (students[i].StudentNumber == number)
                    return i;
            return -1;
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
