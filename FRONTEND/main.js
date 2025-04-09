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

// Tárgy hozzáadása
addBtn.addEventListener("click", function() {
    const itemName = document.getElementById("floatingInputGroup1").value;
    const itemWidth = parseFloat(document.getElementById("floatingInputGroup2").value);
    const itemHeight = parseFloat(document.getElementById("floatingInputGroup3").value);
    const itemArea = (itemWidth * itemHeight) / 10000;
    const newRoomWidth = roomData.width;
    const newRoomHeight = roomData.height; 
    warningAlert.classList.add("d-none");

    // Hibaüzenetek
    if (roomData.area == null) {
        AlertWrite(warningAlert, "<strong>Először a szoba méretét határozd meg!</strong>");
        return;
    }
    if (!itemName || isNaN(itemWidth) || isNaN(itemHeight) || itemWidth <= 0 || itemHeight <= 0) {
        AlertWrite(warningAlert, "<strong>Adj meg érvényes nevet és méretet!</strong>");
        return;
    }
    if (itemWidth > newRoomWidth || itemHeight > newRoomHeight || itemArea > roomData.area) {
        AlertWrite(warningAlert, "<strong>A tárgy biztosan nem fér bele a szobába!</strong>");
        return;
    }

    // Tárgyak hozzáadása a tömbhöz
    items.push({ itemName, itemArea, itemWidth, itemHeight });

    // Ellenőrzés, hogy az eddigi elemek összmérete meghaladja-e a szoba összméretét
    let sumArea = (items.reduce((sum, element) => sum + element.itemArea, 0));

    if (sumArea > roomData.area){
        items.pop({ itemName, itemArea, itemWidth, itemHeight });
        AlertWrite(warningAlert, "<strong>Az össznégyzet méter-t meghaladja!</strong>");
        return;
    }

    // Tárgy hozzáadása szerveroldalon
    fetch(apiUrlItem, {
        method: "POST",
        headers: { "Content-Type" : "application/json", },
        body: JSON.stringify({
            name: itemName,
            width: itemWidth,
            height: itemHeight
        })
    })
    .then(resp => {
        console.log("Response: ", resp);
        displayItems();
    })
    .catch(error => console.log(error))

    // Táblázathoz új sor hozzáadása
    const table = document.getElementById("itemsTable");
    const row = table.insertRow();

    // Cellákba való értékek beszúrása
    row.insertCell(0).textContent = itemName;
    row.insertCell(1).textContent = itemWidth + "x" + itemHeight + "cm" + " (" + itemArea + "m²)";

    // Törlés gomb létrehozása
    const deleteCell = row.insertCell(2);
    const deleteBtn = document.createElement("button");
    deleteBtn.textContent = "Törlés";
    deleteBtn.classList.add("btn", "btn-danger");
    deleteBtn.addEventListener("click", async function() {
        const itemNameToDelete = row.cells[0].textContent;
        const itemIndex = items.findIndex(item => item.itemName === itemNameToDelete);
        
        if (itemIndex > -1) {
            try {            
                const response = await fetch(apiUrlItem);
                const dbItems = await response.json();
                
                // Megkeressük a nevet a kapott adatok között
                const dbItem = dbItems.find(item => item.name === itemNameToDelete);
                
                if (dbItem && dbItem.id) {
                    // Ha találtunk azonosítót, akkor töröljük az adatbázisból
                    await fetch(`${apiUrlItem}/${dbItem.id}`, {
                        method: "DELETE"
                    });
                    
                    // Ha sikeres volt, töröljük a memóriából és a táblázatból
                    items.splice(itemIndex, 1);
                    table.deleteRow(row.rowIndex);

                    // Frissítjük a szoba div-t is
                    const roomItems = document.querySelectorAll(".item");
                    roomItems.forEach(item => {
                        item.remove();
                    });
                    
                    displayItems();
                } else {
                    AlertWrite(warningAlert, "<strong>Nem található az elem azonosítója!</strong>");
                }
            } catch (error) {
                console.error("Hiba történt:", error);
                AlertWrite(warningAlert, "<strong>Hiba történt a törlés során!</strong>");
            }
        }
    });
    deleteCell.appendChild(deleteBtn);

    // Input mezők törlése
    document.getElementById("floatingInputGroup1").value = "";
    document.getElementById("floatingInputGroup2").value = "";
    document.getElementById("floatingInputGroup3").value = "";
    displayItems();
});

// Koordináták lekérése
function fetchCoordinates() {
    return fetch("http://localhost:5166/api/RoomPlanner/generate")
        .then(response => {
            if (!response.ok) {
                throw new Error("Hálózati hiba vagy nincs szoba adat!");
            }
            return response.json();
        })
        .then(data => {
            console.log("Elhelyezett tárgyak koordinátái: ", data);
            return data;
        })
        .catch(error => {
            console.error("Hiba történt: ", error);
        });
}

// Szoba megjelenítés generálás gombra
generateBtn.addEventListener("click", function () {
    if (roomData.area !== null) {
        fetchCoordinates().then(coords => {
            if (coords) {
                const rendered = renderItemsInRoom(coords);
                console.log("Renderelt: " + rendered);
                if (rendered) {
                    // Csak akkor görgessen le, ha sikerült a generálás
                    let offset = 350;
                    const elementPosition = roomElement.getBoundingClientRect().top + window.pageYOffset;
                    window.scrollTo({
                        top: elementPosition - offset,
                        behavior: 'smooth'
                    });
                }
            }
        });
    } else {
        AlertWrite(lightAlert, "<strong>Még nem adtál meg adatokat!</strong>");
    }
});