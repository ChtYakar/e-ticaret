$(function () {

  



    $('.flexslider').flexslider({
        animation: "slide",
        slideshowSpeed: 5000,
        controlNav: false,               //Boolean: Create navigation for paging control of each slide? Note: Leave true for manualControls usage
        directionNav: false,
        pausePlay: true
});
    var toggles = $('.toggle a'),
        codes = $('.code');

    toggles.on("click", function (event) {
        event.preventDefault();
        var $this = $(this);

        if (!$this.hasClass("active")) {
            toggles.removeClass("active");
            $this.addClass("active");
            codes.hide().filter(this.hash).show();
        }
    });
    toggles.first().click();


    $('input[id^="product-quantity"]').keypress(function (e) {
        debugger;
        if (String.fromCharCode(e.keyCode).match(/[^0-9]/g)) return false;
    });
    var sum = 0;
    $('[id^=urunToplamFiyat]').each(function () {
        debugger;
        var urunFiyat = $(this).text();
        sum += parseFloat(urunFiyat.replace(',', '.'));
    });
    $('#totalPrice').html(sum.toFixed(2));
});

