var APP_SCROLL = {
    doc: $(document),
    elem: $('[data-sidebar-tab-scroll]'),
    list: $('.tab-pane.active'),
    init: function () {
        APP_SCROLL.elem = $('[data-sidebar-tab-scroll]');
    },
    bind: function () {
        APP_SCROLL.init();
        // Apply Custom Scrollbar
        APP_SCROLL.elem.mCustomScrollbar({
            autoHideScrollbar: false,
            axis: 'y',
            contentTouchScroll: true,
            scrollbarPosition: 'inside',
            scrollInertia: 200,
            scrollButtons: { enable: false },
            theme: 'dark',
            live: true,
            liveSelector: '[data-sidebar-tab-scroll]',
            advanced: { updateOnContentResize: true }
        });

        APP_SCROLL.resize();

        $('#whatAreWidgets[data-toggle="popover"]').popover({
            html: true,
            trigger: 'hover',
            content: '<small>IRIS Widgets are like like shortcuts to different IRIS functionalities like your Leave, MORP, Performance, Team Validation, etc. <strong>Take note that most functionalities are still being ported to the new platform.<strong></small>'
        });
    },
    set_height: function (element) {
        var win = $(window).innerHeight(),
            h = win - $('#sidebarLeftStatic').outerHeight(),
            offset = h + $('.widget-trks').innerHeight() + $('.widget-feedback').innerHeight() + 10;
            $(element).css('height', win - offset);
    },
    resize: function () {
        var resizeTimer;

        $(window).resize(function () {
            clearTimeout(resizeTimer);
            resizeTimer = setTimeout(
                function () {
                    APP_SCROLL.set_height(APP_SCROLL.elem);
                    APP_SCROLL.elem.mCustomScrollbar('update');
                }, 200);
        });
        //window.addEventListener("resize", function () {
          
        //});
    },
    build: function () {
        this.init();
        this.set_height(APP_SCROLL.elem);
        this.bind();
    }
}


// Toggle Sidebar View
var TOGGLE_SIDEBAR = {
    hidden: false,
    $leftSidebar: $('#sidebarLeftStatic'),
    $rightSidebar: $('#sidebarRightStatic'),
    $rBtn: $('#btnRightSidebar'),
    $lBtn: $('#btnLeftSidebar'),
    $main: $('.main-content'),
    sb_width: $('#sidebarRightStatic').width(),
    win: $(document).width(),

    bind: function () {
        TOGGLE_SIDEBAR.$rBtn.on('click', function () {
            setTimeout(function () {
                $("#divAdsArea").masonry({
                    itemSelector: ".ad-grid", percentPosition: "true", isResizeBound: "true", transitionDuration: 0
                });
            }, 2000);
            if ($(this).hasClass('js_untouched')) {
                TOGGLE_SIDEBAR.hidden == false ? TOGGLE_SIDEBAR.hide() : TOGGLE_SIDEBAR.show();
            }
        });
        TOGGLE_SIDEBAR.$lBtn.on('click', function () { TOGGLE_SIDEBAR.toggle_left_sidebar(); });

    },
    on_resize: function () {
        var resizeTimer;
        window.addEventListener("resize", function () {
            clearTimeout(resizeTimer);
            resizeTimer = setTimeout(TOGGLE_SIDEBAR.check_width, 200);
        });
    },
    check_width: function () { this.win < 992 && this.hidden == false ? this.hidden = true : this.hidden = false; },
    toggle_left_sidebar: function () {
        TOGGLE_SIDEBAR.$lBtn.find('i').velocity({ rotateZ: '-180deg' });
        TOGGLE_SIDEBAR.$leftSidebar.velocity(
            {
                left: function () {
                    var x = $(this).position();
                    if (x.left != 0) { return 0; }
                    else { return -TOGGLE_SIDEBAR.sb_width; }
                }
            },
            {
                duration: 500,
                easing: [.99, .01, .33, 1],
            }
        );
    },
    hide: function () {
        TOGGLE_SIDEBAR.hidden = true;
        TOGGLE_SIDEBAR.$rBtn.removeClass('js_untouched');
        TOGGLE_SIDEBAR.$rBtn.find('i').velocity({ rotateZ: '-180deg' });
        TOGGLE_SIDEBAR.$rightSidebar.velocity(
            { right: -TOGGLE_SIDEBAR.sb_width },
            {
                duration: 500,
                easing: [.99, .01, .33, 1],
                complete: function () {
                    TOGGLE_SIDEBAR.$main.addClass('sidebar-hidden');
                    TOGGLE_SIDEBAR.$rBtn.addClass('js_untouched');
                    if ($('#appListControls').length) {
                        $('#appListControls').css('right', '15px');
                    }
                }
            }
        );
    },
    show: function () {
        TOGGLE_SIDEBAR.hidden = false;
        TOGGLE_SIDEBAR.$rBtn.find('i').velocity({ rotateZ: '0deg' });
        TOGGLE_SIDEBAR.$main.removeClass('sidebar-hidden');
        TOGGLE_SIDEBAR.$rBtn.removeClass('js_untouched');

        TOGGLE_SIDEBAR.$rightSidebar.velocity('reverse', { complete: function () { TOGGLE_SIDEBAR.$rBtn.addClass('js_untouched'); } });

        if ($('#appListControls').length) $('#appListControls').removeAttr('style');
    },
    build: function () {
        this.check_width();
        this.on_resize();
        this.bind();
    }
}

// Ads Rotator Widget
var CONNECTORSAD = {
    timer: null,
    title: '[24]7 Connectors',
    run: function () {
        var ease = [.49, .87, .05, .97];
        var connectorsBlock = $(".widget-connectors");

        connectorsBlock.find('.logo').velocity('transition.flipYIn', { duration: 500 }).velocity({ translateX: '-80%' }, { duration: 400, easing: ease });
        connectorsBlock.find('.b1').velocity({ translateY: ['55%', '55%'], translateX: ['0', '-20%'], opacity: 1 },
            {
                display: 'block',
                easing: 'ease',
                duration: 400,
                delay: 500,
                begin: function () {
                    connectorsBlock.find('.b1 .text').velocity({ opacity: 1 }, { duration: 400, easing: ease });
                },
                complete: function () {
                    connectorsBlock.find('.b1').velocity({ translateY: 0 }, { easing: ease, duration: 400, delay: 100 });
                }
            }
        );

        connectorsBlock.find('.b2 .b2a').velocity({ translateY: ['0%', '20%'], opacity: [1, 0] },
            {
                duration: 400,
                easing: ease,
                delay: 1100,
                complete: function () {
                    connectorsBlock.find('.b2 .b2b').velocity({ translateX: ['0%', '20%'], opacity: [1, 0] }, { duration: 400, easing: ease });
                }
            }
        );

        connectorsBlock.find('.text.b3').velocity({ translateX: ['0%', '-20%'], opacity: [1, 0] }, { duration: 500, easing: ease, delay: 1600 });
        connectorsBlock.find('.text.b4').velocity({ translateY: ['0%', '-20%'], opacity: [1, 0] }, { duration: 600, easing: ease, delay: 1800 }).velocity('callout.tada');
        connectorsBlock.find('.txtgrp').velocity('transition.fadeOut', { delay: 5000, duration: 400 });
        connectorsBlock.find('.b5').velocity({ opacity: 1 }, { delay: 6000, duration: 1000, complete: CONNECTORSAD.start_repeat_timer });
    },
    repeat: function () {
        var connectorsBlock = $(".widget-connectors");
        connectorsBlock.find('.logo, .b5').velocity({ opacity: 0 },
            {
                duration: 1000, easing: 'linear',
                complete: function () {
                    connectorsBlock.find('.logo').velocity({ translateX: 0 }, { duration: 200, complete: CONNECTORSAD.run });
                    connectorsBlock.find('.logo, .text, .txtgrp, .b5').removeAttr('style');
                }
            }
        );
    },
    start_repeat_timer: function () {
        var timer = CONNECTORSAD.timer;
        window.clearTimeout(timer);
        timer = window.setTimeout(CONNECTORSAD.end, WIDGET_ROTATOR.repeat_interval);
    },
    end: function () {
        WIDGET_ROTATOR.will_repeat();
    }
}
var INITIATIVE = {
    $showBtn: $('#openInitiative'),
    $hideBtn: $('#closeInitiative'),
    $sb_initiative: $('#sidebarConnector'),
    flag: false,
    bind: function () {
        var $flag = INITIATIVE.flag;
        INITIATIVE.$showBtn.on('click', function () {
            INITIATIVE.toggle_view(INITIATIVE.$sb_initiative, $(this));
        });
        INITIATIVE.$hideBtn.on('click', function () { INITIATIVE.toggle_view(INITIATIVE.$sb_initiative, $(this)); });
    },
    toggle_view: function (sidebar, btn) {
        if (!btn.hasClass('no-click')) {
            sidebar.velocity({ translateX: toggle_flag() }, {
                translateX: toggle_flag(),
                duration: 800,
                easing: [.99, .01, .33, 1],
                complete: function () {
                    INITIATIVE.flag == false ? INITIATIVE.flag = true : INITIATIVE.flag = false;

                    btn.removeClass('no-click');
                }
            });
        }

        btn.addClass('no-click');

        function toggle_flag() {
            if (INITIATIVE.flag == false) { return '-220px'; }
            else { return '220px'; }
        }
    },
    build: function () {
        this.bind();
    }
}

var LINGAP = {
    $showBtn: $('#openLingap'),
    $showBt_initiatives: $('#openLingap2'),
    $hideBtn: $('#closeLingap'),
    $sb_lingap: $('#sidebarLingap'),
    flag: false,
    timer: null,
    title: 'LINGAP',
    bind: function () {
        LINGAP.$showBtn.on('click', function () {
            LINGAP.toggle_view(LINGAP.$sb_lingap, $(this));
        });
        LINGAP.$showBt_initiatives.on('click', function () {
            LINGAP.toggle_view(LINGAP.$sb_lingap, $(this));
        });
        LINGAP.$hideBtn.on('click', function () {
            LINGAP.toggle_view(LINGAP.$sb_lingap, $(this));
        });

        $(document).on('keydown', '#customAmount', function (e) {
            if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                return false;
            }
        });
    },

    toggle_view: function (sidebar, btn) {
        if (!btn.hasClass('no-click')) {
            sidebar.velocity({ translateX: toggle_flag() }, {
                translateX: toggle_flag(),
                duration: 800,
                easing: [.99, .01, .33, 1],
                complete: function () {
                    LINGAP.flag == false ? LINGAP.flag = true : LINGAP.flag = false;

                    btn.removeClass('no-click');
                }
            });
        }

        btn.addClass('no-click');

        function toggle_flag() {
            if (LINGAP.flag == false) { return '-220px'; }
            else { return '220px'; }
        }
    },

    back_first: function () {
        // LINGAP.$second_screen.addClass('hidden');
        // LINGAP.$first_screen.removeClass('hidden');
    },

    run: function () {
        var text1 = $('#lingapText1'), text2 = $('#lingapText2'), logo = $('#lingapLogo');
        var my_easing = [0, 0.96, 0.86, 0.99];

        text1.find('.vel-ref').velocity('transition.fadeIn', { stagger: 400 });
        text1.velocity({ 'translateX': ['30px', '0'] }, {
            easing: my_easing, duration: 500, delay: 3000,
            complete: function () {

                text1.find('span').velocity({ opacity: [0, 1] }, { easing: my_easing, delay: 3000 });
                logo.velocity('transition.flipYIn', { duration: 300 });

                text2.find('span').velocity('transition.fadeIn', {
                    stagger: 400, duration: 1000, delay: 4000, display: 'block',
                    complete: function () {
                        $(this).find('i.vel-ref').velocity('transition.fadeOut', {
                            stagger: 600, duration: 500, delay: 2000,
                            complete: function () {
                                $('.text2-d1').velocity({ 'translateY': ['22px', '0'] }, { easing: my_easing, duration: 1000 });
                                $('.text2-d2').velocity({ 'translateX': ['36px', '0'] }, { easing: my_easing, duration: 1000 });
                                $('.text3').velocity({ opacity: [1, 0] }, {
                                    display: 'block',
                                    easing: my_easing,
                                    duration: 500,
                                    delay: 2000,
                                    complete: function () {
                                        LINGAP.start_repeat_timer();
                                    }
                                });
                            }
                        });
                    }
                });
            }
        });
    },

    repeat: function () {
        $('#lingapLogo, #lingapText2, .text3').velocity({ opacity: [0, 1] }, {
            duration: 500, easing: [0, 0.96, 0.86, 0.99],
            complete: function () {
                $('#widgetLingap .vel-ref').attr('style', '');
                WIDGET_ROTATOR.run();
            }
        });
    },

    start_repeat_timer: function () {
        var timer = LINGAP.timer;
        window.clearTimeout(timer);
        timer = window.setTimeout(LINGAP.end, WIDGET_ROTATOR.repeat_interval);
    },

    end: function () {
        WIDGET_ROTATOR.will_repeat();
    },

    build: function () {
        // this.run();
        this.bind();
    }
}

var WIDGET_ROTATOR = {
    // ads: [LINGAP, CONNECTORSAD], // Uncomment this together with the html at _Layout.cshtml if lingap will be displayed 
    ads: [LINGAP,CONNECTORSAD], // Ad's object name
    rotator: $('#widgetRotatorParent'),
    title: $('#widgetTitle'),
    doc: $(document),
    interval: 1000,
    timerid: null,
    count: null,
    pointer: 0,
    per_ad_repeat: 3, // how many times the ad will repeat before it goes to the next
    ad_repeat_counter: 1, // plays the current displayed ad for the first time
    repeat_interval: 6000, // how long will each 

    bind: function () {
        this.doc.on('click', '#nextAd', WIDGET_ROTATOR.next_ad);
        this.doc.on('click', '#prevAd', WIDGET_ROTATOR.prev_ad);
    },
    init: function () {
        this.rotator.find('.widget-rotator-item').not(':eq(0)').hide();

        //Check how many items
        this.count = this.rotator.children('.widget-rotator-item').length;

        // Run first ad on the list
        WIDGET_ROTATOR.ads[0].run();
        WIDGET_ROTATOR.title.text(WIDGET_ROTATOR.ads[0].title);
    },
    navigate: function (direction) {
        if (direction == 'next') { WIDGET_ROTATOR.next_ad(); }
        else if (direction == 'prev') { WIDGET_ROTATOR.prev_ad(); }

        // Toggle visibility and animation of the ads
        WIDGET_ROTATOR.rotator.find('.widget-rotator-item').not(':eq(' + WIDGET_ROTATOR.pointer + ')').hide(0, function () {
            WIDGET_ROTATOR.stop_prev_ad();

            WIDGET_ROTATOR.rotator.find('.widget-rotator-item').eq(WIDGET_ROTATOR.pointer).show(0, function () {
                WIDGET_ROTATOR.ads[WIDGET_ROTATOR.pointer].run();
                WIDGET_ROTATOR.title.text(WIDGET_ROTATOR.ads[WIDGET_ROTATOR.pointer].title);
            });
        });
    },
    next_ad: function () {
        if (WIDGET_ROTATOR.pointer == WIDGET_ROTATOR.count - 1) { WIDGET_ROTATOR.pointer = 0; }
        else { WIDGET_ROTATOR.pointer = WIDGET_ROTATOR.pointer + 1; }
    },
    prev_ad: function () {
        if (WIDGET_ROTATOR.pointer == 0) { WIDGET_ROTATOR.pointer = WIDGET_ROTATOR.count - 1; }
        else { WIDGET_ROTATOR.pointer = WIDGET_ROTATOR.pointer - 1; }
    },
    stop_prev_ad: function () {
        $('.vel-ref, .velocity-animating').velocity('stop', true);
        $('.vel-ref').attr('style', '');
    },
    will_repeat: function () {
        if (WIDGET_ROTATOR.ad_repeat_counter < WIDGET_ROTATOR.per_ad_repeat) {
            WIDGET_ROTATOR.ad_repeat_counter = WIDGET_ROTATOR.ad_repeat_counter + 1;
            WIDGET_ROTATOR.ads[WIDGET_ROTATOR.pointer].repeat();
        }
        else {
            WIDGET_ROTATOR.ad_repeat_counter = 0;
            WIDGET_ROTATOR.navigate('next');
        }
    },
    run: function () {
        this.init();
        this.bind();
    }
}
// Ads Rotator Widget

//Splash Note
// var SPLASHNOTE = {
//     first_time: true,
//     init: function () {
//         var currentUser = $(".avatar-name").first().text();
//         var dateNow = new Date();

//         if (localStorage.getItem("splashCache") == null) {
//             SPLASHNOTE.set_cache(currentUser, dateNow);
//             setTimeout(function () { SPLASHNOTE.bind(); }, 1500);
//         }
//         else {
//             var cache = $.parseJSON(localStorage.getItem("splashCache"));
//             if (cache.User != currentUser || GetDaysBetween(dateNow, new Date(cache.DateLastSplash)) != 0) {
//                 SPLASHNOTE.set_cache(currentUser, dateNow);
//                 setTimeout(function () { SPLASHNOTE.bind(); }, 1500);
//             }
//         }
//     },
//     set_cache: function (currentUser, dateNow) {
//         var cacheObj = JSON.stringify({
//             "User": currentUser,
//             "DateLastSplash": dateNow
//         });
//         localStorage.setItem("splashCache", cacheObj);
//     },
//     bind: function () {
//         $(document).on('hide.bs.modal', '#splashNote', SPLASHNOTE.hide_arrow);
//     },
//     hide_arrow: function () {
//         $('.modal-backdrop').removeClass('display-arrow');
//         $('.splash-note-arrow').velocity('stop').remove();
//     },
//     show: function () {
//         $('#splashNote').modal({ show: true, keyboard: true, backdrop: true }).attr('tabIndex', -1).focus();
//         $('.modal-backdrop').addClass('display-arrow');
//         $('<i class="fa fa-arrow-left splash-note-arrow"></i>').insertAfter('.modal-backdrop').velocity({
//             translateX: '-25%'
//         },
//         {
//             loop: true, easing: 'linear', duration: 1000
//         });
//     },
//     build: function () {
//         // this.bind();
//         this.set_cache();
//         this.init();
//     }
// }

// Assorted Snippets for initializing minor UI interactions
var UI_HELPERS = {
    doc: $(document),
    run: function () {
        this.doc.on('click', '.nav-tabs-portfolio .dropdown-menu li', function () {
            $(this).removeClass('active');
            $(this).closest('li[role="presentation"]').addClass('active');
        });
        this.doc.on('click', '.dropdown-toggle-manual', function () {
            $(this).parent('.dropdown').addClass('open');
            $(this).next('.dropdown-menu-groups').after('<div class="modal-backdrop modal-backdrop-dropdown" style="background: transparent; z-index: 999;"></div>')
        });
        this.doc.on('click', '.modal-backdrop-dropdown', function () {
            $(this).remove();
            $('.dropdown').removeClass('open');
        });
        this.doc.on('click', '#profilePhotoSelection li a[data-toggle="tab"]', function () {
            $(this).next('.js_profilePhotoSelectionValue').click();
        });
        this.doc.on('click', '#btnViewGuidelines', function () {
            $('#containerGuidelines').toggle();
            $(this).find('span:first').toggle();
            $(this).find('span:last').toggle();
        });
        this.doc.on('mouseenter', '#displayWeatherPopoverBtn', function () {
            $('#weatherPopover').velocity({ opacity: 1 },
            {
                display: 'block',
                easing: [0, 0.96, 0.86, 0.99]
            });
        });
        this.doc.on('mouseleave', '#displayWeatherPopoverBtn', function () {
            $('#weatherPopover').velocity({ opacity: 0 },
            {
                display: 'none',
                easing: [0, 0.96, 0.86, 0.99]
            });
        });

        this.doc.on('click', '.js_btn-trks-info', function () {
            $('.popover-trks-info').velocity("fadeIn", {
                duration: 400,
                easing: [.99, .01, .33, 1]
            });
        });

        this.doc.on('click', '.js_popover-trks-close', function () {
            $('.popover-trks-info').velocity("fadeOut", {
                duration: 400,
                easing: [.99, .01, .33, 1]
            });
        });

        // Toggle User Credentials in Login Screen
        this.doc.on('click', '.js_switch-user', function () {
            $('.js_is-not-user').show();
            $('.js_is-confirm').hide();
        });

        $('[data-toggle="tooltip"]').tooltip();

        this.mask_password();
    },
    InitializeDraggableModal: function () {
        $('.js_draggable-widget').tinyDraggable({
            handle: '.js_modal-draggable-handle',
            exclude: '.btn-add, .close'
        });
    },
    mask_password: function () {
        $('#areaCodeSection').hide();
        //this.doc.find('.js_ConnectorsWidget_MobileNumber').simpleMask({
        //    'mask': ['###-###-####', '###-###-####'],
        //    'nextInput': false
        //});

        this.doc.on('click', '#contactTypeOptions li a', function (event) {
            event.preventDefault();
            $('#connectorsContactNumber').val('');
            $('#contactNumberText').html($(this).text());

            UI_HELPERS.doc.find('#areaCode').simpleMask({
                'mask': ['####', '####'],
                'nextInput': false
            });

            if ($(this).attr('href') === '#mobile') {
                UI_HELPERS.doc.find('#connectorsContactNumber').simpleMask({
                    'mask': ['###-###-####', '###-###-####'],
                    'nextInput': false
                });
                $('#countryCode').text('+63');
                $('#mobileNumberZero').show();
                $('#areaCodeSection').hide();
            }

            if ($(this).attr('href') === '#landline') {
                UI_HELPERS.doc.find('#connectorsContactNumber').simpleMask({
                    'mask': ['###-####', '###-####'],
                    'nextInput': false
                });
                $('#countryCode').text('+63');
                $('#mobileNumberZero').hide();
                $('#areaCodeSection').show();
            }
        });
    }
}
var HELP_UI = {
    doc: $(document),
    collapsed: false,

    bind: function () {
        this.doc.on('click', '[data-toggle="widget-feedback"]', function () {
            HELP_UI.toggle($(this));
        });

        this.doc.on('click', '.widget-feedback-apps li a', function () {
            $('[data-toggle="widget-feedback"]').click();
        });
    },
    toggle: function (elem) {
        elem.parent().toggleClass('expanded');
        // if (!elem.parent().hasClass('expanded')) {
        //     elem.parent().addClass('expanded');
        // }
        // else {
        //     elem.parent()
        //     .velocity({ translateY: '50px', opacity: 0 },{ easing: [0, 0.96, 0.86, 0.99], duration: 200, 
        //         complete: function() {
        //             $(this).removeClass('expanded').show().css('opacity','1');
        //         }
        //     })
        //     .velocity({ translateY: '0' },{ easing: [0, 0.96, 0.86, 0.99], duration: 300, 
        //         begin: function() { $(this).removeClass('expanded'); }
        //     });
        // }
    },
    build: function () {
        this.bind();
    }
}


// Ads Preview Modal
var VIEW_ADS = {
    doc: $(document),
    $previewAd: $('#previewAd'),
    $previewTitle: $('#previewAd .modal-title'),
    $previewBody: $('#previewAd .modal-body'),

    bind: function () {
        //this.doc.on('click', '.js_open-preview-modal', function() {
        //    VIEW_ADS.preview($(this));
        //});

        //this.doc.on('click', '.js_like', function () {
        //    var is_like = $(this).data("like");

        //    if (is_like == false) {
        //        $(this).find('i').addClass('md-favorite animated pulse').removeClass('md-favorite-outline');
        //        is_like = true;
        //    }
        //    else {
        //        is_like = false;
        //        $(this).find('i').addClass('md-favorite-outline').removeClass('md-favorite animated pulse');
        //    }
        //});

        //VIEW_ADS.$previewAd.on('hidden.bs.modal', VIEW_ADS.destroy);
    },

    preview: function (obj) {
        VIEW_ADS.$previewTitle.html(obj.find('.ad-title').text()); // Populate the modal-title
        obj.find('img').clone().appendTo(this.$previewBody); // Copy the image to the modal
        VIEW_ADS.$previewAd.modal({ show: true }); // Display the Modal
    },

    destroy: function () {
        VIEW_ADS.$previewTitle.empty();
        VIEW_ADS.$previewBody.empty();
    },

    relocate_all_modals: function () {
        var modal = $("body").find("div.modal");
        $.each(modal, function (index, item) {
            var modalDom = $(this).detach();
            modalDom.appendTo(".js_divModalContainer");
        });
    },

    build: function () {
        this.bind();
        this.relocate_all_modals();
    }
}

// LMVP
var LMVP = {
    doc: $(document),
    img_sb: $('sidebar-carousel'),
    modal_badge: $('.modal-lmvp-list'),
    modal_left: $('.lmvp-left-nav'),
    modal_right: $('.lmvp-left-nav'),
    t: true,

/*    play_sidebar: function () {
        LMVP.doc.find(LMVP.img_sb).owlCarousel({
            singleItem: true,
            autoPlay: true,
            goToFirst: true,
            goToFirstSpeed: 1000,
            pagination: false,
            transitionStyle: 'fade',
            rewindNav: true,
        });
    },*/

    play_modal: function () {
        LMVP.doc.find(LMVP.modal_badge).owlCarousel({
            items: 4,
            singleItem: false,
            navigation: true,
            autoPlay: false,
            pagination: false,
            rewindNav: true,
            scrollPerPage: true,
            lazyLoad: true,
            navigationText: ["<i class='md md-chevron-left md-4x'></i>","<i class='md md-chevron-right md-4x'></i>" ],
            beforeInit: function(){
                var c = $(".modal-lmvp-list .col");
                for(var i = 0; i < c.length; i+=3) {
                  c.slice(i, i+3).wrapAll("<div class='lmvp-modal-column'></div>");
                };
                $('.modal-lmvp-list .col').css({"clear":"both", "width":"100%"});              
            }
        });
    },
  
    toggle_modal: function () {
        if (LMVP.t == true) {
            LMVP.doc.find('#popLMVPModal').on('click', function () {
                LMVP.play_modal();
            });
            LMVP.t = false;
        }
        else {
            LMVP.doc.find('#closeLMVPModal').on('click', function () {
                LMVP.modal_badge.trigger('owl.stop');
            });
            LMVP.t = true;
        }
    },


    build: function () {
        LMVP.toggle_modal();
    }
}

//DAT
var DAT = {
    doc: $(document),
    isHidden: true,
    submit_dat: $('#datModalSubmit'),
    cancel_dat: $('#datModalCancel'),
    current_date: $('#datShiftDateCurrent'),
    edit_date: $('#datShiftDateEdit'),
    shift_dates: $('#modalShiftBtn'),
    apply_date: $('#datShiftDateApply'),
    leave_notif: $('#LeaveTypeNotificationModal'),
    isToday: true,  
    left_width: 0,
    right_width: 0,
    bind: function(){
/*        $('#datValidationPendingDocs, #datValidationInProgressDocs' ).change(function(){
            $('.dat-datepicker').toggle($(this).is(':checked'));
        });*/

        DAT.doc.find('#datWidenView').on('click', function(){
            DAT.toggle_widen();
        });
/*        DAT.submit_dat.on('click', function(){
            DAT.doc.find('.modal-backdrop').css("background-color","#808080");
            DAT.doc.find('#datApplicationModal').css("opacity", "0.2");
        });
        DAT.leave_notif.find(".btn").on('click', function(){
            DAT.doc.find('#datApplicationModal').css("opacity", "1").modal('hide');
        });
*/
        DAT.doc.find("#datCalendarFilterCancel").on('click', function(){
            $('#datDropdownMenu').removeClass('open');
        });

        DAT.doc.find("#datCalendarFilterApply").on('click', function(){
            $('#datDropdownMenu').removeClass('open');
        });

    },

    toggle_widen: function () {
        DAT.left_width = $('.sidebar-left-static').width();
        DAT.right_width = $('.sidebar-right-static').width() + 20;
        var to;
        if(DAT.isHidden== true) {
            DAT.doc.find('.sidebar-left-static').css({ "margin-left": -DAT.left_width  });
            //DAT.doc.find('.sidebar-left-static').velocity({translateX: - left_width},{
            //    duration: 1000,
            //    easing: [.99, .01, .33, 1],
            //    display: "none"
            //});
            DAT.doc.find('.sidebar-right-static').css({ "margin-right": -DAT.right_width });
            //DAT.doc.find('.sidebar-right-static').velocity({translateX: right_width},{
            //    duration: 1000,
            //    easing: [.99, .01, .33, 1],
            //    display: "none"
            //});
            clearTimeout(to);
            to = setTimeout(function(){
                DAT.doc.find('#retina-main-content').css({"margin-left":"0", "margin-right":"0"});
            }, 1000);           
            //DAT.doc.find('#datWidenView').html('<i class="md md-fullscreen-exit"></i> Revert View');
            DAT.isHidden = false;
        }
        else {
            DAT.doc.find('#retina-main-content').css({ "margin-left": DAT.left_width, "margin-right": DAT.right_width });
            DAT.doc.find('.sidebar-left-static').velocity("reverse",{
                duration: 1000,
                easing: [.99, .01, .33, 1],
                display: "block"
            });
            DAT.doc.find('.sidebar-right-static').velocity("reverse",{
                duration: 1000,
                easing: [.99, .01, .33, 1],
                display: "block"
            });
            //DAT.doc.find('#datWidenView').html('<i class="md md-fullscreen"></i> Fit to Screen');
            DAT.isHidden = true;
        }
    },
    reset: function () {
        DAT.doc.find('.sidebar-left-static').removeAttr('style');
        DAT.doc.find('.sidebar-right-static').removeAttr('style');
        DAT.doc.find('#retina-main-content').removeAttr('style');
    },
    //change_shift: function(){
    //    DAT.shift_dates.find("a").on('click', function() {
    //        $(this).addClass("active").siblings().removeClass("active");
    //        if ($(this).is("a:last-child") == true) {
    //            DAT.isToday = true;
    //        } else {
    //            DAT.isToday = false;
    //        };
    //    });
    //},


    //change_date: function(){
    //    DAT.current_date.find("#btnShiftPen").on('click', function(){
    //        DAT.doc.find(DAT.current_date).addClass('hidden');
    //        DAT.doc.find(DAT.edit_date).removeClass('hidden');
    //        /*(DAT.submit_dat).prop('disabled', true).addClass('disabled');*/
    //        DAT.edit_date.find("div a:last-child").html("Today");
    //        if (DAT.isToday == true) {
    //            DAT.edit_date.find("div a:last-child").addClass("active");
    //        } else {
    //            DAT.edit_date.find("div a:last-child").removeClass("active");
    //        }            
    //    });
    //}, 

    //apply_change: function(){
    //    var new_date = $('#modalShiftBtn a').html();
    //    DAT.edit_date.find("a").on('click', function(){
    //        DAT.doc.find(DAT.submit_dat, DAT.cancel_dat).prop('disabled', false).removeClass('disabled');
    //        DAT.doc.find(DAT.edit_date).addClass('hidden');
    //        DAT.doc.find(DAT.current_date).removeClass('hidden');
    //    });
    //},

    build: function(){
        //this.change_date();
        //this.change_shift();
       // this.apply_change();
        this.bind();
    }
}

// DOM Ready
$(document).ready(function () {
    VIEW_ADS.build();
    TOGGLE_SIDEBAR.build();
    // SPLASHNOTE.build();
    APP_SCROLL.build();
    INITIATIVE.build();
    LINGAP.build();
    DROPDOWN_MANUAL.build();
    WIDGET_ROTATOR.run();
    UI_HELPERS.run();
    LMVP.build();
    DAT.build();
    HELP_UI.build();
    // CSS Pointer events polyfill
    PointerEventsPolyfill.initialize({});
});

$(document).ajaxComplete(function (event, request, settings) {
    if (request.status === 401) { location.reload(); }
});

function ToasterMultilineMessage(title, message, toasterType) {
    var splitMessage = message.split("\n");
    var modMessage = "<ul>";
    modMessage += "<li class='h4'>" + title + "</li>";
    $.each(splitMessage, function (index, item) {
        modMessage += "<li>&nbsp&nbsp" + item + "</li>";
    });
    modMessage += "</ul>";

    toastr[toasterType](modMessage);
}

function GetDaysBetween(first, second) {
    // Copy date parts of the timestamps, discarding the time parts.
    var one = new Date(first.getFullYear(), first.getMonth(), first.getDate());
    var two = new Date(second.getFullYear(), second.getMonth(), second.getDate());

    // Do the math.
    var millisecondsPerDay = 1000 * 60 * 60 * 24;
    var millisBetween = two.getTime() - one.getTime();
    var days = millisBetween / millisecondsPerDay;

    // Round down.
    return Math.floor(days);
}