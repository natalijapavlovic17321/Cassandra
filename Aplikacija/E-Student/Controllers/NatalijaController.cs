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
    [Route("getProfesorIspiti/{email}")]
    public IActionResult profesorIspiti(string email) //preuzimanje info o predmetima nekog profesora
    {
        Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();

        try
        {
            Cassandra.ISession localSession = cluster.Connect("test");
            IMapper mapper = new Mapper(localSession);
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
    [Route("getAllAboutProfesor/{email}")]
    public IActionResult getAllAboutProfesor(string email) //preuzimanje info o predmetima nekog profesora i info
    {
        Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();

        try
        {
            Cassandra.ISession localSession = cluster.Connect("test");
            IMapper mapper = new Mapper(localSession);
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
    [Route("getProfesorIspitiNazivi/{email}")]
    public IActionResult profesorNazivi(string email) //preuzimanje info o predmetima nekog profesora
    {
        Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();

        try
        {
            Cassandra.ISession localSession = cluster.Connect("test");
            IMapper mapper = new Mapper(localSession);
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
    [Route("getZabrana/{email}")]
    public IActionResult ZabraneProfesora(string email) //preuzimanje info o svim zabranama s kojima profesor moze da barata
    {
        TypeSerializerDefinitions definitions = new TypeSerializerDefinitions();
        definitions.Define(new DateCodec());
        Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").WithTypeSerializers(definitions).Build();
        try
        {
            Cassandra.ISession localSession = cluster.Connect("test");
            IMapper mapper = new Mapper(localSession);
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
            Student prof = mapper.Single<Student>("WHERE email=?", zabrana.Email_student);
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
                if(i.GetValue<string>("naziv_sale")!="tbd")
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
                    if (i.Naziv == j )
                    {
                        slovodneSale.Remove(i);
                    }
                }
                if(i.Kapacitet<br)
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
    [Route("getIspitiZaSalu/{email}")]
    public IActionResult ispitiZaSalu(string email) //svi ispiti za koje sale jos nisu zauzete
    {
        Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();

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
    [Route("updateSatnica/{sifra}")]
    public IActionResult updateSatnica(string sifra, [FromBody] Satnica satnica)
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
            Sala sale = mapper.Single<Sala>("WHERE naziv=? ALLOW FILTERING", satnica.Naziv_sale);
            var sat = localSession.Execute("SELECT * FROM satnica WHERE rok_id='" + rokID + "' AND sifra_predmeta='" + sifra + "' ALLOW FILTERING");
            string satnicaID = "";
            foreach (var i in sat)
            {
                satnicaID = i.GetValue<string>("id");
                break;
            }
            var inc = localSession.Execute("UPDATE satnica SET naziv_sale='" + satnica.Naziv_sale + "' WHERE id='" + satnicaID + "' AND rok_id='" + rokID + "'");
            cluster.Shutdown();
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }
}