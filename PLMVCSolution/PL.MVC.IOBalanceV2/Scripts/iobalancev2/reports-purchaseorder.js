var REPORTSPO = (function () {
    var _variables =
    {
        params: null,
        gvHeight: 180,
        defaultSearchValue: null
    };




    var _doInitializeElements = function () {

        $('body').on('click', '#exportexcelbutton', function () {
            window.open(_variables.params.exportUrl);
        });

        $('body').on('click', '#searchbutton', function () {
            $('#mdlsearch').modal({
                'show': true,
                'backdrop': 'static',
                'keyboard': false
            });
        });

        $('body').on('click', '#searchdetails', function () {
            _doSearchDetails();
        });

        $('body').on('shown.bs.modal', '#mdlsearch', function () {
            $(document).off('focusin.modal');
        });

        $('body').on('click', '#removefilter', function () {
            $('#frmSearch').deserialize(_variables.defaultSearchValue, true, null);
        });

    }

    var _doInitializeKendoElements = function () {
        $("#SearchProductId").kendoDropDownList({
            filter: 'contains',
            optionLabel: 'Select All',
            dataTextField: "ProductDropDownDisplay",
            dataValueField: "ProductId",
            dataSource: {
                transport: {
                    read: {
                        dataType: "json",
                        url: _variables.params.productUrl
                    }
                }
            }
        });

        $("#SearchSupplierId").kendoDropDownList({
            filter: 'contains',
            optionLabel: 'Select All',
            dataTextField: "SupplierInfoDisplay",
            dataValueField: "SupplierId",
            dataSource: {
                transport: {
                    read: {
                        dataType: "json",
                        url: _variables.params.supplierUrl
                    }
                }
            }
        });
    }

    var _doSearchDetails = function () {
        var $gv = $("#gvPO").data('kendoGrid'), $frm = $('#frmSearch');

        $gv.dataSource.read($frm.serializeToJson());
        $('#mdlsearch').modal('hide');
    }


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

        _variables.defaultSearchValue = $('#frmSearch').serialize();
    }

    return {
        initialize: initialize,
        _variables: _variables
    }
})();

$(document).ready(function () {
    var options = window.reportPOOptions;
    REPORTSPO.initialize(options);

    var $gv = $('#gvPO');


    $gv.kGridResizeHeight({
        height: REPORTSPO._variables.gvHeight,
        willRefreshGrid: false,
        isManual: true
    });

    $(window).resize(function () {
        $gv.kGridResizeHeight({
            height: REPORTSPO._variables.gvHeight,
            willRefreshGrid: false,
            isManual: true
        });
    });
});