$(document).ready(function () {
    $("#share-textarea").hashtags();
})


toastr.options = {
    "closeButton": false,
    "debug": false,
    "newestOnTop": false,
    "progressBar": false,
    "positionClass": "toast-top-right"
};

var pageIndex = 0;

$(window).scroll(function () {

    if ($(window).scrollTop() == $(document).height() - $(window).height()) {

        pageIndex++;

        alert("test");

        var id = sessionStorage.getItem("data-feed-id");

        if (sessionStorage.getItem("data-feed-id") == null)
            id = 1;


        if (id == 1) {
            var uri = "api/feed/around-me?$expand=User:$top=2:$orderby=ShareID desc:$skip=" + (pageIndex * 2);
            FeedScroll("/HomeUI/GetFeedAroundMe?uri=", uri);
        }
        else if (id == 2) {
            var uri = "api/feed/idea?$expand=Idea:$top=2:$orderby=ShareID desc:$skip=" + (pageIndex * 2);
            FeedScroll("/HomeUI/GetFeedIdea?uri=", uri);
        }
        else if (id == 3) {
            var uri = "api/feed/story-tellin?$expand=Project:$top=2:$orderby=ShareID desc:$skip=" + (pageIndex * 2);
            FeedScroll("/HomeUI/GetFeedStoryTellin?uri=", uri);
        }
        else if (id == 4) {
            var uri = "api/feed/feedback?$expand=FeedBack:$top=2:$orderby=ShareID desc:$skip=" + (pageIndex * 2);
            FeedScroll("/HomeUI/GetFeedFeedBack?uri=", uri);
        }
        else if (id == 5) {
            var uri = "api/feed/launch?$expand=Launch:$top=2:$orderby=ShareID desc:$skip=" + (pageIndex * 2);
            FeedScroll("/HomeUI/GetFeedLaunch?uri=", uri);
        }
        else if (id == 6) {
            var uri = "api/feed/community?$expand=Community:$top=2:$orderby=ShareID desc:$skip=" + (pageIndex * 2);
            FeedScroll("/HomeUI/GetFeedCommunity?uri=", uri);
        }
    }
})

function FeedScroll(webUrl, apiUrl) {

    $.ajax({
        url: webUrl + apiUrl,
        type: "Get",
        success: function (response) {
            $("#feeds").append(response);
        },
        beforeSend: function () {
            var target = document.getElementById('scroll-loading');
            $(target).show();
            var spinner = new Spinner().spin();
            spinner.opts.position = "fixed";
            spinner.opts.top = parseInt(opts.top.replace("%")) + pageIndex * $(window).height + "%";
            spinner.opts.scale = 1;
            spinner.spin();
            target.appendChild(spinner.el);
        },
        complete: function () {
            var target = document.getElementById('scroll-loading');
            $(target).hide();
        }
    })
}

function MenuFilter(id) {

    var oldid = sessionStorage.getItem("data-feed-id");

    //önceki id şuanki tıklanandan farklı ise sayfalamayı 0'lar

    if (oldid != id)
        pageIndex = 0;

    //tıklanan menüyü session içinde saklıyoruz
    sessionStorage.setItem("data-feed-id", id);

    //menülerin class değişimi

    $(".feed-tag").removeClass("active");
    $(this).addClass("active");


    switch (id) {
        case 1:
            var uri = "api/feed/around-me?$expand=User:$top=2:$orderby=ShareID desc";
            FeedAjax("/HomeUI/GetFeedAroundMe?uri=", uri);
            break;
        case 2:
            var uri = "api/feed/idea?$expand=Idea:$top=2:$orderby=ShareID desc";
            FeedAjax("/HomeUI/GetFeedIdea?uri=", uri);
            break;
        case 3:
            var uri = "api/feed/story-tellin?$expand=Project:$top=2:$orderby=ShareID desc";
            FeedAjax("/HomeUI/GetFeedStoryTellin?uri=", uri);
            break;
        case 4:
            var uri = "api/feed/feedback?$expand=FeedBack:$top=2:$orderby=ShareID desc";
            FeedAjax("/HomeUI/GetFeedFeedBack?uri=", uri);
            break;
        case 5:
            var uri = "api/feed/launch?$expand=Launch:$top=2:$orderby=ShareID desc";
            FeedAjax("/HomeUI/GetFeedLaunch?uri=", uri);
            break;
        case 6:
            var uri = "api/feed/community?$expand=Community:$top=2:$orderby=ShareID desc";
            FeedAjax("/HomeUI/GetFeedCommunity?uri=", uri);

    }
}

var CommentPageIndex = 0;
var CommentPageFirstValue = 0;


function Next(ownerid, shareid, shareTypeid) {
    CommentPageIndex = CommentPageIndex + 1;

    if (CommentPageIndex<=CommentPageFirstValue) {
        GetCommentsPager(ownerid, shareid, shareTypeid, CommentPageIndex);
    }
}

function Prev(ownerid, shareid, shareTypeid) {
    CommentPageIndex = CommentPageIndex - 1;

    if (CommentPageIndex>=0) {
        GetCommentsPager(ownerid, shareid, shareTypeid, CommentPageIndex);
    }
}

function GetCommentsPager(ownerid, shareid, shareTypeid, pageIndex) {

    $.ajax({
        type: "Get",
        url: "/HomeUI/GetComments?ShareID=" + shareid + "&ShareTypeID=" + shareTypeid + "&pageIndex=" + pageIndex,
        success: function (response) {

            var currentUserID = sessionStorage.getItem("UserID");
            $("#comments-modal-contentID").empty();

            $.each(response.ShareComments, function (index, item) {

                $("#comments-modal-contentID").append(
                    '<div class="comments-modal-box">' +
                    '<div class="comments-header">' +
                        '<a href="/profile/' + item.CommentUser.UserName + '/' + item.CommentUser.UserCode + '"> ' +
                   '<img src="' + item.CommentUser.UserProfilePhoto + '" class="img-responsive"></a>' +
                        '<h6>' + item.CommentUser.UserName + '</h6>' +
                        '<span>' + item.PrettyDate + '</span>' +
                    '</div>' +
                    '<div class="comment">' +
                        '<p>' + item.CommentText + '</p>' +
                    '</div>' +
                '</div>')
            })

        }
    })
}


function GetComments(ownerid, shareid, shareTypeid) {

    CommentPageIndex = 0;

    $.ajax({
        type: "Get",
        url: "/HomeUI/GetComments?ShareID="+shareid+"&ShareTypeID=" + shareTypeid+"&pageIndex=" + CommentPageIndex,
        success: function (response) {

            var currentUserID = sessionStorage.getItem("UserID");
            $("#comments-modal-contentID").empty();

            CommentPageIndex = response.PreviousPagerCount;
            CommentPageFirstValue = response.PreviousPagerCount;

            $("#pager").empty();
            $("#pager").append(

                  '<a href="javascript:Prev(' + ownerid + ',' + shareid + ',' + shareTypeid + ');" class="pull-left" style="padding:10px;">Prev</a>' +
        '<a href="javascript:Next(' + ownerid + ',' + shareid + ',' + shareTypeid + ');" style="padding:10px;" class="pull-right">Next</a>'

                );

            $.each(response.ShareComments, function (index, item) {

              
                $("#comments-modal-contentID").append(
                    '<div class="comments-modal-box">' +
                    '<div class="comments-header">' +
                        '<a href="/profile/' + item.CommentUser.UserName + '/' + item.CommentUser.UserCode + '"> ' +
                   '<img src="' + item.CommentUser.UserProfilePhoto + '" class="img-responsive"></a>' +
                        '<h6>' + item.CommentUser.UserName + '</h6>' +
                        '<span>' + item.PrettyDate + '</span>' +
                    '</div>' +
                    '<div class="comment">' +
                        '<p>' + item.CommentText + '</p>' +
                    '</div>' +
                '</div>')
            })

            $("#comment-button").trigger("click");

            $("#comments-modal-bottom").empty();
            $("#comments-modal-bottom").append('<button onclick="PostComment(' + ownerid + ',' + shareid + ',' + shareTypeid + ',' + currentUserID + ')" class="comment-send-btn">Send</button>');

     
        }
    })
}


function FeedAjax(webUrl, apiUrl) {
    $.ajax({
        url: webUrl + apiUrl,
        type: "Get",
        success: function (response) {
            console.log(response);
            var target = document.getElementById('loading');
            $(target).hide();
            $("#feeds").html(response);
            $(".remodal-overlay").attr("style", "display: none;")
        },
        beforeSend: function () {
            var target = document.getElementById('loading');
            $(target).show();
            var spinner = new Spinner();
            console.log(spinner.opts);
            spinner.opts.scale = 1.5;
            spinner.opts.top = "150px";
            spinner.opts.color = "#f8f6f7";
            spinner.opts.opacity = 0.10;
            spinner.spin();
            target.appendChild(spinner.el);
            $(".remodal-overlay").attr("style", "display: block;")
           
        }
    })
}