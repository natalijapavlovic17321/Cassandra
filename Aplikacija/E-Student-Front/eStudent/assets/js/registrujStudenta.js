function registerStudent() {
  let ime = document.getElementById("imeRegister").value;
  let prezime = document.getElementById("prezimeRegister").value;
  let brIndeksa = document.getElementById("brIndeks").value;
  let smer = document.getElementById("smer").value;
  let semestar = document.getElementById("semestar").value;
  let email = document.getElementById("emailRegister").value;
  let pass = document.getElementById("passwordRegister").value;
  let confPass = document.getElementById("confirmLReg").value;
  let godinaUpisa = document.getElementById("godUpisa").value;

  console.log(pass);
  console.log(confPass);
  if (pass != confPass) {
    alert("Sifre se ne slazu!");
    return;
  }
  fetch("https://localhost:7078/Account/registerStudent", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      email: email,
      password: pass,
      confirmPassword: confPass,
      godinaUpisa: godinaUpisa,
      ime: ime,
      indeks: brIndeksa,
      prezime: prezime,
      semestar: semestar,
      smer: smer,
    }),
  })
    .then((p) => {
      if (p.ok) {
        alert("Student uspesno registrovan");
      } else {
        alert("Postoji student sa takvim emailom");
      }
    })
    .catch(() => {
      alert("Greska sa konekcijom");
    });
}
let btn = document.getElementById("registrujSe");
btn.addEventListener("click", function () {
  registerStudent();
});
