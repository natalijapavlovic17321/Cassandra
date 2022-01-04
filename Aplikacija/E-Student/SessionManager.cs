using Cassandra;
namespace E_Student
{
    public class SessionManager //Mozda ce koristimo mozda ne
    {
        public static Cassandra.ISession? _session;

        public static Cassandra.ISession GetSession()
        {
            if (_session == null)
            {
                Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
                _session = cluster.Connect("test");
            }
            return _session;
        }
    }

}