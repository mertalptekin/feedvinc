
function OnBegin() {
    var target = document.getElementById('loading');
    var spinner = new Spinner().spin();
    target.appendChild(spinner.el);
}

function OnFailure(error) {
    console.log(error.message);
}

function OnSuccess(response) {
    if (response.IsValid == false) {
        $("#AccountIsValid").text(response.message);
    }
    else {
        $("#loginForm").trigger("reset");
        toastr.success(response.message);
        setTimeout(function () {
            toastr.clear();
            window.location.href = response.RedirectURL;
        }, 2000)
    }
}

function OnComplete() {
    var target = document.getElementById('loading');
    $(target).hide();
}