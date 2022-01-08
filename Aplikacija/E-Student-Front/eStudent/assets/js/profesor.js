import {predmet} from "./predmet.js"
class profesor{
    consructor(email,ime,prezime,godina,brtel, kancelarija){
        this.kontejnerProfesor=null;
        this.ime = ime;
        this.prezime = prezime;
        this.email = email;
        this.brtel = brtel;
        this.kancelarija = kancelarija;
        this.ispiti=[]
    }
    dodajProfesor(email,ime,prezime,godina,brtel, kancelarija){
        this.ime = ime;
        this.prezime = prezime;
        this.email = email;
        this.brtel = brtel;
        this.kancelarija = kancelarija;
    }
    dodajPredmet(p)
    {
        this.ispiti.append(p);
    }
    setEmail(e)
    {
        this.email=e;
    }
    crtajProfesor(){
        const host= document.getElementById("bodyProfesor");
        if (!host)
        throw new Error("Greska u hostu");

        this.crtajProfesorHTML(document.getElementById("mainProfesor"));
        this.crtajIspite("crtajPredmeteprofesora")
        this.crtajDodavanjeObavestenja(document.getElementById("obavestenjaProfesor"));
        this.crtajZabranaPrijave(document.getElementById("zabranaProfesor"));
        this.crtajRaspodelaMesta(document.getElementById("mestaProfesor"));
    }
    crtajProfesorHTML(host) {
        document.getElementById("imeProf").innerHTML = this.ime  +this.prezime;
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
        //btn on click
        host.appendChild(dugmeObavestenja);
    }
    crtajZabranaPrijave(host)
    {
        var zabranaPrijave=document.createElement("button");
        zabranaPrijave.innerHTML="Dodaj Zabranu"
        //btn on click
        host.appendChild(zabranaPrijave);
    }
    crtajDodavanjeObavestenja(host)
    {
        var dugmeObavestenja=document.createElement("button");
        dugmeObavestenja.innerHTML="Dodaj Obaveštenje"
        //btn on click
        host.appendChild(dugmeObavestenja);
    }
    getProfesor()
    {
        fetch("https://localhost:7078/Natalija/getProfesor/"+this.email, {
            method: "GET",
            headers: {
              "Content-Type": "application/json",
            },
            body: JSON.stringify({
            }),
          })
            .then((response) => response.json())
            .then((data) => {
              if (data.title == "Unauthorized") alert("Lose korisnicko ime ili sifra.");
              else {
                this.email=data.email;
                this.ime=data.ime;
                this.prezime=data.prezime;
                this.kancelarija=data.kancelarija;
                this.brtel=data.br_telefona;
                this.crtajProfesorHTML(document.getElementById("mainProfesor"));
              }
            })
            .catch((error) => console.error("Greska sa prijavljivanjem", error));
    }
    PreuzmiIspite()
    {
        fetch("https://localhost:7078/Natalija/getProfesor/"+this.email, {
            method: "GET",
            headers: {
              "Content-Type": "application/json",
            },
            body: JSON.stringify({
            }),
          })
            .then((response) => response.json())
            .then((data) => {
              if (data.title == "Unauthorized") alert("Lose korisnicko ime ili sifra.");
              else {
                data.forEach(element => {
                    var p1 = new predmet( element.sifra_predmeta, element.espb,element.naziv_predmeta,element.semestar,element.smer);
                    /*p1.sifra_predmeta = element.sifra_predmeta;
                    p1.espb = element.espb;
                    p1.semestar=element.semestar;
                    p1.smer = element.smer;*/
                    this.dodajPredmet(p1);
                });
                this.crtajIspite(document.getElementById("crtajPredmeteprofesora"));
              }
            })
            .catch((error) => console.error("Greska sa prijavljivanjem", error));
    }
}