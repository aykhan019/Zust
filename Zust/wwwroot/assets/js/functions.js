let toastId = "liveToast";

function createToast(text, color) {
    let toast = `
        <div class="position-fixed bottom-0 end-0 p-3" style="z-index: 11">
            <div id="${toastId}" lass="toast" role="alert" aria-live="assertive" aria-atomic="true" style="border: 1px solid ${color}; background-color: white;">
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
    var toastHTML = createToast(message, color);
    $('body').append(toastHTML);
    let toast = $(toastId);
    const bsToast = new bootstrap.Toast(toast);
    bsToast.show();
    setTimeout(() => {
        bsToast.hide();
    }, 6000);

    // Handle close button click event
    const closeButton = toast.querySelector(".btn-close");
    closeButton.addEventListener("click", () => {
        bsToast.hide();
    });
    let toast2 = new bootstrap.Toast(toast);
    toast2.show();
}
