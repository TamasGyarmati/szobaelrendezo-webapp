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

// Szoba méret mentés gomb
saveBtn.addEventListener("click", function() {
    const newRoomWidth = parseFloat(roomWidthInput.value);
    const newRoomHeight = parseFloat(roomHeightInput.value);
    const newRoomArea = (newRoomHeight * newRoomWidth) / 10000;
    const invalidItem = items.some(item =>
        item.itemWidth > newRoomWidth || item.itemHeight > newRoomHeight    
    );
    let sumArea = (items.reduce((sum, element) => sum + element.itemArea, 0));
    dangerAlert.classList.add("d-none");

    // Hibaüzenetek
    if (newRoomWidth > 1000 || newRoomHeight > 1000) {
        AlertWrite(dangerAlert, "<strong>Maximum szoba méret: 1000x1000!</strong>");
        return;
    }
    if (!newRoomWidth || !newRoomHeight || isNaN(newRoomWidth) || isNaN(newRoomHeight)) {
        AlertWrite(dangerAlert, "<strong>Adj meg érvényes szobaméretet!</strong>");
        return;
    }
    if (sumArea > newRoomArea) {
        AlertWrite(dangerAlert, "<strong>A hozzáadott tárgyak mérete nagyobb mint az új szoba mérete!</strong>");
        return;
    }
    if (invalidItem) {
        AlertWrite(dangerAlert, "<strong>A szoba szélessége/magassága kevesebb mint egy adott tárgyé!</strong>");
        return;
    }

    // Mentés gomb 'PUT' request
    fetch(apiUrlRoom, {
        method: "PUT",
        headers: { "Content-Type" : "application/json" },
        body: JSON.stringify({
            id: 1,
            width: newRoomWidth,
            height: newRoomHeight
        })
    })
    .then(resp => {
        console.log("Response: ", resp);
    })
    .catch(error => console.log(error));

    // Az új értékeket elmentjük a tömbbe
    roomData.width = newRoomWidth;
    roomData.height = newRoomHeight;
    roomData.area = newRoomArea;

    // Szoba méret kiírása
    roomSizePtag.textContent = "Szoba mérete: " + roomData.width + "x" + roomData.height + "cm" + " (" + roomData.area + "m²)" ;

    // Input mezők nullázása (ez miatt kellett a 'roomData')
    roomWidthInput.value = "";
    roomHeightInput.value = "";
});