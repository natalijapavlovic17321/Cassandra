import predmet from "./predmet.js"
class profesor{
    consructor(id, email,ime,prezime,godina,brtel, kancelarija){
        this.kontejnerProfesor=null;
        this.id = id;
        this.ime = ime;
        this.prezime = prezime;
        this.email = email;
        this.brtel = brtel;
        this.kancelarija = kancelarija;
        this.ispiti=[]
    }
    dodajProfesor(id, email,ime,prezime,godina,brtel, kancelarija){
        this.id = id;
        this.ime = ime;
        this.prezime = prezime;
        this.email = email;
        this.brtel = brtel;
        this.kancelarija = kancelarija;
    }
    crtajProfesor(){
        const host= document.getElementById("bodyProfesor");
        if (!host)
        throw new Error("Greska u hostu");

        this.crtajProfesorHTML(document.getElementById("mainProfesor"));
        this.crtajIspite("ispitiProfesor")
        this.crtajDodavanjeObavestenja(document.getElementById("obavestenjaProfesor"));
        this.crtajZabranaPrijave(document.getElementById("zabranaProfesor"));
        this.crtajRaspodelaMesta(document.getElementById("mestaProfesor"));
    }
    crtajProfesorHTML(host) {
        document.getElementById("imeUser").innerHTML = this.ime + " " +this.prezime;
        document.getElementById("emailUser").innerHTML = this.email;
        document.getElementById("kancelarijaUser").innerHTML = this.kancelarija;
        document.getElementById("brojUser").innerHTML = this.brtel;
    }
    crtajIspite(host)
    {
        this.PreuzmiIspite();
        //crtanje svih ispita na kojima on predaje
        //izlistavanje liste
    }
    crtajDodavanjeObavestenja(host)
    {
        var dugmeObavestenja=document.createElement("button");
        dugmeObavestenja.innerHTML="Dodaj Obaveštenje"
        host.appendChild(dugmeObavestenja);
    }
    crtajZabranaPrijave(host)
    {
        var zabranaPrijave=document.createElement("button");
        zabranaPrijave.innerHTML="Dodaj Zabranu"
        host.appendChild(zabranaPrijave);
    }
    crtajDodavanjeObavestenja(host)
    {
        var dugmeObavestenja=document.createElement("button");
        dugmeObavestenja.innerHTML="Dodaj Obaveštenje"
        host.appendChild(dugmeObavestenja);
    }
    PreuzmiIspite()
    {
        //fetch
    }
}