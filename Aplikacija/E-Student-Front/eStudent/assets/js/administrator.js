import {student} from "./student.js";
import {predmet} from "./predmet.js";

export class admin{
    constructor(){
        this.kontejnerAdmin=null;
        this.studenti=[];
        this.sala=[];
        this.satnica=[];
        this.profesori=[];
        this.rok=[];
        this.ispiti=[];
    }

    dodajIspit(p)
    {
        this.ispiti.push(p);
    }

    dodajStudenta(p)
    {
        this.studenti.push(p);
    }

    crtajAdmina(){
        const host= document.getElementById("bodyAdmin");
        if (!host)
        throw new Error("Greska u hostu");
        this.crtajStudente("crtajNeodobreneStudente");
        this.crtajDodavanjeRoka("crtajDnjeRoka");   

    }

    OdobriStudenta(email)
    {
      console.log
      fetch("https://localhost:7078/acceptStudent/"+email,{
          method: "PUT",
          headers: {
            "Content-Type": "application/json",
           // Authorization: "Bearer " + sessionStorage.getItem("token"),
          },
          body: JSON.stringify({
            
          }),
        })
          .then((p) => {
            if (p.ok) {
              alert("Student uspesno odobren");
              location.href = "administrator.html";
            } else {
              alert("Greska kod dodavanja");
            }
          })
          .catch((p) => {
            alert("Greška sa konekcijom.");
          });
        }

crtajStudente(host)
    {   
        var b=document.getElementById("btnPrikaziStudente");
        b.hidden=true;
        var table1 = document.createElement("table");
        table1.id = "neodobreniStudenti";       
      var row1 = table1.insertRow();
      this.kontejnerAdmin = row1;
      let staticInfo = [
        " ",
        "Email",
        "Godina upisa",
        "Ime",
        "Indeks",
        "Prezime",
        "Semestar", 
        "Smer",
        " "
      ];
      row1 = table1.insertRow();
      for (let i = 0; i < staticInfo.length; i++) {
        let cell = row1.insertCell();
        cell.innerHTML = staticInfo[i];
      }
      var j=0;
      this.studenti.forEach(i=>{
        row1 = table1.insertRow();
        row1.id = i.email;
        var cell1;
        cell1 = row1.insertCell();
        cell1.innerHTML = j+1;

        cell1 = row1.insertCell();
        cell1.innerHTML = i.email;

        cell1 = row1.insertCell();
        cell1.innerHTML = i.godina;

        cell1 = row1.insertCell();
        cell1.innerHTML = i.ime;

        cell1 = row1.insertCell();
        cell1.innerHTML = i.indeks;

        cell1 = row1.insertCell();
        cell1.innerHTML = i.prezime;

        cell1 = row1.insertCell();
        cell1.innerHTML = i.semestar;

        cell1 = row1.insertCell();
        cell1.innerHTML = i.smer;

        cell1 = row1.insertCell();
        var btn = document.createElement("button")
        btn.innerHTML = "Odobri";
        btn.onclick = (ev) => {
          this.OdobriStudenta(i.email);
        }
        cell1.appendChild(btn);
        
        j++;
      });
      host.appendChild(table1);
    }

    PreuzmiStudente()
    {
        this.studenti.length=0;
        fetch("https://localhost:7078/Student/getStudents", {
            method: "GET",
            headers: {
              "Content-Type": "application/json",
               //Authorization: "Bearer " + sessionStorage.getItem("token")
            }
          })
            .then((response) => response.json())
            .then((data) => {
              if (data.title == "Unauthorized") alert("Lose korisnicko ime ili sifra.");
              else {
                data.forEach(element => {
                    var p1 = new student(element.id,element.indeks,element.ime,element.prezime,element.email,element.godinaUpisa,element.semestar,element.smer);
                    this.dodajStudenta(p1);
                });
                this.crtajStudente(document.getElementById("crtajNeodobreneStudente"));
              }
            })
            .catch((error) => console.error("Greska", error));
        }

    crtajDodavanjeRoka()
    {
      var d5=document.getElementById("crtajDnjeRoka");
      d5.className="red";
      var l5=document.createElement("label");
      l5.innerHTML="Naziv roka:";
      d5.appendChild(l5);
      var inp5=document.createElement("input");
      inp5.id="nazivRoka";
      inp5.style="width:700px";
      d5.appendChild(inp5);

      var l1=document.createElement("label");
      l1.innerHTML="Pocetak roka:";
      d5.appendChild(l1);
      var inp1=document.createElement("input");
      inp1.type="date";
      inp1.id="pocetakRoka";
      inp1.style="width:700px";
      d5.appendChild(inp1);

      var l2=document.createElement("label");
      l2.innerHTML="Kraj roka:";
      d5.appendChild(l2);
      var inp2=document.createElement("input");
      inp2.type="date";
      inp2.id="krajRoka";
      inp2.style="width:700px";
      d5.appendChild(inp2);

      var l3=document.createElement("label");
      l3.innerHTML="Pocetak prijave:";
      d5.appendChild(l3);
      var inp3=document.createElement("input");
      inp3.type="date";
      inp3.id="pocetakPrijave";
      inp3.style="width:700px";
      d5.appendChild(inp3);

      var l4=document.createElement("label");
      l4.innerHTML="Kraj prijave:";
      d5.appendChild(l4);
      var inp4=document.createElement("input");
      inp4.type="date";
      inp4.id="krajPrijave";
      inp4.style="width:700px";
      d5.appendChild(inp4);

      var butt=document.createElement("button");
      butt.classList="btn btna";
      butt.style="width:130px";
      butt.innerHTML="Dodaj";
      d5.appendChild(butt);
      
      butt.addEventListener("click", (e) => {
        this.DodajRok();
        
    });
  }

    DodajRok()
    {
      var nazivR = document.getElementById("nazivRoka").value;
      var pocetakR=document.getElementById("pocetakRoka").value;
      var krajR=document.getElementById("krajRoka").value;
      var pocetakP=document.getElementById("pocetakPrijave").value;
      var krajP=document.getElementById("krajPrijave").value;
      if(nazivR!="" && pocetakR!="" && krajR!="" && pocetakP!="" && krajP!="")
      {
      fetch("https://localhost:7078/addRok", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
         // Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
        body: JSON.stringify({
          id: "string",
          pocetak_roka: pocetakR,
          kraj_prijave: krajP,
          naziv: nazivR,
          pocetak_prijave: pocetakP,
          zavrsetak_roka: krajR
        }),
      })
        .then((p) => {
          if (p.ok) {
            alert("Uspesno dodavanje roka");
            location.href = "administrator.html";
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

    crtajDodavanjeSale()
    {
      var d5=document.getElementById("crtanjeSale");
      d5.className="red";
      var l1=document.createElement("label");
      l1.innerHTML="Naziv sale:";
      d5.appendChild(l1);
      var inp1=document.createElement("input");
      inp1.id="nazivSale";
      inp1.style="width:700px";
      d5.appendChild(inp1);
  
      var l2=document.createElement("label");
      l2.innerHTML="Sprat:";
      d5.appendChild(l2);
      var inp2=document.createElement("input");
      inp2.type="number";
      inp2.id="spratSale";
      inp2.style="width:700px";
      d5.appendChild(inp2);

      var l3=document.createElement("label");
      l3.innerHTML="Kapacitet:";
      d5.appendChild(l3);
      var inp3=document.createElement("input");
      inp3.type="number";
      inp3.id="kapacitetSale";
      inp3.style="width:700px";
      d5.appendChild(inp3);

      var butt=document.createElement("button");
      butt.classList="btn btna";
      butt.style="width:130px";
      butt.innerHTML="Dodaj";
      d5.appendChild(butt);
      
      butt.addEventListener("click", (e) => {
        this.DodajSalu(); 
    });
  }

    DodajSalu()
    {
      var nazivS = document.getElementById("nazivSale").value;
      var kapacitetS=document.getElementById("kapacitetSale").value;
      var spratS=document.getElementById("spratSale").value;
      if(nazivS!="" && kapacitetS!="" && spratS!="")
      {
      fetch("https://localhost:7078/addSala", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
         // Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
        body: JSON.stringify({
          naziv: nazivS,
          sprat: spratS,
          kapacitet: kapacitetS
        }),
      })
        .then((p) => {
          if (p.ok) {
            alert("Uspesno dodavanje sale");
            location.href = "administrator.html";
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

    crtajDodavanjeSatnice()
    {
      var d5=document.getElementById("crtanjeSatnice");
      d5.className="red";
      var l1=document.createElement("label");
      l1.innerHTML="Datum:";
      d5.appendChild(l1);
      var inp1=document.createElement("input");
      inp1.type="date";
      inp1.id="datumSatnice";
      inp1.style="width:700px";
      d5.appendChild(inp1);
  
      var sel=document.createElement("select");
      sel.style="width:200px";
      sel.id="selectID";
      d5.appendChild(sel);
      this.ispiti.forEach(i=>{
          var o=document.createElement("option");
          o.value=i.sifra_predmeta;
          o.innerHTML=i.sifra_predmeta;
          o.selected=false;
          o.style="width:200px";
          sel.appendChild(o);
      });

      var l3=document.createElement("label");
      l3.innerHTML="Vreme:";
      d5.appendChild(l3);
      var inp3=document.createElement("input");
      inp3.type="time";
      inp3.id="vremeSatnice";
      inp3.style="width:700px";
      d5.appendChild(inp3);

      var butt=document.createElement("button");
      butt.classList="btn btna";
      butt.style="width:130px";
      butt.innerHTML="Dodaj";
      d5.appendChild(butt);
      
      butt.addEventListener("click", (e) => {
        this.DodajSatnicu(); 
    });
  }

  DodajSatnicu()
    {
      var datumS = document.getElementById("datumSatnice").value;
      var sifraPredmetaS="A123B";//=document.getElementById("selectID").value;
      var vremeS=document.getElementById("vremeSatnice").value;
      if(datumS!="" && sifraPredmetaS!="" && vremeS!="")
      {
      fetch("https://localhost:7078/addSatnica", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
         // Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
        body: JSON.stringify({
          id: "string",
          rok_id: "string",   
          datum: datumS,
          sifra_predmeta: sifraPredmetaS,
          vreme: vremeS,
          naziv_sale: "string"
        }),
      })
        .then((p) => {
          if (p.ok) {
            alert("Uspesno dodavanje satnice");
            location.href = "administrator.html";
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