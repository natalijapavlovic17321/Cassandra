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

var d4 = document.getElementById('btnPrikaziStudente');
d4.addEventListener("click", preuzmiStudente); 


}
function preuzmiStudente()
{
    p.PreuzmiStudente();
}