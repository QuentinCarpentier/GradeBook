using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Grades
{
    public class GradeBook
    {
        public GradeBook()
        {
            //_name = "Empty";
            grades = new List<float>();
        }

        // GradeStatistics object holds all the logic > encapsulation
        public virtual GradeStatistics ComputeStatistics()
        {
            GradeStatistics stats = new GradeStatistics();
            float sum = 0;

            foreach (float grade in grades)
            {
                stats.HighestGrade = Math.Max(grade, stats.HighestGrade);
                stats.LowestGrade = Math.Min(grade, stats.LowestGrade);
                sum += grade;
            }
            stats.AverageGrade = sum / grades.Count;

            return stats;
        }

        public void AddGrade(float grade)
        {
            grades.Add(grade);
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                // Throw an exception when the name is null
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Name cannot be null or empty.");
                }

                if (_name != value && NameChanged != null)
                {
                    NameChangedEventArgs args = new NameChangedEventArgs();
                    args.ExistingName = _name;
                    args.NewName = value;

                    NameChanged(this, args);
                }

                _name = value;
            }
        }

        public void WriteGrades(TextWriter destination)
        {
            for (int i = 0; i < grades.Count; i++)
            {
                destination.WriteLine(grades[i]);
            }
        }

        // Declare Delegate
        //public NameChangedDelegate NameChanged;

        // Declare Event from a Delegate
        public event NameChangedDelegate NameChanged;

        // Set to protected since we need this list when using inheritance
        protected List<float> grades;

        // Explicit access modifier set to private
        private string _name;
    }
}
