export {polozeniIspiti} from "./polozeniIspiti" //mslm da ide import i da fajl mora sa ekstenzijom .js
export class student{
    consructor(id, brindeksa,ime,prezime,email,godina,semestar, polozeniIspiti){
        this.kontejner=null;
        this.id = id;
        this.ime = ime;
        this.prezime = prezime;
        this.brindeksa = brindeksa;
        this.email = email;
        this.godina = godina;
        this.semestar = semestar;
        this.polozeniIspiti=polozeniIspiti;
        //vrtn ce da treba i prijavljeni ispiti 
        // mozda i zabrane
        // mada nzm dal ce one preko konstruktora da ti se psoledjuju 
    }
    dodajUser(id, brindeksa, ime, prezime, godina,semestar, polozeniIspiti) {
        this.id = id;
        this.ime = ime;
        this.prezime = prezime;
        this.brindeksa = brindeksa;
        this.email = email;
        this.godina = godina;
        this.semestar = semestar;
        this.polozeniIspiti=polozeniIspiti;
        this.pIspiti=[];
    }
    crtajStudent(){
        const host= document.getElementById("bodyStudent");

        if (!host)
        throw new Error("Greska u hostu");

        this.crtajStudentHTML(document.getElementById("mainStudent"));
        this.crtajIspite(document.getElementById("polozeniIspiti"));
    }
    crtajStudentHTML(host) {
        document.getElementById("brindeksaUser").innerHTML = this.username; //nemas username
        document.getElementById("imeUser").innerHTML = this.ime + " " +this.prezime;
        document.getElementById("emailUser").innerHTML = this.email;

    }
    crtajIspite(host) {
        var tableisp=document.getElementById("tablepispiti");
        const s=document.createElement("td");
       
        var table = document.createElement('TABLE');
        table.border='1';

        var tableBody = document.createElement('TBODY');
        table.appendChild(tableBody);

        this.pIspiti.forEach(ispit=>{
            ispit.crtajIspitUser(tableBody, tableisp);
        })
        host.appendChild(tableisp);
    }
}