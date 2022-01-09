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
    [HttpPost]
    [Authorize(Roles = "Student")]
    [Route("prijaviIspite")]
    public IActionResult prijaviIspite([FromBody] CCC testiram)
    {
        Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();

        try
        {
            Cassandra.ISession localSession = cluster.Connect("test");
            IMapper mapper = new Mapper(localSession);

            var ids = localSession.Execute("SELECT counting FROM counting_id WHERE tabela='prijave_ispita' ALLOW FILTERING");
            DateTime today = DateTime.Today;
            var date = today.Year + "-" + today.Month + "-" + today.Day;

            var result = localSession.Execute("SELECT id FROM rok WHERE pocetak_prijave<='" + date + "' " + "AND kraj_prijave>='" + date + "' ALLOW FILTERING");
            var rokID = "";
            foreach (var i in result)
            {
                rokID = i.GetValue<string>("id");
            }
            var id = "";
            foreach (var i in ids)
            {
                id = i.GetValue<string>("counting");
            }
            int newId = Int32.Parse(id) + 1;


            PrijaveIspita prijava = new PrijaveIspita();
            prijava.Email_studenta = HttpContext.User.Identity!.Name;
            prijava.Rok_id = rokID;


            foreach (string pr in testiram.listaSifri!)
            {
                prijava.Sifra_predmeta = pr;
                prijava.Id = newId.ToString();
                newId++;
                mapper.Insert(prijava);

            }
            localSession.Execute("UPDATE student SET dugovanje='" + testiram.dugovanje + "' WHERE email='" + HttpContext.User.Identity!.Name + "'");

            localSession.Execute("UPDATE counting_id SET counting='" + newId + "' WHERE tabela='prijave_ispita'");
            return Ok();
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
    [Route("prijavljeniIspitiUOvomRoku")]
    public IActionResult prijavljeniIspitiUovomRoku()
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

            var result0 = localSession.Execute("SELECT id FROM rok WHERE pocetak_prijave<='" + date + "' " + "AND kraj_prijave>='" + date + "' ALLOW FILTERING");
            string rokID = "";
            foreach (var i in result0)
            {
                rokID = i.GetValue<string>("id");
                break;
            }
            var ispiti = mapper.Fetch<PrijaveIspita>("WHERE email_studenta=? AND rok_id=? ALLOW FILTERING", studentEmail, rokID).ToList();
            Predmet predmet = new Predmet();
            Satnica satnica = new Satnica();
            var returnValue = new List<Object>();
            foreach (var i in ispiti)
            {
                predmet = mapper.Single<Predmet>("WHERE sifra_predmeta=?", i.Sifra_predmeta);
                var r = localSession.Execute("SELECT datum,vreme FROM satnica WHERE sifra_predmeta='" + i.Sifra_predmeta + "'ALLOW FILTERING");
                foreach (var j in r)
                {
                    satnica.Datum = j.GetValue<DateTime>("datum");
                    satnica.Vreme = j.GetValue<string>("vreme");
                    returnValue.Add(new { Naziv = predmet.NazivPredmeta, Datum = satnica.Datum, Vreme = satnica.Vreme });
                    break;
                }
            }
            return new JsonResult(returnValue);

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
    public IActionResult ispitiKojeMozePrijaviti()//dodati ako je vec prijavljen da ne moze da ga prijavljuje/ to mogu iz fetcha drugog pa na frontu da ga maknem
    {
        TypeSerializerDefinitions definitions = new TypeSerializerDefinitions();
        definitions.Define(new DateCodec());
        Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").WithTypeSerializers(definitions).Build();
        try
        {
            Cassandra.ISession localSession = cluster.Connect("test");
            IMapper mapper = new Mapper(localSession);
            var studentEmail = HttpContext.User.Identity!.Name;

            RowSet dug = localSession.Execute("SELECT dugovanje FROM student WHERE email='" + studentEmail + "'");
            string dugovanje = "";

            foreach (var i in dug)
            {
                dugovanje = i.GetValue<string>("dugovanje");
                break;
            }
            if (dugovanje != "0")
            {
                return BadRequest("Izmerite prethodna dugovanja");
            }

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

            var vecPrijavljeni = localSession.Execute("SELECT sifra_predmeta FROM prijave_ispita WHERE email_studenta='" + studentEmail + "' AND rok_id='" + rok1.Id + "' ALLOW FILTERING");
            List<String> izbaciPredmete = new List<String>(); //spajam polozene i zabrane u jednu listu stringova
            List<Predmet> mogucePrijave = new List<Predmet>(predmeti);
            foreach (var row in vecPrijavljeni)
            {
                izbaciPredmete.Add(row.GetValue<String>("sifra_predmeta"));
            }
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