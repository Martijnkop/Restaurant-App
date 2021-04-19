var valid = false

var isNewItem = true

var itemDiet

function popup(isNew, currentlyShowing, conn, name, diet) {
    isNewItem = isNew
    itemDiet = diet

    var popup = document.createElement("div")
    popup.id = "popup"

    var popupBack = document.createElement("div")
    popupBack.classList = "popup-back"
    popupBack.id = "popup-back"
    popup.appendChild(popupBack)

    var popupMain = document.createElement("div")
    popupMain.classList = "popup"

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

    if (currentlyShowing === "dish") {

    } else if (currentlyShowing === "ingredient") {
        var html =
            `
        <p class="name">Name:</p>
        ` + getNameBox(name) + `
        
        <p class="diet">Diet:</p>
        <div class="dietInput">
            <label class="box">Vegan
                <input type="radio" ` + getDietEnabled(2) + `id="vegan" name="dietPicker">
                <span class="checkmark"></span>
            </label>

            <label class="box">Vegetarian
                <input type="radio" `+ getDietEnabled(1) + `id="vegetarian" name="dietPicker">
                <span class="checkmark"></span>
            </label>

            <label class="box">Meat
                <input type="radio" `+ getDietEnabled(0) + `id="meat" name="dietPicker">
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
    createConfirmListener(isNew, conn, name)
    nameListener()
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

function createConfirmListener(isNew, conn, oldName) {
    document.getElementById("confirm").addEventListener("click", function () {
        console.log("test")
        if (!valid) return;
        var name = document.getElementById("name").value
        var vegan;
        if (document.getElementById("meat").checked === true) vegan = 0
        else if (document.getElementById("vegetarian").checked === true) vegan = 1
        else vegan = 2

        if (isNew) conn.invoke("AddIngredient", name, vegan).catch(function (err) { });
        else conn.invoke("EditIngredient", oldName, name, vegan).catch(function (err) { });
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

function getDietEnabled(num) {
    if (itemDiet === num || (isNewItem && num === 2)) return `checked="checked"`;
    return "";
}

function getNameBox(name) {
    if (name === null) return `<input class="namebox" id="name">
        <p class="namewarning" id="namewarning">Name can't be empty!</p>`;
    else return `<input class="namebox" id="name" value="` + name + `">
        <p class="namewarning" id="namewarning"></p>`;
}

export { popup };