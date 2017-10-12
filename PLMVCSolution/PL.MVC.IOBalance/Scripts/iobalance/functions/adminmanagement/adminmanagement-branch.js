var ADMINMANAGEMENTBRANCH = (function () {
    var _variables =
    {
        params: null,
        gvBranchHeight: 200,
        loaderMsg: 'Please wait while we process your request',
        defaultValue: null,
        defaultEditValue: null,
        defaultSearchValue: null
    };


    var doShowTrackerLoader = function (msg) {
        $('body').showLoader({
            type: "page",
            message: msg
        });
    }

    var doRemoveTrackerLoader = function () {
        $('body').hideLoader();
    }

    var _doBindTblBranch = function () {
        var $gv = $('#gvBranch'), schemaField = {}, columns = [],
            width, field;

        $gv.kGridResizeHeight({
            height: _variables.gvBranchHeight,
            willRefreshGrid: false,
            isManual: true
        });
    }

    var _doSaveDetails = function (element) {
        var datas = $('#frmBranch').serialize(),
            elementUrl = element.data('url'),
            $form = $('#frmBranch');

        doShowTrackerLoader(_variables.loaderMsg);
        $.validator.unobtrusive.parse($form);
        $form.validate();
        if ($form.valid()) {
            $.ajax({
                url: elementUrl,
                type: 'POST',
                data: datas,
                success: function (data) {
                    doRemoveTrackerLoader();
                    $('#divResultContainer').show().html(data.alertMessage);
                    if (INFRA.ConvertToBoolean(data.isSuccess)) {
                        _doSearchDetails();
                        _doDiscardNewForm();
                    } else {

                    }

                }
            });

        } else {
            doRemoveTrackerLoader();
        }
    }

    var _doUpdateDetails = function (element) {
        var datas = $('#frmUpdateBranch').serialize(),
            elementUrl = element.data('url'),
            $form = $('#frmUpdateBranch');

        doShowTrackerLoader(_variables.loaderMsg);

        $.validator.unobtrusive.parse($form);
        $form.validate();

        if ($form.valid()) {
            $.ajax({
                url: elementUrl,
                type: 'POST',
                data: datas,
                success: function (data) {
                    doRemoveTrackerLoader();
                    $('#divResultContainer').show().html(data.alertMessage);

                    if (INFRA.ConvertToBoolean(data.isSuccess)) {
                        _doSearchDetails();

                        $('#mdleditbranch').modal('hide');
                        INFRA.doRemoveOpenedModal();
                    } else {

                    }
                    
                }
            });
        } else {
            doRemoveTrackerLoader();
        }

        
    }

    var _doSearchDetails = function () {
        var $gv = $("#gvBranch").data('kendoGrid'), $frmSearch = $('#frmSearchBranch');

        doShowTrackerLoader(_variables.loaderMsg);

        $gv.dataSource.read($frmSearch.serializeToJson());

        doRemoveTrackerLoader();
        $('#mdlsearchbranch').modal('hide');
        INFRA.doRemoveOpenedModal();
    }

    var _doRefreshDetails = function () {
        _doSearchDetails();
    }

    var _doShowNewForm = function () {
        $('#branch-new-form').show();
        $('#divBranch').hide();

        $('#searchbutton').hide();
        $('#refreshbutton').hide();
        $('#btnnewform').hide();

        $('#btndiscard').show();

    }

    var _doDiscardNewForm = function () {
        $('#branch-new-form').hide();
        $('#divBranch').show();

        $('#searchbutton').show();
        $('#refreshbutton').show();
        $('#btnnewform').show();

        $('#btndiscard').hide();

        //$('#frmBranch #BranchName').val('');
        //$('#frmBranch #BranchAddress').val('');
        //$('#frmBranch #BranchDetails').val('');
        $('#frmBranch').deserialize(_variables.defaultValue, true, null);
    }

    var _doInitializeElements = function () {
        $('body').on('click', '#savedetails', function () {
            var $elem = $(this);
            _doSaveDetails($elem);
        });

        $('body').on('click', '#updatedetails', function () {
            var $elem = $(this);
            _doUpdateDetails($elem);
        });

        $('body').on('click', '#searchdetails', function () {
            _doSearchDetails();
        });

        $('body').on('click', '[data-action-type="editbutton"]', function () {
            var $elem = $(this);
            $('[data-action-type="editbutton"]').removeClass('selected');
            $elem.addClass('selected');
            $('#mdleditbranch').modal('show');
        });

        $('body').on('show.bs.modal', '#mdleditbranch', function () {
            var $caller = $('[data-action-type="editbutton"].selected');
            $('#frmUpdateBranch #hdfBranchId').val($caller.data('branch-id'));
            $('#frmUpdateBranch #BranchName').val($caller.data('branch-name'));
            $('#frmUpdateBranch #BranchDetails').val($caller.data('branch-details'));
            $('#frmUpdateBranch #BranchAddress').val($caller.data('branch-address'));
        });

        $('#mdleditbranch').on('hidden.bs.modal', function () {
            $('#frmUpdateBranch').deserialize(_variables.defaultEditValue, true, null);
        });

        $('body').on('click', '#searchbutton', function () {
            var $elem = $(this);
            $('#mdlsearchbranch').modal('show');
        });

        $('body').on('click', '#refreshbutton', function () {
            _doRefreshDetails();
        });

        $('body').on('click', '#btnnewform', function () {
            _doShowNewForm();
        });

        $('body').on('click', '#btndiscard', function () {
            _doDiscardNewForm();
        });

        $('body').on('click', '#removefilter', function () {
            //$('#frmSearchBranch #BranchName').val('');
            $('#frmSearchBranch').deserialize(_variables.defaultSearchValue, true, null);
        });
    }

    var initialize = function (options) {
        try {
            _variables.params = options || {};
            _doInitializeElements();
            INFRA.doRemoveOpenedModal();
            _doBindTblBranch();
        } catch (ex) {
            if (typeof console !== "undefined" && console !== null) {
                console.error("Error parsing inline options", ex);
            }
        }

        _variables.defaultValue = $('#frmBranch').serialize();
        _variables.defaultEditValue = $('#frmUpdateBranch').serialize();
        _variables.defaultSearchValue = $('#frmSearchBranch').serialize();
    }



    return {
        initialize: initialize,
        _variables: _variables
    }
})();

$(document).ready(function () {
    var options = window.branchOptions;
    ADMINMANAGEMENTBRANCH.initialize(options);

    $(window).resize(function () {
        $('#gvBranch').kGridResizeHeight({
            height: ADMINMANAGEMENTBRANCH._variables.gvBranchHeight,
            willRefreshGrid: false,
            isManual: true
        });
    });
});