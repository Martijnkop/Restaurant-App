import connection from "../connection.js";

var order = []

function orderPopup() {
    var popup = document.createElement("div")
    popup.id = "popup"

    var popupBack = document.createElement("div")
    popupBack.classList = "popup-back"
    popupBack.id = "popup-back"
    popup.appendChild(popupBack)

    var popupMain = document.createElement("div")
    popupMain.classList = "popup order_popup"

    var top = document.createElement("div")
    top.classList = "top"

    var popupHeader = document.createElement("h2")
    popupHeader.innerText = "Dishes"

    top.appendChild(popupHeader)

    var confirm = document.createElement("i")
    confirm.classList = "confirm fas fa-check"
    confirm.id = "confirm"

    top.appendChild(confirm)

    var cancel = document.createElement("i")
    cancel.classList = "cancel fas fa-times"
    cancel.id = "cancel"

    top.appendChild(cancel)

    popupMain.appendChild(top)


    var popupContent = document.createElement("div")
    popupContent.classList = "popup-content"

    var ingredients = document.createElement("div")
        ingredients.classList = "dishIngredients"
        ingredients.id = "dishIngredients"

        popupMain.appendChild(ingredients)

    popupMain.appendChild(popupContent)


    popup.appendChild(popupMain)

    document.getElementById("restaurant-home").append(popup)

    createCancelListener()

    connection.invoke("GetAllDishes")
}

function createCancelListener() {
    document.getElementById("cancel").addEventListener("click", function (event) {
        var popup = document.getElementById("popup")
        order = []
        if (popup !== null) popup.remove()
    })

    document.getElementById("popup-back").addEventListener("click", function (event) {
        var popup = document.getElementById("popup")
        order = []
        if (popup !== null) popup.remove()
    })
}

function createConfirmListener(dishes) {
    document.getElementById("confirm").addEventListener("click", function (e) {
        connection.invoke("CreateOrder", dishes)
        var popup = document.getElementById("popup")
        if (popup !== null) popup.remove()
    })
}

export {
    orderPopup,
    order
};