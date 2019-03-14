jQuery(function () {
    "use strict";

    function tjekEmail(email) {
        var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        return re.test(email);
    }

    function validate() {
        var $result = $("#result");
        var email = $("#email").val();
        $result.text("");

        if (tjekEmail$result.text(email + " er god nok     &#10084");
            $result.css("color", "green");
        } else {
            $result.text(email + " er ikke god nok &#128148");
            $result.css("color", "red");
        }
        return false;
    }

    $("#validate").bind("click", validate);

//    //VALIDATOR
//    //Opfind en funktion der laver et flueben
//    function ok(ting) {
//        $(ting).parent().removeClass('udraabstegn').addClass('flueben')
//    }
//
//    //Opfind en funktion der laver et udråbstegn
//    function error(ting) {
//        $(ting).parent().removeClass('flueben').addClass('udraabstegn')
//    }
//    //opfind en funktion der tjekker om noget er en email
//    function erDerSnabela(ting) {
//        //Tjek om længden af tingen er større end nul
//        if (ting.length > 0) {
//            //Tjek det der står i feltets bogstavsrække har et @ - tjek hele rækken, OG tjek for . i hele rækken
//            if ($.inArray('@', ting.val()) === -1 && $.inArray('.', ting.val()) === -1) {
//                //Hvis der hverken er @ eller . i tekstrækken så gem falsk i ting
//                return false;
//            }
//            //i andre tilfælde
//            else {
//                return true;
//            }
//        }
//    }
//    //Jeg opfinder en funktion "validering" som gælder hvor "ting"
//    function validering(ting) {
//        //for hver ting gælder en funktion. Dels afhængigt af index og dels afhængigt af ting
//        ting.each(function (index, ting) {
//
//            //denne funktion venter på blur for at køre en ny funktion
//            $(ting).on('blur', function () {
//
//                //TJEK ALLE FELTERNE:
//
//                //EMAIL-FELT
//                //Betingelse der validerer data i dette felt
//                if ($(this).data().validering = "email") {
//                    //hvis det er rigtigt
//                    if (erDerSnabela($(this)) === true) {
//                        ok($(this));
//                    }
//                    //Hvis det ikke er rigtigt
//                    else {
//                        error($(this));
//                    }
//                }
//            });
//        });
//    }
//
//    //VALIDERINGSFUNKTION
//    validering($('input'));
//
//    //Opfind en funktion der tjekker for ting
//    function tjekTiBogstaver(ting) {
//        //husk en value:
//        var val = $(ting).val();
//        //Hvis value er under 2
//        if (val.length < 10) {
//            //Så gem value som falsk
//            return false;
//        }
//        //I andre tilfælde
//        else {
//            //Gem value som rigtig
//            return true;
//        }
//    }





});
