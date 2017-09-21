function login() {

    $("#login").modal("show");

    $("#kirjaudu").click(function () {
        var logindetails = {
            Username: $("#username").val(),
            Password: $("#password").val()
        };

        $.ajax({
            type: 'POST',
            url: '/account/login',
            data: logindetails,
            success: function (status) {
                $("#login").modal("hide");
            },
            error: function (err) {
                alert("virhe tapahtui poistamisessa" + err);
            }

        });
    });
    
    
}


