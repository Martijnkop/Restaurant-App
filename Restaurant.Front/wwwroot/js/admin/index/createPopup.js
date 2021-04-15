var valid = false

function createPopup(currentlyShowing, conn) {

    var popup = document.createElement("div")
    popup.id = "popup"

    var popupBack = document.createElement("div")
    popupBack.classList = "popup-back"
    popup.appendChild(popupBack)

    var popupMain = document.createElement("div")
    popupMain.classList = "popup"

    var top = document.createElement("div")
    top.classList = "top"

    var popupHeader = document.createElement("h2")
    popupHeader.innerText = "Create new " + currentlyShowing

    top.appendChild(popupHeader)

    var cancel = document.createElement("i")
    cancel.classList = "cancel fas fa-times"
    cancel.id = "cancel"

    top.appendChild(cancel)

    popupMain.appendChild(top)

    if (currentlyShowing === "dish") {

    } else if (currentlyShowing === "ingredient") {
        var html =
            `
        <p class="name">Name:</p>
        <input class="namebox" id="name">
        <p class="namewarning" id="namewarning">Name can't be empty!</p>
        
        <p class="diet">Diet:</p>
        <div class="dietInput">
            <label class="box">Vegan
                <input type="radio" checked="checked" id="vegan" name="dietPicker">
                <span class="checkmark"></span>
            </label>

            <label class="box">Vegetarian
                <input type="radio" id="vegetarian" name="dietPicker">
                <span class="checkmark"></span>
            </label>

            <label class="box">Meat
                <input type="radio" id="meat" name="dietPicker">
                <span class="checkmark"></span>
            </label>
        </div>
        `;


        popupMain.innerHTML += html
    }


    var btn = document.createElement("input")
    btn.type = "button"
    btn.id = "confirm"
    btn.value = "Confirm"
    btn.classList = "confirm"

    popupMain.appendChild(btn)

    popup.appendChild(popupMain)

    document.getElementById("admin-home").append(popup)
    createCancelListener()
    createConfirmListener(conn)
    nameListener()
}


function createCancelListener() {
    document.getElementById("cancel").addEventListener("click", function (event) {
        var popup = document.getElementById("popup")
        if (popup !== null) popup.remove()
    })
}

function createConfirmListener(conn) {
    document.getElementById("confirm").addEventListener("click", function () {
        console.log("test")
        if (!valid) return;
        var name = document.getElementById("name").value
        var vegan;
        if (document.getElementById("meat").checked === true) vegan = 0
        else if (document.getElementById("vegetarian").checked === true) vegan = 1
        else vegan = 2

        conn.invoke("AddIngredient", name, vegan).catch(function (err) { })
    })
}

function nameListener() {
    document.getElementById("name").addEventListener("input", function (event) {
        var nameWarning = document.getElementById("namewarning")
        if (event.target.value === "") {
            nameWarning.innerText = "Name can't be empty!"
            valid = false
        }
        else {
            nameWarning.innerText = " "
            valid = true
        }
    })
}

export { createPopup };