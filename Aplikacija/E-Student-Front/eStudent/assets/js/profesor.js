import {predmet} from "./predmet.js";
export class profesor{
    constructor(email,ime,prezime,godina,brtel, kancelarija){
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
        this.ispiti.push(p);
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
        document.getElementById("imeProf").innerHTML = this.ime+" "  +this.prezime;
        document.getElementById("emailUser").innerHTML = this.email;
        document.getElementById("kancelarijaUser").innerHTML = this.kancelarija;
        document.getElementById("brojUser").innerHTML = this.brtel;
    }
    crtajIspite(host)
    {
        var b=document.getElementById("btnPrikaziPredmete");
        b.hidden=true;
        var table1 = document.createElement("table");
        table1.id = "predmetiProf";
      var row1 = table1.insertRow();
      let staticInfo = [
        " ",
        "Sifra predmeta",
        "ESPB",
        "Naziv predmeta",
        "Semestar",
        "Smer",
      ];
      row1 = table1.insertRow();
      for (let i = 0; i < staticInfo.length; i++) {
        let cell = row1.insertCell();
        cell.innerHTML = staticInfo[i];
      }
      var j=0;
      this.ispiti.forEach(i=>{
        row1 = table1.insertRow();
        row1.id = i.sifra_predmeta;
        var cell1;
        cell1 = row1.insertCell();
        cell1.innerHTML = j+1;

        cell1 = row1.insertCell();
        cell1.innerHTML = i.sifra_predmeta;

        cell1 = row1.insertCell();
        cell1.innerHTML = i.espb;

        cell1 = row1.insertCell();
        cell1.innerHTML = i.naziv_predmeta;

        cell1 = row1.insertCell();
        cell1.innerHTML = i.semestar;

        cell1 = row1.insertCell();
        cell1.innerHTML = i.smer;
        j++;
      });
      host.appendChild(table1);
    }
    crtajDodavanjeObavestenja()
    {
      var d5=document.getElementById("divGdeSeRadiSve");
      d5.className="red";
      var lab= document.createElement("h3");
      lab.innerText="Unesite obavestenje:";
      d5.appendChild(lab);
      var l1=document.createElement("label");
      l1.innerHTML="Predmet:";
      d5.appendChild(l1);
      var sel=document.createElement("select");
      sel.style="width:200px";
      sel.id="selectID";
      d5.appendChild(sel);
      this.ispiti.forEach(i=>{
          var o=document.createElement("option");
          o.value=i;
          o.innerHTML=i;
          o.selected=false;
          o.style="width:200px";
          sel.appendChild(o);
      });
      var inp=document.createElement("textarea");
      inp.className="textareas";
      inp.id="tekstObavestenja";
      d5.appendChild(inp);
      var butt=document.createElement("button");
      butt.classList="btn btna";
      butt.style="width:130px";
      butt.innerHTML="Dodaj";
      d5.appendChild(butt);
      
      butt.addEventListener("click", (e) => {
        this.dodajObavestenjeFetch();
      });
    }
    crtajDodavanjeZabrane()
    {
      var d5=document.getElementById("divGdeSeRadiSve");
      d5.className="red";
      var lab= document.createElement("h3");
      lab.innerText="Unesite zabranu:";
      d5.appendChild(lab);
      var l2=document.createElement("label");
      l2.innerHTML="Predmet:";
      d5.appendChild(l2);
      var sel=document.createElement("select");
      sel.style="width:200px";
      sel.id="selectID";
      d5.appendChild(sel);
      this.ispiti.forEach(i=>{
          var o=document.createElement("option");
          o.value=i;
          o.innerHTML=i;
          o.selected=false;
          o.style="width:200px";
          sel.appendChild(o);
      });

      var l1=document.createElement("label");
      l1.innerHTML="Email studenta:";
      d5.appendChild(l1);
      var inp1=document.createElement("input");
      inp1.id="emailZabrane";
      inp1.style="width:700px";
      d5.appendChild(inp1);

      var l5=document.createElement("label");
      l5.innerHTML="Do datuma:";
      d5.appendChild(l5);
      var inp5=document.createElement("input");
      inp5.type="date";
      inp5.id="datumZabrane";
      inp5.style="width:700px";
      d5.appendChild(inp5);

      var l=document.createElement("label");
      l.innerHTML="Razlog:";
      d5.appendChild(l);
      var inp=document.createElement("input");
      inp.id="tekstZabrane";
      inp.style="width:700px";
      d5.appendChild(inp);

      var butt=document.createElement("button");
      butt.classList="btn btna";
      butt.style="width:130px";
      butt.innerHTML="Dodaj";
      d5.appendChild(butt);
      
      butt.addEventListener("click", (e) => {
        this.dodajZabranaFetch();
      });
    }
    crtajDodavanjeMesta()
    {
      var d5=document.getElementById("divGdeSeRadiSve");
    }
    getProfesor()
    {
        fetch("https://localhost:7078/Natalija/getProfesor/"+this.email, {
            method: "GET",
            headers: {
              "Content-Type": "application/json",
            }
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
        this.ispiti.length=0;
        fetch("https://localhost:7078/Natalija/getProfesorIspiti/"+this.email, {
            method: "GET",
            headers: {
              "Content-Type": "application/json",
            }
          })
            .then((response) => response.json())
            .then((data) => {
              if (data.title == "Unauthorized") alert("Lose korisnicko ime ili sifra.");
              else {
                data.forEach(element => {
                    var p1 = new predmet(element.sifra_Predmeta, element.espb,element.nazivPredmeta,element.semestar,element.smer);
                    this.dodajPredmet(p1);
                });
                this.crtajIspite(document.getElementById("crtajPredmeteprofesora"));
              }
            })
            .catch((error) => console.error("Greska sa prijavljivanjem", error));
    }
    dodajObavestenjeFetch()
    {
      var today = new Date();
      var dd = String(today.getDate()).padStart(2, '0');
      var mm = String(today.getMonth() + 1).padStart(2, '0'); 
      var yyyy = today.getFullYear();
      today = yyyy + '-' + mm + '-' + dd;
      var tekst=document.getElementById("tekstObavestenja").value;
      var selectoption=document.getElementById("selectID").value;
      console.log(today);
      if(tekst!="")
      {
      fetch("https://localhost:7078/Natalija/postObavestenje", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
        body: JSON.stringify({
          id_obavestenja: "string",
          datum_objave: today,
          email_profesor: this.email,
          sifra_predmeta: selectoption,
          tekst: tekst
        }),
      })
        .then((p) => {
          if (p.ok) {
            alert("Uspesno dodavanje obavestenja");
          } else {
            alert("Greska kod dodavanja");
          }
        })
        .catch((p) => {
          alert("Greška sa konekcijom.");
        });
      }
      else alert("Unesite tekst obavestenja");
    }
    dodajObavestenje()
    {
      this.ispiti.length=0;
      fetch("https://localhost:7078/Natalija/getProfesorIspitiNazivi/"+this.email, {
            method: "GET",
            headers: {
              "Content-Type": "application/json",
            }
          })
            .then((response) => response.json())
            .then((data) => {
              if (data.title == "Unauthorized") alert("Lose korisnicko ime ili sifra.");
              else {
                data.forEach(element => {
                    this.ispiti.push(element);
                });
                this.crtajDodavanjeObavestenja();
              }
            })
            .catch((error) => console.error("Greska sa prijavljivanjem", error));
    }
    dodajZabrana()
    {
      this.ispiti.length=0;
      fetch("https://localhost:7078/Natalija/getProfesorIspitiNazivi/"+this.email, {
            method: "GET",
            headers: {
              "Content-Type": "application/json",
            }
          })
            .then((response) => response.json())
            .then((data) => {
              if (data.title == "Unauthorized") alert("Lose korisnicko ime ili sifra.");
              else {
                data.forEach(element => {
                    this.ispiti.push(element);
                });
                this.crtajDodavanjeZabrane();
              }
            })
            .catch((error) => console.error("Greska sa prijavljivanjem", error));
    }
    dodajZabranaFetch()
    {
      var razlog=document.getElementById("tekstZabrane").value;
      var datum=document.getElementById("datumZabrane").value;
      var email=document.getElementById("emailZabrane").value;
      var selectoption=document.getElementById("selectID").value;
      if(razlog!="" && datum!="" && email!="")
      {
      fetch("https://localhost:7078/Natalija/postZabrana", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          //Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
        body: JSON.stringify({
          id: "string",
          datum_isteka: datum,
          email_student: email,
          razlog: razlog,
          sifra_predmeta: selectoption
        }),
      })
        .then((p) => {
          if (p.ok) {
            alert("Uspesno dodavanje obavestenja");
          } else {
            alert("Greska kod dodavanja");
          }
        })
        .catch((p) => {
          alert("Greška sa konekcijom.");
        });
      }
      else alert("Unesite sva polja");

    }
}