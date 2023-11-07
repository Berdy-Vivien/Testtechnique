"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/pingHub").build();
document.getElementById("startPing").addEventListener("click", function () {
    connection.start().then(function () {
        connection.invoke("SendPing");
    });
});

connection.on("ReceivePing", function (message) {
    console.log(message);
});