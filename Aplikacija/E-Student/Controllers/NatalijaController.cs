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
    [Authorize(Roles = "Profesor")]
    [Route("getProfesor")]
    public IActionResult profesor() //preuzimanje info o profesoru
    {
        Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();

        try
        {
            Cassandra.ISession localSession = cluster.Connect("test");
            IMapper mapper = new Mapper(localSession);
            var email = HttpContext.User.Identity!.Name;
            Profesor prof = mapper.Single<Profesor>("WHERE email=?", email);
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
    [Authorize(Roles = "Profesor")]
    [Route("getProfesorIspiti")]
    public IActionResult profesorIspiti() //preuzimanje info o predmetima nekog profesora
    {
        Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();

        try
        {
            Cassandra.ISession localSession = cluster.Connect("test");
            IMapper mapper = new Mapper(localSession);
            var email = HttpContext.User.Identity!.Name;
            //var result = localSession.Execute("SELECT * FROM predaje_predmet WHERE email_profesora='" + email + "' ALLOW FILTERING");
            List<PredajePredmet> result = mapper.Fetch<PredajePredmet>("WHERE email_profesora=? ALLOW FILTERING", email).ToList();
            List<Predmet> ispiti = new List<Predmet>();
            foreach (var row in result)
            {
                Predmet p = new Predmet();
                p = mapper.Single<Predmet>("WHERE sifra_predmeta=?", row.Sifra_predmeta);
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
    [Authorize(Roles = "Profesor")]
    [Route("getAllAboutProfesor")]
    public IActionResult getAllAboutProfesor() //preuzimanje info o predmetima nekog profesora i info
    {
        Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();

        try
        {
            Cassandra.ISession localSession = cluster.Connect("test");
            IMapper mapper = new Mapper(localSession);
            var email = HttpContext.User.Identity!.Name;
            Profesor prof = mapper.Single<Profesor>("WHERE email=?", email);
            //var result = localSession.Execute("SELECT * FROM predaje_predmet WHERE email_profesora='" + email + "' ALLOW FILTERING");
            List<PredajePredmet> result = mapper.Fetch<PredajePredmet>("WHERE email_profesora=? ALLOW FILTERING", email).ToList();
            List<Predmet> ispiti = new List<Predmet>();
            foreach (var row in result)
            {
                Predmet p = new Predmet();
                p = mapper.Single<Predmet>("WHERE sifra_predmeta=?", row.Sifra_predmeta);
                //row.GetValue<String>("sifra_predmeta"));
                ispiti.Add(p);
            }
            return new JsonResult(new
            {
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
    [Authorize(Roles = "Profesor")]
    [Route("getProfesorIspitiNazivi")]
    public IActionResult profesorNazivi() //preuzimanje info o predmetima nekog profesora
    {
        Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();

        try
        {
            Cassandra.ISession localSession = cluster.Connect("test");
            IMapper mapper = new Mapper(localSession);
            var email = HttpContext.User.Identity!.Name;
            //var result = localSession.Execute("SELECT * FROM predaje_predmet WHERE email_profesora='" + email + "' ALLOW FILTERING");
            List<PredajePredmet> result = mapper.Fetch<PredajePredmet>("WHERE email_profesora=? ALLOW FILTERING", email).ToList();
            List<String> ispiti = new List<String>();
            foreach (var row in result)
            {
                if (row.Sifra_predmeta != null)
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
    [Authorize(Roles = "Profesor")]
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
    [Authorize(Roles = "Profesor")]
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
            var result = localSession.Execute("SELECT * FROM obavestenje " + " ALLOW FILTERING");
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
    [Authorize(Roles = "Profesor")]
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
    [Authorize(Roles = "Profesor")]
    [Route("getZabrana")]
    public IActionResult ZabraneProfesora() //preuzimanje info o svim zabranama s kojima profesor moze da barata
    {
        TypeSerializerDefinitions definitions = new TypeSerializerDefinitions();
        definitions.Define(new DateCodec());
        Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").WithTypeSerializers(definitions).Build();
        try
        {
            Cassandra.ISession localSession = cluster.Connect("test");
            IMapper mapper = new Mapper(localSession);
            var email = HttpContext.User.Identity!.Name;
            List<PredajePredmet> result = mapper.Fetch<PredajePredmet>("WHERE email_profesora=? ALLOW FILTERING", email).ToList();
            List<ZabranjenaPrijava> zabrana = new List<ZabranjenaPrijava>();
            DateTime today = DateTime.Today;
            var date = today.Year + "-" + today.Month + "-" + today.Day;
            foreach (var row in result)
            {
                var result2 = localSession.Execute("SELECT * FROM zabranjena_prijava WHERE sifra_predmeta='" + row.Sifra_predmeta + "' AND datum_isteka>='" + date + "' ALLOW FILTERING");
                foreach (var arow in result2)
                {
                    ZabranjenaPrijava o = new ZabranjenaPrijava();
                    o.Id = arow.GetValue<String>("id");
                    o.Datum_isteka = arow.GetValue<DateTime>("datum_isteka");
                    o.Email_student = arow.GetValue<String>("email_student");
                    o.Sifra_predmeta = arow.GetValue<String>("sifra_predmeta");
                    o.Razlog = arow.GetValue<String>("razlog");
                    //if(DateTime.Compare(o.Datum_isteka,DateTime.Now)>=0)
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
    [Authorize(Roles = "Profesor")]
    [Route("postObavestenje")]
    public IActionResult postObavestenje([FromBody] Obavestenje obavestenje) //postavljanje obavestenja
    {
        TypeSerializerDefinitions definitions = new TypeSerializerDefinitions();
        definitions.Define(new DateCodec());
        Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").WithTypeSerializers(definitions).Build();
        try
        {
            Cassandra.ISession localSession = cluster.Connect("test");
            IMapper mapper = new Mapper(localSession);
            var id = localSession.Execute("SELECT counting FROM counting_id WHERE tabela='obavestenje' ALLOW FILTERING").First();
            obavestenje.Id_obavestenja = id.GetValue<String>("counting");
            Profesor prof = mapper.Single<Profesor>("WHERE email=?", obavestenje.Email_profesor);
            Predmet pred = mapper.Single<Predmet>("WHERE sifra_predmeta=?", obavestenje.Sifra_predmeta);
            //var result = localSession.Execute("SELECT * FROM obavestenje WHERE sifra_predmeta='"+obavestenje.Sifra_predmeta+"' AND email_profesor='"+ obavestenje.Email_profesor+"' AND tekst='"+ obavestenje.Tekst +"' ALLOW FILTERING");
            //duplikati
            if (prof != null && pred != null)
            {
                string noviID = (Int32.Parse(obavestenje.Id_obavestenja) + 1).ToString();
                mapper.Insert<Obavestenje>(obavestenje);
                var inc = localSession.Execute("UPDATE counting_id SET counting='" + noviID + "' WHERE tabela='obavestenje'");
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
    [Authorize(Roles = "Profesor")]
    [Route("postZabrana")]
    public IActionResult postZabrana([FromBody] ZabranjenaPrijava zabrana) //postavljanje zabrane studentu
    {
        TypeSerializerDefinitions definitions = new TypeSerializerDefinitions();
        definitions.Define(new DateCodec());
        Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").WithTypeSerializers(definitions).Build();
        try
        {
            Cassandra.ISession localSession = cluster.Connect("test");
            IMapper mapper = new Mapper(localSession);
            var id = localSession.Execute("SELECT counting FROM counting_id WHERE tabela='zabranjena_prijava' ALLOW FILTERING").First();
            zabrana.Id = id.GetValue<String>("counting");
            Student prof = mapper.Single<Student>("WHERE indeks=? ALLOW FILTERING", zabrana.Email_student);
            zabrana.Email_student = prof.Email;
            Predmet pred = mapper.Single<Predmet>("WHERE sifra_predmeta=?", zabrana.Sifra_predmeta);
            var result = localSession.Execute("SELECT * FROM zabranjena_prijava WHERE sifra_predmeta='" + zabrana.Sifra_predmeta + "' AND email_student='" + zabrana.Email_student + "' ALLOW FILTERING");
            if (prof != null && pred != null)//&& result== null) duplikati
            {
                mapper.Insert<ZabranjenaPrijava>(zabrana);
                string noviID = (Int32.Parse(zabrana.Id) + 1).ToString();
                var inc = localSession.Execute("UPDATE counting_id SET counting='" + noviID + "' WHERE tabela='zabranjena_prijava'");
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
    [Authorize(Roles = "Profesor")]
    [Route("deleteZabrana/{id}")]
    public IActionResult DeleteZabrana(string id)
    {
        TypeSerializerDefinitions definitions = new TypeSerializerDefinitions();
        definitions.Define(new DateCodec());
        Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").WithTypeSerializers(definitions).Build();
        try
        {
            Cassandra.ISession localSession = cluster.Connect("test");
            // var result=localSession.Execute("SELECT * FROM zabranjena_prijava WHERE sifra_predmeta='"+sifra+"' AND email_student='"+ email+"' ");
            //string id= result.Id;
            var result = localSession.Execute("DELETE FROM zabranjena_prijava WHERE id='" + id + "' ");
            cluster.Shutdown();
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }
    [HttpGet]
    [Authorize(Roles = "Profesor")]
    [Route("getSSala/{sifra}")]
    public IActionResult SlobSale(string sifra)
    {
        TypeSerializerDefinitions definitions = new TypeSerializerDefinitions();
        definitions.Define(new DateCodec());
        Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").WithTypeSerializers(definitions).Build();
        try
        {
            Cassandra.ISession localSession = cluster.Connect("test");
            IMapper mapper = new Mapper(localSession);
            DateTime today = DateTime.Today;
            var date = today.Year + "-" + today.Month + "-" + today.Day;

            var result = localSession.Execute("SELECT id FROM rok WHERE kraj_prijave<='" + date + "' AND  zavrsetak_roka>='" + date + "' ALLOW FILTERING");//sve mpguce sale
            string rokID = "";
            foreach (var i in result)
            {
                rokID = i.GetValue<string>("id");
                break;
            }
            var free = localSession.Execute("SELECT * FROM satnica WHERE rok_id='" + rokID + "' AND sifra_predmeta='" + sifra + "' ALLOW FILTERING");
            foreach (var i in free)
            {
                if (i.GetValue<string>("naziv_sale") != "tbd")
                    return BadRequest("Vec je rezervisana sala");
                break;
            }
            var zauzete = localSession.Execute("SELECT * FROM satnica WHERE rok_id='" + rokID + "'  ALLOW FILTERING");
            List<string> zauzeteSale = new List<string>();
            foreach (var i in zauzete)
            {
                if (i.GetValue<string>("naziv_sale") != "tbd")
                {
                    zauzeteSale.Add(i.GetValue<string>("naziv_sale"));
                }
            }
            zauzeteSale.Distinct();

            var sveSale = mapper.Fetch<Sala>().ToList();
            var slovodneSale = new List<Sala>(sveSale);

            Int64 br = 0;
            RowSet broj = localSession.Execute("SELECT COUNT(*) FROM prijave_ispita WHERE sifra_predmeta='" + sifra + "' AND rok_id='" + rokID + "'ALLOW FILTERING");
            foreach (var b in broj)
            {
                br = b.GetValue<Int64>("count");
                break;
            }
            foreach (var i in sveSale)
            {
                foreach (var j in zauzeteSale)
                {
                    if (i.Naziv == j)
                    {
                        slovodneSale.Remove(i);
                    }
                }
                if (i.Kapacitet < br)
                    slovodneSale.Remove(i);
            }
            return new JsonResult(slovodneSale);
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
    [Authorize(Roles = "Profesor")]
    [Route("getIspitiZaSalu")]
    public IActionResult ispitiZaSalu() //svi ispiti za koje sale jos nisu zauzete
    {
        Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();

        try
        {
            Cassandra.ISession localSession = cluster.Connect("test");
            IMapper mapper = new Mapper(localSession);
            var email = HttpContext.User.Identity!.Name;
            DateTime today = DateTime.Today;
            var date = today.Year + "-" + today.Month + "-" + today.Day;
            var result = localSession.Execute("SELECT id FROM rok WHERE kraj_prijave<='" + date + "' AND  zavrsetak_roka>='" + date + "' ALLOW FILTERING");//sve mpguce sale
            string rokID = "";
            foreach (var i in result)
            {
                rokID = i.GetValue<string>("id");
                break;
            }
            List<PredajePredmet> result2 = mapper.Fetch<PredajePredmet>("WHERE email_profesora=? ALLOW FILTERING", email).ToList();
            List<Predmet> ispiti = new List<Predmet>();
            List<String> nazivi = new List<String>();
            foreach (var row in result2)
            {
                Predmet p = new Predmet();
                var sat = localSession.Execute("SELECT * FROM satnica WHERE rok_id='" + rokID + "' AND sifra_predmeta='" + row.Sifra_predmeta + "' ALLOW FILTERING");
                foreach (var i in sat)
                {
                    if (i.GetValue<String>("naziv_sale") == "tbd" || i.GetValue<String>("naziv_sale") == "")
                    {
                        p = mapper.Single<Predmet>("WHERE sifra_predmeta=?", row.Sifra_predmeta);
                        ispiti.Add(p);
                    }
                }
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
    [HttpPut]
    [Authorize(Roles = "Profesor")]
    [Route("updateSatnica/{sifra}/{naziv}")]
    public IActionResult updateSatnica(string sifra, string naziv)
    {
        try
        {
            Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
            Cassandra.ISession localSession = cluster.Connect("test");
            IMapper mapper = new Mapper(localSession);
            DateTime today = DateTime.Today;
            var date = today.Year + "-" + today.Month + "-" + today.Day;
            var result = localSession.Execute("SELECT id FROM rok WHERE kraj_prijave<='" + date + "' AND  zavrsetak_roka>='" + date + "' ALLOW FILTERING");//sve mpguce sale
            string rokID = "";
            foreach (var i in result)
            {
                rokID = i.GetValue<string>("id");
                break;
            }
            Sala sale = mapper.Single<Sala>("WHERE naziv=? ALLOW FILTERING", naziv);
            var sat = localSession.Execute("SELECT * FROM satnica WHERE rok_id='" + rokID + "' AND sifra_predmeta='" + sifra + "' ALLOW FILTERING");
            string satnicaID = "";
            foreach (var i in sat)
            {
                satnicaID = i.GetValue<string>("id");
                break;
            }
            var inc = localSession.Execute("UPDATE satnica SET naziv_sale='" + naziv + "' WHERE id='" + satnicaID + "' AND rok_id='" + rokID + "'");
            cluster.Shutdown();
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }
    [HttpPost]
    // [Authorize(Roles = "Profesor")]
    [Route("postPolozeniIspit")]
    public IActionResult postPolozeniIspit([FromBody] PolozeniIspiti ispit) //postavljanje ocene studentu
    {
        TypeSerializerDefinitions definitions = new TypeSerializerDefinitions();
        definitions.Define(new DateCodec());
        Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").WithTypeSerializers(definitions).Build();
        try
        {
            Cassandra.ISession localSession = cluster.Connect("test");
            IMapper mapper = new Mapper(localSession);
            DateTime today = DateTime.Today;
            var date = today.Year + "-" + today.Month + "-" + today.Day;
            DateTime today2 = DateTime.Today.AddDays(-30);
            var date2 = today.Year + "-" + today.Month + "-" + today.Day;
            var result = localSession.Execute("SELECT id FROM rok WHERE kraj_prijave<='" + date + "' AND  zavrsetak_roka>='" + date2 + "' ALLOW FILTERING");//sve mpguce sale
            string rokID = "";
            foreach (var i in result)
            {
                rokID = i.GetValue<string>("id");
                break;
            }
            ispit.Rok = rokID;
            var id = localSession.Execute("SELECT counting FROM counting_id WHERE tabela='zabranjena_prijava' ALLOW FILTERING").First();
            ispit.ID = id.GetValue<String>("counting");
            Student prof = mapper.Single<Student>("WHERE indeks=? ALLOW FILTERING", ispit.Email_Studenta);
            ispit.Email_Studenta = prof.Email;
            PolozeniIspiti result2 = mapper.FirstOrDefault<PolozeniIspiti>("WHERE sifra_predmeta=? AND email_studenta=? ALLOW FILTERING", ispit.Sifra_Predmeta, ispit.Email_Studenta);
            if (prof != null && ispit.Ocena > 5 && ispit.Ocena < 11 && result2 == null) //duplikati
            {
                mapper.Insert<PolozeniIspiti>(ispit);
                string noviID = (Int32.Parse(ispit.ID) + 1).ToString();
                var inc = localSession.Execute("UPDATE counting_id SET counting='" + noviID + "' WHERE tabela='zabranjena_prijava'");
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
}