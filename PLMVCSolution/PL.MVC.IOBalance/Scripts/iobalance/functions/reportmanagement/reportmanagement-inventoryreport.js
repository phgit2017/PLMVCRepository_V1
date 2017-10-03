var REPORTMANAGEMENTINVENTORYREPORT = (function () {
    var _variables = {
        params: null,
        loaderMsg: 'Please wait while we process your request',
        gvInventoryReportHeight: 180,
        defaultCRType: ""
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
        $('body').on('click', '#searchbutton', function () {
            $('#mdlsearchinventoryreport').modal('show');
        });

        $('body').on('click', '#searchdetails', function () {
            _doSearchItem();
        });

        $('body').on('click', '#refreshbutton', function () {
            _doSearchItem();
        });


        $('body').on('shown.bs.modal', '#mdlsearchinventoryreport', function () {
            $(document).off('focusin.modal');
        });

        $('body').on('click', '#removefilter', function () {
            $('#frmSearchInventoryReport #SearchProduct').data('kendoDropDownList').value(null);
            $('#frmSearchInventoryReport #SearchProductBranch').data('kendoDropDownList').value(null);
        });

        $('body').on('click', '#extractbutton', function () {
            $frmSearchProduct = $('#frmSearchProduct');
            window.open(_variables.params.extractToExcelUrl + '?' + $frmSearchProduct.serialize());
        });

    }

    var _doInitializeDropDownElements = function () {
       //$('#SearchProductBranch').kendoDropDownList({
       //     filter: "contains",
       //     optionLabel: 'Select All',
       //     dataTextField: "BranchName",
       //     dataValueField: "BranchId",
       //     dataSource: {
       //         transport: {
       //             read: {
       //                 dataType: "json",
       //                 url: _variables.params.branchGetUrl
       //             }
       //         }
       //     }
       //});

       $('#SearchProductBranch').kendoDropDownList({
            filter: "contains",
            optionLabel: 'Select All',
            dataTextField: "BranchName",
            dataValueField: "BranchId",
            change: _doChangeProductBranch,
            dataSource: {
                transport: {
                    read: {
                        dataType: "json",
                        url: _variables.params.branchGetUrl
                    }
                }
            }
        });

       $('#SearchProduct').kendoDropDownList({
            filter: "contains",
            optionLabel: 'Select All',
            dataTextField: "ProductFullDisplayWithExtension",
            dataValueField: "ProductID",
            dataSource: {
                transport: {
                    read: {
                        dataType: "json",
                        url: _variables.params.productGetUrl
                    }
                }
            }
        });


        
    }

    var _doBindTblInventoryReport = function () {
        var $gvInventoryReport = $('#gvInventoryReport'), schemaField = {}, columns = [],
            width, field;

        $gvInventoryReport.kGridResizeHeight({
            height: _variables.gvInventoryReportHeight,
            willRefreshGrid: false,
            isManual: true
        });
    }


    var _doSearchItem = function () {
        

        var $gvInventoryReport = $("#gvInventoryReport").data('kendoGrid'), $frmSearchInventoryReport = $('#frmSearchInventoryReport');

        doShowTrackerLoader(_variables.loaderMsg);

        $gvInventoryReport.dataSource.read($frmSearchInventoryReport.serializeToJson());

        //if ($gvInventoryReport.pager.page() !== 1) {
        //    $gvInventoryReport.pager.page(1);
        //}

        doRemoveTrackerLoader();
        $('#mdlsearchinventoryreport').modal('hide');
        INFRA.doRemoveOpenedModal();

    }

    var _doRefreshInventoryReport = function () {
        _doSearchItem();
    }

    var _doChangeProductBranch = function () {
        var $branch = $('#SearchProductBranch').data('kendoDropDownList');

        if (!INFRA.IsNullOrEmpty($branch.value())) {
            $("#SearchProduct").kendoDropDownList({
                filter: "startswith",
                optionLabel: 'Select All',
                dataTextField: "ProductFullDisplayWithExtension",
                dataValueField: "ProductID",
                dataSource: {
                    transport: {
                        read: {
                            dataType: "json",
                            url: _variables.params.productBranchIdGetUrl,
                            data: {
                                branchId: $branch.value()
                            }
                        }
                    }
                }
            });

        } else {
            $('#SearchProduct').kendoDropDownList({
                filter: "contains",
                optionLabel: 'Select All',
                dataTextField: "ProductFullDisplayWithExtension",
                dataValueField: "ProductID",
                dataSource: {
                    transport: {
                        read: {
                            dataType: "json",
                            url: _variables.params.productGetUrl
                        }
                    }
                }
            });
        }
    }

    var initialize = function (options) {
        try {
            _variables.params = options || {};
        } catch (ex) {
            if (typeof console !== "undefined" && console !== null) {
                console.error("Error parsing inline options", ex);
            }
        }
        _doInitializeDropDownElements();
        _doInitializeElements();
        _doBindTblInventoryReport();
    }

    return {
        initialize: initialize,
        _variables: _variables
    }

})();

$(document).ready(function () {
    var options = window.inventoryReportOptions;
    REPORTMANAGEMENTINVENTORYREPORT.initialize(options);

    $(window).resize(function () {
        $('#gvInventoryReport').kGridResizeHeight({
            height: REPORTMANAGEMENTINVENTORYREPORT._variables.gvInventoryReportHeight,
            willRefreshGrid: false,
            isManual: true
        });
    });
});