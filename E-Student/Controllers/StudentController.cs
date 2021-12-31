using Cassandra;
using E_Student.Models;
using Microsoft.AspNetCore.Mvc;
using Cassandra.Mapping;
namespace E_Student.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentController : ControllerBase
{

    public StudentController()
    {
        MappingConfiguration.Global.Define<MyMappings>();
    }


    // [HttpGet]
    // [Route("getStudents")]
    // public List<Student> vratiStudente()
    // {
    //     List<Student> studenti = new List<Student>();
    //     Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
    //     Cassandra.ISession localSession = cluster.Connect("test");
    //     var podaci = localSession.Execute("select * from \"Student\" ");
    //     foreach (var p in podaci)
    //     {
    //         Student s = new Student();

    //         s.Email = p.GetValue<string>("Email");
    //         s.BrojIndeksa = p.GetValue<string>("BrojIndeksa");
    //         s.GodinaUpisa = p.GetValue<string>("GodinaUpisa");
    //         s.Ime = p.GetValue<string>("Ime");
    //         s.Prezime = p.GetValue<string>("Prezime");
    //         s.Semestar = p.GetValue<string>("Semestar");
    //         s.Sifra = p.GetValue<string>("Sifra");

    //         studenti.Add(s);
    //     }
    //     cluster.Shutdown();
    //     return studenti;
    // }
    // [HttpGet]
    // [Route("getStudent/{email}")]
    // public Student vratiStudenta(string email)
    // {
    //     List<Student> studenti = new List<Student>();
    //     Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
    //     Cassandra.ISession localSession = cluster.Connect("test");
    //     var p = localSession.Execute("select * from \"Student\" where \"Email\" ='" + email + "'").FirstOrDefault();
    //     Student s = new Student();

    //     if (p != null)
    //     {


    //         s.Email = p.GetValue<string>("Email");
    //         s.BrojIndeksa = p.GetValue<string>("BrojIndeksa");
    //         s.GodinaUpisa = p.GetValue<string>("GodinaUpisa");
    //         s.Ime = p.GetValue<string>("Ime");
    //         s.Prezime = p.GetValue<string>("Prezime");
    //         s.Semestar = p.GetValue<string>("Semestar");
    //         s.Sifra = p.GetValue<string>("Sifra");

    //         studenti.Add(s);
    //     }
    //     cluster.Shutdown();
    //     return s;
    // }
    // [HttpPost]
    // [Route("addStudent")]
    // public IActionResult AddStudent([FromBody] Student student)
    // {
    //     try
    //     {
    //         Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
    //         Cassandra.ISession localSession = cluster.Connect("test");

    //         localSession.Execute("INSERT INTO \"Student\" (\"Email\",\"BrojIndeksa\",\"GodinaUpisa\",\"Ime\",\"Prezime\",\"Semestar\",\"Sifra\")"
    //         + "VALUES ('" + student.Email + "','" + student.BrojIndeksa + "','" + student.GodinaUpisa + "','" + student.Ime + "','" + student.Prezime + "','" + student.Semestar + "','" + student.Sifra + "')");
    //         return Ok();
    //     }
    //     catch (Exception ex)
    //     {
    //         return BadRequest(ex.ToString());
    //     }
    // }
    [HttpGet]
    [Route("getStudentss")]
    public List<Student> vratiStudentaa()
    {
        List<Student> studenti = new List<Student>();
        // Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
        var localSession = SessionManager.GetSession();
        // Cassandra.ISession localSession = cluster.Connect("test");

        IMapper mapper = new Mapper(localSession);

        var ss = mapper.Fetch<Student>();
        // cluster.Shutdown();

        // foreach (var sss in ss)
        // {
        //     studenti.Add(sss);
        // }
        return ss.ToList();
    }
    [HttpPost]
    [Route("addStudent")]
    public IActionResult AddStudent([FromBody] Student student)
    {
        try
        {
            // Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
            // Cassandra.ISession localSession = cluster.Connect("test");
            var localSession = SessionManager.GetSession();
            IMapper mapper = new Mapper(localSession);

            mapper.Insert<Student>(student);
            // cluster.Shutdown();

            // localSession.Execute("INSERT INTO \"Student\" (\"Email\",\"BrojIndeksa\",\"GodinaUpisa\",\"Ime\",\"Prezime\",\"Semestar\",\"Sifra\")"
            // + "VALUES ('" + student.Email + "','" + student.BrojIndeksa + "','" + student.GodinaUpisa + "','" + student.Ime + "','" + student.Prezime + "','" + student.Semestar + "','" + student.Sifra + "')");
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }
}