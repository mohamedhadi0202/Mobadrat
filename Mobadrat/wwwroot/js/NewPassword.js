$(document).ready(function () {
    $('.form-textbox').val("");
    $('.form-group input, .form-group textarea').focusout(function () {
        var text_val = $(this).val();
        if (text_val === "") {
            console.log("empty!");
            $(this).removeClass('has-value');
        } else {
            $(this).addClass('has-value');
        }
    });

});


$(function () {

    $('#SubmitFormNewPassword').click(function (e) {
        e.preventDefault();

        var usperpass = $("#UserPassword").val();
        var passconfirm = $("#ConfirmUserPassword").val();
        if (usperpass == "") {
            usperpass.css("border-bottom", "1px solid red")
            return false
        }

        if (passconfirm == "") {
            passconfirm.css("border-bottom", "1px solid red")
            return false
        }

        if (usperpass != passconfirm) {
            swal({
                title: 'كلمتي المرور غير متطابقتين',
                text: "تأكد مرة أخرى",
                type: 'error',
                timer: 4000,
                confirmButtonText: 'شكرًا لك'
            });
            return false
        }
        else {
            $.ajax({
                url: '/NewPassword/Index/',
                type: "POST",
                data: { UserPassword: usperpass, ConfirmUserPassword: passconfirm },
                success: function (data) {
                    swal({
                        title: 'تمّ تغيير كلمة المرور بنجاح!',
                        type: 'success',
                        timer: 4000,
                        confirmButtonText: 'شكرًا لك'
                    });
                    $("#UserPassword").val("");
                    $("#ConfirmUserPassword").val("");
                    $('.form-textbox').removeClass('has-value');
                },
                error: function () {
                    console.log("error");
                }
            });
        }

        

    });
});
