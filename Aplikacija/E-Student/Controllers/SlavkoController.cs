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
public class SlavkoController : ControllerBase
{
    [HttpGet]
    [Route("getCurrentExasmPeriodInformations")]
    public IActionResult prijavljeni()
    {
        Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();

        try
        {
            Cassandra.ISession localSession = cluster.Connect("test");
            IMapper mapper = new Mapper(localSession);
            List<PrijaveIspita> predmeti = new List<PrijaveIspita>();
            predmeti = mapper.Fetch<PrijaveIspita>().ToList();
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
    [Route("getCurrentExamPeriodInformations")]
    public IActionResult getCurrentExamPeriodInformations()
    {
        TypeSerializerDefinitions definitions = new TypeSerializerDefinitions();
        definitions.Define(new DateCodec());
        Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").WithTypeSerializers(definitions).Build();
        try
        {
            Cassandra.ISession localSession = cluster.Connect("test");

            DateTime today = DateTime.Today;


            var date = today.Year + "-" + today.Month + "-" + today.Day;
            var result = localSession.Execute("SELECT * FROM rok WHERE pocetak_prijave<='" + date + "' " + "AND kraj_prijave>='" + date + "' ALLOW FILTERING");

            Rok rok = new Rok();
            foreach (var row in result)
            {
                rok.Id = row.GetValue<string>("id");
                rok.Naziv = row.GetValue<string>("naziv");
                rok.Pocetak_prijave = row.GetValue<DateTime>("pocetak_roka");
                rok.Kraj_prijave = row.GetValue<DateTime>("kraj_prijave");
                rok.Pocetak_roka = row.GetValue<DateTime>("pocetak_roka");
                rok.Zavrsetak_roka = row.GetValue<DateTime>("zavrsetak_roka");
            }

            return new JsonResult(rok);

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
    [Route("ispitiKojeMozePrijaviti")]
    public IActionResult ispitiKojeMozePrijaviti()
    {
        TypeSerializerDefinitions definitions = new TypeSerializerDefinitions();
        definitions.Define(new DateCodec());
        Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").WithTypeSerializers(definitions).Build();
        try
        {
            Cassandra.ISession localSession = cluster.Connect("test");
            IMapper mapper = new Mapper(localSession);
            var studentEmail = HttpContext.User.Identity!.Name;

            DateTime today = DateTime.Today;
            var date = today.Year + "-" + today.Month + "-" + today.Day;
            var result0 = localSession.Execute("SELECT * FROM rok WHERE pocetak_prijave<='" + date + "' " + "AND kraj_prijave>='" + date + "' ALLOW FILTERING");

            Rok rok1 = new Rok();
            foreach (var row in result0)
            {
                rok1.Id = row.GetValue<string>("id");
                rok1.Naziv = row.GetValue<string>("naziv");
                rok1.Pocetak_prijave = row.GetValue<DateTime>("pocetak_roka");
                rok1.Kraj_prijave = row.GetValue<DateTime>("kraj_prijave");
                rok1.Pocetak_roka = row.GetValue<DateTime>("pocetak_roka");
                rok1.Zavrsetak_roka = row.GetValue<DateTime>("zavrsetak_roka");
            }


            Student student = mapper.Single<Student>("WHERE email=? ALLOW FILTERING", studentEmail);
            if (student.Odobren == false)
            {
                return BadRequest("Studentu nije odobren account");
            }
            List<Predmet> predmeti = mapper.Fetch<Predmet>("WHERE smer=? AND semestar<=? ALLOW FILTERING", student.Smer, student.Semestar).ToList();//uzimam sve predmete
            var polozeni = localSession.Execute("SELECT sifra_predmeta FROM polozeni_ispiti WHERE email_studenta='" + student.Email + "' ALLOW FILTERING");//uzimam predmete koje je polozio
            var result = localSession.Execute("SELECT sifra_predmeta FROM zabranjena_prijava WHERE email_student='" + student.Email + "' AND datum_isteka>='" + date + "' ALLOW FILTERING");//uzimam predmete gde ima zabranu

            List<String> izbaciPredmete = new List<String>(); //spajam polozene i zabrane u jednu listu stringova
            List<Predmet> mogucePrijave = new List<Predmet>(predmeti);
            foreach (var row in polozeni)
            {
                izbaciPredmete.Add(row.GetValue<String>("sifra_predmeta"));
            }
            foreach (var row in result)
            {
                izbaciPredmete.Add(row.GetValue<String>("sifra_predmeta"));
            }
            foreach (var p in predmeti)// izbacujem prdemete koje je polozio ili gde ima zabranu
            {
                foreach (var i in izbaciPredmete)
                {
                    if (p.Sifra_Predmeta == i)
                    {
                        mogucePrijave.Remove(p);
                    }
                }

            }
            var returnValue = new List<object>();//vracam predmet + broj prijava, cela funckija je malcice lose napisana ali radi ^^
            foreach (var p in mogucePrijave)
            {
                Int64 br = 0;
                RowSet broj = localSession.Execute("SELECT COUNT(*) FROM prijave_ispita WHERE email_studenta='" + student.Email + "' AND sifra_predmeta='" + p.Sifra_Predmeta + "' ALLOW FILTERING");
                foreach (var b in broj)
                {
                    br = b.GetValue<Int64>("count");
                    break;
                }
                RowSet rok = localSession.Execute("SELECT datum , vreme FROM satnica WHERE rok_id='" + rok1.Id + "' AND sifra_predmeta='" + p.Sifra_Predmeta + "' ALLOW FILTERING");
                var div = new List<object>();
                foreach (var b in rok)
                {
                    var datum = b.GetValue<DateTime>("datum");
                    var vreme = b.GetValue<string>("vreme");
                    div.Add(new { datum, vreme });
                    break;
                }

                returnValue.Add(new
                {
                    Predmet = p,
                    BrojPrijava = br,
                    DatumIVreme = div
                });
            }
            return new JsonResult(new
            {
                Rok = rok1,
                InfoPredmeti = returnValue
            });
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

}