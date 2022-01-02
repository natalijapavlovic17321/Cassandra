using Cassandra;
using E_Student.Models;

public class MyMappings : Cassandra.Mapping.Mappings
{
    public MyMappings()
    {
        For<Student>().TableName("student").PartitionKey(x => x.Email)
        .Column(x => x.Indeks)
        .Column(x => x.GodinaUpisa)
        .Column(x => x.Ime)
        .Column(x => x.Prezime)
        .Column(x => x.Semestar)
        .Column(x => x.Sifra)
        .Column(x => x.Smer);

        For<Predmet>().TableName("predmet").PartitionKey(x => x.Sifra_Predmeta)
        .Column(x => x.Espb)
        .Column(x => x.NazivPredmeta, cm => cm.WithName("naziv_predmeta"))
        .Column(x => x.Semestar)
        .Column(x => x.Smer);

        For<LoginRegisterModels>().TableName("login_register").PartitionKey(x => x.Email)
        .Column(x => x.Password_Hash)//, cm => cm.WithName("passwordHash"))
        .Column(x => x.Role)
        .Column(x => x.Salt);


        For<PolozeniIspiti>().TableName("polozeni_ispiti").PartitionKey(x => x.ID)
       .Column(x => x.Datum)
       .Column(x => x.Email_Studenta, cm => cm.WithName("email_studenta"))
       .Column(x => x.Ocena)
       .Column(x => x.Sifra_Predmeta, cm => cm.WithName("sifra_predmeta"));

    }
}
