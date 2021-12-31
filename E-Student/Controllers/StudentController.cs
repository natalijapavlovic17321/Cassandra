using Cassandra;
using E_Student.Models;
using Microsoft.AspNetCore.Mvc;
using Cassandra.Mapping;
namespace E_Student.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentController : ControllerBase
{

    [HttpGet]
    [Route("getStudents")]
    public List<Student> GetAllStudents()
    {
        Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
        Cassandra.ISession localSession = cluster.Connect("test");
        //var localSession = SessionManager.GetSession();
        IMapper mapper = new Mapper(localSession);

        List<Student> studenti = mapper.Fetch<Student>().ToList();
        cluster.Shutdown();

        return studenti;
    }
    [HttpPost]
    [Route("addStudent")]
    public IActionResult AddStudent([FromBody] Student student)
    {
        try
        {
            Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
            Cassandra.ISession localSession = cluster.Connect("test");
            //var localSession = SessionManager.GetSession();
            IMapper mapper = new Mapper(localSession);

            mapper.Insert<Student>(student);
            cluster.Shutdown();
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }
    [HttpPut]
    [Route("updateStudent/{email}/{newSemestar}")]
    public IActionResult UpdateStudent(string email, string newSemestar)
    {
        try
        {
            Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
            Cassandra.ISession localSession = cluster.Connect("test");
            //var localSession = SessionManager.GetSession();
            IMapper mapper = new Mapper(localSession);

            Student student = mapper.Single<Student>("WHERE email=?", email);
            student.Semestar = newSemestar;//updajtujem mu samo indeks

            mapper.Update<Student>(student);
            cluster.Shutdown();
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }
    [HttpDelete]
    [Route("deleteStudent/{email}")]
    public IActionResult DeleteStudent(string email)
    {
        try
        {
            Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
            Cassandra.ISession localSession = cluster.Connect("test");
            //var localSession = SessionManager.GetSession();
            IMapper mapper = new Mapper(localSession);

            mapper.Delete<Student>(mapper.Single<Student>("WHERE email=?", email));
            cluster.Shutdown();
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }
}