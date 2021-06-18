"use strict";

import connection from "../connection.js";

connection.on("ReturnConnected", function () { // If page is connected to backend this runs
    console.log("Connected to backend!")
    connection.invoke("GetMenuItems", "admin").catch(function (err) { console.error(err) })
})

connection.on("SendKitchenOrder", function (order) {
    console.log(order)

    var temp = document.createElement("div")
    temp.className = "fullOrder"


    order.forEach(function (dish) {
        console.log(dish)
        var div = document.createElement("p")
        div.classList = "dish"
        div.id = "dish_" + dish.name
        div.innerText = dish.name

        temp.appendChild(div)
        
    })
    
    document.getElementById("list").appendChild(temp)
})