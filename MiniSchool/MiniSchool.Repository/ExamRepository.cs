using MiniSchool.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MiniSchool.Repository
{
    public class ExamRepository
    {
        public static List<Dto.ExamYearTermGroupDto> GetExamYearTermGroupByStudentId(int studentId)
        {
            using (AppContext appContext = new AppContext())
            {
                var studentds = (
                                      from e in appContext.Exams
                                      join s in appContext.Students on e.StudentId equals s.StudentId
                                      where s.StudentId == studentId
                                      group e by new { Year = e.Year, TermNumber = e.TermNumber } into g
                                      select new Dto.ExamYearTermGroupDto
                                      {
                                          Year = g.Key.Year,
                                          TermNumber = g.Key.TermNumber,
                                          CountSubjects = g.Count()
                                      }
                                  ).ToList();
                return studentds;
            }
        }

        public static List<Exam> GetExamByYearTermStudentId(int studentId , string termYear)
        {
            using (AppContext appContext = new AppContext())
            {
                var data = (
                                      from e in appContext.Exams
                                      join s in appContext.Students on e.StudentId equals s.StudentId
                                      where s.StudentId == studentId
                                      && e.Year.ToString() + "-" + e.TermNumber == termYear
                                      select e
                                  ).ToList();
                return data;
            }
        }
    }
}
