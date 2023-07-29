"use strict"

var connection = new signalR.HubConnectionBuilder().withUrl("/userhub").build();

connection.start().then(function () {
    console.log("Connected to chathub");
}).catch(function (err) {
    return console.error(err.toString());
})
