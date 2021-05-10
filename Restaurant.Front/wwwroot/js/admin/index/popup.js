var nameValid = false
var priceValid = false

var isNewItem = true

var itemData

var connection

function popup(isNew, currentlyShowing, conn, name, data) {
    connection = conn
    isNewItem = isNew
    itemData = data

    var popup = document.createElement("div")
    popup.id = "popup"

    var popupBack = document.createElement("div")
    popupBack.classList = "popup-back"
    popupBack.id = "popup-back"
    popup.appendChild(popupBack)

    var popupMain = document.createElement("div")
    popupMain.classList = "popup " + currentlyShowing + "_popup"

    var top = document.createElement("div")
    top.classList = "top"

    var popupHeader = document.createElement("h2")
    if (isNew) popupHeader.innerText = "Create new " + currentlyShowing;
    else popupHeader.innerText = "Edit " + currentlyShowing + " " + name;

    top.appendChild(popupHeader)

    var cancel = document.createElement("i")
    cancel.classList = "cancel fas fa-times"
    cancel.id = "cancel"

    top.appendChild(cancel)

    popupMain.appendChild(top)

    var popupContent = document.createElement("div")
    popupContent.classList = "popup-content"

    if (currentlyShowing === "dish") {
        var html = getNameBox(name) + getPriceBox(data)
            

    } else if (currentlyShowing === "ingredient") {
        var html =
            getNameBox(name) + `
        <p class="diet">Diet:</p>
        <div class="dietInput">
            <label class="box">Vegan
                <input type="radio" ` + getDietEnabled(2) + `id="vegan" name="dietPicker">
                <span class="checkmark"></span>
            </label>

            <label class="box">Vegetarian
                <input type="radio" ` + getDietEnabled(1) + `id="vegetarian" name="dietPicker">
                <span class="checkmark"></span>
            </label>

            <label class="box">Meat
                <input type="radio" ` + getDietEnabled(0) + `id="meat" name="dietPicker">
                <span class="checkmark"></span>
            </label>
        </div>
        `;


    }
    popupContent.innerHTML += html

    var btn = document.createElement("input")
    btn.type = "button"
    btn.id = "confirm"
    btn.value = "Confirm"
    btn.classList = "confirm"

    popupContent.appendChild(btn)


    popupMain.appendChild(popupContent)

    if (currentlyShowing === "dish") {
        var ingredients = document.createElement("div")
        ingredients.classList = "dishIngredients"
        ingredients.id = "dishIngredients"

        popupMain.appendChild(ingredients)
    }

    popup.appendChild(popupMain)

    document.getElementById("admin-home").append(popup)
    createCancelListener()
    createConfirmListener(isNew, conn, name, currentlyShowing)
    nameListener()
    if (currentlyShowing === "dish") {
        priceListener()
        connection.invoke("GetDishIngredients", name)
    }
}


function createCancelListener() {
    document.getElementById("cancel").addEventListener("click", function (event) {
        var popup = document.getElementById("popup")
        if (popup !== null) popup.remove()
    })

    document.getElementById("popup-back").addEventListener("click", function (event) {
        var popup = document.getElementById("popup")
        if (popup !== null) popup.remove()
    })
}

function createConfirmListener(isNew, conn, oldName, currentlyShowing) {
    document.getElementById("confirm").addEventListener("click", function (event) {
        console.log("test")
        var name = document.getElementById("name").value
        if (currentlyShowing === "ingredient") {
            var vegan;
            if (document.getElementById("meat").checked === true) vegan = 0
            else if (document.getElementById("vegetarian").checked === true) vegan = 1
            else vegan = 2

            if (isNew) connection.invoke("AddIngredient", name, vegan).catch(function (err) {});
            else connection.invoke("EditIngredient", oldName, name, vegan).catch(function (err) {});
        } else {
            var price = document.getElementById("price").value
            
            console.log(price)
            var dishIngredients = getDishIngredients()

            console.log(dishIngredients)

            if (isNew) connection.invoke("AddDish", name, price, dishIngredients).catch(function (err) {console.log(err)});
            else connection.invoke("EditDish", oldName, name, price, dishIngredients).catch(function (err) {});
        }
    })
}

function nameListener() {
    document.getElementById("name").addEventListener("input", function (event) {
        var nameWarning = document.getElementById("namewarning")
        if (event.target.value === "") {
            nameWarning.innerText = "Name can't be empty!"
            valid = false
        } else {
            nameWarning.innerText = " "
            nameValid = true
        }
    })
}

function getPriceBox(price) {
    if (price == null) return `
        <div class="pricecontainer">
            <p class="price">Price:</p>
            <input id="price" class="pricebox" >
            <p class="pricewarning" id="pricewarning">Price can't be empty!</p>
        </div>
        `
    else return `
        <div class="pricecontainer">
            <p class="price">Price:</p>
            <input id="price" class="pricebox" value="` + price + `">
            <p class="pricewarning" id="pricewarning"></p>
    `
}

function priceListener() {
    document.getElementById("price").addEventListener("input", function (event) {
        var priceWarning = document.getElementById("pricewarning")
        if (event.target.value === "") {
            priceWarning.innerText = "Price can't be empty!"
            valid = false
        } else {
            priceWarning.innerText = " "
            priceValid = true
        }
    })
}



function getDietEnabled(num) {
    if (itemData === num || (isNewItem && num === 2)) return `checked="checked"`;
    return "";
}

function getNameBox(name) {
    if (name === null || name === undefined) return `
    <div class="namecontainer">    
        <p class="name">Name:</p>
        <input class="namebox" id="name" value="">
        <p class="namewarning" id="namewarning">Name can't be empty!</p>
    </div>`
    else return `
    <div class="namecontainer">    
        <p class="name">Name:</p>
        <input class="namebox" id="name" value="` + name + `">
        <p class="namewarning" id="namewarning"></p>
    </div>`;
}

function getDishIngredients() {
    var elements = Array.prototype.slice.call(document.getElementById("dishIngredients").children)
    var ingredientNames = []
    elements.forEach(element => {
        if (element.children[0].children[0].checked) {
            ingredientNames.push(element.id.substring(15))
        }
    })

    return ingredientNames
}

export {
    popup
};