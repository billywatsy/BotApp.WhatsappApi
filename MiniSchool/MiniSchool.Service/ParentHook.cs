
using BotApp.Domain.Entity.Dto;
using BotApp.Service.Extensions;
using BotApp.Service.Model;
using MiniSchool.Repository;
using Newtonsoft.Json.Linq;
using System;

namespace MiniSchool.Service
{
    public class ParentHook
    {
        public static ResponseClientWebHookTemplate GetStudentsForParent(JObject jObject)
        {
            var app = jObject.GetJsonObjectStringValue("_app");
            var cellNumber = jObject.GetJsonObjectStringValue("wa_id");
            var studentId = jObject.GetJsonObject<int>("list_parent_students");
            var yearTerm = jObject.GetJsonObject<string>("student_exam_by_year");
            
            ResponseClientWebHookTemplate hookResponse = new ResponseClientWebHookTemplate();
            hookResponse.FormValues = jObject;

            var value = jObject.Last;

            if(string.IsNullOrEmpty(value.ToString()))
            {

            }
            var fieldPropertySelected = value.Path;

            if (fieldPropertySelected == "list_parent_students")
                return GetParentStudentList(cellNumber, hookResponse);
            else if (fieldPropertySelected == "student_exam_by_year")
                return GetExamYearTermGroupByStudentId(studentId, hookResponse);
            else if (fieldPropertySelected == "student_exam_results_for_term")
                return GetStudentExamByYearTermList(studentId, yearTerm , hookResponse);
            else
            {
                hookResponse.Code = "DEC";
                hookResponse.Description = "Failed getting parent request";
                return hookResponse;
            }
        }

        public static ResponseClientWebHookTemplate GetParentStudentList(JObject jObject)
        {
            var app = jObject.GetJsonObjectStringValue("_app");
            var cellNumber = jObject.GetJsonObjectStringValue("wa_id");
            ResponseClientWebHookTemplate hookResponse = new ResponseClientWebHookTemplate();
            hookResponse.FormValues = jObject;
            hookResponse.PreRenderText = "Select Student";
            return GetParentStudentList(cellNumber, hookResponse);
        }

        public static ResponseClientWebHookTemplate GetExamYearTermGroupByStudentId(JObject jObject)
        {
            var app = jObject.GetJsonObjectStringValue("_app");
            var cellNumber = jObject.GetJsonObjectStringValue("wa_id");
            var studentId = jObject.GetJsonObject<int>("list_parent_students");

            ResponseClientWebHookTemplate hookResponse = new ResponseClientWebHookTemplate();
            hookResponse.FormValues = jObject;
            return GetExamYearTermGroupByStudentId(studentId, hookResponse);
        }

        public static ResponseClientWebHookTemplate GetStudentExamByYearTermList(JObject jObject)
        {
            var app = jObject.GetJsonObjectStringValue("_app");
            var cellNumber = jObject.GetJsonObjectStringValue("wa_id");
            var studentId = jObject.GetJsonObject<int>("list_parent_students");
            var yearTerm = jObject.GetJsonObject<string>("student_exam_by_year");

            ResponseClientWebHookTemplate hookResponse = new ResponseClientWebHookTemplate();
            hookResponse.FormValues = jObject;
            return GetStudentExamByYearTermList(studentId, yearTerm, hookResponse);
            
        }



        private static ResponseClientWebHookTemplate GetParentStudentList(string cellNumber, ResponseClientWebHookTemplate hookResponse)
        {
            var students = StudentParentRepository.GetParentStudentsByPhoneNumber(cellNumber) ?? new System.Collections.Generic.List<Repository.Entity.Student>();

            hookResponse.Description = "Approved";
            hookResponse.Code = "APP";
            if (students == null || students?.Count <= 0)
            {
                hookResponse.Description = "No student found for the linked phone number";
                hookResponse.Code = "DEC";
                return hookResponse;
            }
            hookResponse.DataLookUp = new System.Collections.Generic.List<UserScreenLookUpDataTemplateDto>();

            foreach (var item in students)
                hookResponse.DataLookUp.Add(new UserScreenLookUpDataTemplateDto(item.StudentId.ToString(), item.SchoolRegNumber));
            return hookResponse;
        }
        private static ResponseClientWebHookTemplate GetExamYearTermGroupByStudentId(int studentId, ResponseClientWebHookTemplate hookResponse)
        {
            var exams = ExamRepository.GetExamYearTermGroupByStudentId(studentId) ?? new System.Collections.Generic.List<Repository.Dto.ExamYearTermGroupDto>();

            hookResponse.Description = "Approved";
            hookResponse.Code = "APP";
            if (exams == null || exams?.Count <= 0)
            {
                hookResponse.Description = "No exams found for student";
                hookResponse.Code = "DEC";
                return hookResponse;
            }
            hookResponse.DataLookUp = new System.Collections.Generic.List<UserScreenLookUpDataTemplateDto>();

            foreach (var item in exams)
                hookResponse.DataLookUp.Add(new UserScreenLookUpDataTemplateDto($"{item.Year}-{item.TermNumber}" ,$" Year : {item.Year} \n Subjects : {item.CountSubjects} \n Term : {item.TermNumber} "));
            return hookResponse;
        }
        private static ResponseClientWebHookTemplate GetStudentExamByYearTermList(int studentId, string yearTerm , ResponseClientWebHookTemplate hookResponse)
        {
            var exams = ExamRepository.GetExamByYearTermStudentId(studentId, yearTerm) ?? new System.Collections.Generic.List<Repository.Entity.Exam>();

            hookResponse.Description = "Approved";
            hookResponse.Code = "APP";
            if (exams == null || exams?.Count <= 0)
            {
                hookResponse.Description = "No exams found for student";
                hookResponse.Code = "DEC";
                return hookResponse;
            }
            hookResponse.DataLookUp = new System.Collections.Generic.List<UserScreenLookUpDataTemplateDto>();

            foreach (var item in exams)
                hookResponse.DataLookUp.Add(new UserScreenLookUpDataTemplateDto($"{item.Year}-{item.TermNumber}" ,$" Subject : {item.SubjectName} \n Mark : {item.PercentageMark} % \n Grade : {item.ExamGrade} "));
            return hookResponse;
        }

    }
}
