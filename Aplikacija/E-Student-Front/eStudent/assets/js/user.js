export class user {
  Login(usern, pass) {
    fetch("https://localhost:7078/Account/Login", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        email: usern,
        password: pass
      }),
    })
      .then((response) => response.json())
      .then((data) => {
        if (data.title == "Unauthorized")
          alert("Lose korisnicko ime ili sifra.");
        else {
          sessionStorage.setItem("username", usern);
          sessionStorage.setItem("token", data.token);
          sessionStorage.setItem("role", data.role);
          console.log("wtf");
          //  if (data.role == "Student") {
          //   location.href = "student.html";
          //} else {
          //dodajte vi sta treba
          // }
        }
      })
      .catch((error) => console.error("Greska sa prijavljivanjem", error));
  }
}
