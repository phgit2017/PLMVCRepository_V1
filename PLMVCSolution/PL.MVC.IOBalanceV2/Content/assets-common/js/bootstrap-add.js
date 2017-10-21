$(function () {

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


    // Build - Show and Hide Loader
    var loader_dom = '<div class="loader-mask"><div class="loader-container"><div class="loader-cell"><i class="md md-2x md-spin md-settings"></i></div></div></div>';
    var loader = $('*[data-loader]');

    $(document).ready(function () {
        $('.loader-mask').hide();
    });

    var windowPath = window.location.pathname,
        activeLink = $('#mainNavbar').find('a[href="' + windowPath + '"]'),
        subMenu = $('.sub-menu');
    activeLink.closest('li').addClass('active');
    for (var i = 0; i < subMenu.length; i++) {
        var hasActive = $(subMenu[i]).find('.active').length ? true : false;
        if (hasActive) {
            $(subMenu[i]).addClass('in').removeClass('collapse');
        }
    }
    $('#mainNavbar').mCustomScrollbar({
        autoHideScrollbar: false,
        axis: 'y',
        contentTouchScroll: true,
        scrollbarPosition: 'inside',
        scrollInertia: 500,
        scrollButtons: { enable: false },
        theme: 'light',
        live: true,
        liveSelector: '[custom-scroll]',
        advanced: { updateOnContentResize: true },
    });
    $('.navbar-toggle').on('click', function () {
        var mainNavbar = $('#mainNavbar');

        if (mainNavbar.hasClass('showed')) {
            mainNavbar.removeClass('showed');
            mainNavbar.velocity({
                translateX: '-100%'
            })
        }
        else {
            mainNavbar.addClass('showed');
            mainNavbar.velocity({
                translateX: '0'
            })
        }
    });

    var resizeId;
    $(window).resize(function () {
        clearTimeout(resizeId);
        resizeId = setTimeout(doneResizing, 500);
    });
    function doneResizing() {
        var win = $(this);
        var mainNavbar = $('#mainNavbar');
        if (win.width() <= 768) {
            mainNavbar.removeClass('showed');
            mainNavbar.velocity({
                translateX: '-100%'
            });
        }
        else {
            mainNavbar.removeClass('showed');
            mainNavbar.velocity({
                translateX: '0'
            })
        }
    }
    doneResizing();
});



