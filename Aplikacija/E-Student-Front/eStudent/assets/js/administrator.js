import { student } from "./student.js";
import { predmet } from "./predmet.js";
import { rok } from "./rok.js";
import { sala } from "./sala.js";
import { satnica } from "./satnica.js";
import { profesor } from "./profesor.js";

export class admin {
  constructor() {
    this.kontejnerAdmin = null;
    this.studenti = [];
    this.sala = [];
    this.satnica = [];
    this.profesori = [];
    this.rok = [];
    this.ispiti = [];
  }

  dodajProfesora(p) {
    this.profesori.push(p);
  }

  dodajSatnicu(p) {
    this.satnica.push(p);
  }

  dodajSalu(p) {
    this.sala.push(p);
  }

  dodajRok(p) {
    this.rok.push(p);
  }

  dodajIspit(p) {
    this.ispiti.push(p);
  }

  dodajStudenta(p) {
    this.studenti.push(p);
  }

  crtajAdmina() {
    const host = document.getElementById("bodyAdmin");
    if (!host) throw new Error("Greska u hostu");
    this.crtajStudente("crtajNeodobreneStudente");
    this.crtajDodavanjeRoka("crtajDnjeRoka");
  }

  OdobriStudenta(email) {
    fetch("https://localhost:7078/acceptStudent/" + email, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + sessionStorage.getItem("token"),
      },
      body: JSON.stringify({}),
    })
      .then((p) => {
        if (p.ok) {
          alert("Student uspesno odobren");
          location.href = "administrator.html";
        } else {
          alert("Greska kod dodavanja");
        }
      })
      .catch((p) => {
        alert("Greška sa konekcijom.");
      });
  }

  crtajStudente(host) {
    var b = document.getElementById("btnPrikaziStudente");
    //var b = document.getElementById("crtaj");
    b.hidden = true;
    var table1 = document.createElement("table");
    table1.id = "neodobreniStudenti";
    var row1 = table1.insertRow();
    this.kontejnerAdmin = row1;
    let staticInfo = [
      " ",
      "Email",
      "Godina upisa",
      "Ime",
      "Indeks",
      "Prezime",
      "Semestar",
      "Smer",
      " ",
    ];
    row1 = table1.insertRow();
    for (let i = 0; i < staticInfo.length; i++) {
      let cell = row1.insertCell();
      cell.innerHTML = staticInfo[i];
    }
    var j = 0;
    this.studenti.forEach((i) => {
      row1 = table1.insertRow();
      row1.id = i.email;
      var cell1;
      cell1 = row1.insertCell();
      cell1.innerHTML = j + 1;

      cell1 = row1.insertCell();
      cell1.innerHTML = i.email;

      cell1 = row1.insertCell();
      cell1.innerHTML = i.godina;

      cell1 = row1.insertCell();
      cell1.innerHTML = i.ime;

      cell1 = row1.insertCell();
      cell1.innerHTML = i.indeks;

      cell1 = row1.insertCell();
      cell1.innerHTML = i.prezime;

      cell1 = row1.insertCell();
      cell1.innerHTML = i.semestar;

      cell1 = row1.insertCell();
      cell1.innerHTML = i.smer;

      cell1 = row1.insertCell();
      var btn = document.createElement("button");
      btn.innerHTML = "Odobri";
      btn.onclick = (ev) => {
        this.OdobriStudenta(i.email);
      };
      cell1.appendChild(btn);

      j++;
    });
    host.appendChild(table1);
  }

  crtajOdobreneStudente(host) {
    var b = document.getElementById("btnPrikaziOdobreneStudente");
    //var b = document.getElementById("crtaj");
    b.hidden = true;
    var table1 = document.createElement("table");
    table1.id = "odobreniStudenti";
    var row1 = table1.insertRow();
    this.kontejnerAdmin = row1;
    let staticInfo = [
      " ",
      "Email",
      "Dugovanje",
      "Godina upisa",
      "Ime",
      "Indeks",
      "Prezime",
      "Semestar",
      "Smer",
      " ",
      " ",
      " "
    ];
    row1 = table1.insertRow();
    for (let i = 0; i < staticInfo.length; i++) {
      let cell = row1.insertCell();
      cell.innerHTML = staticInfo[i];
    }
    var j = 0;
    this.studenti.forEach((i) => {
      row1 = table1.insertRow();
      row1.id = i.email;
      var cell1;
      cell1 = row1.insertCell();
      cell1.innerHTML = j + 1;

      cell1 = row1.insertCell();
      cell1.innerHTML = i.email;

      cell1 = row1.insertCell();
      cell1.innerHTML = i.dugovanje;

      cell1 = row1.insertCell();
      cell1.innerHTML = i.godina;

      cell1 = row1.insertCell();
      cell1.innerHTML = i.ime;

      cell1 = row1.insertCell();
      cell1.innerHTML = i.indeks;

      cell1 = row1.insertCell();
      cell1.innerHTML = i.prezime;

      cell1 = row1.insertCell();
      cell1.innerHTML = i.semestar;

      cell1 = row1.insertCell();
      cell1.innerHTML = i.smer;

      cell1 = row1.insertCell();
      var btn = document.createElement("button");
      btn.innerHTML = "Obrisi dugovanja";
      btn.onclick = (ev) => {
        this.ObrisiDugovanje(i.email);
      };
      cell1.appendChild(btn);

      cell1 = row1.insertCell();
      var btn1 = document.createElement("button");
      btn1.innerHTML = "Obrisi studenta";
      btn1.onclick = (ev) => {
        this.ObrisiStudenta(i.email);
      };
      cell1.appendChild(btn1);

      cell1 = row1.insertCell();
      var btn2 = document.createElement("button");
      btn2.innerHTML = "Izmeni smer";
      var l = document.createElement("input");
      l.id = i.indeks;
      cell1.appendChild(l);
      btn2.onclick = (ev) => {      
        this.IzmeniSmer(i.email, i.indeks);
      };
      cell1.appendChild(btn2);

      j++;
    });
    host.appendChild(table1);
  }

  IzmeniSmer(email, indeks)
  {
    var smer = document.getElementById(indeks).value;
    if (smer != "")
    {
      fetch("https://localhost:7078/updateStudent/" + email + "/" + smer, {
        method: "PUT",
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
        body: JSON.stringify({}),
      })
        .then((p) => {
          if (p.ok) {
            alert("Smer uspesno izmenjen");
            location.href = "administrator.html";
          } else {
            alert("Greska kod izmene");
          }
        })
        .catch((p) => {
          alert("Greška sa konekcijom.");
        });
      } else alert("Unesite naziv smera");
  }

  ObrisiStudenta(email) {
    fetch("https://localhost:7078/Student/deleteStudent/" + email, {
      method: "DELETE",
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + sessionStorage.getItem("token"),
      },
      body: JSON.stringify({}),
    })
      .then((p) => {
        if (p.ok) {
          alert("Student uspesno obrisan");
          location.href = "administrator.html";
        } else {
          alert("Greska kod brisanja");
        }
      })
      .catch((p) => {
        alert("Greška sa konekcijom.");
      });
  }

  ObrisiDugovanje(email) {
    fetch("https://localhost:7078/StudentPay/" + email, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + sessionStorage.getItem("token"),
      },
      body: JSON.stringify({}),
    })
      .then((p) => {
        if (p.ok) {
          alert("Dugovanje uspesno obrisano");
          location.href = "administrator.html";
        } else {
          alert("Greska kod brisanja");
        }
      })
      .catch((p) => {
        alert("Greška sa konekcijom.");
      });
  }

  PreuzmiOdobreneStudente() {
    this.studenti.length = 0;
    fetch("https://localhost:7078/getValidStudents", {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + sessionStorage.getItem("token"),
      },
    })
      .then((response) => response.json())
      .then((data) => {
        if (data.title == "Unauthorized")
          alert("Lose korisnicko ime ili sifra.");
        else {
          data.forEach((element) => {
            var p1 = new student(
              element.id,
              element.indeks,
              element.ime,
              element.prezime,
              element.email,
              element.godinaUpisa,
              element.semestar,
              element.smer,
              element.dugovanje
            );
            this.dodajStudenta(p1);
          });
          this.crtajOdobreneStudente(
            document.getElementById("crtajOdobreneStudente")
          );
        }
      })
      .catch((error) => console.error("Greska", error));
  }

  PreuzmiStudente() {
    this.studenti.length = 0;
    fetch("https://localhost:7078/Student/getStudents", {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + sessionStorage.getItem("token"),
      },
    })
      .then((response) => response.json())
      .then((data) => {
        if (data.title == "Unauthorized")
          alert("Lose korisnicko ime ili sifra.");
        else {
          data.forEach((element) => {
            var p1 = new student(
              element.id,
              element.indeks,
              element.ime,
              element.prezime,
              element.email,
              element.godinaUpisa,
              element.semestar,
              element.smer
            );
            this.dodajStudenta(p1);
          });
          this.crtajStudente(
            document.getElementById("crtajNeodobreneStudente")
          );
        }
      })
      .catch((error) => console.error("Greska", error));
  }

  crtajDodavanjeRoka() {
    var d5 = document.getElementById("crtajDnjeRoka");
    d5.className = "red";
    var l5 = document.createElement("label");
    l5.innerHTML = "Naziv roka:";
    d5.appendChild(l5);
    var inp5 = document.createElement("input");
    inp5.id = "nazivRoka";
    inp5.style = "width:700px";
    d5.appendChild(inp5);

    var l1 = document.createElement("label");
    l1.innerHTML = "Pocetak roka:";
    d5.appendChild(l1);
    var inp1 = document.createElement("input");
    inp1.type = "date";
    inp1.id = "pocetakRoka";
    inp1.style = "width:700px";
    d5.appendChild(inp1);

    var l2 = document.createElement("label");
    l2.innerHTML = "Kraj roka:";
    d5.appendChild(l2);
    var inp2 = document.createElement("input");
    inp2.type = "date";
    inp2.id = "krajRoka";
    inp2.style = "width:700px";
    d5.appendChild(inp2);

    var l3 = document.createElement("label");
    l3.innerHTML = "Pocetak prijave:";
    d5.appendChild(l3);
    var inp3 = document.createElement("input");
    inp3.type = "date";
    inp3.id = "pocetakPrijave";
    inp3.style = "width:700px";
    d5.appendChild(inp3);

    var l4 = document.createElement("label");
    l4.innerHTML = "Kraj prijave:";
    d5.appendChild(l4);
    var inp4 = document.createElement("input");
    inp4.type = "date";
    inp4.id = "krajPrijave";
    inp4.style = "width:700px";
    d5.appendChild(inp4);

    var butt = document.createElement("button");
    butt.classList = "btn btna";
    butt.style = "width:130px";
    butt.innerHTML = "Dodaj";
    d5.appendChild(butt);

    butt.addEventListener("click", (e) => {
      this.DodajRok();
    });
  }

  crtajRokove(host) {
    var b = document.getElementById("btnPrikaziRokove");
    //var b = document.getElementById("crtaj");
    b.hidden = true;
    var table1 = document.createElement("table");
    table1.id = "rokovi";
    var row1 = table1.insertRow();
    this.kontejnerAdmin = row1;
    let staticInfo = [
      " ",
      "Naziv",
      "Pocetak roka",
      "Kraj roka",
      "Pocetak prijave",
      "Kraj prijave",
      " ",
    ];
    row1 = table1.insertRow();
    for (let i = 0; i < staticInfo.length; i++) {
      let cell = row1.insertCell();
      cell.innerHTML = staticInfo[i];
    }
    var j = 0;
    this.rok.forEach((i) => {
      row1 = table1.insertRow();
      row1.id = i.id;
      var cell1;
      cell1 = row1.insertCell();
      cell1.innerHTML = j + 1;

      cell1 = row1.insertCell();
      cell1.innerHTML = i.naziv;

      cell1 = row1.insertCell();
      cell1.innerHTML = i.pocetak_roka;

      cell1 = row1.insertCell();
      cell1.innerHTML = i.zavrsetak_roka;

      cell1 = row1.insertCell();
      cell1.innerHTML = i.pocetak_prijave;

      cell1 = row1.insertCell();
      cell1.innerHTML = i.kraj_prijave;

      cell1 = row1.insertCell();
      var btn = document.createElement("button");
      btn.innerHTML = "Obrisi";
      btn.onclick = (ev) => {
        this.ObrisiRok(i.id);
      };
      cell1.appendChild(btn);

      j++;
    });
    host.appendChild(table1);
  }

  PreuzmiRokove() {
    this.rok.length = 0;
    fetch("https://localhost:7078/getRokovi", {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + sessionStorage.getItem("token"),
      },
    })
      .then((response) => response.json())
      .then((data) => {
        if (data.title == "Unauthorized")
          alert("Lose korisnicko ime ili sifra.");
        else {
          data.forEach((element) => {
            var p1 = new rok(
              element.id,
              element.pocetak_roka.slice(0, 10),
              element.kraj_prijave.slice(0, 10),
              element.naziv,
              element.pocetak_prijave.slice(0, 10),
              element.zavrsetak_roka.slice(0, 10)
            );
            this.dodajRok(p1);
          });
          this.crtajRokove(document.getElementById("crtajRokove"));
        }
      })
      .catch((error) => console.error("Greska", error));
  }

  ObrisiRok(id) {
    fetch("https://localhost:7078/deleteRok/" + id, {
      method: "DELETE",
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + sessionStorage.getItem("token"),
      },
      body: JSON.stringify({}),
    })
      .then((p) => {
        if (p.ok) {
          alert("Rok uspesno obrisan");
          location.href = "administrator.html";
        } else {
          alert("Greska kod brisanja");
        }
      })
      .catch((p) => {
        alert("Greška sa konekcijom.");
      });
  }

  DodajRok() {
    var nazivR = document.getElementById("nazivRoka").value;
    var pocetakR = document.getElementById("pocetakRoka").value;
    var krajR = document.getElementById("krajRoka").value;
    var pocetakP = document.getElementById("pocetakPrijave").value;
    var krajP = document.getElementById("krajPrijave").value;
    if (
      nazivR != "" &&
      pocetakR != "" &&
      krajR != "" &&
      pocetakP != "" &&
      krajP != ""
    ) {
      fetch("https://localhost:7078/addRok", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
        body: JSON.stringify({
          id: "string",
          pocetak_roka: pocetakR,
          kraj_prijave: krajP,
          naziv: nazivR,
          pocetak_prijave: pocetakP,
          zavrsetak_roka: krajR,
        }),
      })
        .then((p) => {
          if (p.ok) {
            alert("Uspesno dodavanje roka");
            location.href = "administrator.html";
          } else {
            alert("Greska kod dodavanja");
          }
        })
        .catch((p) => {
          alert("Greška sa konekcijom.");
        });
    } else alert("Unesite sva polja");
  }

  crtajDodavanjeSale() {
    var d5 = document.getElementById("crtanjeSale");
    d5.className = "red";
    var l1 = document.createElement("label");
    l1.innerHTML = "Naziv sale:";
    d5.appendChild(l1);
    var inp1 = document.createElement("input");
    inp1.id = "nazivSale";
    inp1.style = "width:700px";
    d5.appendChild(inp1);

    var l2 = document.createElement("label");
    l2.innerHTML = "Sprat:";
    d5.appendChild(l2);
    var inp2 = document.createElement("input");
    inp2.type = "number";
    inp2.id = "spratSale";
    inp2.style = "width:700px";
    d5.appendChild(inp2);

    var l3 = document.createElement("label");
    l3.innerHTML = "Kapacitet:";
    d5.appendChild(l3);
    var inp3 = document.createElement("input");
    inp3.type = "number";
    inp3.id = "kapacitetSale";
    inp3.style = "width:700px";
    d5.appendChild(inp3);

    var butt = document.createElement("button");
    butt.classList = "btn btna";
    butt.style = "width:130px";
    butt.innerHTML = "Dodaj";
    d5.appendChild(butt);

    butt.addEventListener("click", (e) => {
      this.DodajSalu();
    });
  }

  crtajSale(host) {
    var b = document.getElementById("btnPrikaziSale");
    b.hidden = true;
    var table1 = document.createElement("table");
    table1.id = "sale";
    var row1 = table1.insertRow();
    this.kontejnerAdmin = row1;
    let staticInfo = [" ", "Naziv", "Kapacitet", "Sprat", " "];
    row1 = table1.insertRow();
    for (let i = 0; i < staticInfo.length; i++) {
      let cell = row1.insertCell();
      cell.innerHTML = staticInfo[i];
    }
    var j = 0;
    this.sala.forEach((i) => {
      row1 = table1.insertRow();
      row1.id = i.naziv;
      var cell1;
      cell1 = row1.insertCell();
      cell1.innerHTML = j + 1;

      cell1 = row1.insertCell();
      cell1.innerHTML = i.naziv;

      cell1 = row1.insertCell();
      cell1.innerHTML = i.kapacitet;

      cell1 = row1.insertCell();
      cell1.innerHTML = i.sprat;

      cell1 = row1.insertCell();
      var btn = document.createElement("button");
      btn.innerHTML = "Obrisi";
      btn.onclick = (ev) => {
        this.ObrisiSalu(i.naziv);
      };
      cell1.appendChild(btn);

      j++;
    });
    host.appendChild(table1);
  }

  PreuzmiSale() {
    this.sala.length = 0;
    fetch("https://localhost:7078/getSale", {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + sessionStorage.getItem("token"),
      },
    })
      .then((response) => response.json())
      .then((data) => {
        if (data.title == "Unauthorized")
          alert("Lose korisnicko ime ili sifra.");
        else {
          data.forEach((element) => {
            var p1 = new sala(element.naziv, element.kapacitet, element.sprat);
            this.dodajSalu(p1);
          });
          this.crtajSale(document.getElementById("crtajSale"));
        }
      })
      .catch((error) => console.error("Greska", error));
  }

  ObrisiSalu(naziv) {
    fetch("https://localhost:7078/deleteSala/" + naziv, {
      method: "DELETE",
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + sessionStorage.getItem("token"),
      },
      body: JSON.stringify({}),
    })
      .then((p) => {
        if (p.ok) {
          alert("Sala uspesno obrisan");
          location.href = "administrator.html";
        } else {
          alert("Greska kod brisanja");
        }
      })
      .catch((p) => {
        alert("Greška sa konekcijom.");
      });
  }

  DodajSalu() {
    var nazivS = document.getElementById("nazivSale").value;
    var kapacitetS = document.getElementById("kapacitetSale").value;
    var spratS = document.getElementById("spratSale").value;
    if (nazivS != "" && kapacitetS != "" && spratS != "") {
      fetch("https://localhost:7078/addSala", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
        body: JSON.stringify({
          naziv: nazivS,
          sprat: spratS,
          kapacitet: kapacitetS,
        }),
      })
        .then((p) => {
          if (p.ok) {
            alert("Uspesno dodavanje sale");
            location.href = "administrator.html";
          } else {
            alert("Greska kod dodavanja");
          }
        })
        .catch((p) => {
          alert("Greška sa konekcijom.");
        });
    } else alert("Unesite sva polja");
  }

  crtajDodavanjeSatnice() {
    var d5 = document.getElementById("crtanjeSatnice");
    d5.className = "red";
    var l1 = document.createElement("label");
    l1.innerHTML = "Datum:";
    d5.appendChild(l1);
    var inp1 = document.createElement("input");
    inp1.type = "date";
    inp1.id = "datumSatnice";
    inp1.style = "width:700px";
    d5.appendChild(inp1);


    var l2 = document.createElement("label");
    l2.innerHTML = "Sifra predmeta";
    d5.appendChild(l2);
    var sel = document.createElement("select");
    sel.style = "width:200px";
    sel.id = "selectPredmet";
    d5.appendChild(sel);
    this.ispiti.forEach((i) => {
      var o = document.createElement("option");
      o.value = i.sifra_predmeta;
      o.innerHTML = i.sifra_predmeta;
      o.selected = false;
      o.style = "width:200px";
      sel.appendChild(o);
    });

    var l3 = document.createElement("label");
    l3.innerHTML = "Vreme:";
    d5.appendChild(l3);
    var inp3 = document.createElement("input");
    inp3.type = "time";
    inp3.id = "vremeSatnice";
    inp3.style = "width:700px";
    d5.appendChild(inp3);

    var butt = document.createElement("button");
    butt.classList = "btn btna";
    butt.style = "width:130px";
    butt.innerHTML = "Dodaj";
    d5.appendChild(butt);

    butt.addEventListener("click", (e) => {
      this.DodajSatnicu();
    });

    //sel.addEventListener("change", (e) => {
    //  this.PreuzmiPredmeteZaSatnicu(
     //   document.getElementById("selectPredmet").value
     // );
   // });
   // this.PreuzmiPredmeteZaSatnicu(
   //   document.getElementById("selectPredmet").value
    //);
  }

  PreuzmiPredmeteZaSatnicu(p) {
    this.ispiti.length = 0;
    fetch("https://localhost:7078/getSubjects", {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + sessionStorage.getItem("token"),
      },
    })
      .then((response) => response.json())
      .then((data) => {
        if (data.title == "Unauthorized")
          alert("Lose korisnicko ime ili sifra.");
        else {
          data.forEach((element) => {
            var p1 = new predmet(
              element.sifra_Predmeta,
              element.espb,
              element.nazivPredmeta,
              element.semestar,
              element.smer
            );
            this.dodajIspit(p1);
          });
          this.crtajDodavanjeSatnice(p);
        }
      })
      .catch((error) => console.error("Greska", error));
  }

  crtajSatnice(host) {
    var b = document.getElementById("btnPrikaziSatnice");
    b.hidden = true;
    var table1 = document.createElement("table");
    table1.id = "satnice";
    var row1 = table1.insertRow();
    this.kontejnerAdmin = row1;
    let staticInfo = [
      " ",
      "Naziv sale",
      "Datum",
      "Vreme",
      "Sifra predmeta",
      " ",
    ];
    row1 = table1.insertRow();
    for (let i = 0; i < staticInfo.length; i++) {
      let cell = row1.insertCell();
      cell.innerHTML = staticInfo[i];
    }
    var j = 0;
    this.satnica.forEach((i) => {
      row1 = table1.insertRow();
      row1.id = i.id;
      var cell1;
      cell1 = row1.insertCell();
      cell1.innerHTML = j + 1;

      cell1 = row1.insertCell();
      cell1.innerHTML = i.naziv_sale;

      cell1 = row1.insertCell();
      cell1.innerHTML = i.datum;

      cell1 = row1.insertCell();
      cell1.innerHTML = i.vreme;

      cell1 = row1.insertCell();
      cell1.innerHTML = i.sifra_predmeta;

      cell1 = row1.insertCell();
      var btn = document.createElement("button");
      btn.innerHTML = "Obrisi";
      btn.onclick = (ev) => {
        this.ObrisiSatnicu(i.id);
      };
      cell1.appendChild(btn);

      j++;
    });
    host.appendChild(table1);
  }

  PreuzmiSatnice() {
    this.satnica.length = 0;
    fetch("https://localhost:7078/getSatnice", {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + sessionStorage.getItem("token"),
      },
    })
      .then((response) => response.json())
      .then((data) => {
        if (data.title == "Unauthorized")
          alert("Lose korisnicko ime ili sifra.");
        else {
          data.forEach((element) => {
            var p1 = new satnica(
              element.id,
              element.rok_id,
              element.datum.slice(0, 10),
              element.naziv_sale,
              element.sifra_predmeta,
              element.vreme
            );
            this.dodajSatnicu(p1);
          });
          this.crtajSatnice(document.getElementById("crtajSatnice"));
        }
      })
      .catch((error) => console.error("Greska", error));
  }

  ObrisiSatnicu(id) {
    fetch("https://localhost:7078/deleteSatnica/" + id, {
      method: "DELETE",
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + sessionStorage.getItem("token"),
      },
      body: JSON.stringify({}),
    })
      .then((p) => {
        if (p.ok) {
          alert("Satnica uspesno obrisana");
          location.href = "administrator.html";
        } else {
          alert("Greska kod brisanja");
        }
      })
      .catch((p) => {
        alert("Greška sa konekcijom.");
      });
  }

  DodajSatnicu() {
    var datumS = document.getElementById("datumSatnice").value;
    var sifraPredmetaS = document.getElementById("selectPredmet").value;
    var vremeS = document.getElementById("vremeSatnice").value;
    if (datumS != "" && sifraPredmetaS != "" && vremeS != "") {
      fetch("https://localhost:7078/addSatnica", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
        body: JSON.stringify({
          id: "string",
          rok_id: "string",
          datum: datumS,
          sifra_predmeta: sifraPredmetaS,
          vreme: vremeS,
          naziv_sale: "string",
        }),
      })
        .then((p) => {
          if (p.ok) {
            alert("Uspesno dodavanje satnice");
            location.href = "administrator.html";
          } else {
            alert("Greska kod dodavanja");
          }
        })
        .catch((p) => {
          alert("Greška sa konekcijom.");
        });
    } else alert("Unesite sva polja");
  }

  crtajDodavanjePredmeta() {
    var d5 = document.getElementById("crtanjePredmeta");
    d5.className = "red";
    var l1 = document.createElement("label");
    l1.innerHTML = "Naziv predmeta:";
    d5.appendChild(l1);
    var inp1 = document.createElement("input");
    inp1.id = "nazivDodavanjaPredmeta";
    inp1.style = "width:700px";
    d5.appendChild(inp1);

    var l2 = document.createElement("label");
    l2.innerHTML = "Espb:";
    d5.appendChild(l2);
    var inp2 = document.createElement("input");
    inp2.type = "number";
    inp2.id = "espbPredmeta";
    inp2.style = "width:700px";
    d5.appendChild(inp2);

    var l3 = document.createElement("label");
    l3.innerHTML = "Sifra predmeta:";
    d5.appendChild(l3);
    var inp3 = document.createElement("input");
    inp3.id = "sifraDodavanjaPredmeta";
    inp3.style = "width:700px";
    d5.appendChild(inp3);

    var l3 = document.createElement("label");
    l3.innerHTML = "Semestar:";
    d5.appendChild(l3);
    var inp3 = document.createElement("input");
    inp3.id = "semestarPredmeta";
    inp3.style = "width:700px";
    d5.appendChild(inp3);

    var l3 = document.createElement("label");
    l3.innerHTML = "Smer:";
    d5.appendChild(l3);
    var inp3 = document.createElement("input");
    inp3.id = "smerPredmeta";
    inp3.style = "width:700px";
    d5.appendChild(inp3);

    var butt = document.createElement("button");
    butt.classList = "btn btna";
    butt.style = "width:130px";
    butt.innerHTML = "Dodaj";
    d5.appendChild(butt);

    butt.addEventListener("click", (e) => {
      this.DodajPredmet();
    });
  }

  crtajPredmete(host) {
    var b = document.getElementById("btnPrikaziPredmete");
    b.hidden = true;
    var table1 = document.createElement("table");
    table1.id = "predmeti";
    var row1 = table1.insertRow();
    this.kontejnerAdmin = row1;
    let staticInfo = [
      " ",
      "Naziv predmeta",
      "Espb",
      "Sifra predmeta",
      "Semestar",
      "Smer",
      " ",
    ];
    row1 = table1.insertRow();
    for (let i = 0; i < staticInfo.length; i++) {
      let cell = row1.insertCell();
      cell.innerHTML = staticInfo[i];
    }
    var j = 0;
    this.ispiti.forEach((i) => {
      row1 = table1.insertRow();
      row1.id = i.sifra_predmeta;
      var cell1;
      cell1 = row1.insertCell();
      cell1.innerHTML = j + 1;

      cell1 = row1.insertCell();
      cell1.innerHTML = i.naziv_predmeta;

      cell1 = row1.insertCell();
      cell1.innerHTML = i.espb;

      cell1 = row1.insertCell();
      cell1.innerHTML = i.sifra_predmeta;

      cell1 = row1.insertCell();
      cell1.innerHTML = i.semestar;

      cell1 = row1.insertCell();
      cell1.innerHTML = i.smer;

      cell1 = row1.insertCell();
      var btn = document.createElement("button");
      btn.innerHTML = "Obrisi";
      btn.onclick = (ev) => {
        this.ObrisiPredmet(i.sifra_predmeta);
      };
      cell1.appendChild(btn);

      j++;
    });
    host.appendChild(table1);
  }

  PreuzmiPredmete() {
    this.ispiti.length = 0;
    fetch("https://localhost:7078/getSubjects", {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + sessionStorage.getItem("token"),
      },
    })
      .then((response) => response.json())
      .then((data) => {
        if (data.title == "Unauthorized")
          alert("Lose korisnicko ime ili sifra.");
        else {
          data.forEach((element) => {
            var p1 = new predmet(
              element.sifra_Predmeta,
              element.espb,
              element.nazivPredmeta,
              element.semestar,
              element.smer
            );
            this.dodajIspit(p1);
          });
          this.crtajPredmete(document.getElementById("crtajPredmete"));
        }
      })
      .catch((error) => console.error("Greska", error));
  }

  ObrisiPredmet(sifra) {
    fetch("https://localhost:7078/deletePredmet/" + sifra, {
      method: "DELETE",
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + sessionStorage.getItem("token"),
      },
      body: JSON.stringify({}),
    })
      .then((p) => {
        if (p.ok) {
          alert("Predmet uspesno obrisan");
          location.href = "administrator.html";
        } else {
          alert("Greska kod brisanja");
        }
      })
      .catch((p) => {
        alert("Greška sa konekcijom.");
      });
  }

  DodajPredmet() {
    var sifraP = document.getElementById("sifraDodavanjaPredmeta").value;
    var espbP = document.getElementById("espbPredmeta").value;
    var nazivP = document.getElementById("nazivDodavanjaPredmeta").value;
    var semestarP = document.getElementById("semestarPredmeta").value;
    var smerP = document.getElementById("smerPredmeta").value;
    if (
      sifraP != "" &&
      espbP != "" &&
      nazivP != "" &&
      semestarP != "" &&
      smerP != ""
    ) {
      fetch("https://localhost:7078/addPredmet", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
        body: JSON.stringify({
          sifra_Predmeta: sifraP,
          espb: espbP,
          nazivPredmeta: nazivP,
          semestar: semestarP,
          smer: smerP,
        }),
      })
        .then((p) => {
          if (p.ok) {
            alert("Uspesno dodavanje predmeta");
            location.href = "administrator.html";
          } else {
            alert("Greska kod dodavanja");
          }
        })
        .catch((p) => {
          alert("Greška sa konekcijom.");
        });
    } else alert("Unesite sva polja");
  }

  crtajProfesore(host) {
    var b = document.getElementById("btnPrikaziProfesore");
    b.hidden = true;
    var table1 = document.createElement("table");
    table1.id = "profesori";
    var row1 = table1.insertRow();
    this.kontejnerAdmin = row1;
    let staticInfo = [
      " ",
      "Email",
      "Ime",
      "Prezime",
      "Broj telefona",
      "Kancelarija",
      " ",
      " "
    ];
    row1 = table1.insertRow();
    for (let i = 0; i < staticInfo.length; i++) {
      let cell = row1.insertCell();
      cell.innerHTML = staticInfo[i];
    }
    var j = 0;
    this.profesori.forEach((i) => {
      row1 = table1.insertRow();
      row1.id = i.email;
      var cell1;
      cell1 = row1.insertCell();
      cell1.innerHTML = j + 1;
      cell1 = row1.insertCell();
      cell1.innerHTML = i.email;

      cell1 = row1.insertCell();
      cell1.innerHTML = i.ime;

      cell1 = row1.insertCell();
      cell1.innerHTML = i.prezime;

      cell1 = row1.insertCell();
      cell1.innerHTML = i.brtel;

      cell1 = row1.insertCell();
      cell1.innerHTML = i.kancelarija;

      cell1 = row1.insertCell();
      var btn = document.createElement("button");
      btn.innerHTML = "Obrisi";
      btn.onclick = (ev) => {
        this.ObrisiProfesora(i.email);
      };
      cell1.appendChild(btn);

      cell1 = row1.insertCell();
      
      var sel = document.createElement("input");
      sel.style = "width:200px";
      sel.id = i.brtel;
      cell1.appendChild(sel);
      
      var btn1 = document.createElement("button");
      btn1.innerHTML = "Dodaj predmet";
      btn1.onclick = (ev) => {
        this.DodajPredmetProfesoru(i.email, i.brtel);
      };
      cell1.appendChild(btn1);

      j++;
    });
    host.appendChild(table1);
  }

  DodajPredmetProfesoru(email, brtelefona)
  {
    var sifra = document.getElementById(brtelefona).value;
    if (email != "")
    {
    fetch("https://localhost:7078/AddPredajePredmet", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
        body: JSON.stringify({
          id: "string",
          sifra_predmeta: sifra,
          email_profesora: email,
        }),
      })
        .then((p) => {
          if (p.ok) {
            alert("Uspesno dodavanje predmeta");
            location.href = "administrator.html";
          } else {
            alert("Greska kod dodavanja");
          }
        })
        .catch((p) => {
          alert("Greška sa konekcijom.");
        });
      }
     else alert("Unesite sva polja");
      }

  VratiNazivePredmetaZaProfesore(email)
  {
    this.ispiti.length = 0;
    fetch("https://localhost:7078/VratiPredmeteKojeNePredaje/" +email, {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + sessionStorage.getItem("token"),
      },
    })
      .then((response) => response.json())
      .then((data) => {
        if (data.title == "Unauthorized")
          alert("Lose korisnicko ime ili sifra.");
        else {
          data.forEach((element) => {
            var p1 = new predmet(
              element.sifra_Predmeta,
              element.espb,
              element.nazivPredmeta,
              element.semestar,
              element.smer
            );
            this.dodajIspit(p1);
          });
          //this.PreuzmiProfesore();
          //this.crtajPredmete(document.getElementById("crtajPredmete"));
        }
      })
      .catch((error) => console.error("Greska", error));
  }

  ObrisiProfesora(email) {
    fetch("https://localhost:7078/deleteProfesor/" + email, {
      method: "DELETE",
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + sessionStorage.getItem("token"),
      },
      body: JSON.stringify({}),
    })
      .then((p) => {
        if (p.ok) {
          alert("Profesor uspesno obrisan");
          location.href = "administrator.html";
        } else {
          alert("Greska kod brisanja");
        }
      })
      .catch((p) => {
        alert("Greška sa konekcijom.");
      });
  }

  PreuzmiProfesore() {
    this.profesori.length = 0;
    fetch("https://localhost:7078/getProfesore", {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + sessionStorage.getItem("token"),
      },
    })
      .then((response) => response.json())
      .then((data) => {
        if (data.title == "Unauthorized")
          alert("Lose korisnicko ime ili sifra.");
        else {
          data.forEach((element) => {
            var p1 = new profesor(
              element.email,
              element.ime,
              element.prezime,
              element.godina,
              element.br_telefona,
              element.kancelarija
            );
            this.dodajProfesora(p1);
          });
          this.crtajProfesore(document.getElementById("crtajProfesore"));
          
        }
      })
      .catch((error) => console.error("Greska", error));
  }
}
