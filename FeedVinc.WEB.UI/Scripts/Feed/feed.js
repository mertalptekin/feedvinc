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
        alert("deneme");

        var id = sessionStorage.getItem("data-feed-id");

        if (sessionStorage.getItem("data-feed-id") == null)
            id = 1;


        if (id == 1) {
            var uri = "api/feed/around-me?$expand=User:$filter=ShareTypeID eq " + id + ":$top=2:$skip=" + (pageIndex * 2);
            FeedScroll("/HomeUI/GetFeed?uri=", uri);
        }
        else if (id == 2) {
            var uri = "api/feed/idea?$expand=Idea:$filter=ShareTypeID eq " + id + ":$top=2:$skip=" + (pageIndex * 2);
            FeedScroll("/HomeUI/GetFeed?uri=", uri);
        }
        else if (id == 3) {
            var uri = "api/feed/story-tellin?$expand=Project:$filter=ShareTypeID eq " + id + ":$top=2:$skip=" + (pageIndex * 2);
            FeedScroll("/HomeUI/GetFeed?uri=", uri);
        }
        else if (id == 4) {
            var uri = "api/feed/feedback?$expand=Project:$filter=ShareTypeID eq " + id + ":$top=2:$skip=" + (pageIndex * 2);
            FeedScroll("/HomeUI/GetFeed?uri=", uri);
        }
        else if (id == 5) {
            var uri = "api/feed/launch?$expand=Project:$filter=ShareTypeID eq " + id + ":$top=2:$skip=" + (pageIndex * 2);
            FeedScroll("/HomeUI/GetFeed?uri=", uri);
        }
        else if (id == 6) {
            var uri = "api/feed/community?$expand=Community:$filter=ShareTypeID eq " + id + ":$top=2:$skip=" + (pageIndex * 2);
            FeedScroll("/HomeUI/GetFeed?uri=", uri);
        }
    }
})

function FeedScroll(webUrl, apiUrl) {

    $.ajax({
        url: webUrl + apiUrl,
        type: "Get",
        success: function (response) {
            $("#feeds").append(response);
            console.log(response);
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

    console.log($(".comment-modal-wrapper"));

    //$(".comment-modal-wrapper").remove();

    switch (id) {
        case 1:
            var uri = "api/feed/around-me?$expand=User:$filter=ShareTypeID eq 1:$top=2";
            FeedAjax("/HomeUI/GetFeed?uri=", uri);
            break;
        case 2:
            var uri = "api/feed/idea?$expand=Idea:$filter=ShareTypeID eq 2:$top=2";
            FeedAjax("/HomeUI/GetFeed?uri=", uri);
            break;
        case 3:
            var uri = "api/feed/story-tellin?$expand=Project:$filter=ShareTypeID eq 3:$top=2";
            FeedAjax("/HomeUI/GetFeed?uri=", uri);
            break;
        case 4:
            var uri = "api/feed/feedback?$expand=FeedBack:$filter=ShareTypeID eq 4:$top=2";
            FeedAjax("/HomeUI/GetFeed?uri=", uri);
            break;
        case 5:
            var uri = "api/feed/launch?$expand=Launch:$filter=ShareTypeID eq 5:$top=2";
            FeedAjax("/HomeUI/GetFeed?uri=", uri);
            break;
        case 6:
            var uri = "api/feed/community?$expand=Community:$filter=ShareTypeID eq 6:$top=2";
            FeedAjax("/HomeUI/GetFeed?uri=", uri);

    }
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
        },
        beforeSend: function () {
            var target = document.getElementById('loading');
            $(target).show();
            var spinner = new Spinner();
            console.log(spinner.opts);
            spinner.opts.top = "10%";
            spinner.opts.scale = 2;
            spinner.spin();
            target.appendChild(spinner.el);
        }
    })
}