using System;
using System.Collections.Generic;

namespace Problem3
{
    struct StudentRequest
    {
        public string StudentNumber;
        public string StudentName;
        public string RequestType;
    }

    class Program
    {
        static void Main()
        {
            Queue<StudentRequest> requestQueue = new Queue<StudentRequest>();

            while (true)
            {
                Console.WriteLine("========================================");
                Console.WriteLine("           STUDENT REQUEST QUEUE");
                Console.WriteLine("========================================");
                Console.WriteLine();
                Console.WriteLine("1. Add Request");
                Console.WriteLine("2. View Pending Requests");
                Console.WriteLine("3. Process Request");
                Console.WriteLine("4. Exit");
                Console.WriteLine();
                Console.Write("Enter choice: ");
                string choice = Console.ReadLine() ?? "";
                Console.WriteLine();

                switch (choice)
                {
                    case "1": AddRequest(requestQueue); break;
                    case "2": ViewPending(requestQueue); break;
                    case "3": ProcessRequest(requestQueue); break;
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

        static void AddRequest(Queue<StudentRequest> requestQueue)
        {
            StudentRequest r = new StudentRequest();
            r.StudentNumber = ReadText("Enter Student Number: ");
            r.StudentName = ReadText("Enter Student Name: ");
            r.RequestType = ReadText("Enter Request Type: ");

            requestQueue.Enqueue(r);   
            Console.WriteLine();
            Console.WriteLine("Request added successfully!");
        }

        static void ViewPending(Queue<StudentRequest> requestQueue)
        {
            if (requestQueue.Count == 0)
            {
                Console.WriteLine("There are no pending requests.");
                return;
            }

            Console.WriteLine("REQUEST QUEUE");
            int number = 1;

            foreach (StudentRequest r in requestQueue)
            {
                Console.WriteLine(number + ". " + r.StudentName + " - " + r.RequestType);
                number++;
            }
        }

        static void ProcessRequest(Queue<StudentRequest> requestQueue)
        {
            if (requestQueue.Count == 0)
            {
                Console.WriteLine("There are no pending requests to process.");
                return;
            }

            StudentRequest r = requestQueue.Dequeue();  
            Console.WriteLine("Processing Request: " + r.StudentName + " - " + r.RequestType);
            Console.WriteLine();
            Console.WriteLine("Request processed successfully!");
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
    }
}
