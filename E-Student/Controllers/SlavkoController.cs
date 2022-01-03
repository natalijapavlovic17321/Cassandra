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
    [Route("getPrijavljeni")]
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
    [Route("test2")]
    public IActionResult prijavljeni2()
    {
        Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();

        try
        {
            Cassandra.ISession localSession = cluster.Connect("test");
            IMapper mapper = new Mapper(localSession);
            List<Mesto> predmeti = new List<Mesto>();
            predmeti = mapper.Fetch<Mesto>().ToList();
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
    [Route("test3")]
    public IActionResult prijavljeni3()
    {
        Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();

        try
        {
            Cassandra.ISession localSession = cluster.Connect("test");
            IMapper mapper = new Mapper(localSession);
            List<PredajePredmet> predmeti = new List<PredajePredmet>();
            predmeti = mapper.Fetch<PredajePredmet>().ToList();
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
    [Route("test4")]
    public IActionResult prijavljeni4()
    {
        Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();

        try
        {
            Cassandra.ISession localSession = cluster.Connect("test");
            IMapper mapper = new Mapper(localSession);
            List<Profesor> predmeti = new List<Profesor>();
            predmeti = mapper.Fetch<Profesor>().ToList();
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
    [Route("test5")]
    public IActionResult prijavljeni5()
    {
        Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();

        try
        {
            Cassandra.ISession localSession = cluster.Connect("test");
            IMapper mapper = new Mapper(localSession);
            List<Sala> predmeti = new List<Sala>();
            predmeti = mapper.Fetch<Sala>().ToList();
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
}