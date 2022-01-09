using Cassandra;
using E_Student.Models;
using Microsoft.AspNetCore.Mvc;
using Cassandra.Mapping;
using Cassandra.Serialization;
using E_Student.Converters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace E_Student.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentController : ControllerBase
{
    [HttpGet]
    //[Authorize(Roles = "Student")]
    [Route("getPassedExams/{email}")]
    public List<PolozeniIspiti> GetAllPassedExams(string email)//znaci svuda gde imamo datetime type moramo ovako valjda //ne moze preko mappera zato sto nije podrzano jos uvek valjda
    {
        TypeSerializerDefinitions definitions = new TypeSerializerDefinitions();
        definitions.Define(new DateCodec());
        Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").WithTypeSerializers(definitions).Build();
        Cassandra.ISession localSession = cluster.Connect("test");
        //var localSession = SessionManager.GetSession();
        //IMapper mapper = new Mapper(localSession);

        var result = localSession.Execute("SELECT * FROM polozeni_ispiti WHERE email_studenta='" + email + "' ALLOW FILTERING");
        List<PolozeniIspiti> predmeti = new List<PolozeniIspiti>();
        foreach (var row in result)
        {
            PolozeniIspiti p = new PolozeniIspiti();
            p.ID = row.GetValue<String>("id");
            p.Rok = row.GetValue<String>("rok");
            p.Email_Studenta = row.GetValue<String>("email_studenta");
            p.Ocena = row.GetValue<int>("ocena");
            p.Sifra_Predmeta = row.GetValue<String>("sifra_predmeta");

            predmeti.Add(p);

        }
        cluster.Shutdown();

        return predmeti;
    }
    [HttpGet]
    [Route("getExams/{smer}/{semestar}")]
    public IActionResult GetAllExams(string smer, string semestar)
    {
        Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();

        try
        {
            Cassandra.ISession localSession = cluster.Connect("test");
            IMapper mapper = new Mapper(localSession);
            List<Predmet> predmeti = new List<Predmet>();
            predmeti = mapper.Fetch<Predmet>("WHERE smer=? AND semestar=? ALLOW FILTERING", smer, semestar).ToList();
            return new JsonResult(predmeti);
        }
        catch (Exception exc)
        {
            return BadRequest(exc.ToString());
        }
        finally
        {
            cluster.Shutdown();
        }
    }

    [HttpGet]
    //[Authorize(Roles = "Student")]
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

            //student.Odobren = false;
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