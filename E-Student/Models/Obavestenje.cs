namespace E_Student.Models;
public class Obavestenje
{
    public String? Id_obavestenja { get; set; }
    public DateTime Datum_objave { get; set; }
    public String? Email_profesor { get; set; }
    public String? Sifra_predmeta { get; set; }
    public String? Tekst { get; set; }
}

