import {profesor} from "./profesor.js";
var p=new profesor();
if ( sessionStorage.getItem("token") == null || sessionStorage.getItem("token") == "")  //mslm da treba local ?
{
  alert("Niste prijavljeni");
  location.href = "index.html";
} 
else 
{
    if(sessionStorage.getItem("role")=="Profesor")
    {
        p.setEmail(sessionStorage.getItem("username"));
        var d1 = document.getElementById("btnDodajObavestenje");
        var d2 = document.getElementById("btnDodajZabranu");
        var d3 = document.getElementById("btnDodajSalu");
        d1.addEventListener("click",dodajObavestenje); 
        d2.addEventListener("click",dodajZabranu); 
        d3.addEventListener("click",dodajSalu); 
    }
    else
    {
        //p.setEmail(sessionStorage.getItem("username")); za email tog provesora zavisi kod podesavnja
        var d = document.getElementById("radnjeProfesora");
        d.hidden=true;
    }
    p.getProfesor();
    var d4 = document.getElementById("btnPrikaziPredmete");
    d3.addEventListener("click",preuzmiIspite); 
}
preuzmiIspite()
{
    p.preuzmiIspite();
}
dodajObavestenje()
{

}
dodajZabranu()
{

}
dodajSalu()
{

}