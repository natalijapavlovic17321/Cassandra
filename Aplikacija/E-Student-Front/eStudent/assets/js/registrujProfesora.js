function Register(ime,prezime,email,password,passwordConf,brTel,kanc){
  if (ime == "" || prezime == "" || email == "" || password == "" || passwordConf == "" || brTel == "" || kanc == "")
  {
    alert("Unesite sva polja.");
    return;
  }
  fetch("https://localhost:7078/Account/registerProfesor", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + sessionStorage.getItem("token"),
      },
      body: JSON.stringify({
      email: email,  
      password: password,
      confirmPassword: passwordConf,
      ime: ime,
      br_telefona: brTel,
      prezime: prezime,                           
      kancelarija: kanc
      }),
    })
    .then((p) => {
      if (p.ok) {
        alert("Profesor uspesno registrovan");
      } else {
        alert("Postoji profesor sa takvim emailom");
      }
    })
    .catch(() => {
      alert("Greska sa konekcijom");
    });
  }

  if (sessionStorage.getItem("token") == null ||
      sessionStorage.getItem("token") == "") {
      var d = document.getElementById("registrujSe");
      d.addEventListener("click", function () {
          Register(
              document.getElementById("imeRegister").value,
              document.getElementById("prezimeRegister").value,
              document.getElementById("emailRegister").value,
              document.getElementById("passwordRegister").value,
              document.getElementById("confirmLReg").value,
              document.getElementById("brtelefonaRegister").value,
              document.getElementById("kancelarijaRegister").value
              );
      });
  }
      