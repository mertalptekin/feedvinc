


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



$(function () {

    var id = sessionStorage.getItem("UserID");

    var hub = $.connection.notificationHub;
    $.connection.hub.qs = "UserID=" + id;
    var userID = "UserID=" + id;

    $.connection.hub.qs = userID;

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
                        '<p>'+data.NotificationText+'</p>' +
                    '</div>' +
                    '<span class="time">' + data.SharePrettyDate + '</span>' +
            '</li>'
            )


        toastr["info"](data.ShareProfileName + " " + data.NotificationText);

        //<ul class="notification-list">
        //        @foreach (var item in Model)
        //{
        //            <li>
        //                <div class="notifications">
        //                    <img src="@item.ProfilePhotoPath">
        //                    <div>
        //                        <a href="@item.ShareProfileLink">@item.ShareProfileName</a>
        //                        <p>@item.NotificationText</p>
        //                    </div>
        //                    <span class="time">@item.SharePrettyDate</span>
        //                </div>
        //            </li>
        //}
        //    </ul>

    }
    
    $.connection.hub.start().done(function () {

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

    })

})