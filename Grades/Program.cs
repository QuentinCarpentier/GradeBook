using System;

namespace Grades
{
    class Program
    {
        static void Main(string[] args)
        {
            GradeBook gradeBook = new GradeBook();

            // Calling Delegate when the name changed
            //gradeBook.NameChanged = new NameChangedDelegate(OnNameChanged);

            // Multicast delegate > call multiple methods when the name is changed
            // Events are used, so we say adding subscribers
            //gradeBook.NameChanged += new NameChangedDelegate(OnNameChanged);
            //gradeBook.NameChanged += new NameChangedDelegate(OnNameChanged2);

            // Good thing: Compiler is smart enough to understand we want to instanciate
            // the NameChangedDelegate implicitly
            gradeBook.NameChanged += OnNameChanged;

            // With delegate, we can assign what we want 
            //gradeBook.NameChanged = null;
            // With events, we can assign something only with += or -= > add subscribers
            //gradeBook.NameChanged += null;

            gradeBook.Name = "Quentin's Grade Book";
            gradeBook.Name = "Grade book";

            gradeBook.AddGrade(91);
            gradeBook.AddGrade(89.5f);
            gradeBook.AddGrade(75);

            GradeStatistics gradeStatistics = gradeBook.ComputeStatistics();

            Console.WriteLine(gradeBook.Name);
            WriteResult("Average", gradeStatistics.AverageGrade);
            WriteResult("Highest", gradeStatistics.HighestGrade);
            WriteResult("Lowest", gradeStatistics.LowestGrade);
        }

        static void OnNameChanged(object sender, NameChangedEventArgs args)
        {
            Console.WriteLine($"Grade book changing name from {args.ExistingName} to {args.NewName}");
        }

        //static void OnNameChanged2(string existingName, string newName)
        //{
        //    Console.WriteLine("****");
        //}

        static void WriteResult(string description, float result)
        {
            Console.WriteLine(description + " grade is " + result);
        }
    }
}