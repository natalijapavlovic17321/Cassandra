import {student} from "./student.js";

export class admin{
    constructor(){
        this.kontejnerAdmin=null;
        this.studenti=[];
        this.sala=[];
        this.satnica=[];
        this.profesori=[];
        this.rok=[];
    }

    dodajStudenta(p)
    {
        this.studenti.push(p);
    }

    crtajAdmina(){
        const host= document.getElementById("bodyAdmin");
        if (!host)
        throw new Error("Greska u hostu");
        this.crtajStudente("crtajNeodobreneStudente")   

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
      }