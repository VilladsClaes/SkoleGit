/* Denne js fil sætter en CSS class på header-elementet hvis der scrolles, og fjerner den igen hvis toppen af siden nås */

$(window).scroll(function () {
    $('.header-container').addClass('header-shadow');
    if ($(window).scrollTop() <= 0) {
        $('.header-container').removeClass('header-shadow');
    }
});