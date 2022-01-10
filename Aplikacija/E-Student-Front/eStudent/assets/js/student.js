import {polozeniIspiti} from "./polozeniIspiti.js"
import { zabranjeniIspiti } from "./zabranjeniIspit.js";

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
        this.prosecnaOcena=0;
        this.espb=0;
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
        this.smer=smer;
        this.prosecnaOcena=0;
        this.espb=0;
    }
    crtajStudent(){
        const host= document.getElementById("bodyStudent");

        if (!host)
        throw new Error("Greska u hostu");

        this.crtajStudentHTML(document.getElementById("mainStudent"));
        this.crtajIspite();
        this.crtajZabrane();
        this.getProsecnaOcena();
    }
    crtajStudentHTML(host) {
      document.getElementById("mainIme").innerHTML=this.indeks+" "+this.ime+" " +this.prezime;
        
        document.getElementById("emailUser").innerHTML = this.email;
        document.getElementById("emailUser").classList.add("card-header");
        //document.getElementById("emailUser").classList.add("name");
        document.getElementById("semestarUser").innerHTML = this.semestar;
        document.getElementById("semestarUser").classList.add("card-body");
        document.getElementById("smerlUser").innerHTML = this.smer;
        document.getElementById("smerlUser").classList.add("card-body");
        
        document.getElementById("prosecnaOcenaUser").innerHTML = this.prosecnaOcena;
        document.getElementById("prosecnaOcenaUser").classList.add("card-body");
    }
    crtajZabrane() {
      var tableDiv = document.getElementById("zabranjeniIspiti")
      let header = document.createElement("h2");
      header.innerHTML = "Zabranjeni ispiti";
      tableDiv.appendChild(header); 
      var table = document.createElement("table");
      var row = table.insertRow();
      let staticInfo1 = [
        "Naziv ispita",
        "Razlog zabrane",
        "Datum isteka"
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
            var p1=new zabranjeniIspiti();
            var cell;
            row=table.insertRow();
            cell=row.insertCell();
            p1.naziv=element.naziv;
            cell.innerHTML=element.naziv;
            cell=row.insertCell();
            p1.ocena=element.raz;
            cell.innerHTML=element.raz;
            cell=row.insertCell();
            p1.ocena=element.datumr;
            cell.innerHTML=element.datumr;
            
          });
          
        });
      });tableDiv.appendChild(table);
    }
    
    crtajIspite() {
      var tableDiv = document.getElementById("polozeniIspiti")
      let header = document.createElement("h2");
      header.innerHTML = "Položeni ispiti";
      tableDiv.appendChild(header); 
      var table = document.createElement("table");
      var row = table.insertRow();
      row.classList.add("tr");
      let staticInfo1 = [
        "Naziv ispita",
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
            var p1=new polozeniIspiti();
            var cell;
            row.classList.add("tr");
            row=table.insertRow();
            cell=row.insertCell();
            p1.naziv=element.ime;
            cell.innerHTML=element.ime;
            cell=row.insertCell();
            p1.ocena=element.ocena;
            cell.innerHTML=element.ocena;
            
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
                this.indeks = data.indeks;
                this.semestar = data.semestar;
                this.smer=data.smer;
                this.crtajStudent();
              });
          });
      }
    setEmail(e)
    {
        this.email=e;
    }
    getProsecnaOcena()
    {
      fetch("https://localhost:7078/Saske/getPolozeniIspitiOcene", {
              method: "GET",
              headers: {
                  //"Content-Type": "application/json",
                  "accept": "text/plain",
                  "Authorization": "Bearer " + sessionStorage.getItem("token") 
              },
          }).then(p => {
              p.json().then(data => {
                this.prosecnaOcena=data.ocena;
                this.espb=data.ocena;
                document.getElementById("prosecnaOcenaUser").innerHTML = data.ocena;
                document.getElementById("prosecnaOcenaUser").classList.add("card-body");
                document.getElementById("espbUser").innerHTML = data.espb;
                document.getElementById("prosecnaOcenaUser").classList.add("card-body");
              });
          });
    }
} 