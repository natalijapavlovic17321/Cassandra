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
        try
        {
            Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
            Cassandra.ISession localSession = cluster.Connect("test");
            IMapper mapper = new Mapper(localSession);

            Student student = mapper.Single<Student>("WHERE email=?", email);
            student.Smer = smer; //updajtujem mu samo indeks

            mapper.Update<Student>(student);
            cluster.Shutdown();
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }

    [HttpPost]
    [Route("addRok")]
    public IActionResult AddRok([FromBody] Rok rok)
    {
        try
        {
            TypeSerializerDefinitions definitions = new TypeSerializerDefinitions();
            definitions.Define(new DateCodec());
            Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").WithTypeSerializers(definitions).Build();
            Cassandra.ISession localSession = cluster.Connect("test");

         //   Rok result = new Rok();
         //   result.Pocetak_roka = rok.Pocetak_roka;
        //    result.Kraj_prijave = rok.Kraj_prijave;
         //   result.Naziv = rok.Naziv;
        //    result.Pocetak_prijave = rok.Pocetak_prijave;
        //    result.Zavrsetak_roka = rok.Zavrsetak_roka;

            var result = localSession.Execute("insert into " + "rok" + "(" + "id" + "," + "pocetak_roka" +","+"kraj_prijave"+","+"naziv"+","+"pocetak_prijave"+","+"zavrsetak_roka" + ")" + "values('"+ rok.Id +"','"+ rok.Pocetak_roka +"','"+ rok.Kraj_prijave +"','"+ rok.Naziv +"','"+ rok.Pocetak_prijave +"','"+ rok.Zavrsetak_roka +"');");
            cluster.Shutdown();
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }
}