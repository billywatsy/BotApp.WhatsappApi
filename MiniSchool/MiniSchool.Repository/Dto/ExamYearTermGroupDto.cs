using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSchool.Repository.Dto
{
    public class ExamYearTermGroupDto
    {
        public int Year { get; set; }
        public int CountSubjects { get; set; }
        public string TermNumber { get; set; }
    }
}
