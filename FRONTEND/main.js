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