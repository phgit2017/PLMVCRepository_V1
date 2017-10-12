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

    for (var i = 0; i < subMenu.length; i++) {
       var hasActive = $(subMenu[i]).find('.active').length ? true : false;
        if (hasActive) {
            $(subMenu[i]).addClass('in').removeClass('collapse');
        }
    }

});



