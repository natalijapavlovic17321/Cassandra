import {polozeniIspiti} from "./polozeniIspiti.js" 
export class student{
    constructor(id, indeks,ime,prezime,email,godina,semestar,smer){
        this.kontejner=null;
       // this.id = id;
        this.ime = ime;
        this.prezime = prezime;
        this.indeks = indeks;
        this.email = email;
        this.godina = godina;
        this.semestar = semestar;
        this.polozeniIspiti=[];
        this.zabranjeniIspiti=[];
        this.smer=smer
        //vrtn ce da treba i prijavljeni ispiti 
        // mozda i zabrane
        // mada nzm dal ce one preko konstruktora da ti se psoledjuju 
    }
    dodajUser(id, brindeksa, ime, prezime, godina,semestar, polozeniIspiti) {
        //this.id = id;
        this.ime = ime;
        this.prezime = prezime;
        this.indeks = indeks;
        this.email = email;
        this.godina = godina;
        this.semestar = semestar;
        this.polozeniIspiti=polozeniIspiti;
        this.polozeniIspiti=[];
        this.zabranjeniIspiti=[];
        this.smer=smer
    }
    crtajStudent(){
        const host= document.getElementById("bodyStudent");

        if (!host)
        throw new Error("Greska u hostu");

        this.crtajStudentHTML(document.getElementById("mainStudent"));
        this.crtajIspite(document.getElementById("polozeniIspiti"));
        this.crtajZabrane(document.getElementById("zabranjeniIspiti"));
    }
    crtajStudentHTML(host) {
        document.getElementById("brindeksaUser").innerHTML = this.username; //nemas username
        document.getElementById("imeUser").innerHTML = this.ime + " " +this.prezime;
        document.getElementById("emailUser").innerHTML = this.email;

    }
    crtajIspite() {
      var tableDiv = document.getElementById("polozeniIspiti")
      let header = document.createElement("h2");
      header.innerHTML = "Položeni ispiti";
      tableDiv.appendChild(header); 
      var table = document.createElement("table");
      var row = table.insertRow();
      let staticInfo1 = [
        "NazivIspita",
        "Ocena",
      ];
      row = table.insertRow();
      for (let i = 0; i < staticInfo1.length; i++) {
        let cell = row.insertCell();
        cell.innerHTML = staticInfo1[i];
      }
      
      fetch("https://localhost:7078/Saske/getPolozeniIspiti", {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
      }).then((p) => {
        p.json().then((data) => {
          data.forEach((element) => {
            var p1=new polozeniIspiti(element.ime,element.ocena);
            var cell;
            row=table.insertRow();
            cell=row.insertCell();
           // p1.naziv=element.ime;
            cell.innerHTML=element.ime;
            cell=row.insertCell();
            //p1.ocena=element.ocena;
            cell.innerHTML=element.ocena;//console.log(p1);
            //this.polozeniIspiti.push(p1);
            //this.dodajPolozeniIspit(p1);
          });
          
        });
      });tableDiv.appendChild(table);
    }
    crtajZabrane() {
      var tableDiv = document.getElementById("polozeniIspiti")
      let header = document.createElement("h2");
      header.innerHTML = "Zabranjeni ispiti";
      tableDiv.appendChild(header); 
      var table = document.createElement("table");
      var row = table.insertRow();
      let staticInfo1 = [
        "NazivIspita",
        "Ocena",
      ];
      row = table.insertRow();
      for (let i = 0; i < staticInfo1.length; i++) {
        let cell = row.insertCell();
        cell.innerHTML = staticInfo1[i];
      }
      
      fetch("https://localhost:7078/Saske/getZabrane", {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
      }).then((p) => {
        p.json().then((data) => {
          data.forEach((element) => {
            var p1=new polozeniIspiti();
            var cell;
            row=table.insertRow();
            cell=row.insertCell();
            p1.naziv=element.ime;
            cell.innerHTML=element.ime;
            cell=row.insertCell();
            p1.ocena=element.ocena;
            cell.innerHTML=element.ocena;console.log(element.ime);
            //this.dodajZabranjeniIspit(p1);
          });
          
          
        });
      });tableDiv.appendChild(table);
    }
    dodajPolozeniIspit(ispit) {
      this.polozeniIspiti.push(ispit);
    }
    dodajZabranjeniIspit(ispit) {
      this.zabranjeniIspiti.push(ispit);
    }
        getStudent(){
          fetch("https://localhost:7078/Saske/getStudent", {
              method: "GET",
              headers: {
                  //"Content-Type": "application/json",
                  "accept": "text/plain",
                  "Authorization": "Bearer " + sessionStorage.getItem("token") 
              },
          }).then(p => {
              p.json().then(data => {
                this.email=data.email;
                this.ime=data.ime;
                this.prezime=data.prezime;
                //this.indeks = indeks;
                //this.semestar = semestar;
                //this.smer=smer;
                this.crtajStudent();
              });
          });
      }
    setEmail(e)
    {
        this.email=e;
    }
} 