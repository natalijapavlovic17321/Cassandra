import {profesor} from "./profesor.js";
var p=new profesor();
/*if ( sessionStorage.getItem("token") == null || sessionStorage.getItem("token") == "")  //mslm da treba local ?
{
  alert("Niste prijavljeni");
  location.href = "index.html";
} 
else 
{*/
    //if(sessionStorage.getItem("role")=="Profesor")
    //{
        //p.setEmail(sessionStorage.getItem("username"));
        p.setEmail("prof@elfak.rs");
        var d1 = document.getElementById("btnDodajObavestenje");
        var d2 = document.getElementById("btnDodajZabranu");
        var d3 = document.getElementById("btnDodajSalu");
        d1.addEventListener("click",dodajObavestenje); 
        d2.addEventListener("click",dodajZabranu); 
        d3.addEventListener("click",dodajSalu); 
        var d5 = document.getElementById("btOPrikaziZabranu");
        d5.addEventListener("click",prikaziZabranu);
   // }
    /*else
    {
        //p.setEmail(sessionStorage.getItem("username")); za email tog provesora zavisi kod podesavnja
        var d = document.getElementById("radnjeProfesora");
        d.hidden=true;
    }*/
    p.getProfesor();
    var d4 = document.getElementById('btnPrikaziPredmete');
    d4.addEventListener("click",preuzmiIspite); 
//}
function preuzmiIspite()
{
    p.PreuzmiIspite();
}
function dodajObavestenje()
{
    var d5=document.getElementById("divGdeSeRadiSve");
    d5.innerHTML="";
    p.dodajObavestenje();
}
function dodajZabranu()
{
    var d5=document.getElementById("divGdeSeRadiSve");
    d5.innerHTML="";
    p.dodajZabrana();
}
function prikaziZabranu()
{
    var d5=document.getElementById("divGdeSeRadiSve");
    d5.innerHTML="";
    p.prikaziZabrana();
}
function dodajSalu()
{
    var d5=document.getElementById("divGdeSeRadiSve");
    d5.innerHTML="";
}