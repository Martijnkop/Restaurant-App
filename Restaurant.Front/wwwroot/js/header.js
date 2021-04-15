"use strict";

import connection from "./connection.js";

connection.on("SendMenuItems", function (items) {
    items.forEach(item => {
        var menuItem = document.createElement("a")
        menuItem.innerText = item.text
        menuItem.href = item.href
        menuItem.classList = item.classes
        document.getElementById("navbar-menu").appendChild(menuItem)
    })
})