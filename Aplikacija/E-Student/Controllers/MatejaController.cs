//s
using Cassandra;
using E_Student.Models;
using Microsoft.AspNetCore.Mvc;
using Cassandra.Mapping;
using Cassandra.Serialization;
using E_Student.Converters;

namespace E_Student.Controllers;

public class MatejaController : ControllerBase
{
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
            rok.Id=id.GetValue<String>("counting");
            if(rok != null)
            {
                mapper.Insert<Rok>(rok);
                string noviID=(Int32.Parse(rok.Id)+1).ToString();
                var inc = localSession.Execute("UPDATE counting_id SET counting='"+noviID+"' WHERE tabela='rok'");
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
    [Route("deleteRok/{id}")]
    public IActionResult DeleteRok(string id)
    {
        TypeSerializerDefinitions definitions = new TypeSerializerDefinitions();
        definitions.Define(new DateCodec());
        Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").WithTypeSerializers(definitions).Build();
        try
        {
            Cassandra.ISession localSession = cluster.Connect("test");
            var result=localSession.Execute("DELETE FROM rok WHERE id='"+id+"'");        
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
            if(predmet != null && provera.NazivPredmeta == null && provera.Sifra_Predmeta == null)
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

    [HttpPut]
    [Route("updatePredmet/{sifra}/{naziv}/{espb}/{semestar}/{smer}")]
    public IActionResult UpdatePredmet(string sifra, string naziv, string espb, string semestar, string smer)
    {
        Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();

        try
        {        
            Cassandra.ISession localSession = cluster.Connect("test");
            IMapper mapper = new Mapper(localSession);

            Predmet predmet = mapper.Single<Predmet>("WHERE sifra_predmeta=?", sifra);
            predmet.NazivPredmeta = naziv;
            predmet.Espb = espb;
            predmet.Semestar = semestar;
            predmet.Smer = smer;

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
    }

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
            var result=localSession.Execute("DELETE FROM predmet WHERE sifra_predmeta='"+sifra+"'");
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
            if(sala != null && provera.Naziv == null)
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
            var result=localSession.Execute("DELETE FROM sala WHERE naziv='"+naziv+"'");
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
    [Route("addMesto")]
    public IActionResult AddMesto([FromBody] Mesto mesto)
    {
        Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();

        try
        {
            Cassandra.ISession localSession = cluster.Connect("test");
            IMapper mapper = new Mapper(localSession);
            var id = localSession.Execute("SELECT counting FROM counting_id WHERE tabela='mesto' ALLOW FILTERING").First();
            Sala provera = new Sala();
            List<Sala> result = mapper.Fetch<Sala>("WHERE naziv=? ALLOW FILTERING", mesto.Sala_naziv).ToList();
            foreach (var p in result)
            {
               provera.Naziv = p.Naziv;
            }
            mesto.Id=id.GetValue<String>("counting");
            mesto.Mesto_br = Int32.Parse(mesto.Id);
            if(mesto != null && provera.Naziv != null)
            {
                mapper.Insert<Mesto>(mesto);
                string noviID=(Int32.Parse(mesto.Id)+1).ToString();
                var inc = localSession.Execute("UPDATE counting_id SET counting='"+noviID+"' WHERE tabela='mesto'");
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
    [Route("deleteMesto/{id}")]
    public IActionResult DeleteMesto(string id)
    {
        TypeSerializerDefinitions definitions = new TypeSerializerDefinitions();
        definitions.Define(new DateCodec());
        Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").WithTypeSerializers(definitions).Build();
        try
        {
            Cassandra.ISession localSession = cluster.Connect("test");
            var result=localSession.Execute("DELETE FROM mesto WHERE id='"+id+"'");
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
            if(profesor != null && provera.Br_telefona == null && provera.Email == null)
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
}