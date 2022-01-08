function Login(usern, pass) {
  fetch("https://localhost:7078/Account/Login", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      email: usern,
      password: pass,
    }),
  })
    .then((response) => response.json())
    .then((data) => {
      if (data.title == "Unauthorized") alert("Lose korisnicko ime ili sifra.");
      else {
       
        sessionStorage.setItem("username", usern); console.log(usern);
        sessionStorage.setItem("token", data.token);
        sessionStorage.setItem("role", data.role);
        console.log("wtf");
        if (data.role == "Student") {
          location.href = "student.html";
        } else {
          (data.role == "Profesor") 
           location.href = "profesor.html";
        }
      }
    })
    .catch((error) => console.error("Greska sa prijavljivanjem", error));
}
if ( sessionStorage.getItem("token") == null || sessionStorage.getItem("token") == "")  //mslm da treba local ?
{
  var d = document.getElementById("login");
  var userEmail = document.getElementById("usernameLogin").value;
  var userPassword = document.getElementById("passwordLogin").value;

  d.addEventListener("click", function () {
        Login(userEmail, userPassword);
  });
} 
else 
{
  alert("Vec ste prijavljeni");
  location.href = "student.html";
}
