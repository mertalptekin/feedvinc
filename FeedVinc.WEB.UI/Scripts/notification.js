$(function () {
    var hub = $.connection.notificationHub;
    var userID = sessionStorage.getItem("UserID");

    $.connection.hub.qs = userID;


    

    $.connection.hub.start().done(function () {


    })

})