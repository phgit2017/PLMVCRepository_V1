var REPORTSINVENTORY = (function () {

    var _variables =
    {
        gvHeight: 180,
        params: null,
        defaultSearchValue: null
    };




    var _doInitializeElements = function () {

        $('body').on('click', '#exportexcelbutton', function () {
            var productId = $('#ProductId').data('kendoDropDownList').value();

            window.open(_variables.params.exportUrl + '?' + 'productId=' + productId);
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


    }

    var _doInitializeKendoElements = function () {
        $("#ProductId").kendoDropDownList({
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
    }

    var _doSearchDetails = function () {
        var $gv = $("#gvInventoryReport").data('kendoGrid');
        var productId = $('#ProductId').data('kendoDropDownList').value();

        $gv.dataSource.read({ 'productId': productId });
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

    }

    return {
        initialize: initialize,
        _variables: _variables
    }
})();

$(document).ready(function () {
    var options = window.reportInventoryReportOptions;
    REPORTSINVENTORY.initialize(options);

    var $gv = $('#gvInventoryReport');


    $gv.kGridResizeHeight({
        height: REPORTSINVENTORY._variables.gvHeight,
        willRefreshGrid: false,
        isManual: true
    });

    $(window).resize(function () {
        $gv.kGridResizeHeight({
            height: REPORTSINVENTORY._variables.gvHeight,
            willRefreshGrid: false,
            isManual: true
        });

    });

});