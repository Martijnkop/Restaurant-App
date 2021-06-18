"use strict";

import connection from "../connection.js";
import { popup } from "./index/popup.js";

var currentlyShowing = "dish"

var conn

connection.on("ReturnConnected", function () { // If page is connected to backend this runs
    console.log("Connected to backend!")
    conn = connection
    connection.invoke("GetMenuItems", "admin").catch(function (err) { console.error(err) })
    connection.invoke("GetAllDishes").catch(function (err) { console.error(err) })
})

connection.on("SendAllIngredients", function (ingredients) {
    currentlyShowing = "ingredient"
    document.getElementById("pickedList").classList = "pickedlist ingredient"
    document.getElementById("pickedList").innerText = "Ingredients"
    document.getElementById("create").innerText = "Create Ingredient"
    document.getElementById("list").innerHTML = '';
    document.getElementById("list").classList = "list ingredients"
    ingredients.forEach(ingredient => {
        console.log(ingredient)
        
        var div = document.createElement("div")
        div.classList = "list-element"
        div.id = "ingredient_" + ingredient.name
        
        var html =
        `
        <p class="name">`+ingredient.name+`</p>
        <p class="diet">`+ingredientDiet(ingredient.diet)+`</p>
        <i class="edit fas fa-edit" id="edit_ingredient_`+ingredient.name+`"></i>
        <i class="trash fas fa-trash-alt" id="remove_ingredient_`+ingredient.name+`"></i>
        `

        div.innerHTML += html

        document.getElementById("list").appendChild(div)
        addIngredientEditListener(ingredient.name, ingredient.diet)
        addRemoveListener(ingredient.name, "ingredient")
    })
})

connection.on("SendAllDishes", function (dishes) {
    currentlyShowing = "dish"
    document.getElementById("pickedList").classList = "pickedlist dish"
    document.getElementById("pickedList").innerText = "Dishes"
    document.getElementById("create").innerText = "Create Dish"
    document.getElementById("list").innerHTML = '';
    document.getElementById("list").classList = "list dishes"

    dishes.forEach(dish => {
        console.log(dish)

        var div = document.createElement("div")
        div.classList = "list-element"
        div.id = "dish_" + dish.name

        var html =
        `
        <p class="name">`+dish.name+`</p>
        <p class="price">€ `+dish.price+`</p>
        <i class="edit fas fa-edit" id="edit_dish_`+dish.name+`"></i>
        <i class="trash fas fa-trash-alt" id="remove_dish_`+dish.name+`"></i>
        `
        
        div.innerHTML += html

        document.getElementById("list").appendChild(div)
        addDishEditListener(dish.name, dish.price)
        addRemoveListener(dish.name, "dish")
    })
})

connection.on("SendAllTables", function(tables) {
    currentlyShowing = "table"
    document.getElementById("pickedList").classList = "pickedlist table"
    document.getElementById("pickedList").innerText = "Tables"
    document.getElementById("create").innerText = "Create Table"
    document.getElementById("list").innerHTML = '';
    document.getElementById("list").classList = "list tables"

    tables.forEach(table => {
        console.log(table)
        var div = document.createElement("div")
        div.classList = "list-element"
        div.id = "table_" + table.tableNumber

        if (table.status == "Free") var html =
        `
        <p class="name">`+table.tableNumber+`</p>
        <p class="name">Free</p>
        <i class="edit fas fa-edit" id="edit_table_`+table.tableNumber+`"></i>
        <i class="trash fas fa-trash-alt" id="remove_table_`+table.tableNumber+`"></i>
        `
        
        else var html = `
        <p class="name">`+table.tableNumber+`</p>
        <p class="name">`+getTableStatus(table.status)+`</p>
        <p class="name">€ `+table.totalPrice+`</p>
        <i class="edit fas fa-edit" id="edit_table_`+table.tableNumber+`"></i>
        <i class="trash fas fa-trash-alt" id="remove_table_`+table.tableNumber+`"></i>
        `

        div.innerHTML += html

        document.getElementById("list").appendChild(div)
        addTableEditListener(table.tableNumber)
        addRemoveListener(table.tableNumber, "table")
    })
})

document.getElementById("dishButton").addEventListener("click", function (event) {
    connection.invoke("GetAllDishes").catch(function (err) { console.error(err) })
})

document.getElementById("ingredientButton").addEventListener("click", function (event) {
    connection.invoke("GetAllIngredients").catch(function (err) { console.error(err) })
})

document.getElementById("tableButton").addEventListener("click", function (event) {
    connection.invoke("GetAllTables").catch(function (err) { console.error(err) })
})

document.getElementById("create").addEventListener("click", function () {
    popup(true, currentlyShowing, connection)
})

connection.on("SendDishIngredients", function (ingredients) {
    //console.log(ingredients)
    Object.entries(ingredients).forEach(([key, value]) => {
        var div = document.createElement("div")
        div.classList = "dishIngredient"
        div.id = "dishIngredient_" + key

        var html =
        `
        <label class="add">`+key+`
            <input type="checkbox" ` +checked(value)+ ` id="checkbox" >
            <span class="checkmark"></span>
        </label>
        `

        div.innerHTML = html

        document.getElementById("dishIngredients").appendChild(div)
    })
})

function checked(bool) {
    if (bool) return `checked="checked"`;
    else return "";
}


function addIngredientEditListener(id, diet) {
    var edit = document.getElementById("edit_ingredient_" + id)
    edit.addEventListener("click", function (e) {
        popup(false, currentlyShowing, connection, id, diet)
    })
}

function addDishEditListener(id, price) {
    var edit = document.getElementById("edit_dish_" + id)
    edit.addEventListener("click", function (e) {
        popup(false, currentlyShowing, conn, id, price)
        //connection.invoke("GetDishInfo", id)
    })
}

function addTableEditListener(number) {
    var edit = document.getElementById("edit_table_" + number)
    edit.addEventListener("click", function (e) {
        popup(false, currentlyShowing, connection, number)
    })
}

function addRemoveListener(id, type) {
    var remove = document.getElementById("remove_" + currentlyShowing + "_" + id)
    switch (type) {
        case "ingredient":
            remove.addEventListener("click", function (e) { connection.invoke("RemoveIngredient", id ) })
            break;
        case "dish":
            remove.addEventListener("click", function (e) { connection.invoke("RemoveDish", id ) })
            break;
        case "table":
            remove.addEventListener("click", function(e) {connection.invoke("RemoveTable", id ) })
            break;
    }
}

function ingredientDiet(id) {
    switch (id) {
        case 0:
            return 'meat';
        case 1:
            return 'vegetarian';
        case 2:
            return 'vegan';
    }
}

function getTableStatus(status) {
    switch (status) {
        case 0:
            return "Free"
        case 1:
            return "Taken"
        case 2:
            return "Ordered"
        case 3:
            return "Delivered"
        case 4:
            return "Paid"
}}