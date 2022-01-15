import { obavestenja } from "./obavestenja.js";
var o=new obavestenja();
if(sessionStorage.getItem("token")==null || sessionStorage.getItem("token")=="")
{
    
}
else
{
    var r=sessionStorage.getItem("role");console.log(r);

        if (r == "Student" || r=="Profesor")
         {
          o.crtajSvaObavestenja(r);
         }
}
 