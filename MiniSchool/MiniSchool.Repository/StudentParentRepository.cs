using MiniSchool.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MiniSchool.Repository
{
    public class StudentParentRepository
    {
        public static List<Student> GetParentStudentsByPhoneNumber(string phoneNumber)
        {
            using (AppContext appContext = new AppContext())
            {
                var studentds = (
                                      from s in appContext.Students
                                      join e in appContext.StudentParents on s.StudentId equals e.StudentId
                                      join p in appContext.Parents on e.ParentId equals p.ParentId
                                      where p.PhoneNumber != null
                                      && p.PhoneNumber == phoneNumber
                                      select s
                                  ).ToList();
                return studentds;
            }
        }
    }
}
