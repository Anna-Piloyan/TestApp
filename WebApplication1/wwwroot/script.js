
// Get all contacts
async function GetUsers() {
    const response = await fetch("/api/contacts", {
        method: "GET",
        headers: { "Accept": "application/json" }
    });

    if (response.ok === true) {
        const contacts = await response.json();
        let rows = document.querySelector("tbody");
        contacts.forEach(contacts => {
            rows.append(row(contacts));
        });
    }
}
// Get one contact
async function GetUser(id) {
    const response = await fetch("/api/contacts/" + id, {
        method: "GET",
        headers: { "Accept": "application/json" }
    });
    if (response.ok === true) {
        const contact = await response.json();
        const form = document.forms["userForm"];
        form.elements["id"].value = contact.id;
        form.elements["firstName"].value = contact.firstName;
        form.elements["lastName"].value = contact.lastName;
        form.elements["email"].value = contact.email;
    }
}
// Add contact
async function CreateUser(FirstName, LastName, Email) {

    const response = await fetch("api/contacts", {
        method: "POST",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({
            firstName: FirstName,
            lastName: LastName,
            email: Email
        })
    });
    if (response.ok === true) {
        const contact = await response.json();
        reset();
        document.querySelector("tbody").append(row(contact));
    }
}
// Change contact
async function EditUser(Id, FirstName, LastName, Email) {
    const response = await fetch("api/contacts", {
        method: "PUT",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({
            id: parseInt(Id, 10),
            firstName: FirstName,
            lastName: LastName,
            email: Email
        })
    });
    if (response.ok === true) {
        const contact = await response.json();
        reset();
        document.querySelector("tr[data-rowid='" + contact.id + "']").replaceWith(row(contact));
    }
}
// Delete contact
async function DeleteUser(id) {
    const response = await fetch("/api/contacts/" + id, {
        method: "DELETE",
        headers: { "Accept": "application/json" }
    });
    if (response.ok === true) {
        const contact = await response.json();
        document.querySelector("tr[data-rowid='" + contact.id + "']").remove();
    }
}

// Reset form
function reset() {
    const form = document.forms["userForm"];
    form.reset();
    form.elements["id"].value = 0;
}
// Create row for table
function row(contact) {

    const tr = document.createElement("tr");
    tr.setAttribute("data-rowid", contact.id);

    const idTd = document.createElement("td");
    idTd.append(contact.id);
    tr.append(idTd);

    const firstNameTd = document.createElement("td");
    firstNameTd.append(contact.firstName);
    tr.append(firstNameTd);

    const lastNameTd = document.createElement("td");
    lastNameTd.append(contact.lastName);
    tr.append(lastNameTd);

    const emailTd = document.createElement("td");
    emailTd.append(contact.email);
    tr.append(emailTd);

    const linksTd = document.createElement("td");

    const editLink = document.createElement("a");
    editLink.setAttribute("data-id", contact.id);
    editLink.setAttribute("style", "cursor:pointer;padding:15px;");
    editLink.append("Update");
    editLink.addEventListener("click", e => {

        e.preventDefault();
        GetUser(contact.id);
    });
    linksTd.append(editLink);

    const removeLink = document.createElement("a");
    removeLink.setAttribute("data-id", contact.id);
    removeLink.setAttribute("style", "cursor:pointer;padding:15px;");
    removeLink.append("Delete");
    removeLink.addEventListener("click", e => {

        e.preventDefault();
        DeleteUser(contact.id);
    });

    linksTd.append(removeLink);
    tr.appendChild(linksTd);

    return tr;
}
// Reset form
document.getElementById("reset").click(function (e) {

    e.preventDefault();
    reset();
})

// Send form
document.forms["userForm"].addEventListener("submit", e => {
    e.preventDefault();
    const form = document.forms["userForm"];
    const id = form.elements["id"].value;
    const firstName = form.elements["firstName"].value;
    const lastName = form.elements["lastName"].value;
    const email = form.elements["email"].value;
    if (id == 0)
        CreateUser(firstName, lastName, email);
    else
        EditUser(id, firstName, lastName, email);
});

// Load Contacts
GetUsers();
