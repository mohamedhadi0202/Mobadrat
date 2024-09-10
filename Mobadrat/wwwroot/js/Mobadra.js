$(document).ready(function () {

    function onSubmitValidation() {
        $("#SubmitAddForm").on("click", function () {
            if ($('#CategoryName').val() == '') {
                $('#CategoryNameValidation').fadeIn();
                $('#CategoryNameValidation').text('يجب كتابة اسم الصنف')
                return false
            } else {
                $('#CategoryNameValidation').fadeOut();
                $('#CategoryNameValidation').text('')
            }
        });

    }

    onSubmitValidation();
});

showAttachInPopup = (url, title) => {
    $.ajax({
        type: 'GET',
        url: url,
        success: function (res) {
            $('#form-modal .modal-body').html(res);
            $('#form-modal .modal-title').html(title);
            $('#form-modal').modal('show');
        }
    })


}

showInPopup = (url, title) => {
    $.ajax({
        type: 'GET',
        url: url,
        success: function (res) {
            $('#form-modal .modal-body').html(res);
            $('#form-modal .modal-title').html(title);
            $('#form-modal').modal('show');

            $('.form-textbox').addClass('has-value');
            //if ($('.form-textbox').val() === "") {
            //    $('.form-textbox').removeClass('has-value');
            //} else {
            //    $('.form-textbox').addClass('has-value');
            //}
        }
    })

    
}

//jQueryAjaxPost = form => {
//    try {
//        $.ajax({
//            type: 'POST',
//            url: form.action,
//            data: new FormData(form),
//            contentType: "application/json; charset=utf-8",
//            dataType: "json",
//            success: function (response) {
//                if (response) {
//                    alert("Ok!")
//                }
//            },
//            error: function (err) {
//                console.log(err)
//            }
//        })
//        //to prevent default form submit event
//        return false;
//    } catch (ex) {
//        console.log(ex)
//    }
//}

jQueryAjaxPost = form => {

    if ($("#MobadraTitle").val() == "") {
        $("#MobadraTitle").css("border-bottom", "1px solid RED");
        $(".MobadraTitleValidation").text("حقل إلزامي")
        $(".MobadraTitleValidation").slideDown();
        return false
    }
    else {
        $(".MobadraTitleValidation").text("")
        $(".MobadraTitleValidation").slideUp();
        $("#MobadraTitle").css("border-bottom", "2px solid #cadb4f");
    }

    if ($("#MobadraDesc").val() == "") {
        $("#MobadraDesc").css("border-bottom", "1px solid RED");
        return false
    }
    else {
        $("#MobadraDesc").css("border-bottom", "2px solid #cadb4f");
    }

    if ($("#MobadraTarget").val() == "") {
        $("#MobadraTarget").css("border-bottom", "1px solid RED");
        return false
    }
    else {
        $("#MobadraTarget").css("border-bottom", "2px solid #cadb4f");
    }

    if ($("#MobadraImplement").val() == "") {
        $("#MobadraImplement").css("border-bottom", "1px solid RED");
        return false
    }
    else {
        $("#MobadraImplement").css("border-bottom", "2px solid #cadb4f");
    }

    if ($('#Geha_TragetList').prop('selectedIndex') == "0") {
        $('#Geha_TragetList').css('border-bottom', '1px solid #eb1509');
        return false;
    }
    else {
        $("#Geha_TragetList").css("border-bottom", "2px solid #cadb4f");
    }

    if ($('#VolunteerIDtList').prop('selectedIndex') == "0") {
        $('#VolunteerIDtList').css('border-bottom', '1px solid #eb1509');
        return false;
    }
    else {
        $("#VolunteerIDtList").css("border-bottom", "2px solid #cadb4f");
    }

    if ($('#DurationTimeIDtList').prop('selectedIndex') == "0") {
        $('#DurationTimeIDtList').css('border-bottom', '1px solid #eb1509');
        return false;
    }
    else {
        $("#DurationTimeIDtList").css("border-bottom", "2px solid #cadb4f");
    }

    if (!$(".file-upload").hasClass('active')) {
        $(".file-upload .file-select").css("border-bottom", "1px solid RED");
        $(".validLabelAttach").text("يجب اختيار ملف للإرفاق")
        $(".validLabelAttach").slideDown();
        return false
    }
    else {
        $(".validLabelAttach").text("")
        $(".validLabelAttach").hide();
        $(".file-upload .file-select").css("border", "#cadb4f");
    }

    //

    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.isValid) {
                    $('#view-all').html(res.html)
                    //$('#view-all').load("/Category/_IndexPartial");
                    //setTimeout(window.location.href = "/Category/Index", 1000);
                    swal({
                        title: 'تمّ إضافة/تعديل السجل بنجاح!',
                        type: 'success',
                        timer: 4000,
                        confirmButtonText: 'شكرًا لك'
                    });

                }
                else {
                    if (res.isNotExist == false) {
                        swal({
                            title: 'عنوان المبادرة موجود مسبقا!',
                            type: 'error',
                            timer: 4000,
                            confirmButtonText: 'شكرًا لك'
                        });
                    }
                    
                }
            },
            error: function (err) {
                console.log(err)
            }
        })
        //to prevent default form submit event
        return false;
    } catch (ex) {
        console.log(ex)
    }
}

jQueryAjaxEditPost = form => {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.isValid) {
                    $('#view-all').html(res.html)
                    $('#form-modal .modal-body').html('');
                    $('#form-modal .modal-title').html('');
                    $('#form-modal').modal('hide');
                    $('#Geha_TragetList').val("0");
                    $('#VolunteerIDtList').val("0");
                    $('#DurationTimeIDtList').val("0");
                    setTimeout(function () {
                        swal({
                            title: 'تمّ تعديل السجل بنجاح!',
                            type: 'success',
                            timer: 5000,
                            confirmButtonText: 'شكرًا لك'
                        });
                    });
                }
            },
            error: function (err) {
                console.log(err)
            }
        })
        //to prevent default form submit event
        return false;
    } catch (ex) {
        console.log(ex)
    }
}

jQueryAjaxDelete = form => {
    if (confirm('هل تريد بالفعل حذف هذا السجل؟')) {
        try {
            $.ajax({
                type: 'POST',
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (res) {
                    $('#view-all').html(res.html);
                    setTimeout(function () {
                        swal({
                            title: 'تمّ حذف السجل بنجاح!',
                            type: 'success',
                            timer: 5000,
                            confirmButtonText: 'شكرًا لك'
                        });
                    });
                },
                error: function (err) {
                    console.log(err)
                }
            })
        } catch (ex) {
            console.log(ex)
        }
    }

    //prevent default form submit event
    return false;
}

jQueryAjaxPostAttatch = form => {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.isValid) {
                    $('#view-all').html(res.html)
                    swal({
                        title: 'تمّ إضافة المرفق بنجاح!',
                        type: 'success',
                        timer: 4000,
                        confirmButtonText: 'شكرًا لك'
                    });
                    $(".file-upload-popup").removeClass('active');
                    $("#noFilePopup").text("لا يوجد ملفات مرفقة ...");
                    $("#theTable").load("/Mobadra/_ShowAttatch");
                    //$('#form-modal').hide();
                    //$('.modal-dialog').hide();
                    //$('.modal-body').hide();
                    //$('.modal-backdrop').hide();
                }
            },
            error: function (err) {
                console.log(err)
            }
        })
        //to prevent default form submit event
        return false;
    } catch (ex) {
        console.log(ex)
    }
}

jQueryAjaxAttatchDelete = form => {
    if (confirm('هل تريد بالفعل حذف هذا السجل؟')) {
        try {
            $.ajax({
                type: 'POST',
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (res) {
                    $('#view-all').html(res.html);
                    setTimeout(function () {
                        swal({
                            title: 'تمّ حذف السجل بنجاح!',
                            type: 'success',
                            timer: 5000,
                            confirmButtonText: 'شكرًا لك'
                        });
                    });
                    $("#theTable").load("/Mobadra/_ShowAttatch");
                },
                error: function (err) {
                    console.log(err)
                }
            })
        } catch (ex) {
            console.log(ex)
        }
    }

    //prevent default form submit event
    return false;
}