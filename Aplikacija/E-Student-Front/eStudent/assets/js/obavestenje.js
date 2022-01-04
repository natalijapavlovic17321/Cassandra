
export class obavestenje{
    constructor(
        ID,
        profesor,
        tekst, 
        ispit, 
        datumObjave

    ){
        this.kontObavestenje=null;
        this.ID=ID;
        this.profesor=profesor;
        this.tekst=tekst;
        this.ispit=ispit;
        tjos.datumObjave=datumObjave;
    }
    dodajObavestenje(
        ID,
        profesor,
        tekst, 
        ispit, 
        datumObjave
    ){
        this.ID=ID;
        this.profesor=profesor;
        this.tekst=tekst;
        this.ispit=ispit;
        tjos.datumObjave=datumObjave;
    }
    crtajObavestenje(host){
        if (!host) 
            throw new Error("Greska u hostu");
        
        var pom= document.createElement("div");
        pom.classList.add("col-md-6 col-lg-6");
        pom.classList.add("feature-block");
        host.appendChild(pom);

        var naziv =document.createElement("h4");
        naziv.innerHTML=this.naziv;
        pom.appendChild(naziv);

        var et = document.createElement("label");
        et.innerHTML="@"
        et.classList.add("autor");
        var nazivProfesora = document.createElement("label");
        nazivProfesora.classList.add("comment-author");
        nazivProfesora.classList.add("autor");
        nazivProfesora.innerHTML=this.profesor;
        glavniDiv.appendChild(et);
        glavniDiv.appendChild(nazivProfesora);
        pom.appendChild(et);
        pom.appendChild(nazivProfesora);
        

    }
}