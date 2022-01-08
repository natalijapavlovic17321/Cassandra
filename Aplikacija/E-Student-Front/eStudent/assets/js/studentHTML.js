import { student } from "./student.js";
var s=new student();
if(sessionStorage.getItem("token")==null || sessionStorage.getItem("token")=="")
{
    alert("Niste prijavljeni! Prijavite se!");
    location.href="index.html";
}
else
{
    //if(sessionStorage.getItem("role")=="Student")
    //{
      s.setEmail(sessionStorage.getItem("username"));
    //}
    
    s.getStudent();

    //var odjaviSeBtn = document.getElementById("btnOdjaviSe");
    //odjaviSeBtn.addEventListener("click",odjaviSe());

 
}
/*odjaviSe()
{
    console.log("odjavi se");
    sessionStorage.clear();
    location.href ="index.html"
}*/