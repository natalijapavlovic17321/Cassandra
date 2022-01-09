using Cassandra;
using E_Student.Models;

public class MyMappings : Cassandra.Mapping.Mappings
{
    public MyMappings()
    {
        For<PrijaveIspita>().TableName("prijave_ispita").PartitionKey(x => x.Id).ClusteringKey(x => x.Rok_id)
        .Column(x => x.Email_studenta)
        .Column(x => x.Mesto)
        .Column(x => x.Naziv_sale)
        .Column(x => x.Sifra_predmeta);

        //For<Mesto>().TableName("mesto").PartitionKey(x => x.Id).ClusteringKey(x => x.Sala_naziv)
        //.Column(x => x.Mesto_br);

        For<PredajePredmet>().TableName("predaje_predmet").PartitionKey(x => x.Id)
        .Column(x => x.Sifra_predmeta)
        .Column(x => x.Email_profesora);

        For<Profesor>().TableName("profesor").PartitionKey(x => x.Email)
        .Column(x => x.Br_telefona)
        .Column(x => x.Ime)
        .Column(x => x.Kancelarija)
        .Column(x => x.Prezime);

        For<Sala>().TableName("sala").PartitionKey(x => x.Naziv)
        .Column(x => x.Kapacitet)
        .Column(x => x.Sprat);


        For<Student>().TableName("student").PartitionKey(x => x.Email)
        .Column(x => x.GodinaUpisa)
        .Column(x => x.Ime)
        .Column(x => x.Indeks)
        .Column(x => x.Odobren)
        .Column(x => x.Prezime)
        .Column(x => x.Semestar)
        .Column(x => x.Smer);


        For<Predmet>().TableName("predmet").PartitionKey(x => x.Sifra_Predmeta)
        .Column(x => x.Espb)
        .Column(x => x.NazivPredmeta, cm => cm.WithName("naziv_predmeta"))
        .Column(x => x.Semestar)
        .Column(x => x.Smer);

        For<LoginRegister>().TableName("login_register").PartitionKey(x => x.Email)
        .Column(x => x.Password_Hash)//, cm => cm.WithName("passwordHash"))
        .Column(x => x.Role)
        .Column(x => x.Salt);

        For<PolozeniIspiti>().TableName("polozeni_ispiti").PartitionKey(x => x.ID)
       .Column(x => x.Email_Studenta, cm => cm.WithName("email_studenta"))
       .Column(x => x.Ocena)
       .Column(x => x.Rok)
       .Column(x => x.Sifra_Predmeta, cm => cm.WithName("sifra_predmeta"));

        For<Obavestenje>().TableName("obavestenje").PartitionKey(x => x.Id_obavestenja)
       .Column(x => x.Datum_objave)
       .Column(x => x.Email_profesor)
       .Column(x => x.Sifra_predmeta)
       .Column(x => x.Tekst);

        For<ZabranjenaPrijava>().TableName("zabranjena_prijava").PartitionKey(x => x.Id)
        .Column(x => x.Datum_isteka)
        .Column(x => x.Email_student)
        .Column(x => x.Razlog)
        .Column(x => x.Sifra_predmeta);

        For<Rok>().TableName("rok").PartitionKey(x => x.Id)
        .Column(x => x.Pocetak_roka)
        .Column(x => x.Kraj_prijave)
        .Column(x => x.Naziv)
        .Column(x => x.Pocetak_prijave)
        .Column(x => x.Zavrsetak_roka);

    }
}
