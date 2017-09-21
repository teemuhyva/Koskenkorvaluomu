$(function () {

    $('.nav li').on('click', function(e) {
        $('.active').removeClass('active');
        $(this).addClass('active');
    });

    $('#datetimepicker1').datepicker();
    $('#datetimepicker2').datepicker();
    $('#datetimepicker3').datepicker();

});