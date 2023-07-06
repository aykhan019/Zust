let toastId = "liveToast";

function createToast(text) {
    let toast = `
    <div class="position-fixed bottom-0 end-0 p-3" style="z-index: 11">
        <div id="${toastId}" class="toast" role="alert" aria-live="assertive" aria-atomic="true">
            <div class="toast-header">
            <strong class="me-auto">Zust</strong>
            <small>Now</small>
            <button type="button" class="btn-close" data-bs-dismiss="toast" aria-label="Close"></button>
            </div>
            <div class="toast-body text-danger">
                ${text}
            </div>
        </div>
    </div>
    `;
    return toast;
}

function showToast(location, toastHTML) {
    document.getElementById(location).innerHTML = toastHTML;
    let toastElement = document.getElementById(toastId);
    let toast = new bootstrap.Toast(toastElement);
    toast.show();
}