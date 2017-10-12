(function ($) {
    $.alertWindow = function (message, okFunction, cancelFunction) {

        var windowMessage, windowOkFunction, windowCancelFunction;
        var windowClass = 'alert-window', currentButton = "";


        if ((typeof (message) === "undefined" || message === "" || typeof (message) === "function" || message === null)) {
            console.log('Message is undefined');
            return;
        }

        if ((typeof (okFunction) === "undefined" || okFunction === "" || typeof (okFunction) !== "function" || okFunction === null)) {
            console.log('OK Function is undefined');
            okFunction = function () {
                //hideModal();
            };
        }

        if ((typeof (cancelFunction) === "undefined" || cancelFunction === "" || typeof (cancelFunction) !== "function" || cancelFunction === null)) {
            cancelFunction = function () {
                removeModal();
            };
        }

        windowMessage = message;

        windowOkFunction = function () {
            okFunction();
            removeModal();
        };

        windowCancelFunction = function () {
            cancelFunction();
            removeModal();
        };

        var window = '<div class="modal fade modal-confirm alert-window" tabindex="-1" role="dialog" aria-labelledby="alertWindow" aria-hidden="true">' +
                    '<div class="modal-dialog modal-sm">' +
                        '<div class="modal-content">' +
                            '<div class="modal-body">' +
                                '<i class="md md-help"></i>' +
                                '<h3 class="modal-confirm-message">' + windowMessage + '</h3>' +
                                '<button type="button" data-dismiss="modal" class="btn btn-default alert-window-cancel">Cancel</button>&nbsp;' +
                                '<button type="button" data-dismiss="modal" class="btn btn-default alert-window-ok" id="btnDeleteEmployer">Yes</button>' +
                            '</div>' +
                        '</div>' +
                    '</div>' +
                '</div>';

        $('body').find(windowClass).remove();
        $('body').append(window);

        $('.alert-window').modal('show');

        $('.alert-window').on('hide.bs.modal', function (e) {
            if (currentButton === "ok") {
                windowOkFunction();
            }
            else if (currentButton === "cancel") {
                windowCancelFunction();
            }
        })

        $('.alert-window-ok').click(function () {
            currentButton = "ok";
        });

        $('.alert-window-cancel').click(function () {
            currentButton = "cancel";
        });

        return this;
    };

    function removeModal() {
        $('.alert-window').remove();
    }
})(jQuery)


