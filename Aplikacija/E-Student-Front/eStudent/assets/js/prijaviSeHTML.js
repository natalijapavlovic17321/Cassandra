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
      console.log(data);
      if (data.title == "Unauthorized") alert("Lose korisnicko ime ili sifra.");
      else {
        if (data.odobren == false && data.role == "Student") {
          alert("Vas nalog jos uvek nije odobren");
          location.href = "index.html";
          return;
        }
        sessionStorage.setItem("username", usern);
        console.log(usern);
        sessionStorage.setItem("token", data.token);
        sessionStorage.setItem("role", data.role);

        if (data.role == "Student") {
          location.href = "student.html";
        } else if (data.role == "Profesor"){        
          location.href = "profesor.html";
        }
        else {
          data.role == "Administrator";
          location.href = "administrator.html";
        }
      }
    })
    .catch((error) => console.error("Greska sa prijavljivanjem", error));
}
if (
  sessionStorage.getItem("token") == null ||
  sessionStorage.getItem("token") == ""
) {
  //mslm da treba local ?
  var d = document.getElementById("login");

  d.addEventListener("click", function () {
    Login(
      document.getElementById("usernameLogin").value,
      document.getElementById("passwordLogin").value
    );
  });
} else if (sessionStorage.getItem("role") == "Student") {
  alert("Vec ste prijavljeni");
  location.href = "student.html";
} else if (sessionStorage.getItem("role") == "Profesor") {
  alert("Vec ste prijavljeni");
  location.href = "profesor.html";
}
  else if (sessionStorage.getItem("role") == "Administrator") {
  alert("Vec ste prijavljeni");
  location.href = "administrator.html";
}
