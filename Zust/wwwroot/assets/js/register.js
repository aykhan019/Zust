document.getElementById("registrationForm").addEventListener("submit", async function (event) {
    event.preventDefault();

    var username = document.getElementById("usernameInput").value;
    var password = document.getElementById("passwordInput").value;

    if (await usernameExists(username)) {
        let toastHTML = createToast("Username is already taken! Please try a different one.");
        showToast("errors", toastHTML);
        return;
    }

    event.target.submit();    
});

function usernameExists(username) {
    return new Promise(function (resolve, reject) {
        $.ajax({
            url: '/authentication/userexists/' + username,
            type: 'GET',
            success: function (data) {
                resolve(data);
            },
            error: function () {
                reject();
            }
        });
    });
}



//// Function to validate form data
// function validateForm(){
//    // Get form fields

//    // Check if user with this username exists
//    if (usernameExists(username)) {
//        return false;
//    }

//    // If all validation checks pass, return true
//    return true;
//} 
