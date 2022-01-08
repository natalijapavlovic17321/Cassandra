if (
  sessionStorage.getItem("token") == null ||
  sessionStorage.getItem("token") == ""
) {
  alert("Niste prijavljeni! Prijavite se!");
  location.href = "prijaviSe.html";
}
mogucePrijave();
function mogucePrijave() {
  var tableDiv = document.getElementById("rokInfo");
  let header = document.createElement("h4");
  header.innerHTML = "Tekuci ispitni rok";
  tableDiv.appendChild(header);
  var table = document.createElement("table");
  var row = table.insertRow();
  let staticInfo1 = [
    "NazivRoka",
    "PocetakPrijave",
    "KrajPrijave",
    "PocetakRoka",
    "KrajRoka",
  ];
  row = table.insertRow();
  for (let i = 0; i < staticInfo1.length; i++) {
    let cell = row.insertCell();
    cell.innerHTML = staticInfo1[i];
  }
  tableDiv.appendChild(table);
  let newDiv = document.createElement("div");
  tableDiv.appendChild(newDiv);
  let header1 = document.createElement("h4");
  header1.innerHTML = "Ispite koje mozete prijaviti";

  newDiv.appendChild(header1);
  var table1 = document.createElement("table");
  table1.id = "prijavaTabla";
  var row1 = table1.insertRow();
  let staticInfo = [
    " ",
    "Naziv ispita",
    "Broj prijava",
    "Datum polaganja",
    "Vreme Polaganja",
    "Cena",
  ];
  row1 = table1.insertRow();
  for (let i = 0; i < staticInfo.length; i++) {
    let cell = row1.insertCell();
    cell.innerHTML = staticInfo[i];
  }
  fetch("https://localhost:7078/Slavko/ispitiKojeMozePrijaviti", {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
      Authorization: "Bearer " + sessionStorage.getItem("token"),
    },
  }).then((p) => {
    p.json().then((data) => {
      if (data == null) {
        alert("Trenutno ne postoji rok");
        location.href("student.html");
        return;
      }

      row = table.insertRow();
      var cell = row.insertCell();
      cell.innerHTML = data.rok.naziv;

      cell = row.insertCell();
      cell.innerHTML = data.rok.pocetak_prijave.slice(0, 10);

      cell = row.insertCell();
      cell.innerHTML = data.rok.kraj_prijave.slice(0, 10);

      cell = row.insertCell();
      cell.innerHTML = data.rok.pocetak_roka.slice(0, 10);

      cell = row.insertCell();
      cell.innerHTML = data.rok.zavrsetak_roka.slice(0, 10);

      data.infoPredmeti.forEach((element) => {
        row1 = table1.insertRow();
        row1.id = element.predmet.sifra_Predmeta + "ROW";
        ////////////////////////////////
        var cell1;
        var f =
          '<input class="checkBozz" type="checkbox" id="' +
          element.predmet.sifra_Predmeta +
          '" value="no">';
        cell1 = row1.insertCell();
        cell1.innerHTML = f;

        cell1 = row1.insertCell();
        cell1.innerHTML = element.predmet.nazivPredmeta;

        cell1 = row1.insertCell();
        cell1.innerHTML = element.brojPrijava;

        cell1 = row1.insertCell();
        cell1.innerHTML = element.datumIVreme[0].datum.slice(0, 10);

        cell1 = row1.insertCell();
        cell1.innerHTML = element.datumIVreme[0].vreme;

        cell1 = row1.insertCell();
        cell1.innerHTML = element.brojPrijava < 1 ? "0" : "1400";
        cell1.id = element.predmet.sifra_Predmeta + "price";
      });
    });
  });
  tableDiv.appendChild(table1);
}

function prijavljeniIspiti() {
  var ispitis = [];

  var i = 1;
  var ck = document.querySelectorAll(".checkBozz");
  var cena = 0;
  ck.forEach((element) => {
    if (element.checked) {
      let res = new Object();
      let cenaRow = document.getElementById(element.id + "ROW");
      cena += Number(cenaRow.lastChild.innerHTML);
      cenaRow.parentNode.removeChild(cenaRow);
      i = i + 3;
      res.Id = i;
      res.Sifra_predmeta = element.id;
      fetch("https://localhost:7078/Slavko/prijaviIspite", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
        body: JSON.stringify({
          id: "4",
          rok_id: "0",
          email_studenta: "email",
          mesto: "string",
          naziv_sale: "tes",
          sifra_predmeta: element.id,
        }),
      })
        .then((p) => {
          if (p.ok) {
            alert("Uspesno dodavanje recepta");
            location.href = "user.html";
          } else {
            alert("Greska kod dodavanja");
          }
        })
        .catch((p) => {
          alert("Greška sa konekcijom.");
        });
    }
  });
  let es = document.getElementById("UkupnoCena");
  es.innerHTML = cena;
}

var dgm = document.getElementById("prijavi");
dgm.addEventListener("click", prijavljeniIspiti);