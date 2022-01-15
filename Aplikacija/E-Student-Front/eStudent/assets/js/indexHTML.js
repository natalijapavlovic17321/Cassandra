var d1 = document.getElementById("sakrij");
if ( sessionStorage.getItem("token") == null || sessionStorage.getItem("token") == "")  
{
   d1.style.display="none";
} 
else 
{
    if(sessionStorage.getItem("role")=="Profesor")
    {
        d1.href="profesor.html";
    }
    else
    {
        if(sessionStorage.getItem("role")=="Student")
           d1.href="student.html";
        else d1.href="administrator.html";
    }
}