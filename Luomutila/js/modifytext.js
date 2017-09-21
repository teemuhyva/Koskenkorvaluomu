function modifyData() {

    //update about field
    $("#modifyabouttext").click(function () {
        $.getJSON("/application/getberryvalues/", { name: berryname }, function (json2) {
            var berry = JSON.parse(json2);

            $("#pricesModal_berryname").val(berry.Berry);
            $("#pricesModal_price").val(berry.Price_per_litre);

            $("#pricesTableDialog").modal("show");

            $("#berryModalSave").click(function () {
                var newdetails = {
                    Berry: $("#pricesModal_berryname").val(),
                    Price_per_litre: $("#pricesModal_price").val()
                };

                $.ajax({
                    type: 'POST',
                    url: '/application/updateberryprice',
                    data: JSON.stringify(newdetails),
                    contentType: 'application/json',
                    success: function (status) {
                        $("#pricesTableDialog").modal("hide");
                        updatePricesList();
                    },
                    error: function (err) {
                        alert("virhe tapahtui " + err);
                    }

                });

            });
        });
    });

}

$(function () {
    modifyData();
});