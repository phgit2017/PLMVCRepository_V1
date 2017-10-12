var ADMINMANAGEMENTSUPPLIER = (function () {
    var _variables =
    {
        params: null,
        defaultModalUpdateValue: null,
        gvSupplierHeight: 200,
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

    var _doBindTblSupplier = function () {
        var $gv = $('#divSupplier');

        $frm = $('#frmSearchSupplier');

        $("#divSupplier").kendoGrid({
            dataSource: {
                type: "aspnetmvc-ajax",
                transport: {
                    read: _variables.params.getKendoUrl,
                    datatype: "json",
                    data: $frm.serialize()
                },
                pageSize: 50,
                serverPaging: true,
                serverSorting: true,
                serverFiltering: true,
                schema: {
                    total: function (data) {
                        return data.Total;
                    },
                    data: function (data) {
                        return data.Data;
                    }
                }
            },
            sortable: true,
            filterable: false,
            pageable: true,
            scrollable: true,
            groupable: false,
            columnMenu: {
                messages: {
                    filter: "Apply filter"
                }
            },
            columns: [
                {
                    field: "",
                    title: "Action",
                    width: 300,
                    locked: true,
                    template: function (e) {
                        return _doBindGVAction(e);
                    },
                    headerAttributes: {
                        "class": "bos-grid-text-center"
                    },
                    attributes: {
                        "class": "bos-grid-text-center"
                    }
                },
                {
                    field: "SupplierCode",
                    title: "Supplier Code",
                    width: 300,
                    locked: false,
                    sortable: true,
                    headerAttributes: {
                        "class": "bos-grid-text-center"
                    },
                    attributes: {
                        "class": "bos-grid-text-center"
                    }
                },
                {
                    field: "SupplierName",
                    title: "Supplier Name",
                    width: 300,
                    locked: false,
                    headerAttributes: {
                        "class": "bos-grid-text-center"
                    },
                    attributes: {
                        "class": "bos-grid-text-center"
                    }
                },
                {
                    field: "IsActive",
                    title: "Active?",
                    width: 210,
                    locked: false,
                    headerAttributes: {
                        "class": "bos-grid-text-center"
                    },
                    attributes: {
                        "class": "bos-grid-text-center"
                    }
                }]
        });

        $gv.kGridResizeHeight({
            height: _variables.gvSupplierHeight,
            willRefreshGrid: true
        });
    }

    var _doBindGVAction = function (e) {
        var editbutton, deletebutton;

        editbutton = "<a href='javascript:void(0)' type='button' class='selected' data-button-type='actionbuttons' data-action-type='editbutton' data-supplier-id='" + e.SupplierID + "' data-supplier-code='" + e.SupplierCode + "' data-supplier-name='" + e.SupplierName + "'> Edit Details</a>";
        deletebutton = "<a href='javascript:void(0)' type='button' class='selected' data-button-type='actionbuttons' data-action-type='deletebutton' data-supplier-id='" + e.SupplierID + "' data-supplier-name='" + e.SupplierName + "' data-supplier-code='" + e.SupplierCode + "'> Deactivate/Activate </a>";
        //editbutton = "<a href='javascript:void(0)' type='button' class='selected' data-button-type='actionbuttons' data-action-type='editbutton' data-category-id='" + e.CategoryID + "' data-category-code='" + e.CategoryCode + "' data-category-name='" + e.CategoryName + "'> Edit Details</a>";
        //deletebutton = "<a href='javascript:void(0)' type='button' class='selected' data-button-type='actionbuttons' data-action-type='deletebutton' data-category-id='" + e.CategoryID + "' data-category-code='" + e.CategoryCode + "' data-category-name='" + e.CategoryName + "' > Deactivate/Activate </a>";

        return editbutton + " | " + deletebutton;
    }

    var _doSaveDetails = function (element) {
        var datas = $('#frmSupplier').serialize(),
            elementUrl = element.data('url'),
            $form = $('#frmSupplier');

        doShowTrackerLoader(_variables.loaderMsg.loaderMsg);

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
                    } else {

                    }
                    _doDiscardNewForm();
                }
            });

        } else {
            doRemoveTrackerLoader();
        }
    }

    var _doUpdateDetails = function (element) {
        var datas = $('#frmUpdateSupplier').serialize(),
            elementUrl = element.data('url'),
            $form = $('#frmUpdateSupplier');

        doShowTrackerLoader(_variables.loaderMsg.loaderMsg);

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

                        $('#mdleditsupplier').modal('hide');
                        INFRA.doRemoveOpenedModal();
                    } else {

                    }

                }
            });
        } else {
            doRemoveTrackerLoader();
        }
    }

    var _doInactiveDetails = function (element) {
        debugger;
        var elementUrl = _variables.params.updateActiveItemUrl,
            supplierid = element.data('supplier-id'), suppliercode = element.data('supplier-code'), suppliername = element.data('supplier-name');

        doShowTrackerLoader(_variables.loaderMsg.loaderMsg);

        $.ajax({
            url: elementUrl,
            type: 'POST',
            data: { supplierId: supplierid, supplierCode: suppliercode, supplierName: suppliername },
            success: function (data) {
                doRemoveTrackerLoader();
                $('#divResultContainer').show().html(data.alertMessage);

                if (INFRA.ConvertToBoolean(data.isSuccess)) {
                    _doSearchDetails();
                }
            }
        });
    }

    var _doRefreshDetails = function () {
        _doSearchDetails();
    }

    var _doSearchDetails = function () {
        var $gv = $("#divSupplier").data('kendoGrid'), $frmSearch = $('#frmSearchSupplier');

        doShowTrackerLoader(_variables.loaderMsg);

        $gv.dataSource.read($frmSearch.serializeToJson());

        doRemoveTrackerLoader();
        $('#mdlsearchsupplier').modal('hide');
        INFRA.doRemoveOpenedModal();
    }

    var initialize = function (options) {
        try {
            _variables.params = options || {};
            _doBindTblSupplier();
            _doInitializeElements();
            _doInitializeKendoElements();
            INFRA.doRemoveOpenedModal();

        } catch (ex) {
            if (typeof console !== "undefined" && console !== null) {
                console.error("Error parsing inline options", ex);
            }
        }

        _variables.defaultValue = $('#frmSupplier').serialize();
        _variables.defaultEditValue = $('#frmUpdateSupplier').serialize();
        _variables.defaultSearchValue = $('#frmSearchSupplier').serialize();
    };

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

        $('body').on('click', '#searchbutton', function () {
            $('#mdlsearchsupplier').modal('show');
        });

        $('body').on('click', '#refreshbutton', function () {
            _doRefreshDetails();
        });

        $('body').on('click', '[data-action-type="editbutton"]', function () {
            var $elem = $(this);
            $('[data-action-type="editbutton"]').removeClass('selected');
            $elem.addClass('selected');
            $('#mdleditsupplier').modal('show');
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

        $('body').on('show.bs.modal', '#mdleditsupplier', function () {
            var $caller = $('[data-action-type="editbutton"].selected');
            $('#frmUpdateSupplier #hdfSupplierId').val($caller.data('supplier-id'));
            $('#frmUpdateSupplier #SupplierCode').val($caller.data('supplier-code'));
            $('#frmUpdateSupplier #SupplierName').val($caller.data('supplier-name'));
        });

        $('#mdleditsupplier').on('hidden.bs.modal', function () {
            $('#frmUpdateSupplier').deserialize(_variables.defaultEditValue, true, null);
        });

        $('body').on('click', '#btnnewform', function () {
            _doShowNewForm();
        });

        $('body').on('click', '#btndiscard', function () {
            _doDiscardNewForm();
        });

        $('body').on('click', '#removefilter', function () {
            $('#frmSearchSupplier').deserialize(_variables.defaultSearchValue, true, null);
        });
    }

    var _doInitializeKendoElements = function () {
        $("#SearchSupplierYesNo").kendoDropDownList({
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
    }

    var _doShowNewForm = function () {
        $('#supplier-new-form').show();
        $('#divSupplier').hide();

        $('#searchbutton').hide();
        $('#refreshbutton').hide();
        $('#btnnewform').hide();

        $('#btndiscard').show();

    }

    var _doDiscardNewForm = function () {
        $('#supplier-new-form').hide();
        $('#divSupplier').show();

        $('#searchbutton').show();
        $('#refreshbutton').show();
        $('#btnnewform').show();

        $('#btndiscard').hide();

        //$('#frmCategory #SupplierCode').val('');
        //$('#frmCategory #SupplierName').val('');
        $('#frmSupplier').deserialize(_variables.defaultValue, true, null);
    }

    return {
        initialize: initialize
    };
})();

$(document).ready(function () {
    var options = window.supplierOptions;
    ADMINMANAGEMENTSUPPLIER.initialize(options);

    $(window).resize(function () {
        $('#gvSupplier').kGridResizeHeight({
            height: ADMINMANAGEMENTSUPPLIER._variables.gvSupplierHeight,
            willRefreshGrid: false,
            isManual: true
        });
    });
});