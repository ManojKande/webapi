using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Model;

namespace WebAppStatic.Controllers
{
    public class ValuesController : ApiController
    {
        private static List<Student> _students = new List<Student>();
        private static List<Group> _groups = new List<Group>();
        // GET: api/Values/GetStudents
        public IEnumerable<Student> GetStudents()
        {
            return _students;
        }
        // GET: api/Values/GetGroups
        public IEnumerable<Group> GetGroups()
        {
            return _groups;
        }
        // GET api/values/GetStudentbyId
        public HttpResponseMessage GetStudentbyId(int id)
        {
            Student student = _students.Find(m => m.StudentId == id);
            if (student == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "NotThere");
            }
            return Request.CreateResponse(HttpStatusCode.OK, student); 
        }
        // GET: api/Values/GetGroupByid
        public HttpResponseMessage GetGroupByid(int id)
        {

            Group group = _groups.Find(m => m.GroupId == id);
            if (group == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "NotThere");
            }
            return Request.CreateResponse(HttpStatusCode.OK, group);
        }
        // POST api/values/AddStudent
        [HttpPost]
        public HttpResponseMessage AddStudent(Student value)
        {
            if (value != null)
            {
                if (_students.Find(m => m.StudentId == value.StudentId) == null)
                {
                    _students.Add(value);
                    return Request.CreateResponse(HttpStatusCode.OK, "Added");
                }
                return Request.CreateResponse(HttpStatusCode.Ambiguous, "DuplicateData");
            }
            return Request.CreateResponse(HttpStatusCode.NotAcceptable, "NotAccepted");
        }
        // POST api/values/AddGroup
        [HttpPost]
        public HttpResponseMessage AddGroup(Group value)
        {
            if (value != null)
            {
                if (_groups.Find(m => m.GroupId == value.GroupId) == null)
                {
                    _groups.Add(value);
                    return Request.CreateResponse(HttpStatusCode.OK, "Added");
                }
                return Request.CreateResponse(HttpStatusCode.Ambiguous, "DuplicateData");
            }
            return Request.CreateResponse(HttpStatusCode.NotAcceptable, "NotAccepted");
        }
    }
}
