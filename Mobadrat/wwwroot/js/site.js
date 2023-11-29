$(function () {
    $('[data-toggle="tooltip"]').tooltip();
    $(".sub-menu .collapse").on("hide.bs.collapse", function () {
        $(this).prev().find(".fa").eq(1).removeClass("arrow").addClass("fa-angle-down");
        alert('ssss')
    });
    $('.sub-menu .collapse').on("show.bs.collapse", function () {
        $(this).prev().find(".fa").eq(1).removeClass("arrow").addClass("fa-angle-right");
    });
})

window.onload = function () {
    var hijryDate = new Intl.DateTimeFormat('ar-TN-u-ca-islamic', { day: 'numeric', month: 'long', weekday: 'long', year: 'numeric' }).format(Date.now());
    var date = document.getElementById('today');

    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth() + 1; //January is 0!
    var yyyy = today.getFullYear();

    if (dd < 10) {
        dd = '0' + dd
    }

    if (mm < 10) {
        mm = '0' + mm
    }
    //////////////
    var day = today.getDay();
    var daylist = ["الأحد", "الأثنين", "الثلاثاء", "الأربعاء", "الخميس", "الجمعة", "السبت"];
    //////////////////

    today = yyyy + '/' + mm + '/' + dd;
    //date.textContent = daylist[day] + " " + today;
    date.textContent = hijryDate + " - " + today;
}