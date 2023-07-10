let toastId = "liveToast";

function createToast(text, color) {
    let toast = `
        <div class="position-fixed bottom-0 end-0 p-3" style="z-index: 11">
            <div id="${toastId}" lass="toast" role="alert" aria-live="assertive" aria-atomic="true" style="border: 1px solid ${color};">
                <div class="toast-header">
                    <strong class="me-auto">Zust</strong>
                    <small>Now</small>
                    <button type="button" class="btn-close" data-bs-dismiss="toast" aria-label="Close"></button>
                </div>
                <div class="toast-body" style="color: ${color};">
                    ${text}
                </div>
            </div>
        </div>
    `;
    return toast;
}

// Function to show the toast
function showToast(message, color) {
    var toastContainer = document.body;
    var toastHTML = createToast(message, color);

    // Append the toast HTML to the container
    toastContainer.innerHTML += toastHTML;
    // Find the newly added toast element
    var toastElement = document.getElementById(toastId);

    // Initialize the toast using Bootstrap's toast() method
    var toast = new bootstrap.Toast(toastElement);

    // Show the toast
    toast.show();
}
