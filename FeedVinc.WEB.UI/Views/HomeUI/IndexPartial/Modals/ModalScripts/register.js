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
        $("#emailIsValid").text(response.error[0]);
    }
}
function OnComplete() {
    var target = document.getElementById('loading');
    $(target).hide();
}