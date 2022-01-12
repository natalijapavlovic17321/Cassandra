
export class obavestenje{
    constructor(datumObjave,emailProfesora,nazivPredmeta,tekst)
    {
        this.datumObjave=datumObjave;
        this.emailProfesora=emailProfesora;
        this.nazivPredmeta=nazivPredmeta;
        this.tekst=tekst;
    }
    crtajObavestenje(host)
    {

        var pom= document.createElement("div");
        pom.classList.add("col-md-6");
        pom.classList.add("col-lg-6");
        pom.classList.add("feature-block");
        
        
        var et = document.createElement("label");
        et.innerHTML="@"
        et.classList.add("autor");
        et.classList.add("obavestenja");
        var nazivProfesora = document.createElement("label");
        nazivProfesora.classList.add("comment-author");
        nazivProfesora.classList.add("autor");
        nazivProfesora.classList.add("obavestenja");
        nazivProfesora.innerHTML=this.emailProfesora;
        
        //this.getObavestenjaStudent();
        var naziv =document.createElement("h3");
        naziv.innerHTML=this.nazivPredmeta;
        naziv.classList.add("obavestenja");
        naziv.classList.add("razmak");
        
        pom.appendChild(et);
        pom.appendChild(nazivProfesora);
        pom.appendChild(naziv);

        //var glavniDiv= document.createElement("div");
        
        //glavniDiv.appendChild(et);
        //glavniDiv.appendChild(nazivProfesora);
        

        var txt=document.createElement("h4");
        txt.innerHTML=this.tekst;
        pom.appendChild(txt);
        pom.classList.add("razmakgore");
        host.appendChild(pom);
        
    }   
}