if (
  sessionStorage.getItem("token") == null ||
  sessionStorage.getItem("token") == ""
) {
  alert("Niste prijavljeni! Prijavite se!");
  location.href = "prijaviSe.html";
}
function prijavljeniIspiti() {
  let tableDiv = document.getElementById("prijavljeni");
  let header = document.createElement("h4");
  header.innerHTML = "Prijavljeni Ispiti U OvomRoku";
  tableDiv.appendChild(header);
  let staticInfo = ["Naziv ispita", "Datum", "Vreme", "Sala"];
  let tablica = document.createElement("table");
  tablica.id = "tablicaPrijavljeni";
  let row = tablica.insertRow();
  for (let i = 0; i < staticInfo.length; i++) {
    let cell = row.insertCell();
    cell.innerHTML = staticInfo[i];
  }
  fetch("https://localhost:7078/Slavko/prijavljeniIspitiUOvomRoku", {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
      Authorization: "Bearer " + sessionStorage.getItem("token"),
    },
  }).then((p) => {
    p.json().then((data) => {
      data.forEach((element) => {
        row = tablica.insertRow();
        let cell = row.insertCell();
        cell.innerHTML = element.naziv;

        cell = row.insertCell();
        cell.innerHTML = element.datum.slice(0, 10);

        cell = row.insertCell();
        cell.innerHTML = element.vreme;

        cell = row.insertCell();
        if (
          element.sala == null ||
          element.sala == "" ||
          element.sala == "null"
        ) {
          cell.innerHTML = "Jos uvek nije odluceno";
        } else {
          cell.innerHTML = element.sala;
        }
      });
    });
  });

  tableDiv.appendChild(tablica);
  fetch;
}
prijavljeniIspiti();
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
  // row = table.insertRow();
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
        cell1.innerHTML = element.brojPrijava < 3 ? "0" : "1400";
        cell1.id = element.predmet.sifra_Predmeta + "price";
      });
    });
  });
  tableDiv.appendChild(table1);
}

function prijaviIspite() {
  var lista = [];
  var ck = document.querySelectorAll(".checkBozz");
  var cena = 0;
  ck.forEach((element) => {
    if (element.checked) {
      let cenaRow = document.getElementById(element.id + "ROW");
      cena += Number(cenaRow.lastChild.innerHTML);

      lista.push(element.id);
    }
  });
  fetch("https://localhost:7078/Slavko/prijaviIspite", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      Authorization: "Bearer " + sessionStorage.getItem("token"),
    },
    body: JSON.stringify({
      dugovanje: cena.toString(),
      listaSifri: lista,
    }),
  })
    .then((p) => {
      if (p.ok) {
        // alert("Uspesno dodavanje recepta");
        //location.href = "user.html";
      } else {
        alert("Greska kod dodavanja");
      }
    })
    .catch((p) => {
      alert("Greška sa konekcijom.");
    });
  let tablica = document.getElementById("tablicaPrijavljeni");
  ck.forEach((element) => {
    if (element.checked) {
      let wholeRow = document.getElementById(element.id + "ROW");
      let row = tablica.insertRow();
      let cell = row.insertCell();
      cell.innerHTML = wholeRow.cells[1].innerHTML;
      cell = row.insertCell();
      cell.innerHTML = wholeRow.cells[3].innerHTML;
      cell = row.insertCell();
      cell.innerHTML = wholeRow.cells[4].innerHTML;
      wholeRow.parentNode.removeChild(wholeRow);
    }
  });
  let es = document.getElementById("UkupnoCena");
  es.innerHTML = cena;
}

var dgm = document.getElementById("prijavi");
dgm.addEventListener("click", prijaviIspite);
