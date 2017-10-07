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
        loader.each(function () {
            if (loader.data('loader') == true) {
                $(this).append(loader_dom);
            }
            else {
                loader.find(loader_dom).remove();
            }
        });
    });

    $('*[data-toggle="tooltip"]').tooltip();

});



