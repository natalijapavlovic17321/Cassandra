//s
using Cassandra;
using E_Student.Models;
using Microsoft.AspNetCore.Mvc;
using Cassandra.Mapping;
using Cassandra.Serialization;
using E_Student.Converters;
using Microsoft.AspNetCore.Authorization;

namespace E_Student.Controllers;

public class MatejaController : ControllerBase
{
    [Authorize(Roles = "Administrator")]
    [HttpPut]
    [Route("updateStudent/{email}/{smer}")]
    public IActionResult UpdateStudentSmer(string email, string smer)
    {
        Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();

        try
        {

            Cassandra.ISession localSession = cluster.Connect("test");
            IMapper mapper = new Mapper(localSession);

            Student student = mapper.Single<Student>("WHERE email=?", email);
            student.Smer = smer;

            mapper.Update<Student>(student);
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
    [Authorize(Roles = "Administrator")]
    [HttpPost]
    [Route("addRok")]
    public IActionResult AddRok([FromBody] Rok rok)
    {
        TypeSerializerDefinitions definitions = new TypeSerializerDefinitions();
        definitions.Define(new DateCodec());
        Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").WithTypeSerializers(definitions).Build();
        try
        {
            Cassandra.ISession localSession = cluster.Connect("test");
            IMapper mapper = new Mapper(localSession);
            var id = localSession.Execute("SELECT counting FROM counting_id WHERE tabela='rok' ALLOW FILTERING").First();
            rok.Id = id.GetValue<String>("counting");
            if (rok != null)
            {
                mapper.Insert<Rok>(rok);
                string noviID = (Int32.Parse(rok.Id) + 1).ToString();
                var inc = localSession.Execute("UPDATE counting_id SET counting='" + noviID + "' WHERE tabela='rok'");
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
    [Authorize(Roles = "Administrator")]
    [HttpDelete]
    [Route("deleteRok/{id}")]
    public IActionResult DeleteRok(string id)
    {
        TypeSerializerDefinitions definitions = new TypeSerializerDefinitions();
        definitions.Define(new DateCodec());
        Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").WithTypeSerializers(definitions).Build();
        try
        {
            Cassandra.ISession localSession = cluster.Connect("test");
            var result = localSession.Execute("DELETE FROM rok WHERE id='" + id + "'");
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
    [HttpPost]
    [Authorize(Roles = "Administrator")]
    [Route("addPredmet")]
    public IActionResult AddPredmet([FromBody] Predmet predmet)
    {
        Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();

        try
        {
            Cassandra.ISession localSession = cluster.Connect("test");
            IMapper mapper = new Mapper(localSession);
            Predmet provera = new Predmet();
            List<Predmet> result = mapper.Fetch<Predmet>("WHERE sifra_predmeta=? ALLOW FILTERING", predmet.Sifra_Predmeta).ToList();
            foreach (var p in result)
            {
                provera.NazivPredmeta = p.NazivPredmeta;
            }
            List<Predmet> result2 = mapper.Fetch<Predmet>("WHERE naziv_predmeta=? ALLOW FILTERING", predmet.NazivPredmeta).ToList();
            foreach (var p in result2)
            {
                provera.Sifra_Predmeta = p.Sifra_Predmeta;
            }
            if (predmet != null && provera.NazivPredmeta == null && provera.Sifra_Predmeta == null)
            {
                mapper.Insert<Predmet>(predmet);
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
   /* [Authorize(Roles = "Administrator")]
    [HttpPut]
    [Route("updatePredmet")]
    public IActionResult UpdatePredmet([FromBody] Predmet predmet)
    {
        Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();

        try
        {
            Cassandra.ISession localSession = cluster.Connect("test");
            IMapper mapper = new Mapper(localSession);

            mapper.Update<Predmet>(predmet);
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
    }*/
    [Authorize(Roles = "Administrator")]
    [HttpDelete]
    [Route("deletePredmet/{sifra}")]
    public IActionResult DeleteZabrana(string sifra)
    {
        TypeSerializerDefinitions definitions = new TypeSerializerDefinitions();
        definitions.Define(new DateCodec());
        Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").WithTypeSerializers(definitions).Build();
        try
        {
            Cassandra.ISession localSession = cluster.Connect("test");
            var result = localSession.Execute("DELETE FROM predmet WHERE sifra_predmeta='" + sifra + "'");
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
    [Authorize(Roles = "Administrator")]
    [HttpPost]
    [Route("addSala")]
    public IActionResult AddSala([FromBody] Sala sala)
    {
        Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();

        try
        {
            Cassandra.ISession localSession = cluster.Connect("test");
            IMapper mapper = new Mapper(localSession);
            Sala provera = new Sala();
            List<Sala> result = mapper.Fetch<Sala>("WHERE naziv=? ALLOW FILTERING", sala.Naziv).ToList();
            foreach (var p in result)
            {
                provera.Naziv = p.Naziv;
            }
            if (sala != null && provera.Naziv == null)
            {
                mapper.Insert<Sala>(sala);
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
    [Authorize(Roles = "Administrator")]
    [HttpDelete]
    [Route("deleteSala/{naziv}")]
    public IActionResult DeleteSala(string naziv)
    {
        TypeSerializerDefinitions definitions = new TypeSerializerDefinitions();
        definitions.Define(new DateCodec());
        Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").WithTypeSerializers(definitions).Build();
        try
        {
            Cassandra.ISession localSession = cluster.Connect("test");
            var result = localSession.Execute("DELETE FROM sala WHERE naziv='" + naziv + "'");
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
    [Authorize(Roles = "Administrator")]
    [HttpPost]
    [Route("addSatnica")]
    public IActionResult AddSatnica([FromBody] Satnica satnica)
    {
        TypeSerializerDefinitions definitions = new TypeSerializerDefinitions();
        definitions.Define(new DateCodec());
        Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").WithTypeSerializers(definitions).Build();

        try
        {
            Cassandra.ISession localSession = cluster.Connect("test");
            IMapper mapper = new Mapper(localSession);
            var id = localSession.Execute("SELECT counting FROM counting_id WHERE tabela='satnica' ALLOW FILTERING").First();
            Satnica provera = new Satnica();
            var proveraDatuma = localSession.Execute("SELECT * FROM rok");
            bool t = false;
            foreach (var p in proveraDatuma)
            {
                Rok r = new Rok();
                r.Pocetak_roka = p.GetValue<DateTime>("pocetak_roka");
                r.Zavrsetak_roka = p.GetValue<DateTime>("zavrsetak_roka");
                r.Id = p.GetValue<String>("id");
                if (r.Pocetak_roka <= satnica.Datum && r.Zavrsetak_roka >= satnica.Datum)
                {
                    satnica.Rok_id = r.Id;
                    t = true;
                }
            }
            var result = localSession.Execute("SELECT rok_id FROM satnica WHERE rok_id='" + satnica.Rok_id + "' AND sifra_predmeta='" + satnica.Sifra_predmeta + "' ALLOW FILTERING");
            foreach (var p in result)
            {
                provera.Rok_id = p.GetValue<String>("rok_id");
            }
            var proveraSifrePredmeta = localSession.Execute("SELECT sifra_predmeta FROM predmet WHERE sifra_predmeta='" + satnica.Sifra_predmeta + "' ALLOW FILTERING");
            Satnica proveraSifre = new Satnica();
            foreach (var p in proveraSifrePredmeta)
            {
                proveraSifre.Sifra_predmeta = p.GetValue<String>("sifra_predmeta");
            }
            satnica.Id = id.GetValue<String>("counting");
            if (satnica != null && provera.Rok_id == null && t == true && proveraSifre.Sifra_predmeta != null)
            {
                satnica.Naziv_sale = "tbd";
                mapper.Insert<Satnica>(satnica);
                string noviID = (Int32.Parse(satnica.Id) + 1).ToString();
                var inc = localSession.Execute("UPDATE counting_id SET counting='" + noviID + "' WHERE tabela='satnica'");
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
    [Authorize(Roles = "Administrator")]
    [HttpDelete]
    [Route("deleteSatnica/{id}")]
    public IActionResult DeleteSatnica(string id)
    {
        TypeSerializerDefinitions definitions = new TypeSerializerDefinitions();
        definitions.Define(new DateCodec());
        Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").WithTypeSerializers(definitions).Build();
        try
        {
            Cassandra.ISession localSession = cluster.Connect("test");
            var result = localSession.Execute("DELETE FROM satnica WHERE id='" + id + "'");
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
    [Authorize(Roles = "Administrator")]
    [HttpPost]
    [Route("addProfesor")]
    public IActionResult AddProfesor([FromBody] Profesor profesor)
    {
        Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();

        try
        {
            Cassandra.ISession localSession = cluster.Connect("test");
            IMapper mapper = new Mapper(localSession);
            Profesor provera = new Profesor();
            List<Profesor> result = mapper.Fetch<Profesor>("WHERE email=? ALLOW FILTERING", profesor.Email).ToList();
            foreach (var p in result)
            {
                provera.Br_telefona = p.Br_telefona;
            }
            List<Profesor> result2 = mapper.Fetch<Profesor>("WHERE br_telefona=? ALLOW FILTERING", profesor.Br_telefona).ToList();
            foreach (var p in result2)
            {
                provera.Email = p.Email;
            }
            if (profesor != null && provera.Br_telefona == null && provera.Email == null)
            {
                mapper.Insert<Profesor>(profesor);
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
    [Authorize(Roles = "Administrator")]
    [HttpPut]
    [Route("acceptStudent/{email}")]
    public IActionResult AcceptStudent(string email)
    {
        Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();

        try
        {

            Cassandra.ISession localSession = cluster.Connect("test");
            IMapper mapper = new Mapper(localSession);
            Student student = mapper.Single<Student>("WHERE email=?", email);

            if (student != null)
            {
                student.Odobren = true;
                mapper.Update<Student>(student);
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

    [HttpGet]
    [Authorize(Roles = "Administrator")]
    [Route("getValidStudents")]
    public List<Student> GetValidStudents()
    {
        Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
        Cassandra.ISession localSession = cluster.Connect("test");
        IMapper mapper = new Mapper(localSession);
        Student provera = new Student();
        List<Student> studenti = new List<Student>();
        var student = localSession.Execute("SELECT * FROM student");
        foreach (var i in student)
        {
            provera.Odobren = i.GetValue<bool>("odobren");
            provera.Email = i.GetValue<String>("email");
            if (provera.Odobren == true)
            {
                List<Student> upis = mapper.Fetch<Student>("WHERE email=? ALLOW FILTERING", provera.Email).ToList();
                studenti.Add(upis[0]);
            }
        }
        cluster.Shutdown();

        return studenti;
    }

    [Authorize(Roles = "Administrator")]
    [HttpPut]
    [Route("StudentPay/{email}")]
    public IActionResult StudentPay(string email)
    {
        Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();

        try
        {

            Cassandra.ISession localSession = cluster.Connect("test");
            IMapper mapper = new Mapper(localSession);
            Student student = mapper.Single<Student>("WHERE email=?", email);

            if (student != null)
            {
                student.Dugovanje = "0";
                mapper.Update<Student>(student);
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

    [HttpGet]
    [Authorize(Roles = "Administrator")]
    [Route("getSubjects")]
    public IActionResult getSubjects()
    {
        Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
        try
        {
            Cassandra.ISession localSession = cluster.Connect("test");
            IMapper mapper = new Mapper(localSession);

            var result = mapper.Fetch<Predmet>("SELECT * from predmet");
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
    [Authorize(Roles = "Administrator")]
    [Route("getRokovi")]
    public IActionResult getRokovi()
    {
        TypeSerializerDefinitions definitions = new TypeSerializerDefinitions();
        definitions.Define(new DateCodec());
        Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").WithTypeSerializers(definitions).Build();
        try
        {
            Cassandra.ISession localSession = cluster.Connect("test");
            IMapper mapper = new Mapper(localSession);
            List<Rok> rokovi = new List<Rok>();
            var result = localSession.Execute("SELECT * from rok ALLOW FILTERING");
            foreach (var p in result)
            {
                Rok rok = new Rok();
                rok.Id = p.GetValue<String>("id");
                rok.Naziv = p.GetValue<String>("naziv");
                rok.Pocetak_roka = p.GetValue<DateTime>("pocetak_roka");
                rok.Pocetak_prijave = p.GetValue<DateTime>("pocetak_prijave");
                rok.Kraj_prijave = p.GetValue<DateTime>("kraj_prijave");
                rok.Zavrsetak_roka = p.GetValue<DateTime>("zavrsetak_roka");
                rokovi.Add(rok);
            }
            return new JsonResult(rokovi);
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
    [Authorize(Roles = "Administrator")]
    [Route("getSale")]
    public IActionResult getSale()
    {
        Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
        try
        {
            Cassandra.ISession localSession = cluster.Connect("test");
            IMapper mapper = new Mapper(localSession);

            var result = mapper.Fetch<Sala>("SELECT * from sala");
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
    [Authorize(Roles = "Administrator")]
    [Route("getSatnice")]
    public IActionResult getSatnice()
    {
        TypeSerializerDefinitions definitions = new TypeSerializerDefinitions();
        definitions.Define(new DateCodec());
        Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").WithTypeSerializers(definitions).Build();
        try
        {
            Cassandra.ISession localSession = cluster.Connect("test");
            IMapper mapper = new Mapper(localSession);
            List<Satnica> satnice = new List<Satnica>();
            var result = localSession.Execute("SELECT * from satnica ALLOW FILTERING");
            foreach (var p in result)
            {
                Satnica satnica = new Satnica();
                satnica.Id = p.GetValue<String>("id");
                satnica.Rok_id = p.GetValue<String>("rok_id");
                satnica.Naziv_sale = p.GetValue<String>("naziv_sale");
                satnica.Datum = p.GetValue<DateTime>("datum");
                satnica.Sifra_predmeta = p.GetValue<String>("sifra_predmeta");
                satnica.Vreme = p.GetValue<String>("vreme");
                satnice.Add(satnica);
            }
            return new JsonResult(satnice);
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

    [Authorize(Roles = "Administrator")]
    [HttpDelete]
    [Route("deleteAccProf/{email;}")]
    public IActionResult deleteAccProf(string email)
    {
        TypeSerializerDefinitions definitions = new TypeSerializerDefinitions();
        definitions.Define(new DateCodec());
        Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").WithTypeSerializers(definitions).Build();
        try
        {
            Cassandra.ISession localSession = cluster.Connect("test");
            var result = localSession.Execute("DELETE FROM login_register WHERE email='" + email + "'");
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
    [Authorize(Roles = "Administrator")]
    [HttpDelete]
    [Route("deleteProfesor/{email}")]
    public IActionResult deleteProfesor(string email)
    {
        try
        {
            Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
            Cassandra.ISession localSession = cluster.Connect("test");
            IMapper mapper = new Mapper(localSession);

            mapper.Delete<Profesor>(mapper.Single<Profesor>("WHERE email=?", email));
            cluster.Shutdown();
            deleteAccProf(email);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }

    [HttpGet]
    [Authorize(Roles = "Administrator")]
    [Route("getProfesore")]
    public IActionResult getProfesore()
    {
        Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
        try
        {
            Cassandra.ISession localSession = cluster.Connect("test");
            IMapper mapper = new Mapper(localSession);

            var result = mapper.Fetch<Profesor>("SELECT * from profesor");
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

    [Authorize(Roles = "Administrator")]
    [HttpPost]
    [Route("AddPredajePredmet")]
    public IActionResult AddPredajePredmet([FromBody] PredajePredmet predaje)
    {
        Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();

        try
        {
            Cassandra.ISession localSession = cluster.Connect("test");
            IMapper mapper = new Mapper(localSession);
            var id = localSession.Execute("SELECT counting FROM counting_id WHERE tabela='predaje_predmet' ALLOW FILTERING").First();
            predaje.Id = id.GetValue<String>("counting");
            List<PredajePredmet> provera = mapper.Fetch<PredajePredmet>("SELECT * FROM predaje_predmet WHERE email_profesora=? AND sifra_predmeta=? ALLOW FILTERING", predaje.Email_profesora, predaje.Sifra_predmeta).ToList();
            var provera2 = mapper.Fetch<Predmet>("SELECT * FROM predmet WHERE sifra_predmeta=?", predaje.Sifra_predmeta).ToList();
            if (predaje != null && provera.Count == 0 && provera2.Count != 0)
            {
                mapper.Insert<PredajePredmet>(predaje);
                string noviID = (Int32.Parse(predaje.Id) + 1).ToString();
                var inc = localSession.Execute("UPDATE counting_id SET counting='" + noviID + "' WHERE tabela='predaje_predmet'");
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

    [HttpGet]
    [Route("VratiPredmeteKojeNePredaje/{email}/{predmet}")]
    public IActionResult VratiPredmeteKojeNePredaje(string email)
    {
        Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();

        try
        {
            Cassandra.ISession localSession = cluster.Connect("test");
            IMapper mapper = new Mapper(localSession);
            List<PredajePredmet> predaje = mapper.Fetch<PredajePredmet>("SELECT sifra_predmeta FROM predaje_predmet WHERE email_profesora='" + email + "' ALLOW FILTERING").ToList();
            List<Predmet> predmet = mapper.Fetch<Predmet>("SELECT * FROM predmet").ToList();
            List<Predmet> result = new List<Predmet>();
            bool t = false;
            foreach (var p in predmet)
            {
                for (int i = 0; i < predaje.Count; i++)
                {
                    if (p.Sifra_Predmeta == predaje[i].Sifra_predmeta)
                        {
                            t = true;
                        }
                }
                if (t == false)
                {
                    result.Add(p);
                }
                t = false;
            }       
            if (email != null)
            {
                return new JsonResult(result);
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