$(document).ready(function () {
    $('.rel-link > li > a').hover(
        function () { $(this).animate({marginRight:'10px'}) },
        function () { $(this).animate({ marginRight: '0' }) }
    );
});