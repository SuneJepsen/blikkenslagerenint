/*================================================================*/
/*	ROTATING TESTIMONIALS jquery.easytabs.js
/*================================================================*/
$(document).ready(function () {
    $('.rotating-testimonials').easytabs({
        animationSpeed: 400,
        updateHash: false,
        cycle: 5000
    });
});



/* ________  flexslider __________________*/

// flex content slider
$(window).load(function () {
    $('.content-slider.flexslider').flexslider({
        animation: "slide",
        touch: true,
        smoothHeight: true,
        useCSS: false //thins type if true and screws up video
    });
});

// flex boxed slider
$(window).load(function () {
    $('.boxed-slider.flexslider').flexslider({
        animation: "fade",
        touch: true,
        useCSS: false
    });
});

// flex image slider (default fallback image slider)
$(window).load(function () {
    $('.image-slider.flexslider').flexslider({
        animation: "slide",
        touch: true,
        animationLoop: false,
        smoothHeight: true,
        controlNav: false,
        useCSS: false
    });
});

//https://github.com/woothemes/FlexSlider/issues/655
// portfolio carousel slider
$(window).load(function () {
    // The slider being synced must be initialized first
    //$('.portfolio-detail #carousel').flexslider({
    //    animation: "slide",
    //    controlNav: false,
    //    animationLoop: false,
    //    slideshow: false,
    //    itemWidth: 90,
    //    itemMargin: 5,
    //    move: 10,
    //    useCSS: false, //thins type if true
    //    asNavFor: '#slider'
    //});

    //$('.portfolio-detail #slider').flexslider({
    //    animation: "slide",
    //    controlNav: false,
    //    animationLoop: false,
    //    slideshow: false,
    //    touch: true,
    //    sync: "#carousel",
    //    keyboard: true,
    //    multipleKeyboard: true,
    //    smoothHeight: true,
    //    useCSS: false //thins type if true
    //});
    
    $('.portfolio-detail #carousel').flexslider({
        animation: "slide",
        controlNav: false,
        animationLoop: false,
        slideshow: false,
        asNavFor: '#slider',
        itemWidth: 90,
        itemMargin: 5
    });

    $('.portfolio-detail #slider').flexslider({
        animation: "slide",
        smoothHeight: true,
        controlNav: false,
        video: true,
        animationLoop: false,
        directionNav: true,
        slideshow: false,
        sync: "#carousel",
        keyboard: true,
        multipleKeyboard: true,
        start: function (slider) {
        }
    });

});
// end flexslider
/* __________________ fancybox __________________*/

$(document).ready(function () {

    /* ________  fancybox with button helpers and thumbnails used in gallery or portfolio page __________________*/

    $(".fancyme").fancybox({
        prevEffect: 'elastic',
        nextEffect: 'elastic',
        padding: 0,
        closeBtn: true,
        helpers: {

            title: {
                type: 'inside'
            },
            buttons: {},
            thumbs: {
                width: 75,
                height: 50
            }
        }
    });

    /* ________  single image __________________*/
    $(".fancysingle").fancybox({
        prevEffect: 'elastic',
        nextEffect: 'elastic',
        padding: 0,
        closeBtn: true,
        helpers: {
            title: {
                type: 'inside'
            }
        }
    });

    /* ________  fancybox with button helper for back and next with no thumbnails __________________*/

    $(".fancybutton").fancybox({
        prevEffect: 'elastic',
        padding: 0,
        nextEffect: 'elastic',
        closeBtn: true,
        helpers: {
            title: {
                type: 'inside'
            },
            buttons: {}
        }
    });

    /* ________  fancybox for media (videos) __________________*/

    $('.fancybox-media').fancybox({
        prevEffect: 'none',
        padding: 0,
        nextEffect: 'none',
        helpers: {
            media: {},
            title: {
                type: 'inside'
            }
        }
    });

    /* ________  fancybox for general items iframes, ajax, etc. __________________*/

    $(".various").fancybox({
        maxWidth: 1000,
        maxHeight: 600,
        fitToView: false,
        width: '100%',
        height: '100%',
        autoSize: false,
        prevEffect: 'elastic',
        nextEffect: 'elastic',
        closeClick: true,
        padding: 0,
        openEffect: 'fade',
        closeEffect: 'fade',
        helpers: {
            title: {
                type: 'inside'
            }
        }

    });

    /* ________  fancybox with summary title __________________*/

    $(".fancytitle").fancybox({
        beforeLoad: function () {
            this.title = $(this.element).next('.entry-summary').html();
        },
        prevEffect: 'elastic',
        padding: 0,
        nextEffect: 'elastic',
        helpers: {
            title: {
                type: 'inside'
            },
            buttons: {} //
        }
    });


}); // end fancybox




/***************************Contactpage*********************/
var Contactpage = function () {
    var init = function () {
        submit();
        cancel();
        initOpenLayers();
    },
     initOpenLayers = function () {
        var map, layer_, setCenter_, showMarker_, locked_, zoomLevel_;

        jQuery(document).ready(InitializeSimpleMap);
        var _initialized = false;
        function InitializeSimpleMap() {
            if (_initialized == true) {

            }
            var longitude = jQuery("[id$=hdfLongitude]").val();
            var latitude = jQuery("[id$=hdfLatitude]").val();

            _initialized = true;




            var controls = [];
            //if (locked_ !== "True") {

            //    controls.push(new OpenLayers.Control.Navigation());
            //}


            map = new OpenLayers.Map('map', {
                controls: controls
            });

            controls = map.getControlsByClass('OpenLayers.Control.Navigation');
            for (var i = 0; i < controls.length; ++i) {
                controls[i].zoomWheelEnabled = false;
                controls[i].disableZoomWheel();

            }


            layer_ = new OpenLayers.Layer.OSM("OSM Map");
            map.addLayer(layer_);

            lonlat = new OpenLayers.LonLat(longitude, latitude);
            SetMapMarker_(map, lonlat);
        }



        function SetMapMarker_(map, lonlat) {
            lonlat = lonlat.transform(new OpenLayers.Projection("EPSG:4326"), map.getProjectionObject());

            map.setCenter(lonlat, 15);
            var markers = new OpenLayers.Layer.Markers("Markers");
            map.addLayer(markers);

            var size = new OpenLayers.Size(62, 54);
            var offset = new OpenLayers.Pixel(-(size.w / 2), -size.h);
            var icon = new OpenLayers.Icon('/images/icons/mapmarker.png', size, offset);

            markers.addMarker(new OpenLayers.Marker(lonlat, icon));


        }
    },
    reset = function () {
        jQuery('#contactFormular .alert-error').addClass("display-none");
        jQuery('#contactFormular .alert-success').addClass("display-none");
        jQuery("#contactFormular .control-group").removeClass("error");
        jQuery("#contactFormular .statusmessage").remove();
        },
    submit = function () {
        reset();
        jQuery('#sendContactFormular').click(function () {
            function isNumber(n) {
                return !isNaN(parseFloat(n)) && isFinite(n);
            }
            contactFormularInputValues = {};
            contactFormularInputValues.name = document.getElementById('name').value;
            contactFormularInputValues.phone = document.getElementById('phone').value;
            contactFormularInputValues.email = document.getElementById('email').value;
            contactFormularInputValues.message = document.getElementById('message').value;

            jQuery('#contactFormular .alert-error').addClass("display-none");
            jQuery('#contactFormular .alert-error .errormessage').remove();

            var errorbar = jQuery('#contactFormular .alert-error');


            if (contactFormularInputValues.email.length == 0 ) {
                if (contactFormularInputValues.email.length == 0)
                    jQuery("#contactFormular #email").closest(".control-group").addClass("error");
               

                jQuery('#contactFormular .alert-error').removeClass("display-none");
                return false;
            }


       

            //Email skal indehold et @
            if (contactFormularInputValues.email.toLowerCase().indexOf("@") < 0) {
                jQuery("#contactFormular #email").closest(".control-group").addClass("error");
                jQuery('#contactFormular .alert-error').removeClass("display-none");
                jQuery("<label class='errormessage'>Email skal være i korrekt format.</label>").appendTo(errorbar);
                return false;
            }


            jQuery("#sendContactFormular").attr("disabled", "disabled");
            jQuery("#cancelContactFormular").attr("disabled", "disabled");
            jQuery("#contactFormularLoader").removeClass("display-none");

            jQuery.ajax({
                url: '/Umbraco/Api/ContactApi/SubmitContactForm',
                type: 'POST',
                data: JSON.stringify(contactFormularInputValues),
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    var successBar = jQuery('#contactFormular .alert-success');
                    successBar.removeClass("display-none");
                    jQuery('#contactFormular .statusmessage').empty();
                    jQuery("<label class='statusmessage'>" + data.StatusMessage + "</label>").appendTo(successBar);
                    jQuery("#contactFormularLoader").addClass("display-none");

                },
                error: function (request, status, error) {
                    jQuery("#contactFormularLoader").addClass("display-none");

                }
            });

            return false;
        });
    },
    cancel = function () {
        jQuery(".reset, #contactFormular").on("click", function (event) {
            reset();
        });
    };

    return {
        init: init

    };
}();