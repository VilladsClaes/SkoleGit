
jQuery.noConflict();

jQuery(function ($) {
    $('#slides').nivoSlider({
        effect: 'fade',
        captionOpacity: 0.7,
        pauseOnHover: true,
        animSpeed: 900,
        keyboardNav: false,
        manualAdvance: false,
        directionNav: false,
        controlNav: false,
        pauseTime: 8000
    });
});