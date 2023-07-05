// Function to handle form submission
function register(event) {
    // Prevent the default form submission
    event.preventDefault();

    // Get the form data
    var password = $('#Password').val();
    var confirmPassword = $('#ConfirmPassword').val()
  
    if (CheckPasswordsValidity(password, confirmPassword)) {
        // Get the form data
        var formData = {
            Username: $('#Username').val(),
            Email: $('#Email').val(),
            Password: password,
            ConfirmPassword: confirmPassword
        };

        console.log(formData);

        // Send the AJAX request to register the user
        $.ajax({
            url: 'https://localhost:7009/api/authentication/register',
            type: 'POST',
            data: JSON.stringify(formData),
            success: function (response) {
                // Handle the success response
                console.log(response);
            },
            error: function (xhr, status, error) {
                // Handle the error response
                console.log(xhr.responseText);
            }
        });
    }
    else {
        //Warn User in View
    }
}

// Function to check "password" and "confirm password" similarity
// Returns true is when they are same. Else false
function CheckPasswordsValidity(password, confirmPassword) {
    return true;
}

// Bind the form submission event to the register function
//$('#registerForm').submit(function (event) {
//    register(event);
//});