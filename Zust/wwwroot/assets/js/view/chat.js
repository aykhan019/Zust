function createMyMessageHtml(message) {
    var sentDate = new Date(message.dateSent); // Convert the date to a JavaScript Date object
    var hours = sentDate.getHours().toString().padStart(2, '0'); // Get hours in two-digit format
    var minutes = sentDate.getMinutes().toString().padStart(2, '0'); // Get minutes in two-digit format

    var content = `
    <div class="chat chat-left">
        <div class="chat-avatar">
            <a href="/home/my-profile" class="d-inline-block">
                <img src="${message.senderUser.imageUrl}" width="50" height="50" class="rounded-circle" alt="image">
            </a>
        </div>

        <div class="chat-body">
            <div class="chat-message">
                <p>${message.text}</p>
                <span class="time d-block">${hours}:${minutes}</span>
            </div>
        </div>
    </div>
    `;
    return content;
}

function createUserMessageHtml(message) {
    var sentDate = new Date(message.dateSent); // Convert the date to a JavaScript Date object
    var hours = sentDate.getHours().toString().padStart(2, '0'); // Get hours in two-digit format
    var minutes = sentDate.getMinutes().toString().padStart(2, '0'); // Get minutes in two-digit format

    var content = `
    <div class="chat">
        <div class="chat-avatar">
            <a href="/home/users?id=${message.receiverUserId}" class="d-inline-block">
                <img src="${message.receiverUser.imageUrl}" width="50" height="50" class="rounded-circle" alt="image">
            </a>
        </div>
        <div class="chat-body">
            <div class="chat-message">
                <p>${message.text}</p>
                <span class="time d-block" style="right: 10px !important;">${hours}:${minutes}</span>
            </div>
        </div>
    </div>
    `;
    aler(content);
    return content;
}


var messagesContainer = document.getElementById("messages");

function sendMessage(e) {
    e.preventDefault();

    var inputElement = document.getElementById("messageText");

    var messageText = inputElement.value.trim();
    if (messageText === '') {
        return;
    }

    var message = {
        senderUserId: '@Model.CurrentUser.Id.ToString()',
        receiverUserId: '@Model.UserToChat.Id.ToString()',
        text: messageText,
        chatId: '@Model.Chat.Id.ToString()'
    };

    $.ajax({
        url: "/api/Chat/AddMessage",
        type: "POST",
        data: JSON.stringify(message),
        contentType: "application/json",
        success: async function (response) {
            var messageResponse = response;
            messagesContainer.innerHTML += createMyMessageHtml(messageResponse);
            await connection.invoke("SendMessageToUser", message);
            inputElement.value = '';
        },
        error: function (error) {
            inputElement.value = '';
            // Show Error
        }
    });
}

connection.on('ReceiveMessage', function (messageModel) {
    var html = createUserMessageHtml(messageModel);
    console.log(html);
    messagesContainer.innerHTML += html;
});
