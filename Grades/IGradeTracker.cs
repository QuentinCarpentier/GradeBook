using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grades
{
    public interface IGradeTracker
    {
        GradeStatistics ComputeStatistics();
        void AddGrade(float grade);
        void WriteGrades(TextWriter destination);
        string Name { get; set; }
        event NameChangedDelegate NameChanged;
    }
}
