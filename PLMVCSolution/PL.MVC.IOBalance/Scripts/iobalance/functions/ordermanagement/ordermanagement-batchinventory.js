var ORDERMANAGEMENTBATCHINVENTORY = (function () {
    var _variables = {
        params: null,
        loaderMsg: 'Please wait while we process your request',
        gvBatchSummaryHeight: 180,
        hasError: false,
        defaultValue: null,
        defaultSearchValue: null
    }

    var doShowTrackerLoader = function (msg) {
        $('body').showLoader({
            type: "page",
            message: msg
        });
    }

    var doRemoveTrackerLoader = function () {
        $('body').hideLoader();
    }

    var _doInitializeElements = function () {
        $('body').on('click', '#uploadinventory', function () {
            _doNewItem();
        });

        $('body').on('click', '#discardbutton', function () {
            _doDiscardItem();
        });

        $('body').on('click', '#searchdetails', function () {
            _doSearchItem();
        });

        $('body').on('click', '#refreshbutton', function () {
            _doSearchItem();
        });

        $('body').on('click', '#removefilter', function () {
            $('#frmSearchBatchInventory').deserialize(_variables.defaultSearchValue, true, null);
        });

    }

    var _doDiscardItem = function () {
        $('#divBatchSummary').show();
        $('#batch-upload-form').hide();

        _doClearItems();

        //buttons
        $('#searchbutton').show();
        $('#refreshbutton').show();
        $('#uploadinventory').show();

        $('#discardbutton').hide();

    }

    var _doClearItems = function ()
    {
        $('#frmUpload').deserialize(_variables.defaultValue, true, null);
    }

    var _doNewItem = function () {
        $('#divBatchSummary').hide();
        $('#batch-upload-form').show();

        //buttons
        $('#searchbutton').hide();
        $('#refreshbutton').hide();
        $('#uploadinventory').hide();

        $('#discardbutton').show();


    }

    var _doBindTblBatchSummary = function () {
        var $gv = $('#gvBatchSummary'), schemaField = {}, columns = [],
            width, field;

        $gv.kGridResizeHeight({
            height: _variables.gvBatchSummaryHeight,
            willRefreshGrid: true
        });
    }

    var _doSearchItem = function ()
    {
        var $gv = $("#gvBatchSummary").data('kendoGrid'), $frm = $('#frmSearchBatchInventory');

        doShowTrackerLoader(_variables.loaderMsg);

        $gv.dataSource.read($frm.serializeToJson());

        doRemoveTrackerLoader();
        $('#mdlsearchbatchinventory').modal('hide');
        INFRA.doRemoveOpenedModal();
    }

    //#region upload events
    var doOnUpload = function (e) {
        _variables.hasError = false;
        $('#spnUploadError').html('');
        e.data = _doSerializeFormUpload(true);
    };

    var _doSerializeFormUpload = function (isJson) {
        var $frmUpload = $('#frmUpload'), result;
        debugger;
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
            $('.k-filename').html('Successfully uploaded');
            //$(".k-upload.k-header").addClass("k-upload-empty");
            //$(".k-upload-button").removeClass("k-state-focused");

            
            doRemoveTrackerLoader();
        }

        _doSearchItem();

        
    };

    var doOnError = function (e) {
        debugger;
        var errorType, uploadErrorTypes,
            $spnUploadError = $('#spnUploadError'),
            response, $txtErrorType, errMsg;

        _variables.hasError = true;

        response = e.XMLHttpRequest.response;
        errMsg = e.response.messageResult;

        //$('#frmUpload').html(response);
        $spnUploadError.html(errMsg);
        $(".k-upload-status").remove();
        $('.k-filename').remove();
        $('.k-progress').remove();
        $('.k-upload-status').hide();

        //$txtErrorType = $('#txtErrorType');
        //errorType = $txtErrorType.data('error-type');
        //uploadErrorTypes = _variables.params.UploadErrorTypes;
        //errMsg = $txtErrorType.data('error-message');

        //if (errorType === uploadErrorTypes.InvalidFileType) {
        //    $spnUploadError.html(errMsg + 'Only excel file is allowed.');
        //}
        //else {
        //}


        doRemoveTrackerLoader();
    };
    //#endregion uploads

    var initialize = function (options) {
        try {
            _variables.params = options || {};
        } catch (ex) {
            if (typeof console !== "undefined" && console !== null) {
                console.error("Error parsing inline options", ex);
            }
        }


        _variables.defaultValue = $('#frmUpload').serialize();
        _variables.defaultSearchValue = $('#frmSearchBatchInventory').serialize();
        //_variables.defaultEditValue = $('#frmEditInventory').serialize();
        //_variables.defaultSearchValue = $('#frmSearchProduct').serialize();
        //_variables.defaultVoidQtyProduct = $('#frmVoidQtyProduct').serialize();
        _doBindTblBatchSummary();
        _doInitializeElements();
    }

    return {
        initialize: initialize,
        _variables: _variables,
        doOnUpload: doOnUpload,
        doOnSuccess: doOnSuccess,
        doOnError: doOnError
    }

})();

$(document).ready(function () {
    var options = window.batchInventoryOptions;
    ORDERMANAGEMENTBATCHINVENTORY.initialize(options);


    //$(window).resize(function () {
    //    $('#gvProduct').kGridResizeHeight({
    //        height: ORDERMANAGEMENTPURCHASEORDER._variables.gvProductHeight,
    //        willRefreshGrid: false,
    //        isManual: true
    //    });
    //});
});