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
public class NatalijaController : ControllerBase
{
    [HttpGet]
    [Route("getProfesor/{email}")]
    public IActionResult profesor(string email) //preuzimanje info o profesoru
    {
        Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();

        try
        {
            Cassandra.ISession localSession = cluster.Connect("test");
            IMapper mapper = new Mapper(localSession);
            Profesor prof=mapper.Single<Profesor>("WHERE email=?", email);
            return new JsonResult(prof);
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
    [Route("getProfesorIspiti/{email}")]
    public IActionResult profesorIspiti(string email) //preuzimanje info o predmetima nekog profesora
    {
        Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();

        try
        {
            Cassandra.ISession localSession = cluster.Connect("test");
            IMapper mapper = new Mapper(localSession);
            //var result = localSession.Execute("SELECT * FROM predaje_predmet WHERE email_profesora='" + email + "' ALLOW FILTERING");
            List<PredajePredmet> result =mapper.Fetch<PredajePredmet>("WHERE email_profesora=? ALLOW FILTERING",email).ToList();
            List<Predmet> ispiti= new List<Predmet>();
            foreach (var row in result)
            {
                Predmet p= new Predmet();
                p= mapper.Single<Predmet>("WHERE sifra_predmeta=?", row.Sifra_predmeta);
                //row.GetValue<String>("sifra_predmeta"));
                ispiti.Add(p);
            }
            return new JsonResult(ispiti);
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
    [Route("getAllAboutProfesor/{email}")]
    public IActionResult getAllAboutProfesor(string email) //preuzimanje info o predmetima nekog profesora i info
    {
        Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();

        try
        {
            Cassandra.ISession localSession = cluster.Connect("test");
            IMapper mapper = new Mapper(localSession);
            Profesor prof=mapper.Single<Profesor>("WHERE email=?", email);
            //var result = localSession.Execute("SELECT * FROM predaje_predmet WHERE email_profesora='" + email + "' ALLOW FILTERING");
            List<PredajePredmet> result =mapper.Fetch<PredajePredmet>("WHERE email_profesora=? ALLOW FILTERING",email).ToList();
            List<Predmet> ispiti= new List<Predmet>();
            foreach (var row in result)
            {
                Predmet p= new Predmet();
                p= mapper.Single<Predmet>("WHERE sifra_predmeta=?", row.Sifra_predmeta);
                //row.GetValue<String>("sifra_predmeta"));
                ispiti.Add(p);
            }
            return new JsonResult(new {
                Profesor = prof,
                Ispiti = ispiti
            });
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
    [Route("getProfesorIspitiNazivi/{email}")]
    public IActionResult profesorNazivi(string email) //preuzimanje info o predmetima nekog profesora
    {
        Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();

        try
        {
            Cassandra.ISession localSession = cluster.Connect("test");
            IMapper mapper = new Mapper(localSession);
            //var result = localSession.Execute("SELECT * FROM predaje_predmet WHERE email_profesora='" + email + "' ALLOW FILTERING");
            List<PredajePredmet> result =mapper.Fetch<PredajePredmet>("WHERE email_profesora=? ALLOW FILTERING",email).ToList();
            List<String> ispiti= new List<String>();
            foreach (var row in result)
            {
                if(row.Sifra_predmeta!=null)
                    ispiti.Add(row.Sifra_predmeta);
            }
            return new JsonResult(ispiti);
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
    [Route("getObavestenjaProfesora/{email}")]
    public IActionResult ObavestenjaProfesora(string email) //preuzimanje info o obavestenjima nekog profesora
    {
        TypeSerializerDefinitions definitions = new TypeSerializerDefinitions();
        definitions.Define(new DateCodec());
        Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").WithTypeSerializers(definitions).Build();
        try
        {

            Cassandra.ISession localSession = cluster.Connect("test");
            IMapper mapper = new Mapper(localSession);
            var result = localSession.Execute("SELECT * FROM obavestenje WHERE email_profesor='" + email + "' ALLOW FILTERING");
            List<Obavestenje> obavestenja= new List<Obavestenje>();
            foreach (var row in result)
            {
                Obavestenje o= new Obavestenje();
                o.Id_obavestenja = row.GetValue<String>("id_obavestenja");
                o.Datum_objave = row.GetValue<DateTime>("datum_objave");
                o.Email_profesor = row.GetValue<String>("email_profesor");
                o.Sifra_predmeta = row.GetValue<String>("sifra_predmeta");
                o.Tekst = row.GetValue<String>("tekst");
                obavestenja.Add(o);
            }
            return new JsonResult(obavestenja);
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
    [Route("getObavestenja")]
    public IActionResult Obavestenja() //preuzimanje info o svim obavestenjima 
    {
        TypeSerializerDefinitions definitions = new TypeSerializerDefinitions();
        definitions.Define(new DateCodec());
        Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").WithTypeSerializers(definitions).Build();
        try
        {

            Cassandra.ISession localSession = cluster.Connect("test");
            IMapper mapper = new Mapper(localSession);
            var result = localSession.Execute("SELECT * FROM obavestenje "+ " ALLOW FILTERING");
            List<Obavestenje> obavestenja= new List<Obavestenje>();
            foreach (var row in result)
            {
                Obavestenje o= new Obavestenje();
                o.Id_obavestenja = row.GetValue<String>("id_obavestenja");
                o.Datum_objave = row.GetValue<DateTime>("datum_objave");
                o.Email_profesor = row.GetValue<String>("email_profesor");
                o.Sifra_predmeta = row.GetValue<String>("sifra_predmeta");
                o.Tekst = row.GetValue<String>("tekst");
                obavestenja.Add(o);
            }
            return new JsonResult(obavestenja);
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
    [Route("getObavestenjaPredmeta/{sifra}")]
    public IActionResult ObavestenjaPredmeta(string sifra) //preuzimanje info o svim obavestenjima Predeta
    {
        TypeSerializerDefinitions definitions = new TypeSerializerDefinitions();
        definitions.Define(new DateCodec());
        Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").WithTypeSerializers(definitions).Build();
        try
        {

            Cassandra.ISession localSession = cluster.Connect("test");
            IMapper mapper = new Mapper(localSession);
            var result = localSession.Execute("SELECT * FROM obavestenje WHERE sifra_predmeta='" + sifra + "' ALLOW FILTERING");
            List<Obavestenje> obavestenja= new List<Obavestenje>();
            foreach (var row in result)
            {
                Obavestenje o= new Obavestenje();
                o.Id_obavestenja = row.GetValue<String>("id_obavestenja");
                o.Datum_objave = row.GetValue<DateTime>("datum_objave");
                o.Email_profesor = row.GetValue<String>("email_profesor");
                o.Sifra_predmeta = row.GetValue<String>("sifra_predmeta");
                o.Tekst = row.GetValue<String>("tekst");
                obavestenja.Add(o);
            }
            return new JsonResult(obavestenja);
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
    [Route("getZabrana/{email}")]
    public IActionResult ZabraneProfesora(string email) //preuzimanje info o svim zabranama s kojima profesor moze da barata
    {
        TypeSerializerDefinitions definitions = new TypeSerializerDefinitions();
        definitions.Define(new DateCodec());
        Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").WithTypeSerializers(definitions).Build();
        try
        {
            //citanje svih zabrana al poredjenje datuma ne treba se prikazuje ono sto je proslo
            Cassandra.ISession localSession = cluster.Connect("test");
            IMapper mapper = new Mapper(localSession);
            List<PredajePredmet> result =mapper.Fetch<PredajePredmet>("WHERE email_profesora=? ALLOW FILTERING",email).ToList();
            List<ZabranjenaPrijava> zabrana= new List<ZabranjenaPrijava>();
            foreach (var row in result)
            {
                //za svaki predmet koji predaje pokupis sve zabrane
                var result2 = localSession.Execute("SELECT * FROM zabranjena_prijava WHERE sifra_predmeta='" + row.Sifra_predmeta + "' ALLOW FILTERING");
                foreach(var arow in result2)
                {
                    ZabranjenaPrijava o= new ZabranjenaPrijava();
                    o.Id = arow.GetValue<String>("id");
                    o.Datum_isteka = arow.GetValue<DateTime>("datum_isteka");
                    o.Email_student = arow.GetValue<String>("email_student");
                    o.Sifra_predmeta = arow.GetValue<String>("sifra_predmeta");
                    o.Razlog = arow.GetValue<String>("razlog");
                    if(DateTime.Compare(o.Datum_isteka,DateTime.Now)>=0)
                       zabrana.Add(o);
                }
            }
            return new JsonResult(zabrana);
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
    [HttpPost]
    [Route("postObavestenje")]
    public IActionResult postObavestenje([FromBody] Obavestenje obavestenje) //postavljanje obavestenja
    {
        TypeSerializerDefinitions definitions = new TypeSerializerDefinitions();
        definitions.Define(new DateCodec());
        Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").WithTypeSerializers(definitions).Build();
        try
        {
            
            //ispituje i da li on moze da postavi obavestenje za taj ispit //predavac
            //za id mora da se izracua i strpa u obavestenje.Id_obavestenja
            Cassandra.ISession localSession = cluster.Connect("test");
            IMapper mapper = new Mapper(localSession);
            var id = localSession.Execute("SELECT counting FROM counting_id WHERE tabela='obavestenje' ALLOW FILTERING").First();
            obavestenje.Id_obavestenja=id.GetValue<String>("counting");
            Profesor prof=mapper.Single<Profesor>("WHERE email=?", obavestenje.Email_profesor);
            Predmet pred=mapper.Single<Predmet>("WHERE sifra_predmeta=?", obavestenje.Sifra_predmeta);
            //var result = localSession.Execute("SELECT * FROM obavestenje WHERE sifra_predmeta='"+obavestenje.Sifra_predmeta+"' AND email_profesor='"+ obavestenje.Email_profesor+"' AND tekst='"+ obavestenje.Tekst +"' ALLOW FILTERING");
            if(prof != null && pred!= null )
            {
                string noviID=(Int32.Parse(obavestenje.Id_obavestenja)+1).ToString();
                mapper.Insert<Obavestenje>(obavestenje);
                var inc = localSession.Execute("UPDATE counting_id SET counting='"+noviID+"' WHERE tabela='obavestenje'");
                return Ok();
            }
            else 
            {
                return BadRequest(400);
            }
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
    [HttpPost]
    [Route("postZabrana")]
    public IActionResult postZabrana([FromBody] ZabranjenaPrijava zabrana) //postavljanje zabrane studentu
    {
        TypeSerializerDefinitions definitions = new TypeSerializerDefinitions();
        definitions.Define(new DateCodec());
        Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").WithTypeSerializers(definitions).Build();
        try
        {
            //da li moze da se postavi zabrana tom studentu tj da li uoste taj student slusa predmet
            Cassandra.ISession localSession = cluster.Connect("test");
            IMapper mapper = new Mapper(localSession);
            var id = localSession.Execute("SELECT counting FROM counting_id WHERE tabela='zabranjena_prijava' ALLOW FILTERING").First();
            zabrana.Id=id.GetValue<String>("counting");
            Student prof=mapper.Single<Student>("WHERE email=?", zabrana.Email_student);
            Predmet pred=mapper.Single<Predmet>("WHERE sifra_predmeta=?", zabrana.Sifra_predmeta);
            var result = localSession.Execute("SELECT * FROM zabranjena_prijava WHERE sifra_predmeta='"+zabrana.Sifra_predmeta+"' AND email_student='"+ zabrana.Email_student+"' ALLOW FILTERING");
            if(prof != null && pred!= null )//&& result== null)
            {
                mapper.Insert<ZabranjenaPrijava>(zabrana);
                string noviID=(Int32.Parse(zabrana.Id)+1).ToString();
                var inc = localSession.Execute("UPDATE counting_id SET counting='"+noviID+"' WHERE tabela='zabranjena_prijava'");
                return Ok();
            }
            else 
            {
                return BadRequest(400);
            }
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
    [HttpDelete]
    [Route("deleteZabrana/{id}")]
    public IActionResult DeleteZabrana(string id)
    {
        TypeSerializerDefinitions definitions = new TypeSerializerDefinitions();
        definitions.Define(new DateCodec());
        Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").WithTypeSerializers(definitions).Build();
        try
        {
            //mozda ispituje dal on moze da izbrise //vrtn ne jer mu vracam sve zabrane koje moze da izbrise
            Cassandra.ISession localSession = cluster.Connect("test");
           // var result=localSession.Execute("SELECT * FROM zabranjena_prijava WHERE sifra_predmeta='"+sifra+"' AND email_student='"+ email+"' ");
           //string id= result.Id;
            var result=localSession.Execute("DELETE FROM zabranjena_prijava WHERE id='"+id+"' ");
            cluster.Shutdown();
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }
}