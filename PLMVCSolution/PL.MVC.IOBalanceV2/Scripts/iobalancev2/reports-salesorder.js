var REPORTSSO = (function () {
    var _variables =
    {
        gvHeight: 180,
        params: null,
        defaultSearchValue: null
    };




    var _doInitializeElements = function () {

        $('body').on('click', '#exportexcelbutton', function () {
            var $frm = $('#frmSearch');
            
            window.open(_variables.params.exportUrl + '?' + $frm.serialize());
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
        var $gv = $("#gvSO").data('kendoGrid'), $frm = $('#frmSearch');

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
    var options = window.reportSOOptions;
    REPORTSSO.initialize(options);

    var $gv = $('#gvSO');
    

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