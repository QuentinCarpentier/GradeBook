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
            gradeBook.NameChanged += OnNameChanged2;

            // With delegate, we can assign what we want 
            //gradeBook.NameChanged = null;
            // With events, we can assign something only with += or -= > add subscribers
            //gradeBook.NameChanged += null;

            // Handling exeption with try...catch statement
            try
            {
                Console.WriteLine("Enter the name of the Grade Book:");
                gradeBook.Name = Console.ReadLine();
                //gradeBook.Name = "Quentin's Grade Book";
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                //Console.WriteLine(ex.StackTrace);
            }
            
            gradeBook.AddGrade(91);
            gradeBook.AddGrade(89.5f);
            gradeBook.AddGrade(75);
            gradeBook.WriteGrades(Console.Out);

            GradeStatistics gradeStatistics = gradeBook.ComputeStatistics();
            WriteResult("Highest", gradeStatistics.HighestGrade);
            WriteResult("Lowest", gradeStatistics.LowestGrade);
            WriteResult("Average", gradeStatistics.AverageGrade);
            WriteResult(gradeStatistics.LetterGrade, gradeStatistics.Description);
        }

        static void OnNameChanged(object sender, NameChangedEventArgs args)
        {
            if(args.ExistingName != null)
            {
                Console.WriteLine($"Grade book changing name from {args.ExistingName} to {args.NewName}");
            }
            else
            {
                Console.WriteLine($"Grade book's name is now {args.NewName}");
            }
        }

        static void OnNameChanged2(object sender, NameChangedEventArgs args)
        {
            Console.WriteLine("****");
        }

        static void WriteResult(string description, float result)
        {
            Console.WriteLine(description + " grade is " + result);
        }

        static void WriteResult(string description, string result)
        {
            Console.WriteLine(description + ": " + result);
        }
    }
}