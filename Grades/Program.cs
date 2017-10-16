using System;
using System.IO;

namespace Grades
{
    class Program
    {
        static void Main(string[] args)
        {
            GradeTracker gradeBook = CreateGradeBook();

            AddEvents(gradeBook);
            GetBookName(gradeBook);
            AddGrades(gradeBook);
            SaveGrades(gradeBook);
            WriteGrades(gradeBook);
        }

        // Doesn't make sense for this little app but in many cases, we want the flexibility 
        // of having something created for you via a factory class, provider or 
        // some port of infrastructure that's in the app
        private static ThrowAwayGradeBook CreateGradeBook()
        {
            return new ThrowAwayGradeBook();
        }

        private static void WriteGrades(GradeTracker gradeBook)
        {
            GradeStatistics gradeStatistics = gradeBook.ComputeStatistics();
            WriteResult("Highest", gradeStatistics.HighestGrade);
            WriteResult("Lowest", gradeStatistics.LowestGrade);
            WriteResult("Average", gradeStatistics.AverageGrade);
            WriteResult(gradeStatistics.LetterGrade, gradeStatistics.Description);
        }

        private static void SaveGrades(GradeTracker gradeBook)
        {
            // Implicit try...catch...finally block > Close/Dispose the File even if an exception is thrown
            using (StreamWriter outputFile = File.CreateText("grades.txt"))
            {
                gradeBook.WriteGrades(outputFile);
            }
        }

        private static void AddGrades(GradeTracker gradeBook)
        {
            gradeBook.AddGrade(91);
            gradeBook.AddGrade(89.5f);
            gradeBook.AddGrade(75);
        }

        private static void GetBookName(GradeTracker gradeBook)
        {
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
        }

        private static void AddEvents(GradeTracker gradeBook)
        {
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