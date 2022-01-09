import {profesor} from "./profesor.js";
import {administrator} from "./administrator.js";

var p=new administrator();
if (sessionStorage.getItem("token") == null || sessionStorage.getItem("token") == "")  //mslm da treba local ?
{
  alert("Niste prijavljeni");
  location.href = "index.html";
} 
