import { student } from "./student.js";
/*if(sessionStorage.getItem("token")==null || sessionStorage.getItem("token")=="")
{
    alert("Niste prijavljeni! Prijavite se!");
    location.href="prijaviSe.html";
}
else{*/
loadStudent()
function loadStudent(){
  console.log("stigo ovde");
  var s=new student();
  s.getStudent();

  var odjaviSeBtn = document.getElementById("btnOdjaviSe");
  odjaviSeBtn.addEventListener("click", console.log("odjavi se"));

  function odjaviSe()
  {
      console.log("odjavi se");
    sessionStorage.clear();
    location.href ="index.html"
  }
}
