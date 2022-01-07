//examPeriodInformations();
mogucePrijave();
function examPeriodInformations() {
  var tableDiv = document.getElementById("rokInfo");
  let header = document.createElement("h4");
  header.innerHTML = "Tekuci ispitni rok";
  tableDiv.appendChild(header);
  var table = document.createElement("table");
  var row = table.insertRow();
  let staticInfo = [
    "NazivRoka",
    "PocetakPrijave",
    "KrajPrijave",
    "PocetakRoka",
    "KrajRoka",
  ];
  row = table.insertRow();
  for (let i = 0; i < staticInfo.length; i++) {
    let cell = row.insertCell();
    cell.innerHTML = staticInfo[i];
  }
  fetch("https://localhost:7078/Slavko/getCurrentExamPeriodInformations", {
    method: "GET",
    headers: {
      //"Content-Type": "application/json",
      accept: "text/plain",
    },
  }).then((p) => {
    p.json().then((data) => {
      console.log(data);
      if (data == null) {
        alert("Trenutno ne postoji rok");
        location.href("student.html");
        return;
      }

      row = table.insertRow();
      var cell = row.insertCell();
      cell.innerHTML = data.naziv;

      cell = row.insertCell();
      cell.innerHTML = data.pocetak_prijave.slice(0, 10);

      cell = row.insertCell();
      cell.innerHTML = data.kraj_prijave.slice(0, 10);

      cell = row.insertCell();
      cell.innerHTML = data.pocetak_roka.slice(0, 10);

      cell = row.insertCell();
      cell.innerHTML = data.zavrsetak_roka.slice(0, 10);
    });
  });
  tableDiv.appendChild(table);
}

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
  var row1 = table1.insertRow();
  let staticInfo = [
    "Naziv ispita",
    "Broj prijava",
    "Datum polaganja",
    "Vreme Polaganja",
    "Cena",
  ];
  row = table1.insertRow();
  for (let i = 0; i < staticInfo.length; i++) {
    let cell = row.insertCell();
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
      console.log(data);
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
    });
  });
  newDiv.appendChild(table1);
}
