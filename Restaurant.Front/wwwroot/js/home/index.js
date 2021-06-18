"use strict";

import connection from "../connection.js";

connection.on("ReturnConnected", function () { // If page is connected to backend this runs
    console.log("Connected to backend!")
    connection.invoke("GetMenuItems", "admin").catch(function (err) { console.error(err) })
})