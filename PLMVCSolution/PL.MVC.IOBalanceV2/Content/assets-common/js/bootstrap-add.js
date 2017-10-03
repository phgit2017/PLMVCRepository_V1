$(function () {
    // Give the chat widget a customizeable window height using data-chat-height attribute
    $('[data-element="chat-card"]').css({ height: $('[data-element="chat-card"]').data('chat-height') });

    // Custom Collapse for Submenus
    $('*[data-toggle="accordion-menu"]').each(function () {
        var $this = $(this);

        $this.on('click', function () {
            var item = $this.closest('*[data-item="accordion-menu"]'),
				parent = '*[data-role="accordion-menu"]';

            if (item.hasClass('open')) {
                return false;
            }
            else {
                item.find('.accordion-menu-content').show(0, function () {
                    item.addClass('open');
                });
                item.siblings().removeClass('open');
                item.siblings().find('.accordion-menu-content').hide();
            }

        });
    });
    //$(document).on('focus', '.dropdown-search input', function () {
    //    $('.dropdown-search').removeClass('open');
    //    $(this).parent('.dropdown-search').addClass('open');
    //});

    //$(document).on('click', '#closeDrodownSearch', function () {
    //    $(this).parent('.dropdown-search').removeClass('open');
    //});
    //$('.dropdown-search').find('input').on('focus', function () {
    //    $('.dropdown-search').removeClass('open');
    //    $(this).parent('.dropdown-search').addClass('open');
    //});

    // Build - Show and Hide Loader
    var loader_dom = '<div class="loader-mask"><div class="loader-container"><div class="loader-cell"><i class="md md-2x md-spin md-settings"></i></div></div></div>';
    var loader = $('*[data-loader]');

    $(document).ready(function () {
        loader.each(function () {
            if (loader.data('loader') == true) {
                $(this).append(loader_dom);
            }
            else {
                loader.find(loader_dom).remove();
            }
        });
    });
});

//#region Common Functions
function _isNull(obj) {
    return obj === null || typeof obj == "undefined";
}

function _isEmpty(str) {
    var result = false;

    if (typeof str === "string") {
        str = str.trim();
    }

    if (str === "" || str.length === 0) {
        result = true;
    }

    return result;
}

function _isNullOrEmpty(obj) {
    return _isNull(obj) || _isEmpty(obj) || _isZeroLength(obj) || _isEmptyObject(obj);
}
function _isZeroLength(obj) {
    if (typeof obj === "function") {
        return false;
    }

    return obj.length === 0;
}

function _isEmptyObject(obj) {
    return typeof obj === "object" && Object.prototype.toString.call(obj) !== "[object Date]" && Object.keys(obj).length === 0;
}

function _isString(obj, willCheckIfEmpty) {
    var isString = typeof obj === "string";

    if (isString && willCheckIfEmpty) {
        return !_isNullOrEmpty(obj);
    }

    return isString;
}

//#endregion Common Functions

var STICKY_NAVBAR = {
    doc: $(document),
    docWidth: $(document).width(),
    profile_info: $('.profile-info'),  // comet blue banner
    trigger: $('.profile-info').outerHeight(),
    header: $('.navbar-fixed-top'), // main navbar
    offset_top: $('.navbar-fixed-top').height(),
    buttons_height: $('.navbar-controls').outerHeight(), // comet 2ndary navbar
    nav_dashboard: $('.nav-dashboard'), // comet 3rd navbar
    is_sticky: false,
    bind: function () {
        $(window).scroll(STICKY_NAVBAR.check_scroll);
    },
    check_scroll: function () {
        STICKY_NAVBAR.trigger = $('.profile-info').outerHeight();
        if (STICKY_NAVBAR.docWidth > 991 && ($(window).scrollTop() > STICKY_NAVBAR.trigger)) {
            STICKY_NAVBAR.add_sticky();
        }
        else if (STICKY_NAVBAR.docWidth > 991 && ($(window).scrollTop() < STICKY_NAVBAR.trigger)) {
            STICKY_NAVBAR.remove_sticky();
        }
        else { return false; }
    },
    add_sticky: function () {
        STICKY_NAVBAR.header = $('.navbar-fixed-top');
        STICKY_NAVBAR.buttons_height = $('.navbar-controls').outerHeight();
        STICKY_NAVBAR.is_sticky = true;

        STICKY_NAVBAR.header.next().css({ 'margin-top': (STICKY_NAVBAR.buttons_height * 2) });
        this.doc.find('.navbar-controls').addClass('navbar-sticky').css('top', STICKY_NAVBAR.offset_top);
        this.doc.find('.nav-dashboard').addClass('navbar-sticky').css('top', STICKY_NAVBAR.offset_top + STICKY_NAVBAR.buttons_height);
    },
    remove_sticky: function () {
        STICKY_NAVBAR.is_sticky = true;
        this.header.next().removeAttr('style');
        this.doc.find('.navbar-controls').removeClass('navbar-sticky').removeAttr('style');
        this.doc.find('.nav-dashboard').removeClass('navbar-sticky').removeAttr('style');
    },
    build: function () {
        this.bind();
    }
}

var SCROLL_TOP = {
    doc: $(document),
    offset: 500,
    btn: $('[data-target="scroll-top"]'),
    bind: function () {
        $(window).scroll(SCROLL_TOP.check_scroll);
        this.doc.on('click', '[data-target="scroll-top"]', SCROLL_TOP.scroll_to_top);
    },
    scroll_to_top: function () {
        //STICKY_BAR.destroyStickyBar();
        $('html').velocity("scroll",
	    {
	        translateZ: 0, // Force Hardware Acceleration
	        easing: [.74, .01, .46, .99],
	        offset: 0,
	        queue: false
	    });
    },
    check_scroll: function () {
        if ($(window).scrollTop() > SCROLL_TOP.offset) {
            SCROLL_TOP.btn.addClass('in')
        }
        else {
            SCROLL_TOP.btn.removeClass('in')
        }
        // $(window).scrollTop() > offset ? SCROLL_TOP.btn.addClass('in') : SCROLL_TOP.btn.removeClass('in');
    },
    build: function () {
        this.bind();
    }
}

// Custom dropdown that will not close when contents are clicked
var DROPDOWN_MANUAL = {
    obj: [],
    doc: $(document),
    bind: function () {
        var hastooltip = false;
      
      
        this.doc.on('click', '[data-toggle="dropdown-custom"]', function (e) {
            e.preventDefault();
            if ($(this).attr('data-toggle', 'tooltip')) {
                $(this).tooltip('hide');
            }

            DROPDOWN_MANUAL.toggle_menu($(this));
        });

        this.doc.on('click', 'html', function (event) {
            var dd = $(event.target).closest('.dropdown');
            if (dd.length == 0) {
                $('.dropdown').removeClass('open');
                DROPDOWN_MANUAL.obj = [];
            }
        });
    },
    toggle_menu: function (elem) {
        var dd = elem.parent('.dropdown');
        $('[data-toggle="dropdown-custom"]').not(elem).parent('.dropdown').removeClass('open'); // avoid multiple view of dropdown
        if (dd.hasClass('open')) {
            dd.removeClass('open'); // enable to hide the dropdown upon click on its trigger
        }
        else {
            dd.addClass('open');
        }
    },
    build: function () {
        this.bind();
    }
}
var STICKY_BAR = {
    //* put data-target="sticky-bar" to element you want to stick
    //* these are the options for this plugin:
    //	1. what are the other elements you want to adjust when sticky-bar is triggered:
    //		adjust-sibling="#element || .element" 
    //      NOTE: In using .element, be sure that it has no similar class to other elements
    //	2. what direction you want to adjust the adjust-sibling:
    //		adjust-direction="left || right"
    //		default value is left
    // NOTE: for AngularJS, add to the project the directive for sticky-bar put an attribute sticky-bar to the element who caused the element to have a height.
    // NOTE: this doesn't auto calculate the coordinates of the sticky bar, instead it adds/removes a class that later you will add a necessary CSS for it to stick.
    elem_allowance: 120,
    elem: $('*[data-target="sticky-bar"'),
    elem_height: 0,
    elem_width: 0,
    elem_top: 0, // get offset top of the element
    elem_bottom: 0,  // get offset bottom of the element
    stick_top: 'stick-top', // class that will be added once offset top is reached
    stick_bottom: 'stick-bottom', // class that will be added once offset bottom is reached
    adjust_sibling: '',
    adjust_direction: '',
    windowHeight: $(window).height(),
    window_height: 0,
    isWithinViewport: false, // if the height of the element is exact on the size of the screen ( this is not about if the element is below the fold)
    scrollTimeout: null,
    //scrollTop: 0,
    bind: function () {
        STICKY_BAR.elem = $('*[data-target="sticky-bar"'),
        STICKY_BAR.elem_height = STICKY_BAR.elem.outerHeight(),
        STICKY_BAR.elem_width = STICKY_BAR.elem.outerWidth(),
        STICKY_BAR.elem_top = STICKY_BAR.elem.offset().top,
        STICKY_BAR.elem_bottom = (STICKY_BAR.elem_top + STICKY_BAR.elem_height + 200), // 200 for some allowance so the it will not activate if the sticky bar is equal or close to equal with the viewport
        STICKY_BAR.adjust_sibling = $(STICKY_BAR.elem.attr('adjust-sibling')),
        STICKY_BAR.adjust_direction = STICKY_BAR.elem.attr('adjust-direction'),
        STICKY_BAR.window_height = $(window).height() - STICKY_BAR.elem_allowance,
        STICKY_BAR.isWithinViewport = STICKY_BAR.window_height >= (STICKY_BAR.elem_height + 30);   // 30 for some allowance for the margins


        $(window).on('scroll', STICKY_BAR.scrollHandler);
        //if (STICKY_BAR.scrollTimeout) {
        //    // clear the timeout, if one is pending
        //    clearTimeout(STICKY_BAR.scrollTimeout);
        //    STICKY_BAR.scrollTimeout = null;
        //}
        //STICKY_BAR.scrollTimeout = setTimeout(STICKY_BAR.scrollHandler, 50);


    },

    scrollHandler: function () {
        scrollTop = $(window).scrollTop() + STICKY_BAR.elem_allowance;
        // conditions
        reachedTopLimit = scrollTop > STICKY_BAR.elem_top;
        reachedBottomLimit = scrollTop > STICKY_BAR.elem_bottom;
        // within viewport
        if (STICKY_BAR.isWithinViewport) {
            STICKY_BAR.reachedLimitChecker(reachedTopLimit, STICKY_BAR.stick_top);
        }
            // not on viewport
        else if (!STICKY_BAR.isWithinViewport) {
            STICKY_BAR.reachedLimitChecker(reachedBottomLimit, STICKY_BAR.stick_bottom);
        }
        else {
            alert('Error calculating the screen, please refresh your page...');
        }
    },
    reachedLimitChecker: function (condition, className) {
        if (condition) {
            STICKY_BAR.addStickyClass(className);
        }
        else {
            STICKY_BAR.removeStickyClass(className);
        }
    },
    addStickyClass: function (className) {
        STICKY_BAR.elem.addClass(className);
        STICKY_BAR.adjustSiblingMargin(STICKY_BAR.adjust_sibling);
    },
    removeStickyClass: function (className) {
        STICKY_BAR.elem.removeClass(className);
        STICKY_BAR.adjust_sibling.removeAttr('style');
    },
    adjustSiblingMargin: function (sibling) {
        if (!(sibling === null || typeof sibling == "undefined")) {
            if (STICKY_BAR.adjust_direction === null || typeof STICKY_BAR.adjust_direction == "undefined" || STICKY_BAR.adjust_direction == "left") {
                return sibling.css({ 'margin-left': STICKY_BAR.elem_width });
            }
            else {
                return sibling.css({ 'margin-right': STICKY_BAR.elem_width });
            }
        }
        else {
            return null;
        }
    },
    destroyStickyBar: function () {
        STICKY_BAR.removeStickyClass(STICKY_BAR.stick_top + " " + STICKY_BAR.stick_bottom);
    },
    build: function () {
        STICKY_BAR.bind();
    }
}
var FLEXI_FONT = {
    doc: $(document),
    element: $('.risk-details .cell-item'),

    init: function () {
        FLEXI_FONT.element = $('.risk-details .cell-item');
    },
    bind: function () {
        element = FLEXI_FONT.element;

        //console.log(element.html().length);

        for (i = 0; i < element.length; i++) {
            var $current_element = $(element[i]);
            console.log("the HTML is" + $current_element.html() + " | characters are:" + $current_element.html().length);

            var elemMaxChar = 30,
            elemMaxChar2 = 50,
            elemCharTxt = $current_element.html(),
            elemCharCount = elemCharTxt.length,
            isMoreThan = elemCharCount > elemMaxChar,
            isOkTho = (elemCharCount <= elemMaxChar2) && (elemCharCount >= (elemMaxChar2 / 2));

            function smFont() {
                $current_element.addClass('font-xs');
            }
            function mdFont() {
                $current_element.addClass('font-md');
            }
            function lgFont() {
                $current_element.addClass('font-lg');
            }

            if (isMoreThan && !isOkTho) {
                smFont();
            }
            else if (isOkTho) {
                mdFont();
            }
            else {
                lgFont();
            }
        }
    },
    build: function () {
        FLEXI_FONT.bind();
    }
}
var PLACEHOLDER = {
    remove: function () {
        $('*[data-placeholder="placeholder"]').remove();
    }
}

//var STICKY_LIST_CONTROLS = {
//    doc: $(document),
//    controls: $('[data-sticky]'),
//    offset: 0,
//    scrollOffset: 0,
//    scrollTimer: null,
//    init: function () {
//        this.offset =
//			STICKY_LIST_CONTROLS.controls.offset().top
//			+ 1
//			- (STICKY_LIST_CONTROLS.doc.find('#subnavbar').height()
//			+ STICKY_LIST_CONTROLS.doc.find('header').height());

//        this.scrollOffset = STICKY_LIST_CONTROLS.controls.offset().top + STICKY_LIST_CONTROLS.controls.height() - 16;
//    },
//    bind: function () {
//        this.init();
//        this.doc.find('body').scrollspy({
//            target: '[data-sticky]',
//            offset: STICKY_LIST_CONTROLS.controls.offset().top + STICKY_LIST_CONTROLS.controls.height()
//        });

//        $(window).scroll(function () {
//            if (STICKY_LIST_CONTROLS.scrollTimer) { clearTimeout(STICKY_LIST_CONTROLS.scrollTimer); } // clear any previous pending timer 
//            STICKY_LIST_CONTROLS.scrollTimer = setTimeout(STICKY_LIST_CONTROLS.handle_scroll, 16);   // set new timer
//        });


//        this.doc.on('click', '[data-sticky] .nav li a', function () {
//            STICKY_LIST_CONTROLS.scroll_to($(this).data('target'));
//        });
//    },
//    handle_scroll: function () {
//        STICKY_LIST_CONTROLS.scrollTimer = null;
//        var pTop = STICKY_LIST_CONTROLS.controls.height() + 15;
//        var fcHeight = STICKY_LIST_CONTROLS.offset;
//        var flag = false;
//        var ScrollTop = $(window).scrollTop();

//        if (ScrollTop > fcHeight) {
//            flag = true;
//            if (flag == true) {
//                STICKY_LIST_CONTROLS.controls.addClass('list-controls-fixed');
//                STICKY_LIST_CONTROLS.controls.next().css('paddingTop', pTop);
//            }
//        } else {
//            flag = false;
//            STICKY_LIST_CONTROLS.controls.removeClass('list-controls-fixed');
//            STICKY_LIST_CONTROLS.controls.next().removeAttr('style');
//        }
//    },
//    scroll_to: function (target) {
//        $(target).velocity('scroll', { duration: 1000, easing: [.74, .36, .36, .85], offset: -(STICKY_LIST_CONTROLS.scrollOffset) });
//    },
//    build: function () {
//        if (STICKY_LIST_CONTROLS.controls.length) { this.bind(); }
//    }
//}


// Pointer-events none polyfill
function PointerEventsPolyfill(t) { if (this.options = { selector: "*", mouseEvents: ["click", "dblclick", "mousedown", "mouseup"], usePolyfillIf: function () { if ("Microsoft Internet Explorer" == navigator.appName) { var t = navigator.userAgent; if (null != t.match(/MSIE ([0-9]{1,}[\.0-9]{0,})/)) { var e = parseFloat(RegExp.$1); if (11 > e) return !0 } } return !1 } }, t) { var e = this; $.each(t, function (t, n) { e.options[t] = n }) } this.options.usePolyfillIf() && this.register_mouse_events() } PointerEventsPolyfill.initialize = function (t) { return null == PointerEventsPolyfill.singleton && (PointerEventsPolyfill.singleton = new PointerEventsPolyfill(t)), PointerEventsPolyfill.singleton }, PointerEventsPolyfill.prototype.register_mouse_events = function () { $(document).on(this.options.mouseEvents.join(" "), this.options.selector, function (t) { if ("none" == $(this).css("pointer-events")) { var e = $(this).css("display"); $(this).css("display", "none"); var n = document.elementFromPoint(t.clientX, t.clientY); return e ? $(this).css("display", e) : $(this).css("display", ""), t.target = n, $(n).trigger(t), !1 } return !0 }) };

// jQuery tinyDraggable v1.0.2
// https://github.com/Pixabay/jQuery-tinyDraggable
!function (e) { e.fn.tinyDraggable = function (n) { var t = e.extend({ handle: 0, exclude: 0 }, n); return this.each(function () { var n, o, u = e(this), a = t.handle ? e(t.handle, u) : u; a.on({ mousedown: function (a) { if (!t.exclude || !~e.inArray(a.target, e(t.exclude, u))) { a.preventDefault(); var f = u.offset(); n = a.pageX - f.left, o = a.pageY - f.top, e(document).on("mousemove.drag", function (e) { u.offset({ top: e.pageY - o, left: e.pageX - n }) }) } }, mouseup: function () { e(document).off("mousemove.drag") } }) }) } }(jQuery);





;(function ($) {
    /* Methods:
     * show
     * hide
     */
    $.fn.popupConfirm = function (action) {

        var $body = $('body'),
            $bodyPopupClass = 'popup-confirm-open';

        if (action === "show") {
            show();
        }

        if (action === "hide") {
            hide();
        }

        function show(){
            $body.addClass($bodyPopupClass);
        }
        function hide() {
            $body.removeClass($bodyPopupClass);
        }
    };

}(jQuery));