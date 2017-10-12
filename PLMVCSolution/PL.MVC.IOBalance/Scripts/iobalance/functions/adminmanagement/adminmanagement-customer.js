var ADMINMANAGEMENTCUSTOMER = (function () {
    var _variables =
    {
        params: null,
        defaultModalUpdateValue: null,
        gvCustomerHeight: 200,
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

    var _doBindTblCustomer = function ()
    {
        var $gv = $('#gvCustomer'), schemaField = {}, columns = [],
            width, field;

        $gv.kGridResizeHeight({
            height: _variables.gvCustomerHeight,
            willRefreshGrid: false,
            isManual: true
        });
    }

    var _doSaveDetails = function (element) {
        var datas = $('#frmCustomer').serialize(),
            elementUrl = _variables.params.saveUrl,
            $form = $('#frmCustomer');

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
                        //_doSearchDetails();
                        _doDiscardNewForm();
                    } else {

                    }
                    
                }
            });

        } else {
            doRemoveTrackerLoader();
        }
    }

    //var _doUpdateDetails = function (element) {
    //    var datas = $('#frmUpdateCustomer').serialize(),
    //        elementUrl = element.data('url'),
    //        $form = $('#frmUpdateCustomer');

    //    doShowTrackerLoader(_variables.loaderMsg);

    //    $.validator.unobtrusive.parse($form);
    //    $form.validate();

    //    if ($form.valid()) {
    //        $.ajax({
    //            url: elementUrl,
    //            type: 'POST',
    //            data: datas,
    //            success: function (data) {
    //                doRemoveTrackerLoader();
    //                $('#divResultContainer').show().html(data.alertMessage);

    //                if (INFRA.ConvertToBoolean(data.isSuccess)) {
    //                    _doSearchDetails();
    //                    $('#mdleditcustomer').modal('hide');
    //                    INFRA.doRemoveOpenedModal();
    //                } else {

    //                }
                    
    //            }
    //        });
    //    } else {
    //        doRemoveTrackerLoader();
    //    }
    //}

    //var _doInactiveDetails = function (element) {
    //    var elementUrl = _variables.params.updateActiveItemUrl,
    //        customerid = element.data('customer-id'),
    //        customercode = element.data('customer-code');

    //    doShowTrackerLoader(_variables.loaderMsg);

    //    $.ajax({
    //        url: elementUrl,
    //        type: 'POST',
    //        data: { customerId: customerid, customerCode: customercode },
    //        success: function (data) {
    //            doRemoveTrackerLoader();
    //            $('#divResultContainer').show().html(data.alertMessage);

    //            if (INFRA.ConvertToBoolean(data.isSuccess)) {
    //                _doSearchDetails();
    //            }
    //        }
    //    });
    //}

    //var _doRefreshDetails = function () {
    //    _doSearchDetails();
    //}

    //var _doSearchDetails = function () {
    //    var $gv = $("#gvCustomer").data('kendoGrid'), $frmSearch = $('#frmSearchCustomer');

    //    doShowTrackerLoader(_variables.loaderMsg);

    //    $gv.dataSource.read($frmSearch.serializeToJson());

    //    doRemoveTrackerLoader();
    //    $('#mdlsearchcustomer').modal('hide');
    //    INFRA.doRemoveOpenedModal();
    //}

    var _doShowNewForm = function () {
        $('#customer-new-form').show();
        $('#divCustomer').hide();

        $('#searchbutton').hide();
        $('#refreshbutton').hide();
        $('#btnnewform').hide();

        $('#btndiscard').show();

    }

    var _doDiscardNewForm = function () {
        $('#customer-new-form').hide();
        $('#divCustomer').show();

        $('#searchbutton').show();
        $('#refreshbutton').show();
        $('#btnnewform').show();

        $('#btndiscard').hide();

        //$('#frmCustomer #CustomerCode').val('');
        //$('#frmCustomer #LastName').val('');
        //$('#frmCustomer #FirstName').val('');
        //$('#frmCustomer #MiddleName').val('');
        //$('#frmCustomer #Extension').val('');
        //$('#frmCustomer #BirthDate').val('');
        //$('#frmCustomer #Address').val('');
        //$('#frmCustomer #dpBirthDate').data('kendoDatePicker').value(null);

        $('#frmCustomer').deserialize(_variables.defaultValue, true, null);
    }

    var initialize = function (options)
    {
        try {
            _variables.params = options || {};
            _doInitializeElements();
            _doInitializeKendoElements();
            INFRA.doRemoveOpenedModal();
            _doBindTblCustomer();
        } catch (e) {
            if (typeof console !== "undefined" && console !== null) {
                console.error("Error parsing inline options", ex);
            }
        }

        _variables.defaultValue = $('#frmCustomer').serialize();
        //_variables.defaultEditValue = $('#frmUpdateCustomer').serialize();
        //_variables.defaultSearchValue = $('#frmSearchCustomer').serialize();
    }

    var _doInitializeElements = function()
    {
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
            $('#mdleditcustomer').modal('show');
        });

        $('body').on('show.bs.modal', '#mdleditcustomer', function () {
            var $caller = $('[data-action-type="editbutton"].selected');
            $('#frmUpdateCustomer #hdfCustomerId').val($caller.data('customer-id'));
            $('#frmUpdateCustomer #CustomerCode').val($caller.data('customer-code'));
            $('#frmUpdateCustomer #LastName').val($caller.data('customer-lastname'));
            $('#frmUpdateCustomer #FirstName').val($caller.data('customer-firstname'));
            $('#frmUpdateCustomer #MiddleName').val($caller.data('customer-middlename'));
            $('#frmUpdateCustomer #Extension').val($caller.data('customer-extension'));
            $('#frmUpdateCustomer #dpBirthDate').val($caller.data('customer-birthdate'));
            $('#frmUpdateCustomer #Address').val($caller.data('customer-address'));
            $('#frmUpdateCustomer #City').val($caller.data('customer-city'));
            $('#frmUpdateCustomer #Region').val($caller.data('customer-region'));
            $('#frmUpdateCustomer #ZipCode').val($caller.data('customer-zipcode'));
        });

        $('#mdleditcustomer').on('hidden.bs.modal', function () {
            //$('#frmUpdateCustomer #hdfCustomerId').val(null);
            //$('#frmUpdateCustomer #CustomerCode').val(null);
            //$('#frmUpdateCustomer #LastName').val(null);
            //$('#frmUpdateCustomer #FirstName').val(null);
            //$('#frmUpdateCustomer #MiddleName').val(null);
            //$('#frmUpdateCustomer #Extension').val(null);
            //$('#frmUpdateCustomer #BirthDate').val(null);
            //$('#frmUpdateCustomer #Address').val(null);
            //$('#frmUpdateCustomer #City').val(null);
            //$('#frmUpdateCustomer #Region').val(null);
            //$('#frmUpdateCustomer #ZipCode').val(null);
            $('#frmUpdateCustomer').deserialize(_variables.defaultEditValue, true, null);
        });

        $('body').on('click', '[data-action-type="deletebutton"]', function () {
            var $elem = $(this), $caller;

            $('[data-action-type="deletebutton"]').removeClass('selected');
            $elem.addClass('selected');

            $caller = $('[data-action-type="deletebutton"].selected');

            $.alertWindow("Are you sure you want to update this record?",
            function () {
                _doInactiveDetails($caller);
            },
            function () {
            });
        });

        $('body').on('click', '#refreshbutton', function () {
            _doRefreshDetails();
        });

        $('body').on('click', '#searchbutton', function () {
            var $elem = $(this);
            $('#mdlsearchcustomer').modal('show');
        });

        $('body').on('click', '#btnnewform', function () {
            _doShowNewForm();
        });

        $('body').on('click', '#btndiscard', function () {
            _doDiscardNewForm();
        });

        $('body').on('click', '#removefilter', function () {
            //$('#frmSearchCustomer #CustomerCode').val('');
            //$('#frmSearchCustomer #LastName').val('');
            //$('#frmSearchCustomer #FirstName').val('');
            //$('#frmSearchCustomer #Address').val('');
            //$('#frmSearchCustomer #SearchCustomerYesNo').data('kendoDropDownList').value(null);
            $('#frmSearchCustomer').deserialize(_variables.defaultSearchValue, true, null);
        });
    }

    var _doInitializeKendoElements = function () {
        $("#SearchCustomerYesNo").kendoDropDownList({
            optionLabel: 'Select All',
            dataTextField: "text",
            dataValueField: "value",
            dataSource: {
                transport: {
                    read: {
                        dataType: "json",
                        url: _variables.params.isActiveYesNoUrl
                    }
                }
            }
        });
        //isActiveYesNoUrl

        $("#frmCustomer #dpBirthDate").kendoDatePicker({
            format: "MM/dd/yyyy"
        });

        $("#frmUpdateCustomer #dpBirthDate").kendoDatePicker({
            format: "MM/dd/yyyy"
        });
    }

    return {
        initialize: initialize,
        _variables: _variables
    }

})();


$(document).ready(function () {
    var options = window.customerOptions;
    ADMINMANAGEMENTCUSTOMER.initialize(options);

    //$('#dpBirthDate').validate({
    //    errorPlacement: $.datepicker.errorPlacement,
    //    rules: {
    //        validDefaultDatepicker: {
    //            required: false,
    //            dpDate: true
    //        }
    //    },
    //    messages: {
    //        validFormatDatepicker: 'Please enter a valid date (mm/dd/yyyy)'
    //    }
    //});
    $('#frmUpdateCustomer #dpBirthDate').mask('00/00/0000');
    $('#frmCustomer #dpBirthDate').mask('00/00/0000');
    $(window).resize(function () {
        $('#gvCustomer').kGridResizeHeight({
            height: ADMINMANAGEMENTCATEGORY._variables.gvCustomerHeight,
            willRefreshGrid: false,
            isManual: true
        });
    });
});