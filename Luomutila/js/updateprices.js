function updatePricesList() {
    $.getJSON("/application/pricestableget", null, function (json) {
        var prices = JSON.parse(json);
        
        $.ajax({
            type: 'GET',
            url: '/account/loggedinornot',
            success: function (status) {
                var html = "";

                if (status == true) {
                    for (var i = 0; i < prices.length; i++) {
                        html += "<tr>" +
                            "<td><a href='#'><span class='glyphicon glyphicon-pencil'></span></a><a href='#'><span class='glyphicon glyphicon-remove'></span></a></td>" +
                            "<td>" + prices[i].Berry + "</td>" +
                            "<td>" + prices[i].Price_per_litre + "</td>" +
                            "</tr>";
                    }

                } else {
                    for (var i = 0; i < prices.length; i++) {
                        html += "<tr>" +
                            "<td>" + prices[i].Berry + "</td>" +
                            "<td>" + prices[i].Price_per_litre + "</td>" +
                            "</tr>";
                    }
                }

                

                $("#pricestable tbody").html(html);

                //update prices list 
                $(".glyphicon-pencil").click(function () {
                    var berryname = $(this).parent().parent().next().text();

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




                //remove item
                $(".glyphicon-remove").click(function () {
                    var berryname = $(this).parent().parent().next().text();

                    $.getJSON("/application/getberryvalues/", { name: berryname }, function (json2) {
                        var berry = JSON.parse(json2);

                        $("#deleteModal_berryname").val(berry.Berry);
                        $("#deleteModal_price").val(berry.Price_per_litre);

                        $("#remotepriceDialog").modal("show");

                        $("#berrymodalDelete").click(function () {
                            var newdetails = {
                                Berry: $("#deleteModal_berryname").val()
                            };

                            $.ajax({
                                type: 'POST',
                                url: '/application/deleteberry',
                                data: JSON.stringify(newdetails),
                                contentType: 'application/json',
                                success: function (status) {
                                    $("#remotepriceDialog").modal("hide");
                                    updatePricesList();
                                },
                                error: function (err) {
                                    alert("virhe tapahtui poistamisessa" + err);
                                }

                            });

                        });
                    });
                });
            },
            error: function (err) {
                alert("virhe tapahtui " + err);
            }
        }); 

        
    });
}

function addnewitem() {
    $("#addpriceDialog").modal("show");

    $("#berrymodalAdd").click(function () {
        var newdetails = {
            Berry: $("#addModal_berryname").val(),
            Price_per_litre: $("#addModal_price").val()
        };

        $.ajax({
            type: 'POST',
            url: '/application/addnewitem',
            data: JSON.stringify(newdetails),
            contentType: 'application/json',
            success: function (status) {
                $("#addpriceDialog").modal("hide");
                updatePricesList();
            },
            error: function (err) {
                alert("virhe tapahtui poistamisessa" + err);
            }

        });

    });
}
    

$(function () {

    updatePricesList();
});