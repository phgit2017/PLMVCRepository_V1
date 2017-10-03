//* put data-target="read-more" to the parent of the element 
//* put a class of "read-more" to the element
//* these are the options for maximum character and height:
//	1. read-more-max-char="number"
//	2. read-more-height="number"
//  3. read-more-modal="true|false"
//		
// NOTE: for AngularJS, add to the project the directive for read-more and put an attribute read-more to the element.



(function ($) {
    $.fn.readmore = function () {

        var element = $(this),
            readMoreEl = $(this).find('.read-more'),
            default_max_char = 1000,
            max_char = parseInt($(this).attr('read-more-max-char')),
            max_height = parseInt($(this).attr('read-more-height')),
            readMoreParent,
            readMoreChar = readMoreEl.html().length,
            readMoreHeight = readMoreEl.outerHeight(),
            noHeight,
            trigger = "<div class='trigger-read-more'></div>",
            activateClass = "read-more-activate",
            elementClass = "read-more-element",
            readmore = {};

        if (_isNullOrEmpty(max_char)) {
            max_char = default_max_char;
        }
        else {
            max_char = max_char;
        }

        element.removeClass('in').find('.trigger-read-more').remove();

        if (_isNullOrEmpty(max_height)) {
            max_height = 0;
            noHeight = true;
        }
        else {
            max_height = max_height;
            noHeight = false;
        }
        if (readMoreHeight >= max_height || readMoreChar >= max_char) {
            element.addClass(activateClass + " " + elementClass).append(trigger);
        }
        else {
            element.removeClass(activateClass).find('.trigger-read-more').remove();
        }

        return this;
    }


})(jQuery);
$(document).on('click', '.read-more-activate[read-more-modal!="true"]', function () {
    if ($(this).hasClass('in')) {
        $(this).removeClass('in');
    }
    else {
        $(this).addClass('in');
    }
});
