var ADMINMANAGEMENTDISCOUNT = (function () {
    var _variables =
    {
        params: null,
        gvDiscountHeight: 200,
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

    var _doBindTblDiscount = function () {
        var $gv = $('#gvDiscount'), schemaField = {}, columns = [],
            width, field;

        $gv.kGridResizeHeight({
            height: _variables.gvDiscountHeight,
            willRefreshGrid: false,
            isManual: true
        });
    }

    var _doSaveDetails = function (element) {
        var datas = $('#frmDiscount').serialize(),
            elementUrl = _variables.params.saveUrl,
            $form = $('#frmDiscount'),
            isDataReady = true,
            $BranchID = $('#frmDiscount #BranchID'), $DiscountPercentage = $('#frmDiscount #DiscountPercentage');

        doShowTrackerLoader(_variables.loaderMsg);
        $.validator.unobtrusive.parse($form);
        $form.validate();
        if ($form.valid()) {


            if (INFRA.IsNullOrEmpty($BranchID.val())) {
                isDataReady = false;
                $('#frmDiscount [data-valmsg-for="BranchID"]').html("<br/>Branch field is a required field");
            } else {
                isDataReady = true;
                $('#frmDiscount [data-valmsg-for="BranchID"]').html("");
            }

            if (INFRA.ConvertToBoolean(isDataReady)) {
                if (!INFRA.IsNullOrEmpty($DiscountPercentage.val())) {
                    if (INFRA.ConvertToInteger($DiscountPercentage.val()) <= 0) {
                        isDataReady = false;
                        $('#frmDiscount [data-valmsg-for="DiscountPercentage"]').html("<br/>Discount should not be less than or equal to 0");
                    } else {
                        isDataReady = true;
                        $('#frmDiscount [data-valmsg-for="DiscountPercentage"]').html("");
                    }
                }
            }


            if (INFRA.ConvertToBoolean(isDataReady)) {
                $.ajax({
                    url: elementUrl,
                    type: 'POST',
                    data: datas,
                    success: function (data) {
                        doRemoveTrackerLoader();
                        $('#divResultContainer').show().html(data.alertMessage);
                        if (INFRA.ConvertToBoolean(data.isSuccess)) {
                            _doSearchItem();
                            _doDiscardNewForm();
                        } else {

                        }

                    }
                });
            } else {
                doRemoveTrackerLoader();
            }
        } else {
            doRemoveTrackerLoader();
        }
    }

    var _doUpdateDetails = function () {
        var datas = $('#frmEditDiscount').serialize(),
            elementUrl = _variables.params.updateUrl,
            $form = $('#frmEditDiscount'),
            isDataReady = true,
            $BranchID = $('#frmEditDiscount #EditBranchID'), $DiscountPercentage = $('#frmEditDiscount #EditDiscountPercentage');

        doShowTrackerLoader(_variables.loaderMsg);
        $.validator.unobtrusive.parse($form);
        $form.validate();
        if ($form.valid()) {
            if (INFRA.IsNullOrEmpty($BranchID.val())) {
                isDataReady = false;
                $('#frmEditDiscount [data-valmsg-for="BranchID"]').html("<br/>Branch field is a required field");
            } else {
                isDataReady = true;
                $('#frmEditDiscount [data-valmsg-for="BranchID"]').html("");
            }

            if (INFRA.ConvertToBoolean(isDataReady)) {
                if (!INFRA.IsNullOrEmpty($DiscountPercentage.val())) {
                    if (INFRA.ConvertToInteger($DiscountPercentage.val()) <= 0) {
                        isDataReady = false;
                        $('#frmEditDiscount [data-valmsg-for="DiscountPercentage"]').html("<br/>Discount should not be less than or equal to 0");
                    } else {
                        isDataReady = true;
                        $('#frmEditDiscount [data-valmsg-for="DiscountPercentage"]').html("");
                    }
                } else {
                    isDataReady = false;
                    $('#frmEditDiscount [data-valmsg-for="DiscountPercentage"]').html("<br/>The Discount Percentage is a requried field.");
                }
            }

            if (INFRA.ConvertToBoolean(isDataReady)) {
                $.ajax({
                    url: elementUrl,
                    type: 'POST',
                    data: datas,
                    success: function (data) {
                        doRemoveTrackerLoader();
                        $('#divResultContainer').show().html(data.alertMessage);
                        if (INFRA.ConvertToBoolean(data.isSuccess)) {
                            _doSearchItem();
                            $('#frmEditDiscount').deserialize(_variables.defaultEditValue, true, null);
                            $('#mdleditdiscount').modal('hide');
                            INFRA.doRemoveOpenedModal();

                        } else {

                        }

                    }
                });
            } else {
                doRemoveTrackerLoader();
            }
        } else {
            doRemoveTrackerLoader();
        }
    }

    var _doUpdateActive = function (discountId, branchId) {

        $.ajax({
            url: _variables.params.deleteUrl,
            type: 'POST',
            data: { discountId: INFRA.ConvertToInteger(discountId), branchId: INFRA.ConvertToInteger(branchId) },
            success: function (data) {
                $('#divResultContainer').show().html(data.alertMessage);
                if (INFRA.ConvertToBoolean(data.isSuccess)) {
                    _doSearchItem();
                }
            }
        });
    }

    var _doSearchItem = function () {
        var $gv = $("#gvDiscount").data('kendoGrid'), $frm = $('#frmSearchDiscount');

        doShowTrackerLoader(_variables.loaderMsg);

        $gv.dataSource.read($frm.serializeToJson());

        doRemoveTrackerLoader();
        $('#mdlsearchdiscount').modal('hide');
        INFRA.doRemoveOpenedModal();
    }

    var _doDiscardNewForm = function () {
        $('#discount-new-form').hide();
        $('#divDiscount').show();

        $('#searchbutton').show();
        $('#refreshbutton').show();
        $('#btnnewform').show();

        $('#btndiscard').hide();

        $('#frmDiscount').deserialize(_variables.defaultValue, true, null);
        $('#frmEditDiscount').deserialize(_variables.defaultEditValue, true, null);


        //$('#frmDiscount [data-valmsg-for="BranchID"]').html("").removeClass('error').addClass('error');
        //$('#frmDiscount [data-valmsg-for="DiscountPercentage"]').html("").removeClass('error').addClass('error');
    }

    var _doShowNewForm = function () {
        $('#discount-new-form').show();
        $('#divDiscount').hide();

        $('#searchbutton').hide();
        $('#refreshbutton').hide();
        $('#btnnewform').hide();

        $('#btndiscard').show();
    }

    var _doInitializeElements = function () {
        $('body').on('click', '#searchdetails', function () {
            _doSearchItem();
        });

        $('body').on('click', '#refreshbutton', function () {
            _doSearchItem();
        });

        $('body').on('click', '#btnnewform', function () {
            _doShowNewForm();
        });

        $('body').on('click', '#btndiscard', function () {
            _doDiscardNewForm();
        });

        $('body').on('click', '#savedetails', function () {
            _doSaveDetails();
        });

        $('body').on('click', '#updatedetails', function () {
            _doUpdateDetails();
        });

        $('body').on('click', '#updateactive', function () {
            var $this = $(this);


            //if (!INFRA.IsNullOrEmpty($this.data())) {
            //}


            $.alertWindow("Are you sure you want to inactive this record?",
            function () {
                _doUpdateActive($this.data('discount-id'), $this.data('branch-id'));
            },
            function () {
            });
        });

        $('body').on('click', '#edititem', function () {
            $this = $(this);
            //$('#discount-new-form').hide();
            $('#mdleditdiscount').modal('show');


        });

        $('body').on('shown.bs.modal', '#mdleditdiscount', function () {
            $(document).off('focusin.modal');

            $('#hdfDiscountId').val($this.data('discount-id'));
            $('#EditDiscountPercentage').val($this.data('discount-percentage'));
            $('#EditBranchID').data('kendoDropDownList').value($this.data('discount-branchid'));
        });

        $('body').on('shown.bs.modal', '#mdlsearchdiscount', function () {
            $(document).off('focusin.modal');
        });

        $('body').on('click', '#removefilter', function () {
            $('#frmSearchDiscount').deserialize(_variables.defaultSearchValue, true, null);
        });


        $('body').on('hidden.bs.modal', '#mdleditdiscount', function () {
            $('#frmEditDiscount').deserialize(_variables.defaultEditValue, true, null);
        });
    }

    var _doInitializeKendoElements = function () {
        $('#SearchProductBranch').kendoDropDownList({
            filter: "contains",
            optionLabel: 'Select All',
            dataTextField: "BranchName",
            dataValueField: "BranchId",
            dataSource: {
                transport: {
                    read: {
                        dataType: "json",
                        url: _variables.params.branchGetUrl
                    }
                }
            }
        });

        $('#BranchID').kendoDropDownList({
            filter: "contains",
            optionLabel: 'Select One',
            dataTextField: "BranchName",
            dataValueField: "BranchId",
            dataSource: {
                transport: {
                    read: {
                        dataType: "json",
                        url: _variables.params.branchGetUrl
                    }
                }
            }
        });

        $('#EditBranchID').kendoDropDownList({
            filter: "contains",
            optionLabel: 'Select One',
            dataTextField: "BranchName",
            dataValueField: "BranchId",
            dataSource: {
                transport: {
                    read: {
                        dataType: "json",
                        url: _variables.params.branchGetUrl
                    }
                }
            }
        });
    }

    var _initialize = function (options) {
        try {
            _variables.params = options || {};
            _doInitializeKendoElements();
            _doInitializeElements();
            _doBindTblDiscount();
        } catch (ex) {
            if (typeof console !== "undefined" && console !== null) {
                console.error("Error parsing inline options", ex);
            }
        }

        _variables.defaultValue = $('#frmDiscount').serialize();
        _variables.defaultEditValue = $('#frmEditDiscount').serialize();
        _variables.defaultSearchValue = $('#frmSearchDiscount').serialize();


    }

    return {
        initialize: _initialize,
        _variables: _variables
    }
})();

$(document).ready(function () {
    var options = window.discountOptions;
    ADMINMANAGEMENTDISCOUNT.initialize(options);


    $(window).resize(function () {
        $('#gvDiscount').kGridResizeHeight({
            height: ADMINMANAGEMENTDISCOUNT._variables.gvDiscountHeight,
            willRefreshGrid: false,
            isManual: true
        });
    });
});