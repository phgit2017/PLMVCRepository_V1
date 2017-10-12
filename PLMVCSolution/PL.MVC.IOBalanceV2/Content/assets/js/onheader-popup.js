; (function () {
    HEADER_POPUP = {
        doc: $(document),
        popup: $('#onheaderPopup'),

        easing: [0, 0.96, 0.86, 0.99],
        bind: function () {
            this.doc.on('click', '[data-dismiss="onheader-popup"]', HEADER_POPUP.destroy);
            this.doc.on('click', '[data-approve="onheader-popup"]', HEADER_POPUP.success);

            this.doc.find('#scrollContainer').marquee('scroll')
				.mouseover(function () { $(this).trigger('stop'); })
				.mouseout(function () { $(this).trigger('start'); })
				.mousemove(function (event) {
				    if ($(this).data('drag') == true) {
				        this.scrollLeft = $(this).data('scrollX') + ($(this).data('x') - event.clientX);
				    }
				})
				.mousedown(function (event) {
				    $(this).data('drag', true).data('x', event.clientX).data('scrollX', this.scrollLeft);
				})
				.mouseup(function () {
				    $(this).data('drag', false);
				});
        },
        slide: function () {
            HEADER_POPUP.popup.find('.icon').velocity({ scaleX: [1, 0], scaleY: [1, 0] }, { easing: 'spring', duration: 1000 });

            //$('.content').find('.scroll').velocity({ translateX: '-50%' }, { easing: HEADER_POPUP.easing, duration: 1000, delay: 6000 });
        },
        success: function () {
            text = 'Your time is valuable to us. Thank you for acknowledging this advisory.';

            HEADER_POPUP.popup.find('.content span')
				.velocity({ opacity: 0 }, {
				    easing: HEADER_POPUP.easing,
				    complete: function () {
				        $(this).text(text);
				    },
				    begin: displayIcon
				})
				.velocity({ opacity: 1 }, { easing: HEADER_POPUP.easing });

            function displayIcon() {
                $('.scroll .text').detach().appendTo('.scroll');
                $('.scroll').removeAttr('id');
                HEADER_POPUP.popup.find('.icon')
					.velocity({ scaleX: 0, scaleY: 0 }, {
					    easing: HEADER_POPUP.easing,
					    complete: function () {
					        $(this).removeClass('md-globe-lock').addClass('md-check-circle');
					    }
					})
					.velocity({ scaleX: 1, scaleY: 1 }, { easing: 'spring', duration: 800 });

                HEADER_POPUP.popup.addClass('success');

                $('#scrollContainer').remove();
                $('[data-approve="onheader-popup"]').hide();
            }

            APP_SCROLL.set_height($("[data-sidebar-tab-scroll]"));
        },
        destroy: function () {
            HEADER_POPUP.popup.find('.content .text').velocity('stop');
            $('body').removeClass('has-reminder-policy');
            HEADER_POPUP.popup.remove();

            APP_SCROLL.set_height($("[data-sidebar-tab-scroll]"));
        },
        build: function () {
            this.slide();
            this.bind();
        }
    }

    $(document).ready(function () {
        HEADER_POPUP.build();
    });
})();