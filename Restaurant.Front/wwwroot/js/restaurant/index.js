"use strict";

import connection from "../connection.js";
import {
    orderPopup,
    order
} from "./popup.js";

connection.on("ReturnConnected", function () { // If page is connected to backend this runs
    console.log("Connected to backend!")
    connection.invoke("GetMenuItems", "admin").catch(function (err) {
        console.error(err)
    })
    connection.invoke("GetAllTables").catch(function (err) {
        console.error(err)
    })
})

connection.on("SendAllTables", function (tables) {
    document.getElementById("list").innerHTML = '';
    tables.forEach(table => {
        console.log(table)
        var div = document.createElement("div")
        div.className = "table";
        div.id = "table_" + table.tableNumber;

        var p = document.createElement("p")
        p.className = "name"
        p.innerText = table.tableNumber

        div.appendChild(p)

        var p2 = document.createElement("p")
        var html = `<i class="button"/>`

        switch (table.status) {
            case 0:
                p2.innerText = "Free"
                p2.className = "status free"
                html = `<i class="button assign fas fa-user-plus" id="assign_guest_` + table.tableNumber + `"></i>`

                break;
            case 1:
                div.classList.add("taken")
                p2.innerText = "Taken"
                p2.className = "status taken"

                html = `
                <p class="price" id="price_"` + table.tableNumber + `">€ ` + table.totalPrice + `</p>
                <i class="button order fas fa-utensils" id="order_guest_` + table.tableNumber + `"></i>
                `

                if (table.totalPrice == 0) html += `<i class="button remove fas fa-user-minus" id="remove_guest_` + table.tableNumber + `"></i>`
                else html += `<i class="button pay fas fa-shopping-cart" id="pay_guest_` + table.tableNumber + `"></i>`
                break;
            case 2:
                p2.innerText = "Ordered"
                p2.className = "status ordered"
                break;
            case 3:
                p2.innerText = "Delivered"
                p2.className = "status delivered"
                break;
            case 4:
                p2.innerText = "Paid"
                p2.className = "status paid"
                break;
        }

        div.appendChild(p2)

        div.innerHTML += html;

        document.getElementById("list").appendChild(div)

        switch (table.status) {
            case 0:
                addAssignListener(table.tableNumber);
                break;
            case 1:
                addOrderListener(table.tableNumber);
                if (table.totalPrice == 0) addRemoveListener(table.tableNumber);
                else addPayListener(table.tableNumber)
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
        }
    });
})

connection.on("SendAllDishes", function (dishes) {
    console.log(dishes)
    dishes.forEach(function (dish) {
        var div = document.createElement("div")
        div.classList = "dish"
        div.id = "dish_" + dish.name

        var item = document.createElement("div")
        item.classList = "item"

        var name = document.createElement("p")
        name.className = "name"
        name.innerText = dish.name
        item.appendChild(name)

        var minus = document.createElement("i")
        minus.className = "fas fa-minus disabled minus"
        minus.id = "minus_" + dish.name
        item.appendChild(minus)

        var amount = document.createElement("p")
        amount.className = "amount"
        amount.id = "amount_" + dish.name
        amount.innerText = "0"
        item.appendChild(amount)

        var plus = document.createElement("i")
        plus.className = "fas fa-plus disabled plus"
        plus.id = "plus_" + dish.name
        item.appendChild(plus)

        div.appendChild(item)

        document.getElementById("dishIngredients").appendChild(div)

        var current = 0;

        document.getElementById("minus_" + dish.name).addEventListener("click", function (e) {
            if (current > 0) {
                current--;
                let index = order.findIndex(d => d.key == dish.name)
                order[index].value = current
                document.getElementById("amount_" + dish.name).innerText = current
            } else {
                order = order.filter(function (d) { return d.key != dish.name})
            }

            console.log(order)
        })

        document.getElementById("plus_" + dish.name).addEventListener("click", function (e) {
            current++
            if (current == 1) {
                order.push({key: dish.name, value: current})
            } else {
                let index = order.findIndex(d => d.key == dish.name)
                order[index].value = current
            }
            document.getElementById("amount_" + dish.name).innerText = current
        })
    })
})

function addOrderListener(tableNum) {
    var order = document.getElementById("order_guest_" + tableNum)
    order.addEventListener("click", function (e) {
        orderPopup();
        createConfirmListener(tableNum);
    })
}

function addRemoveListener(tableNum) {
    var remove = document.getElementById("remove_guest_" + tableNum)
    remove.addEventListener("click", function (e) {
        connection.invoke("RemoveGuestFromTable", tableNum).catch(function (err) {
            console.error(err)
        })
    })
}

function addAssignListener(tableNum) {
    var assign = document.getElementById("assign_guest_" + tableNum)
    assign.addEventListener("click", function (e) {
        connection.invoke("AssignGuestToTable", tableNum).catch(function (err) {
            console.error(err)
        })
    })
}

function createConfirmListener(table) {
    var confirm = document.getElementById("confirm")
    confirm.addEventListener("click", function (e) {
        console.log(JSON.stringify(order))
        order.push({key: "", value: table})
        connection.invoke("CreateOrder", JSON.stringify(order))
    })
}

function addPayListener(num) {
    var pay = document.getElementById("pay_guest_" + num)
    pay.addEventListener("click", function(e) {
        connection.invoke("PayForTable", num).catch(function (err) {
            console.error(err)
        })
    })
}