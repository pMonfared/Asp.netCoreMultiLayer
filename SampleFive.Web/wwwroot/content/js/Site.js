(function () {
    $("#selectLanguage select").change(function () {
        $(this).parent().submit();
    });
}());

$("body").delegate('.refreshcaptcha', 'click', function (event) {
    var alink = $(this);
    alink.html("<i class='fa fa-refresh fa-spin fa-lg'></i>");
    var captchagroup = alink.parents('div.captchagroup');
    var img = captchagroup.find("img.imgcpatcha");
    var random = new Date();
    $.ajax({
        url: '/Captcha/CaptchaImage',
        type: "GET",
        data: null
    })
    .done(function (functionResult) {
        img.attr("src", "/Captcha/CaptchaImage?" + random + functionResult);
        alink.html("<i class='fa fa-refresh fa-lg'></i>")
    });
});
