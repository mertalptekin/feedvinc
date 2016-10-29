


toastr.options = {
    "closeButton": true,
    "debug": false,
    "newestOnTop": false,
    "progressBar": false,
    "positionClass": "toast-bottom-right",
    "preventDuplicates": true,
    "onclick": null,
    "showDuration": "300",
    "hideDuration": "1000",
    "timeOut": "5000",
    "extendedTimeOut": "1000",
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut"
}

var id = sessionStorage.getItem("UserID");

var hub = $.connection.notificationHub;
$.connection.hub.qs = "UserID=" + id;
var userID = "UserID=" + id;

$.connection.hub.qs = userID;

hub.client.notifyFollow = function (data) {
    alert(JSON.stringify(data));

    var counter = parseInt($('#follow-notifications').text());
    counter = counter + 1;

    $("#userFollowModal").prepend(

        '<li>' +
            '<div class="dd-notifications">' +
                '<a href="' + data.Link + '"><img src="' + data.ProfilePhoto + '"></a>' +
            '<div>' +
                '<a href="' + data.Link + '">' + data.NotificationName + '</a>' +
                '<p>' + data.NotificationText + '</p>' +
            '</div>' +
            '</div>' +
        '</li>'

        )

    $("#userFollowDropDown").prepend(

                       '<li>' +
                            '<div class="dd-friend">' +
                                '<a href="' + data.Link + '"><img src="' + data.ProfilePhoto + '"></a>' +
                                '<a href="' + data.Link + '">' + data.NotificationName + '<span style="color:#db9e36 !important;">' + data.NotificationText + '</span></a>' +
                                '<button class="btn btn-default btn-xs">deneme</button>' +
                            '</div>' +
                        '</li>'

        )

    toastr["info"](data.NotificationName + " " + data.NotificationText);

}

hub.client.notifyShare = function (data) {
    alert(JSON.stringify(data));

    var counter = parseInt($('#share-notifications').text());
    counter = counter + 1;

    $('#share-notifications').text(counter);

    $(".user-dropdown-list").prepend(
        '<li>' +
            '<div class="dd-notifications">' +
                '<img src="' + data.ProfilePhotoPath + '">' +
            '<div>' +
                '<a href="' + data.ShareProfileLink + '">' + data.ShareProfileName + '</a>' +
                '<p>' + data.NotificationText + '</p>' +
            '</div>' +
                '<span class="time">' + data.SharePrettyDate + '</span>' +
            '</div>' +
        '</li>'
        );

    $(".notification-list").prepend(
        '<li>' +
            '<div class="notifications">' +
                '<img src="' + data.ProfilePhotoPath + '">' +
                '<div>' +
                    '<a href="' + data.ShareProfileLink + '">' + data.ShareProfileName + '</a>' +
                    '<p>' + data.NotificationText + '</p>' +
                '</div>' +
                '<span class="time">' + data.SharePrettyDate + '</span>' +
        '</li>'
        )


    toastr["info"](data.ShareProfileName + " " + data.NotificationText);

}

hub.client.notifyLike = function (data) {
    alert(JSON.stringify(data));

    var likeEn = "Like";
    var likedEn = "Liked";
    var likeTr = "Beğen";
    var likedTr = "Beğendin";
    var lang = sessionStorage.getItem("lang");
    var likeOwnerEn = "liked your share"
    var likeOwnerTr = "paylaşımınızı beğendiniz"

    if (data.Status == "like") {

        alert("like");

        var counter1 = parseInt($('#share-notifications').text());
        counter1 = counter1 + 1;
        $("#share-notifications").text(counter1);

        var counter = parseInt($('#feedlike_' + data.ShareID).text());
        counter = counter + 1;

        alert(counter);

        $("#feedlike_" + data.ShareID).text(counter);
        $("#feed-like-button_" + data.ShareID).addClass("active");

        if (lang="EN-us") 
            $("#feedlikeText_" + data.ShareID).text(likedEn)
        else
            $("#feedlikeText_" + data.ShareID).text(likedTr)

        $(".user-dropdown-list").prepend(
      '<li>' +
          '<div class="dd-notifications">' +
              '<img src="' + data.ProfilePhotoPath + '">' +
          '<div>' +
              '<a href="' + data.ShareProfileLink + '">' + data.ShareProfileName + '</a>' +
              '<p>' + data.NotificationText + '</p>' +
          '</div>' +
              '<span class="time">' + data.SharePrettyDate + '</span>' +
          '</div>' +
      '</li>'
      );

        $(".notification-list").prepend(
            '<li>' +
                '<div class="notifications">' +
                    '<img src="' + data.ProfilePhotoPath + '">' +
                    '<div>' +
                        '<a href="' + data.ShareProfileLink + '">' + data.ShareProfileName + '</a>' +
                        '<p>' + data.NotificationText + '</p>' +
                    '</div>' +
                    '<span class="time">' + data.SharePrettyDate + '</span>' +
            '</li>'
            )

        if (id == data.OwnerID) {

            if (lang=="EN-us") 
                toastr["info"](likeOwnerEn);
            else
                toastr["info"](likeOwnerTr);
        }
        else {
            toastr["info"](data.ShareProfileName + " " + data.NotificationText);
        }
        
    }
    else if(data.Status=="unlike") {

        alert("unlike");

        var counter = $('#feedlike_' + data.ShareID).text();
        counter = counter - 1;
        $("#feedlike_" + data.ShareID).text(counter);

        $("#feedlike_" + data.ShareID).text(counter);
        $("#feed-like-button_" + data.ShareID).removeClass("active");

        if (lang = "EN-us")
            $("#feedlikeText_" + data.ShareID).text(likeEn)
        else
            $("#feedlikeText_" + data.ShareID).text(likeTr)

    }

}

$.connection.hub.start();


$('#frmShare').ajaxForm({
    beforeSubmit: function () {
        var target = document.getElementById('loading');
        $(target).show();
        var spinner = new Spinner();
        console.log(spinner.opts);
        spinner.opts.top = "10%";
        spinner.opts.scale = 2;
        spinner.spin();
        target.appendChild(spinner.el);
    },
    success: function (response) {
        console.log(response);
        var target = document.getElementById('loading');
        $(target).hide();

        $("#feeds").prepend(response);

        var model = new Object();
        model.ShareID = $(response).find("div").attr("data-feed-id");
        model.UserID = id;
        model.ShareTypeID = 1;
        model.Post = $(".share-textarea").val();

        alert(JSON.stringify(model));

        hub.server.sendShare(userID, model);

        if (response.IsValid == false) {
            toastr.error(response.ErrorMessage);
            console.log(response);
        }
    }
});

function Follow(followerID, followedID, followType) {
    hub.server.sendFollow(followerID, followedID, followType);
}


function Like(likeownerid,likeduserid, shareid, sharetype) {

        var model = new Object();
        model.LikeOwnerID = likeownerid;
        model.LikedUserID = likeduserid;
        model.ShareType = sharetype;
        model.PostShareID = shareid;
        

        alert(JSON.stringify(model));



        hub.server.sendLike(likeduserid, model);
}
