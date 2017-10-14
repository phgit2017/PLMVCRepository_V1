var ORDERMANAGEMENTBATCHINVENTORY = (function () {
    var _variables =
    {
        gvHeight: 180,
        params: null
    };


    var _doInitializeElements = function () {
        $('body').on('click', '#downloadtemplatebutton', function () {
            window.open(_variables.params.templateUrl);
        });
    }

    var _doInitializeKendoElements = function () {
        
    }

    var doOnUpload = function (e) {
        _variables.hasError = false;
        $('#spnUploadError').html('');
        e.data = _doSerializeFormUpload(true);
    };

    var _doSerializeFormUpload = function (isJson) {
        var $frmUpload = $('#frmUpload'), result;
        if (isJson) {
            result = $frmUpload.serializeToJson();
        }
        else {
            result = $frmUpload.serialize();
        }

        return result;
    };

    var doOnSuccess = function (e) {
        
        var $uploaderContainer = $('.js_uploader-container'),
            $errorContainer = $('.js_upload-error-list');

        if (!e.response.result) {
            doOnError(e);
        } else {
            _variables.hasError = false;
            $('#spnUploadError').html('');

            $(".k-upload-status").remove();
            $('.k-filename').html(e.response.messageResult);

            
            window.open(_variables.params.exportUrl + '?' + 'dataResult=' + e.response.dataResult);
            //doRemoveTrackerLoader();
        }
    };

    var doOnError = function (e) {
        
        var errorType, uploadErrorTypes,
            $spnUploadError = $('#spnUploadError'),
            response, $txtErrorType, errMsg;

        _variables.hasError = true;

        response = e.XMLHttpRequest.response;
        errMsg = e.response.messageResult;

        
        $spnUploadError.html(errMsg);
        $(".k-upload-status").remove();
        $('.k-filename').remove();
        $('.k-progress').remove();
        $('.k-upload-status').hide();


        //doRemoveTrackerLoader();
    };

    var initialize = function (options) {
        try {
            _variables.params = options || {};
            _doInitializeElements();
            _doInitializeKendoElements();
            INFRA.doRemoveOpenedModal();
        }
        catch (e) {
            if (typeof console !== "undefined" && console !== null) {
                console.error("Error parsing inline options", ex);
            }
        }
    }

    return {
        initialize: initialize,
        _variables: _variables,
        doOnUpload: doOnUpload,
        doOnSuccess: doOnSuccess
    }
})();

$(document).ready(function () {

    var options = window.batchInventoryOptions;
    ORDERMANAGEMENTBATCHINVENTORY.initialize(options);


});