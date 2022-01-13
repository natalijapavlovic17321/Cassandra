import {admin} from "./administrator.js";

var p=new admin();
if(sessionStorage.getItem("token")==null || sessionStorage.getItem("token")=="")
{
    alert("Niste prijavljeni! Prijavite se!");
    location.href="index.html";
}
else
{
    document.getElementById("emailUser").innerHTML = sessionStorage.getItem("username"); 
    var odjaviSeBtn = document.getElementById("btnOdjaviSe");
    odjaviSeBtn.addEventListener("click",function(){
          console.log("odjavi se");
        sessionStorage.clear();
        location.href ="index.html"
    });

var d1 = document.getElementById('btnPrikaziStudente');
d1.addEventListener("click", preuzmiStudente); 

var d2 = document.getElementById('btnDodajRok');
d2.addEventListener("click", dodajRok); 

var d3 = document.getElementById('btnDodajSalu');
d3.addEventListener("click", dodajSalu); 

var d4 = document.getElementById('btnDodajSatnicu');
d4.addEventListener("click", dodajSatnicu); 

}
function preuzmiStudente()
{
    p.PreuzmiStudente();
}

function dodajRok()
{
    var d5=document.getElementById("crtajDnjeRoka");
    d5.innerHTML="";
    p.crtajDodavanjeRoka();
}

function dodajSalu()
{   
    var d5=document.getElementById("crtanjeSale");
    d5.innerHTML="";
    p.crtajDodavanjeSale();   
}

function dodajSatnicu()
{   
    var d5=document.getElementById("crtanjeSatnice");
    d5.innerHTML="";
    p.crtajDodavanjeSatnice(); 
}