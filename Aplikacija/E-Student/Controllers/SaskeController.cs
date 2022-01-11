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
    [Authorize(Roles = "Student")]
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
    [Authorize(Roles = "Student")]
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
            

                foreach (var i in ispiti)
                { 
                    RowSet ime=localSession.Execute("SELECT  naziv_predmeta FROM predmet WHERE sifra_predmeta='" + i.Sifra_Predmeta + "' ALLOW FILTERING");
                    RowSet ocena=localSession.Execute("SELECT ocena FROM polozeni_ispiti WHERE sifra_predmeta='" + i.Sifra_Predmeta + "' ALLOW FILTERING");
                    returnValue.Add(new { ime, ocena});
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
    [Authorize(Roles = "Student")]
    [Route("getPolozeniIspitiOcene")]
    public IActionResult polozeniIspitiOcena()
    {
        Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
        Cassandra.ISession localSession = cluster.Connect("test");
        IMapper mapper = new Mapper(localSession);
        var studentEmail = HttpContext.User.Identity!.Name;
        try
        {
            //Cassandra.ISession localSession = cluster.Connect("test");
            //IMapper mapper = new Mapper(localSession);
            var email = HttpContext.User.Identity!.Name;
            List<PolozeniIspiti> result =mapper.Fetch<PolozeniIspiti>("WHERE email_studenta=? ALLOW FILTERING",email).ToList();
            int? pr=0;
            int? espb=0;
            int i=0;
            List<Predmet> ispiti=mapper.Fetch<Predmet>().ToList();
            foreach(var r in result)
            {
                
                pr+=r.Ocena;
                i++;
                foreach(var pred in ispiti)
                {
                    if(pred.Sifra_Predmeta==r.Sifra_Predmeta)
                    {
                        espb+=Int32.Parse(pred.Espb);
                    }
                }

            }
            double ocena=(double)pr/i;
           // object returnValue=new object;
            object returnValue=new { ocena,espb};
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
    [Authorize(Roles = "Student")]
    [Route("getZabrane")]
    public IActionResult zabrane()
    {
        TypeSerializerDefinitions definitions = new TypeSerializerDefinitions();
        definitions.Define(new DateCodec());
        Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").WithTypeSerializers(definitions).Build();
        try
        {
            Cassandra.ISession localSession = cluster.Connect("test");
            IMapper mapper = new Mapper(localSession);
            List<ZabranjenaPrijava> zabrana= new List<ZabranjenaPrijava>();
            DateTime today = DateTime.Today;
            var email = HttpContext.User.Identity!.Name;
            var date = today.Year + "-" + today.Month + "-" + today.Day;
            var result2 = localSession.Execute("SELECT * FROM zabranjena_prijava WHERE email_student='" + email + "' AND datum_isteka>='" + date +"' ALLOW FILTERING");
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
            List<Predmet> ispiti =mapper.Fetch<Predmet>().ToList();
            List<Predmet> res= new List<Predmet>();
            foreach(var i in zabrana)
            {
                foreach(var p in ispiti)
                {
                    if(i.Sifra_predmeta==p.Sifra_Predmeta)
                        res.Add(p);

                }
                
            }
            var returnValue = new List<object>();
            foreach(var r in res)
            {
                foreach(var j in zabrana)
                {
                    if(r.Sifra_Predmeta==j.Sifra_predmeta)
                    {
                        string naziv=r.NazivPredmeta;
                        DateTime datumr=j.Datum_isteka;
                        string raz=j.Razlog;
                        returnValue.Add(new { naziv,datumr,raz});
                    }
                }
            }
            return new JsonResult( returnValue);
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
    [Authorize(Roles = "Student")]
    [Route("getObavestenjaStudenta")]
    public IActionResult ObavestenjaStudenta() 
    {
        TypeSerializerDefinitions definitions = new TypeSerializerDefinitions();
        definitions.Define(new DateCodec());
        Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").WithTypeSerializers(definitions).Build();
        try
        {

            Cassandra.ISession localSession = cluster.Connect("test");
            IMapper mapper = new Mapper(localSession);
            var email = HttpContext.User.Identity!.Name;
            var result = localSession.Execute("SELECT * FROM obavestenje ALLOW FILTERING");
            List<Obavestenje> obavestenja = new List<Obavestenje>();
            foreach (var row in result)
            {
                Obavestenje o = new Obavestenje();
                o.Id_obavestenja = row.GetValue<String>("id_obavestenja");
                o.Datum_objave = row.GetValue<DateTime>("datum_objave");
                o.Email_profesor = row.GetValue<String>("email_profesor");
                o.Sifra_predmeta = row.GetValue<String>("sifra_predmeta");
                o.Tekst = row.GetValue<String>("tekst");
                obavestenja.Add(o);
            }
            Student student = mapper.Single<Student>("WHERE email=? ALLOW FILTERING", email);
            
            
            List<Predmet> predmeti = mapper.Fetch<Predmet>("WHERE smer=? AND semestar<=? ALLOW FILTERING", student.Smer, student.Semestar).ToList(); // svi predmeti koje je slusao student

            List<PredajePredmet> profesori = new List<PredajePredmet>();

            foreach (var predmet in predmeti)

            {

                profesori.AddRange(mapper.Fetch<PredajePredmet>("WHERE sifra_predmeta=? ALLOW FILTERING", predmet.Sifra_Predmeta).ToList());

            }

            List<Obavestenje> obavestenjaRet = new List<Obavestenje>();
            foreach(var j in obavestenja)
            {
                foreach(var pr in profesori)
                {
                   if(j.Email_profesor==pr.Email_profesora)
                        obavestenjaRet.Add(j); 
                }
                
            }
            var returnValue = new List<object>();
            foreach(var obav in obavestenjaRet)
            {
                foreach(var predm in predmeti)
                {
                    if(predm.Sifra_Predmeta==obav.Sifra_predmeta)
                    {
                        DateTime datum=obav.Datum_objave;
                        string emailProf=obav.Email_profesor;
                        string predmet=predm.NazivPredmeta;
                        string obavestenje=obav.Tekst;
                        returnValue.Add(new { datum,emailProf,predmet,obavestenje});
                    }
                   
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
    
    [Route("getObavestenjaProfesora")]
    public IActionResult ObavestenjaProfesora() //preuzimanje info o obavestenjima nekog profesora
    {
        TypeSerializerDefinitions definitions = new TypeSerializerDefinitions();
        definitions.Define(new DateCodec());
        Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").WithTypeSerializers(definitions).Build();
        try
        {

            Cassandra.ISession localSession = cluster.Connect("test");
            IMapper mapper = new Mapper(localSession);
            var email = HttpContext.User.Identity!.Name;
            var result = localSession.Execute("SELECT * FROM obavestenje WHERE email_profesor='" + email + "' ALLOW FILTERING");
            List<Obavestenje> obavestenja = new List<Obavestenje>();
            foreach (var row in result)
            {
                Obavestenje o = new Obavestenje();
                o.Id_obavestenja = row.GetValue<String>("id_obavestenja");
                o.Datum_objave = row.GetValue<DateTime>("datum_objave");
                o.Email_profesor = row.GetValue<String>("email_profesor");
                o.Sifra_predmeta = row.GetValue<String>("sifra_predmeta");
                o.Tekst = row.GetValue<String>("tekst");
                obavestenja.Add(o);
            }
            List<Predmet> predmeti = mapper.Fetch<Predmet>().ToList(); // svi predmeti koje je slusao student
            var returnValue = new List<object>();
            foreach(var obav in obavestenja)
            {
                foreach(var predm in predmeti)
                {
                    if(predm.Sifra_Predmeta==obav.Sifra_predmeta)
                    {
                        DateTime datum=obav.Datum_objave;
                        string emailProf=obav.Email_profesor;
                        string predmet=predm.NazivPredmeta;
                        string obavestenje=obav.Tekst;
                        returnValue.Add(new { datum,emailProf,predmet,obavestenje});
                    }
                   
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
}