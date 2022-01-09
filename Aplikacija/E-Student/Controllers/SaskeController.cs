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
public class SaskeController : ControllerBase
{
    [HttpGet]
    //[Authorize(Roles = "Student")]
    [Route("getStudent")]
    public IActionResult student()
    {
       // Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
        TypeSerializerDefinitions definitions = new TypeSerializerDefinitions();
        definitions.Define(new DateCodec());
        Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").WithTypeSerializers(definitions).Build();
        try
        {
            Cassandra.ISession localSession = cluster.Connect("test");
            IMapper mapper = new Mapper(localSession);
            var studentEmail = HttpContext.User.Identity!.Name;
            Student student = mapper.Single<Student>("WHERE email=? ALLOW FILTERING", studentEmail);
            return new JsonResult(student);
        }
        catch (Exception exc)
        {
            return BadRequest(exc.Message);
        }
        finally
        {
            cluster.Shutdown();
        }
    }
    [HttpGet]
    [Route("getPolozeniIspiti")]
    public IActionResult polozeniIspiti()
    {
        //Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
        TypeSerializerDefinitions definitions = new TypeSerializerDefinitions();
        definitions.Define(new DateCodec());
        Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").WithTypeSerializers(definitions).Build();
        try
        {
            Cassandra.ISession localSession = cluster.Connect("test");
            IMapper mapper = new Mapper(localSession);
            var studentEmail = HttpContext.User.Identity!.Name;
            //var result = localSession.Execute("SELECT * FROM predaje_predmet WHERE email_profesora='" + email + "' ALLOW FILTERING");
            List<PolozeniIspiti> result =mapper.Fetch<PolozeniIspiti>("WHERE email_studenta=? ALLOW FILTERING",studentEmail).ToList();
            List<Predmet> ispiti= new List<Predmet>();
            foreach (var row in result)
            {
                Predmet p= new Predmet();
                p= mapper.Single<Predmet>("WHERE sifra_predmeta=?", row.Sifra_Predmeta);
                //row.GetValue<String>("sifra_predmeta"));
                ispiti.Add(p);
            }
            var returnValue = new List<object>();
            
            foreach (var r in result)
            {
                foreach (var i in ispiti)
                { 
                    RowSet ime=localSession.Execute("SELECT  naziv_predmeta FROM predmet WHERE sifra_predmeta='" + i.Sifra_Predmeta + "' ALLOW FILTERING");
                    RowSet ocena=localSession.Execute("SELECT ocena FROM polozeni_ispiti WHERE sifra_predmeta='" + i.Sifra_Predmeta + "' ALLOW FILTERING");
                    returnValue.Add(new { ime, ocena});
                }
            }

            return new JsonResult(returnValue);
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
    [Route("getPolozeniIspitiOcene/{email}")]
    public IActionResult polozeniIspitiOcene(string email)
    {
        Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();

        try
        {
            Cassandra.ISession localSession = cluster.Connect("test");
            IMapper mapper = new Mapper(localSession);
            List<PolozeniIspiti> result =mapper.Fetch<PolozeniIspiti>("WHERE email_studenta=? ALLOW FILTERING",email).ToList();
            return new JsonResult(result);
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
    [Route("getZabrane")]
    public IActionResult zabrane()
    {
         TypeSerializerDefinitions definitions = new TypeSerializerDefinitions();
        definitions.Define(new DateCodec());
        Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").WithTypeSerializers(definitions).Build();

        try
        {
            Cassandra.ISession localSession = cluster.Connect("test");
            //IMapper mapper = new Mapper(localSession);
            DateTime today = DateTime.Today;
            var studentEmail = HttpContext.User.Identity!.Name;
            var date = today.Year + "-" + today.Month + "-" + today.Day;
            //Student student = mapper.Single<Student>("WHERE email=? ALLOW FILTERING", email);
            var result = localSession.Execute("SELECT sifra_predmeta FROM zabranjena_prijava WHERE email_student='" +studentEmail  + "' AND datum_isteka>='" + date + "' ALLOW FILTERING");//uzimam predmete gde ima zabranu

            List<String> izbaciPredmete = new List<String>();
            foreach (var row in result)
            {
                izbaciPredmete.Add(row.GetValue<String>("sifra_predmeta"));
            }
            
            List<Predmet> ispiti= new List<Predmet>();
            List<Predmet> res= new List<Predmet>();
            foreach (var p in ispiti)
            {
                foreach (var i in izbaciPredmete)
                {
                    if (p.Sifra_Predmeta == i)
                    {
                        Predmet pom= new Predmet();
                        res.Add(pom);
                    }
                }

            }

            return new JsonResult(res);
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
}