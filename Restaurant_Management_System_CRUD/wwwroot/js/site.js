// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript c


/*$(document).ready(function () {
    $(".item-add-to-order").on('click', function () {
        var menuId = $(this).attr('data-MenuId');
        var Quantity = $("menu-Quantity").val();

        $.ajax({
            url: '/Order/PlaceOrder',
            type: 'POST',
            data: { "menuId": menuId, "Quantity": Quantity },
            success: function (result) {
                if (result === 'added') {
                    $("#alert-success-message").show();
                }
                else if {
                    $("#alert-error-message").show();
                }
                    setTimeout(function () {
                    window.location.href = "/order/index";
                }, 5000);
            },
            error: function (result) {

            }
        })
    })

})*/