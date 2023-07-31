"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/userhub").build();

function startConnection() {
    connection.start()
        .then(function () {
            console.log("Connected to chathub");
        })
        .catch(function (err) {
            console.error(err.toString());
            console.log("Connection lost. Reconnecting...");
            setTimeout(startConnection, 5000); // Try reconnecting after 5 seconds
        });
}

// Add a close event listener to the connection to handle reconnection on error
connection.onclose(function (error) {
    console.log("Connection closed. Reason: " + error.message);
    console.log("Reconnecting...");
    setTimeout(startConnection, 5000); // Try reconnecting after 5 seconds
});

// Start the connection
startConnection();