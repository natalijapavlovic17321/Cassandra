using Cassandra;

namespace E_Student.Models;

public class PolozeniIspiti
{
    public String? ID { get; set; }
    public String? Email_Studenta { get; set; }
    public int? Ocena { get; set; }
    public String? Rok { get; set; }
    public String? Sifra_Predmeta { get; set; }
}