if(sessionStorage.getItem("token")==null || sessionStorage.getItem("token")=="")
{
//registracija , login

}
else{
  alert("Vec ste prijavljeni");
  location.href="student.html";
}