var cart =
{
    init: function () {
        cart.regEvents();
    },
    regEvents: function () {
        $('#btnUpdate').off('click').on('click', function () {
            var listSP = $('#quantity');
            var cartList = [];
            $.each(listSP, function (i, item) {
                cartList.push({
                    quantity: $(item).val(),
                    sanpham: {
                        MaSP: $(item).data('MaSP')
                    }
                });
            });
            $.ajax(
                {
                    url: '/Cart/Update',
                    data: {
                        cartModel: JSON.stringify(cartList)
                    },
                    contentType: 'json',
                    type: 'POST',
                    success: function (res) {
                        if (res.status == true) {
                            window.location.href = "/Cart";
                        }
                    }
                })
        });
    }
}