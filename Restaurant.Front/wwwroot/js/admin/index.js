"use strict";

import connection from "../connection.js";
import { createPopup } from "./index/createPopup.js";

var currentlyShowing = "dish"

var conn

connection.on("ReturnConnected", function () { // If page is connected to backend this runs
    conn = connection
    connection.invoke("GetMenuItems", "admin").catch(function (err) { })
    connection.invoke("GetAllDishes").catch(function (err) { })
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

        var name = document.createElement("p")
        name.innerText = ingredient.name

        name.classList.add("name")
        div.appendChild(name)

        var diet = document.createElement("p")
        switch (ingredient.diet) {
            case 0:
                diet.innerText += "meat"
                break;
            case 1:
                diet.innerText += "vegetarian"
                break;
            case 2:
                diet.innerText += "vegan"
                break;
        }
        diet.classList.add("diet")
        div.appendChild(diet)

        var edit = document.createElement("i")
        edit.classList = "edit fas fa-edit"
        edit.id = "edit_" + div.id
        div.appendChild(edit)

        var remove = document.createElement("i")
        remove.classList = "trash fas fa-trash-alt"
        remove.id = "remove_" + div.id
        div.appendChild(remove)

        document.getElementById("list").appendChild(div)
        //addEditListener(edit.id)
        addRemoveListener(remove.id)
    })
})

connection.on("SendAllDishes", function (dishes) {
    currentlyShowing = "dish"
    document.getElementById("pickedList").classList = "pickedlist dish"
    document.getElementById("pickedList").innerText = "Dishes"
    document.getElementById("create").innerText = "Create Dish"
    document.getElementById("list").innerHTML = '';
    document.getElementById("list").classList = "list dishes"
})

document.getElementById("dishButton").addEventListener("click", function (event) {
    connection.invoke("GetAllDishes").catch(function (err) { })
})

document.getElementById("ingredientButton").addEventListener("click", function (event) {
    connection.invoke("GetAllIngredients").catch(function (err) { })
})

document.getElementById("create").addEventListener("click", function () {
    createPopup(currentlyShowing, connection)
})

// function addEditListener(id) {
//     var edit = document.getElementById("list").getElementById(id)
//     edit.addEventListener("click", function(e) {

//     })
// }

function addRemoveListener(id) {
    var remove = document.getElementById(id)
    remove.addEventListener("click", function (e) {
        console.log(id.substring(18))
        connection.invoke("RemoveIngredient", id.substring(18))
    })
}