// HTML template for the save button
var btnHtml = `
                    <button id="saveBtn" class="btn btn-primary" disabled onclick="save()">
                        Save Changes
                    </button>`;

// HTML template for the save button
var cancelHtml = `
                        <button class="btn btn-secondary" onclick="cancel()">
                            Cancel Changes
                        </button>`;


// HTML template for the edit link
var editHtml = `
                    <a id="editText" class="dropdown-item d-flex align-items-center" onclick="openEdit()" style="cursor:pointer;">
                        <i class="flaticon-edit"></i> Edit Information
                    </a>`;

// Get the dropdown element and set its content to the edit link
var dropdown = document.getElementById("dropdown");
dropdown.innerHTML = editHtml;

// Get all the elements with the "personal-information" class
const editableElements = document.getElementsByClassName("personal-information");

// Declare the save button variable
var saveButton;

// Function to open the edit mode
function openEdit() {
    console.log("OPENED");
    // Enable editing for all editable elements
    for (let i = 0; i < editableElements.length; i++) {
        const element = editableElements[i];
        element.contentEditable = true;
        element.classList.add("personal-information-editable");
    }

    // Update the dropdown content to show the save button
    dropdown.innerHTML = cancelHtml;
    dropdown.innerHTML += btnHtml;

    // Get the save button and attach an event listener to each editable element
    saveButton = document.getElementById("saveBtn");
    for (let i = 0; i < editableElements.length; i++) {
        const element = editableElements[i];
        element.addEventListener("input", enableSaveButton);
    }
}

// Function to cancel changes and close the edit mode
function cancel() {
    location.reload();
}

// Function to save changes and close the edit mode
function save() {
    console.log("SAVED");

    closeEdit();

    var myUsername = '@user.UserName';

    const personalInfo = {
        username: myUsername,
        email: $('a.personal-information').first().text().trim(),
        birthday: $('span.personal-information').eq(0).text().trim(),
        occupation: $('span.personal-information').eq(1).text().trim(),
        birthplace: $('span.personal-information').eq(2).text().trim(),
        phoneNumber: $('a.personal-information').eq(1).text().trim(),
        gender: $('span.personal-information').eq(3).text().trim(),
        relationshipStatus: $('span.personal-information').eq(4).text().trim(),
        bloodGroup: $('span.personal-information').eq(5).text().trim(),
        website: $('a.personal-information').eq(2).text().trim(),
        socialLink: $('a.personal-information').eq(3).text().trim(),
        languages: $('span.personal-information').eq(6).text().trim(),
        aboutMe: $('p.personal-information').eq(0).text().trim(),
        educationWork: $('p.personal-information').eq(1).text().trim(),
        interests: $('p.personal-information').eq(2).text().trim()
    };

    console.log(personalInfo);

    // Make an AJAX request to update the user profile
    $.ajax({
        url: '/api/Profile/UpdateProfile',
        type: 'PUT',
        contentType: 'application/json',
        data: JSON.stringify(personalInfo),
        success: function (response) {
            // Update the HTML elements with the updated information
            //$('a[href="/home/my-profile"]').text(personalInfo.username);
            //$('a[href="mailto:@user.Email"]').attr('href', 'mailto:' + personalInfo.email).text(personalInfo.email);

            showToast("Your profile has been successfully updated!", "green");
        },
        error: function (error) {
            showToast("There was an error in updating your profile!", "red");
        }
    });
}

// Function to close the edit mode
function closeEdit() {
    console.log("CLOSED");
    // Disable editing for all editable elements
    for (let i = 0; i < editableElements.length; i++) {
        const element = editableElements[i];
        element.contentEditable = false;
        element.classList.remove("personal-information-editable");
    }

    // Update the dropdown content to show the edit link
    dropdown.innerHTML = editHtml;

    // Remove the event listener when closing the edit mode
    dropdown.removeEventListener("input", enableSaveButton);
}

// Function to enable the save button when any editable element is modified
function enableSaveButton() {
    saveButton.disabled = false;
}