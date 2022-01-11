import { obavestenje } from "./obavestenje.js";
export class obavestenja{
    constructor()
    {
        this.svaObavestenja=[];
    }
    dodajObavestenje(o)
    {
        this.svaObavestenja.push(o);
    }
    crtajSvaObavestenja(role){
        /*;*/
        if(role=="Student")
            this.getObavestenjaStudent();
        else
            this.getObavestenjaProfesor();
        
        
        
    }
    crtajObavestenja()
    {
        const naslov= document.getElementById("naslov");
        var pom1= document.createElement("div");
        pom1.classList.add("section-title");
        pom1.classList.add("text-center");
        var t= document.createElement("h1");
        t.innerHTML="Važna obaveštenja";
        pom1.appendChild(t);
        naslov.appendChild(pom1);
        const host= document.getElementById("obavestenjaID");
        if (!host) throw new Error("Greska u hostu");
        var pom= document.createElement("div");
        pom.classList.add("row");
        host.appendChild(pom)

        this.svaObavestenja.forEach((obavestenje) =>{
            obavestenje.crtajObavestenje(pom);
        })
        

    }
    getObavestenjaStudent()
    {
        fetch("https://localhost:7078/Saske/getObavestenjaStudenta", {
              method: "GET",
              headers: {
                  //"Content-Type": "application/json",
                  "accept": "text/plain",
                  "Authorization": "Bearer " + sessionStorage.getItem("token") 
              },
          }).then(p => {
              p.json().then(data => {
                data.forEach((element) => {
                //var o=new obavestenje();
                var datumObjave=element.datum;
                var emailProfesora=element.emailProf;
                 var nazivPredmeta=element.predmet;
                var tekst=element.obavestenje;
                var o=new obavestenje(datumObjave,emailProfesora,nazivPredmeta,tekst);
                this.dodajObavestenje(o);

            });
            this.crtajObavestenja()
              });
          });
    }
    getObavestenjaProfesor()
    {
        fetch("https://localhost:7078/Saske/getObavestenjaProfesora", {
              method: "GET",
              headers: {
                  //"Content-Type": "application/json",
                  "accept": "text/plain",
                  "Authorization": "Bearer " + sessionStorage.getItem("token") 
              },
          }).then(p => {
              p.json().then(data => {
                data.forEach((element) => {
                var o=new obavestenje();
                o.datumObjave=element.datum;
                o.emailProfesora=element.emailProf;
                o.nazivPredmeta=element.predmet;
                o.tekst=element.obavestenje;

                this.dodajObavestenje(o);

            });
            this.crtajObavestenja()
              });
          });
    }
    
}