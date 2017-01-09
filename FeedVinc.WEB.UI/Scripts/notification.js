


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

    var followText = null;
    var unfollowText = null;
    var joinText = null;
    var unjoinText = null;
    var lang = sessionStorage.getItem("Lang");
    var follower = sessionStorage.getItem("UserID");

    if (lang == "en-US") {
        followText = "Following";
        unfollowText = "Follow";
        joinText = "Joined";
        unjoinText = "Join";
    }
    else {
        followText = "Takiptesin",
        unfollowText = "Takip Et"
        joinText = "Katıldın";
        unjoinText = "Katıl";
    }

    if (follower != data.FollowerID) {

        var counter = parseInt($('.follow-notifications').text());
        counter = counter + 1;
        $('.follow-notifications').text(counter);

        $("#userFollowModal").prepend(

            '<li>' +
                '<div class="dd-notifications">' +
                    '<a href="javascript:GotoNotify(\'' + data.Link + '\',' + data.PostID + ');"><img src="' + data.ProfilePhoto + '"></a>' +
                '<div>' +
                    '<a href="javascript:GotoNotify(\'' + data.Link + '\',' + data.PostID + ');">' + data.NotificationName + '</a>' +
                    '<p>' + data.NotificationText + '</p>' +
                '</div>' +
                '</div>' +
            '</li>'

            )

        $("#userFollowDropDown").prepend(

                           '<li>' +
                                '<div class="dd-friend">' +
                                    '<a href="javascript:GotoNotify(\'' + data.Link + '\',' + data.PostID + ');"><img src="' + data.ProfilePhoto + '"></a>' +
                                    '<a href="javascript:GotoNotify(\'' + data.Link + '\',' + data.PostID + ');">' + data.NotificationName + '<span style="color:#db9e36 !important;">' + data.NotificationText + '</span></a>' +
                                '</div>' +
                            '</li>'

            )

        toastr["info"](data.NotificationName + " " + data.NotificationText);
    }
    else {


        var followCount = parseInt($('#followerCount').text());

        if (data.FollowType == "user" && data.FollowStatus == "follow") {

            $("#user-follow_" + data.FollowedID).text(followText);
            followCount = followCount + 1;
            $('#followerCount').text(followCount);

        }
        else if (data.FollowType == "user" && data.FollowStatus == "unfollow") {
            $("#user-follow_" + data.FollowedID).text(unfollowText);
            followCount = followCount - 1;
            $('#followerCount').text(followCount);
        }
        if (data.FollowType == "project" && data.FollowStatus == "follow") {
            $(".project-follow_" + data.FollowedID).text(followText);
            followCount = followCount + 1;
            $('#followerCount').text(followCount);
        }
        else if (data.FollowType == "project" && data.FollowStatus == "unfollow") {
            $(".project-follow_" + data.FollowedID).text(unfollowText);
            followCount = followCount - 1;
            $('#followerCount').text(followCount);
        }
        if (data.FollowType == "community" && data.FollowStatus == "follow") {
            $("#community-follow_" + data.FollowedID).text(joinText);
        }
        else if (data.FollowType == "community" && data.FollowStatus == "unfollow") {
            $("#community-follow_" + data.FollowedID).text(unjoinText);
        }

    }

}

hub.client.notifySecondShare = function (data) {

    var lang = sessionStorage.getItem("Lang");
    var shareText = null;

    if (lang == "tr-TR")
        shareText = "Paylaştın";
    else
        shareText = "Shared";


    if (data.Status == "Owner") {

        var cc = parseInt($("#feed-share_" + data.ShareID).text());
        cc = cc + 1;
        $('#feed-share-button_' + data.ShareID).addClass("active");
        $("#feed-share-text_" + data.ShareID).text(shareText);
        $("#feed-share_" + data.ShareID).text(cc);
        return;
    }

    var counter = parseInt($('#share-notifications').text());
    counter = counter + 1;
    $("#feed-share_" + data.ShareID).text(counterFeedShare);
    $('#share-notifications').text(counter);

    var counterFeedShare = parseInt($("#feed-share_" + data.ShareID).text());
    counterFeedShare = counterFeedShare + 1;
    $('#feed-share-button_' + data.ShareID).addClass("active");
    $("#feed-share-text_" + data.ShareID).text(shareText);


    $("#userNotificationDropDown").prepend(
        '<li>' +
            '<div class="dd-notifications">' +
                '<img src="' + data.ProfilePhotoPath + '">' +
            '<div>' +
                '<a href="' + data.ShareProfileLink + '">' + data.ShareProfileName + '</a>' +
                 '<span  style="color:#db9e36;">' + data.NotificationText + '</span>' +
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

hub.client.notifyShare = function (data) {

    var counter = parseInt($('#share-notifications').text());
    counter = counter + 1;

    $('#share-notifications').text(counter);

    $(".user-dropdown-list").prepend(
        '<li>' +
            '<div class="dd-notifications">' +
                '<img src="' + data.ProfilePhotoPath + '">' +
            '<div>' +
                '<a href="' + data.ShareProfileLink + '">' + data.ShareProfileName + '</a>' +
                 '<span  style="color:#db9e36;">' + data.NotificationText + '</span>' +
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

    var likeEn = "Like";
    var likedEn = "Liked";
    var likeTr = "Beğen";
    var likedTr = "Beğendin";
    var lang = sessionStorage.getItem("Lang");

    var likeOwnerEn = "liked your share"
    var likeOwnerTr = "paylaşımınızı beğendiniz"

    if (data.Status == "Owner") {

        var counter = parseInt($('#feedlike_' + data.ShareID).text());
        counter = counter + 1;

        $("#feedlike_" + data.ShareID).text(counter);
        $("#feed-like-button_" + data.ShareID).addClass("active");

        if (lang == "en-US")
            $("#feedlikeText_" + data.ShareID).text(likedEn)
        else
            $("#feedlikeText_" + data.ShareID).text(likedTr)

        return;
    }

    if (data.Status == "like") {

        var counter1 = parseInt($('#share-notifications').text());
        counter1 = counter1 + 1;
        $("#share-notifications").text(counter1);

        var counter = parseInt($('#feedlike_' + data.ShareID).text());
        counter = counter + 1;

        $("#feedlike_" + data.ShareID).text(counter);
        $("#feed-like-button_" + data.ShareID).addClass("active");

        if (lang == "en-US")
            $("#feedlikeText_" + data.ShareID).text(likedEn)
        else
            $("#feedlikeText_" + data.ShareID).text(likedTr)

        $(".user-dropdown-list").prepend(
      '<li>' +
          '<div class="dd-notifications">' +
              '<img src="' + data.ProfilePhotoPath + '">' +
          '<div>' +
              '<a href="' + data.ShareProfileLink + '">' + data.ShareProfileName + '</a>' +
              '<span  style="color:#db9e36;">' + data.NotificationText + '</span>' +
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
    else if (data.Status == "unlike") {


        var counter = $('#feedlike_' + data.ShareID).text();
        counter = counter - 1;
        $("#feedlike_" + data.ShareID).text(counter);

        $("#feedlike_" + data.ShareID).text(counter);
        $("#feed-like-button_" + data.ShareID).removeClass("active");

        if (lang = "en-US")
            $("#feedlikeText_" + data.ShareID).text(likeEn)
        else
            $("#feedlikeText_" + data.ShareID).text(likeTr)

    }

}

hub.client.notifyMessage = function (data) {

    var messageDiv = '<div class="msg-content recieved">' +
                            '<p>' + data.UserMessage + '</p>' +
                            '<span>' + data.NotificationPrettyDate + '</span>' +
                        '</div>';

    $("#message-content_" + data.SenderID).find('.mCSB_container').prepend(messageDiv);
    $("#message-newuser-content_" + data.SenderID).find('.mCSB_container').prepend(messageDiv);

    $(".message-row").mCustomScrollbar("scrollTo", "bottom");
    $(".msg-to-input").val("");

    toastr["info"](data.NotificationMessage);
}

hub.client.notifyComment = function (data) {

    if (data.Status=="Owner") {

        $("#new-comment-post_" + data.ShareID).val("");
        var counter = parseInt($("#feedcomment_" + data.ShareID).text());
        counter = counter + 1;
        $("#feedcomment_" + data.ShareID).text(counter);

        $('#comments-modal-contentID').append(

    '<div class="comments-modal-box">' +
       '<div class="comments-header">' +
            '<img src="' + data.ProfilePhotoPath + '" class="img-responsive">' +
                '<h6>' + data.ShareProfileName + '</h6>' +
                   '<span>' + data.SharePrettyDate + '</span>' +
                     '</div>' +
                     '<div class="comment">' +
                             '<p>' + data.NotificationPostResult + '</p>' +
                      '</div>' +
      '</div>');

        return;
    }

    $("#new-comment-post_" + data.ShareID).val("");
    var counter = parseInt($("#feedcomment_" + data.ShareID).text());
    counter = counter + 1;
    $("#feedcomment_" + data.ShareID).text(counter);

    var counter = parseInt($('#share-notifications').text());
    counter = counter + 1;

    $('#share-notifications').text(counter);

    $('#post-container_' + data.ShareID).append(

 '<div class="comments-modal-box">' +
    '<div class="comments-header">' +
         '<img src="' + data.ProfilePhotoPath + '" class="img-responsive">' +
             '<h6>' + data.ShareProfileName + '</h6>' +
                '<span>' + data.SharePrettyDate + '</span>' +
                  '</div>' +
                  '<div class="comment">' +
                          '<p>' + data.NotificationPostResult + '</p>' +
                   '</div>' +
   '</div>'
  )

    $(".user-dropdown-list").prepend(
        '<li>' +
            '<div class="dd-notifications">' +
                '<img src="' + data.ProfilePhotoPath + '">' +
            '<div>' +
                '<a href="' + data.ShareProfileLink + '">' + data.ShareProfileName + '</a>' +
                '<span  style="color:#db9e36;">' + data.NotificationText + '</span>' +
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
        model.ShareID = $("#data-feed-id").val();
        model.UserID = parseInt($("#PostUserValue").val());
        model.ShareTypeID = parseInt($("#ShareTypeValue").val());
        model.Post = $("#share-textarea").val();
        model.ProjectID = $("#PostProjectID").val();
        model.CommunityID = $("#PostCommunityID").val();

        $("#Location").val("");
        $(".share-image").attr("src", "");
        $("#share-photo").val("");
        $("#share-video").val("");
        $("#share-textarea").val("");
        $(".highlighter").empty();
        $(".image").removeClass("opened");
        $(".map").removeClass("opened");
        $(".share-attach").removeClass("active");

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

function Like(likeownerid, likeduserid, shareid, sharetype) {

    var model = new Object();
    model.LikeOwnerID = likeownerid;
    model.LikedUserID = likeduserid;
    model.ShareType = sharetype;
    model.PostShareID = shareid;

    hub.server.sendLike(likeduserid, model);
}

function PostComment(shareownerid, shareid, shareTypeid, commentUserid) {

    var shareType = "";
    var commentText = $("#new-feed-comment").val();
    commentText = commentText;

    switch (shareTypeid) {
        case 1:
            shareType = "user";
            break;
        case 2:
            shareType = "idea";
            break;
        case 3:
            shareType = "project";
            break;
        case 6:
            shareType = "community";
            break;
        case 7:
            shareType = "investment";
            break;
        default:
            break;

    }

    var model = new Object();
    model.CommentShareID = shareid;
    model.ShareType = shareType;
    model.CommentText = commentText;
    model.CommentUserID = commentUserid;
    model.ShareTypeID = shareTypeid;

    if (commentText != "") {
        $("#new-feed-comment").val("");
        hub.server.sendComment(shareownerid, model);

    }




}

function sendMsg(el, senderID, reciverID) {


    var d = new Date(),
       h = (d.getHours() < 10 ? '0' : '') + d.getHours(),
       m = (d.getMinutes() < 10 ? '0' : '') + d.getMinutes(),
       timeNow = h + ':' + m;

    var $this = $(el);
    var message = $this.val();


    if (message == null || message == "") {
        return false;
    } else {
        var messageDiv = $("<div>", { class: "msg-content sent" });
        var messageContent = $("<p></p>");

        //var messageDiv2 = $("<div>", { class: "msg-content sent" });
        //var messageContent2 = $("<p></p>");


        $this.parent().prev(".message-row").find('.mCSB_container').prepend(messageDiv);
        messageDiv.append(messageContent);
        messageContent.html(message);
        messageDiv.append("<span>" + timeNow + "</span>");

        //------------------------------------------------------

        //$("#message-newuser-content_" + reciverID).find('.mCSB_container').prepend(messageDiv2);
        //messageDiv2.append(messageContent2);
        //messageContent2.html(message);
        //messageDiv2.append("<span>" + timeNow + "</span>");


        $(".message-row").mCustomScrollbar("scrollTo", "bottom");
        $(".msg-to-input").val("");

        var model = new Object();
        model.SenderID = senderID;
        model.RecieverID = reciverID;
        model.Message = message;

        hub.server.sendMessage(senderID, model);
    }
}

function SendSecondShare(userid, shareid, shareTypeid) {

    hub.server.sendSecondShare(userid, shareid, shareTypeid);
}