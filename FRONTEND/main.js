// Tárgyak listája
const items = [];
const apiUrlItem = "http://localhost:5166/item";
const apiUrlRoom = "http://localhost:5166/room";
const apiUrlRoomById = "http://localhost:5166/room/1";

// DOM elemek kiválasztása
let saveBtn = document.getElementById("save-btn");
let addBtn = document.getElementById("add-btn");
let generateBtn = document.getElementById("generateBtn");
let roomSizePtag = document.getElementById("szoba-size-p-tag");
let roomWidthInput = document.getElementById("szoba-merete-1");
let roomHeightInput = document.getElementById("szoba-merete-2");
let parentElement = document.querySelector("#parentgrid");
let roomElement = document.getElementById("room");
let lightAlert = document.getElementById("light-alert");
let dangerAlert = document.getElementById("danger-alert");
let warningAlert = document.getElementById("warning-alert");

// Szoba méretének beállítása és UI frissítés
let roomData = { width: null, height: null, area: null};

// Az tárgyak és szoba értékeinek nullázása szerveroldalon (oldal frissítéskor)
window.onload = function () {
    fetch(apiUrlItem, {
        method: "DELETE"
    })
    .then(resp => {
        console.log("Itemek törlődtek: ", resp);
        fetch(apiUrlRoom, {
            method: "PUT",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({
                id: 1,
                width: 0,
                height: 0
            })
        })
        .then(resp => {
            console.log("Szoba törlődött: ", resp);
        })
    })
    .catch(error => console.log(error));
}; 

// Displayek
async function displayRoom() {
    const response = await fetch(apiUrlRoomById);
    const room = await response.json(); 
    console.log("Display room: " + room);
}

async function displayItems() {
    const response = await fetch(apiUrlItem);
    const items = await response.json();
    console.log("Display items: " + items);
}

displayItems();
displayRoom();

// Alerteket kiíró fv.
function AlertWrite(x, y) {
    x.innerHTML = y;
    x.classList.remove("d-none");
}