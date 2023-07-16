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

function getDateTimeDifference(dateTime) {
    const requestTime = new Date(dateTime);
    const currentTime = new Date();

    // Calculate the time difference in milliseconds
    const difference = currentTime.getTime() - requestTime.getTime();

    // Convert milliseconds to seconds, minutes, hours, and days
    const seconds = Math.floor(difference / 1000);
    const minutes = Math.floor(seconds / 60);
    const hours = Math.floor(minutes / 60);
    const days = Math.floor(hours / 24);

    // Format the result as a string
    if (days > 0) {
        return days + (days === 1 ? " day ago" : " days ago");
    } else if (hours > 0) {
        return hours + (hours === 1 ? " hour ago" : " hours ago");
    } else if (minutes > 0) {
        return minutes + (minutes === 1 ? " minute ago" : " minutes ago");
    } else {
        return seconds + (seconds === 1 ? " second ago" : " seconds ago");
    }
}

function getNoResultHtml(title, message) {
    let content = `
    <div class="empty-icon-container">
      <div class="animation-container">
        <div class="bounce"></div>
        <div class="pebble1"></div>
        <div class="pebble2"></div>
        <div class="pebble3"></div>
      </div>
      <div>
        <h2>${title}</h2>
        <p>${message}</p>
      </div>
    </div>`;
    return content;
}

function getAllFollowings(currentUserId) {
    return new Promise(function (resolve, reject) {
        $.ajax({
            url: `/api/User/GetFollowings?userId=` + currentUserId,
            method: 'GET',
            success: function (data) {
                resolve(data);
            },
            error: function (error) {
                reject(error);
            }
        });
    });
}


function getAllFollowers(currentUserId) {
    return new Promise(function (resolve, reject) {
        $.ajax({
            url: `/api/User/GetFollowers?userId=` + currentUserId,
            method: 'GET',
            success: function (data) {
                resolve(data);
            },
            error: function (error) {
                reject(error);
            }
        });
    });
}


async function getSentFriendRequests(id) {
    return new Promise(function (resolve, reject) {
        $.ajax({
            url: `/api/FriendRequest/GetSentFriendRequests?userId=` + id,
            method: 'GET',
            success: function (data) {
                resolve(data);
            },
            error: function (error) {
                alert("Error occurred: " + error.responseText);
                reject(false);
            }
        });
    });
}

function sendFriendRequest(receiverId) {
    return new Promise(function (resolve, reject) {
        $.ajax({
            url: '/api/FriendRequest/AddFriendRequest?receiverId=' + receiverId,
            method: 'POST',
            success: async function () {
                resolve(true);
            },
            error: function (error) {
                alert("Error occurred: " + error.responseText);
                reject(false);
            }
        });
    });
}

function cancelFriendRequest(receiverId) {
    return new Promise(function (resolve, reject) {
        $.ajax({
            url: '/api/FriendRequest/CancelFriendRequest?receiverId=' + receiverId,
            method: 'POST',
            success: function () {
                resolve(true);
            },
            error: function (error) {
                alert("Error occurred: " + error.responseText);
                reject(false);
            }
        });
    });
}

function removeFriend(friendId) {
    return new Promise(function (resolve, reject) {
        $.ajax({
            url: '/api/User/RemoveFriend?friendId=' + friendId,
            method: 'POST',
            success: function () {
                resolve(true);
            },
            error: function (error) {
                alert("Error occurred: " + error.responseText);
                reject(false);
            }
        });
    });
}


function getButtonHtml(sentFriendRequests, user) {
    if (sentFriendRequests.some(i => i.receiverId === user.id && i.status === 'Pending')) {
        return `<button onclick="callCancelFriendRequest('${user.id}')" class="cancel-btn">Cancel Friend Request</button>`;
    } else if (sentFriendRequests.some(i => i.receiverId === user.id && i.status === 'Accepted')) {
        return `<button title="Click to stop following" onClick="callRemoveFriend('${user.id}')" class="remove-friend-btn">Following</button>`;
    } else {
        return `<button onclick="callSendFriendRequest('${user.id}')" type="submit" class="send-request-btn">Send Friend Request</button>`;
    }
}

function getIconClass(sentFriendRequests, user) {
    if (sentFriendRequests.some(i => i.receiverId === user.id && i.status === 'Pending')) {
        return 'yellow-icon';
    } else if (sentFriendRequests.some(i => i.receiverId === user.id && i.status === 'Accepted')) {
        return 'red-icon';
    } else {
        return 'main-icon';
    }
}
