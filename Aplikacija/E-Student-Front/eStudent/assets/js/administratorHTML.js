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

var d5 = document.getElementById('btnDodajPredmet');
d5.addEventListener("click", dodajPredmet);

var d6 = document.getElementById('btnPrikaziOdobreneStudente');
d6.addEventListener("click", preuzmiOdobreneStudente); 

var d7 = document.getElementById('btnPrikaziPredmete');
d7.addEventListener("click", preuzmiPredmete);

var d8 = document.getElementById('btnPrikaziRokove');
d8.addEventListener("click", preuzmiRokove); 

var d9 = document.getElementById('btnPrikaziSale');
d9.addEventListener("click", preuzmiSale);

var d10 = document.getElementById('btnPrikaziSatnice');
d10.addEventListener("click", preuzmiSatnice);

var d11 = document.getElementById('btnPrikaziProfesore');
d11.addEventListener("click", preuzmiProfesore);

}
function preuzmiProfesore()
{
    p.PreuzmiProfesore();
    //var b = document.getElementById('id');
    //p.VratiNazivePredmetaZaProfesore();
}

function preuzmiSatnice()
{
    p.PreuzmiSatnice();
}

function preuzmiSale()
{
    p.PreuzmiSale();
}

function preuzmiRokove()
{
    p.PreuzmiRokove();
}

function preuzmiStudente()
{
    p.PreuzmiStudente();
}

function preuzmiOdobreneStudente()
{
    p.PreuzmiOdobreneStudente();
}

function preuzmiPredmete()
{
    p.PreuzmiPredmete();
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
    p.PreuzmiPredmeteZaSatnicu();
    //p.crtajDodavanjeSatnice(); 
}

function dodajPredmet()
{
    var d5=document.getElementById("crtanjePredmeta");
    d5.innerHTML="";
    p.crtajDodavanjePredmeta(); 
}