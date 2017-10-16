using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grades
{
    public class ThrowAwayGradeBook : GradeBook
    {
        public override GradeStatistics ComputeStatistics()
        {
            float lowest = float.MaxValue;

            foreach (var grade in grades)
            {
                lowest = Math.Min(lowest, grade);
            }
            grades.Remove(lowest);

            // Return the base method from the base class
            return base.ComputeStatistics();
        }
    }
}
