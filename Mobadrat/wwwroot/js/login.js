$(document).ready(function () {
    $("#login_btn").on("click", function (e) {
        if ($("#UserName").val() == "") {
            $("#UserName").css("border-bottom", "1px solid RED");
            return false
        }
        else {
            $("#UserName").css("border-bottom", "2px solid #cadb4f");
        }

        if ($("#UserPassword").val() == "") {
            $("#UserPassword").css("border-bottom", "1px solid RED");
            return false
        }
        else {
            $("#UserPassword").css("border-bottom", "2px solid #cadb4f");
        }

        var data = $('#form_data').serializeArray();
        var datastring = JSON.stringify(data)
        //console.log(data);
        e.preventDefault();
        $.ajax({
            type: 'POST',
            url: 'Login/Index',
            cache: false,
            contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
            data: data,
            success: function (result) {
                if (result.isExist == false) {
                    swal({
                        title: 'اسم المستخدم أو كلمة المرور خطأ!',
                        text: 'يرجى التأكد من معلومات دخولك',
                        type: 'error',
                        timer: 4000,
                        confirmButtonText: 'شكرًا لك'
                    });
                } else if (result.isRegularUser) {
                    window.location.href = 'Mobadra/Index/';
                }
                else if (result.isAdmin) {
                    window.location.href = 'Home/Index/';
                }
                else if (result.isSuperAdmin) {
                    window.location.href = 'Home/Index/';
                }
            },
            error: function () {
                swal({
                    title: "حدث خطأ",
                    type: 'warning',
                    timer: 2000,
                    showConfirmButton: false,
                    //confirmButtonText: 'شكرًا لك'
                });
                //alert('Failed to receive the Data');
            }
        })
    });
});

//jQueryAjaxPost = form => {

//    if ($("#UserName").val() == "") {
//        $("#UserName").css("border-bottom", "1px solid RED");
//        return false
//    }
//    else {
//        $("#UserName").css("border-bottom", "2px solid #cadb4f");
//    }

//    if ($("#UserPassword").val() == "") {
//        $("#UserPassword").css("border-bottom", "1px solid RED");
//        return false
//    }
//    else {
//        $("#UserPassword").css("border-bottom", "2px solid #cadb4f");
//    }

//    //
    
//    try {
//        $.ajax({
//            type: 'POST',
//            url: form.action,
//            data: new FormData(form),
//            contentType: false,
//            //processData: false,
//            success: function (result) {
//                if (result.isRegularUser) {
//                    window.location.href = 'Mobadra/Index/';
//                }
//                else if (result.isAdmin) {
//                    window.location.href = 'Home/Index/';
//                }
//                else if (result.isSuperAdmin) {
//                    window.location.href = 'Home/Index/';
//                }
//                else if (result.isExist == false) {
//                    swal({
//                        title: 'اسم المستخدم أو كلمة المرور خطأ!',
//                        text: 'يرجى التأكد من معلومات دخولك',
//                        type: 'error',
//                        timer: 4000,
//                        confirmButtonText: 'شكرًا لك'
//                    });
//                }
//                else {
//                    //document.location = '/Home/Index/';
//                    //alert("ssss")
//                }
//            },
//            error: function (err) {
//                alert("Error")
//            }
//        })
//        //to prevent default form submit event
//        return false;
//    } catch (ex) {
//        console.log(ex)
//    }
//}