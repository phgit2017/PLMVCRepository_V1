var REPORTSSO = (function () {
    var _variables =
    {
        gvHeight: 180,
        params: null,
        defaultSearchValue: null
    };




    var _doInitializeElements = function () {


        $('body').on('click', '#generatereceipt', function () {
            debugger;
            $this = $(this);
            var salesOrderId, customerId = 0;
            var salesNo = "";

            salesOrderId = $this.data('reportlist-salesorderid');
            customerId = $this.data('reportlist-customerid');
            salesNo = $this.data('reportlist-salesno');

            window.open(_variables.params.exportUrl + '?' + 'salesOrderId=' + salesOrderId + '&customerId=' + customerId + '&salesNo=' + salesNo);
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

        $('body').on('click', '#refreshbutton', function () {
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
        $("#SearchCustomerId").kendoDropDownList({
            filter: 'contains',
            optionLabel: 'Select All',
            dataTextField: "CustomerDropDownDisplay",
            dataValueField: "CustomerId",
            dataSource: {
                transport: {
                    read: {
                        dataType: "json",
                        url: _variables.params.customerUrl
                    }
                }
            }
        });
    }

    var _doSearchDetails = function () {
        var $gv = $("#gvSOReceipt").data('kendoGrid'), $frm = $('#frmSearch');

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
    debugger;
    var options = window.reportSOOptions;
    REPORTSSO.initialize(options);
    debugger;
    var $gv = $('#gvSOReceipt');

    $gv.kGridResizeHeight({
        height: REPORTSSO._variables.gvHeight,
        willRefreshGrid: false,
        isManual: true
    });

    $(window).resize(function () {
        $gv.kGridResizeHeight({
            height: REPORTSSO._variables.gvHeight,
            willRefreshGrid: false,
            isManual: true
        });

    });

});