
//************************************Product Look Up Starts*******************************************************
function ProductsLookUp(_phrase, _resultListHeight) {
    if (_phrase !== "" || _phrase !== " ") {
        var params = { searchPhrase: _phrase };
        $.ajax({
            type: 'POST',
            cache: false,
            header: ("Access-Control-Allow-Origin: *"),
            url: siteRoot + '/api/webapi.asmx/GetProductsByCategoryName',
            data: JSON.stringify(params),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (result) {
                if (result.d.length > 0) {
                    _resultListHeight = 35;
                    var resultArray = result.d.split('•');
                    $('#lstSearchResultList').empty();
                    $.each(resultArray, function (index, value) {
                        $('#lstSearchResultList').append("<li><span>&#xf002;</span><span class='searchResultItem'>" + value + "</span></li>");
                        _resultListHeight += 35;
                        $("#lstSearchResultList").css("height", (_resultListHeight - 35) + 'px');
                    });
                    $('#lstSearchResultList li:last-child').remove();
                }
                else {
                    $('#lstSearchResultList').empty();
                    $("#lstSearchResultList").css("height", "35px");
                    $('#lstSearchResultList').append("<li><span>&#xf002;</span>Es gibt keine Ergebnisse..!</li>");
                }
            },
            error: function (result) { alert(result.status + ' ' + result.statusText) },
        });
    }
    else {
        return false;
    }
}
//******************************************************************************************************************************************//
function isMobile() {
    return /Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent);
}
$(document).ready(function () {
    
    _Main = $('.main').width();

    if (_Main > 700) {
        $(window).scroll(function () {
            var sticky = $('.sticky'),
                scroll = $(window).scrollTop();

            if (scroll > 0) sticky.addClass('fixed');
            else sticky.removeClass('fixed');
        });
    }

    $('.menuBtn').click(function () {
        if ($(this).hasClass('is-active')) {
            $(this).removeClass('is-active');
            $('#navigation').slideUp();
        } else {
            $(this).addClass('is-active');
            $('#navigation').slideDown();
        }
    })

    /*Pop Up*/
    $('.icInfo').click(function () {
        $('.moreInfo').fadeOut();
        $(this).children('.moreInfo').slideDown();
    })
    /*Public*/
    $(".goTop").click(function () {
        $("html, body").animate({ scrollTop: 0 }, 1000);
    });

    $('.readMore').click(function () {
        var text = $('.readMore').text();
        if (text == 'View Product') {
            $('.readMore').text('Close');
            $('.seoReadMore').css("display", "inline");
        } else {
            $('.readMore').text('View Product');
            $('.seoReadMore').hide();
        }
    });
    $('.profile-signning-toggle').mouseover(function () {
        _BS = $(this).parent('.icBasketPane');
        if (_BS.hasClass('isLogin')) {
            _Width = $('.main').width();
            if (_Width <= 480) {
                $('.LoginPane,.basketSmall').width(_Width);
            } else {
                $('.LoginPane,.basketSmall').css({ 'width': '' });
            }
            _BS.children('.LoginPane').show();
            return false;
        }

    })
    $(window).resize(function () {
        _Width = $('.main').width();
        if (_Width <= 480) {
            $('.LoginPane,.basketSmall').width(_Width);
        } else {
            $('.LoginPane,.basketSmall').css({ 'width': '' });
        }
    })
    $('.isLogin').mouseleave(function () {
        _BS = $(this);
        _BS.children('.LoginPane').hide();
    });

});


$(document).on('click', 'a.icBasket', function () {
    _BS = $(this).parent('.icBasketPane');
    if (_BS.hasClass('isBsActive')) {
        _BS.children('.basketSmall').toggle();
        return false;
    }
})
/*Product list*/
$(document).on('click', 'a.addBasket', function () {
    $('.cAddItem').removeClass('openList').children('ul').hide();
    $(this).hide();
    $(this).parent().children('.cAddItem').show();
    _ID = $(this).parent('.cAdd').attr('data-id');
    _Number = $(this).parent().children('.cAddItem').children('.selectedItem').text();
    _Title = $(this).parent('.cAdd').attr('data-title');
    _Picture = $(this).parent('.cAdd').attr('data-image');
    _Url = $(this).parent('.cAdd').attr('data-url');
    DoBasketJob(_ID, _Number, _Title, _Picture, _Url);
    $(this).parents('.prPrice').next('.sellLabel').html('<span>Selected Successfully..:)</span>');
    return false;
});

/*Product Detail*/
$(document).on('click', '.cAddItem ul > li', function () {
    $('.cAddItem').removeClass('openList').children('ul').hide();
    $(this).parents('.cAddItem').children('ul').children('li').removeClass('highlighted');
    _Select = $(this).addClass('highlighted').text();
    $(this).parents('.cAddItem').children('.selectedItem').text(_Select);
    $(this).parents('.cAddItem').removeClass('openList').children('ul').hide();
    _ID = $(this).parents('.cAdd').attr('data-id');
    _Title = $(this).parents('.cAdd').attr('data-title');
    _Picture = $(this).parents('.cAdd').attr('data-image');
    _Url = $(this).parents('.cAdd').attr('data-url');
    DoBasketJob(_ID, _Select, _Title, _Picture, _Url);
});

$(document).on('click', '.icAddBasket', function () {
    $(this).hide();
    $(this).next('.sellLabel').show();
    _ID = $(this).next('.sellLabel').children('.cAdd').attr('data-id');
    _Number = $(this).next('.sellLabel').children('.cAdd').children('.cAddItem').children('.selectedItem').text();
    _Title = $(this).next('.sellLabel').children('.cAdd').attr('data-title');
    _Picture = $(this).next('.sellLabel').children('.cAdd').attr('data-image');
    _Url = $(this).next('.sellLabel').children('.cAdd').attr('data-url');
    DoBasketJob(_ID, _Number, _Title, _Picture, _Url);
    return false;
});

$(document).on('click', '.selectedItem', function () {
    _select = $(this).parent('.cAddItem');
    if (_select.hasClass('openList')) {
        _select.removeClass('openList').children('ul').hide();
    } else {
        $('.cAddItem').removeClass('openList').children('ul').hide();
        _select.addClass('openList').children('ul').show();
    }
});

$(document).on('click', '.cAddItem ul > li.dellItem', function () {
    $(this).parents('.pr1Price').children().children('.cAddItem').hide().next('.addBasket').show();
    $(this).parents('.pr1Price').next('.sellLabel').html('');
    _num = $(this).removeClass('highlighted').next().addClass('highlighted').text();
    $(this).parents('.cAddItem').children('.selectedItem').text(_num);
});

$(document).on('click', '.bsBtn > .bsDel', function () {
    _Number = 'delete';
    _ID = $(this).parent('.bsBtn').attr('data-id');
    _Title = $(this).parent('.bsBtn').attr('data-title');
    _Picture = $(this).parent('.bsBtn').attr('data-image');
    _Url = $(this).parent('.bsBtn').attr('data-url');
    DoBasketJob(_ID, _Number, _Title, _Picture, _Url);
    return false;
});

function DoBasketJob(_ID, _Number, _Title, _Picture, _Url) {

    var params = { pId: _ID, Qty: _Number.replace(/[^0-9.]/g, ""), StatusQuo: ((_Number.replace(/[^0-9.]/g, "") == "") ? false : true) };

    alertify.confirm("Um dieses Produkt in den Warenkorb zu legen, müssen Sie zuerst zum Benutzerbereich gehen..!",
        function () {
            window.location = siteRoot + 'default.aspx?ReturnUrl=' + window.location.href;
        }).set({ title: "Möchten Sie sich anmelden?" }).set({ labels: { ok: 'Ich möchte zur Anmeldeseite weitergeleitet werden', cancel: 'Nicht Jetzt ' } }).setting('modal', true);
};







