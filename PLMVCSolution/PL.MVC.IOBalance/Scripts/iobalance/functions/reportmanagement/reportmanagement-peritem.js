var REPORTMANAGEMENTPERITEM = (function () {
    var _variables = {
        params: null,
        loaderMsg: "",
        UpdateMsg: "",
        gvPerItemHeight: 230
    }

    var _doInitializeElements = function () {
        $('body').on('click', '#searchbutton', function () {
            $('#mdlsearchreportperitem').modal('show');
        });

        $('body').on('click', '#searchdetails', function () {
            _doSearchReportPerItem();
        });

        $('body').on('shown.bs.modal', '#mdlsearchreportperitem', function () {
            $(document).off('focusin.modal');
        });
    }

    var _doInitializeKendoElements = function () {

        $("#dpFrom").kendoDatePicker({
            format: "yyyy/MM"
        });

        $('#SearchProductCategory').kendoDropDownList({
            filter: "contains",
            optionLabel: 'Select All',
            dataTextField: "CategoryDisplay",
            dataValueField: "CategoryID",
            dataSource: {
                transport: {
                    read: {
                        dataType: "json",
                        url: _variables.params.categoryGetUrl
                    }
                }
            }
        });

        $('#SearchSOProductBranch').kendoDropDownList({
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
    }

    var _doSearchReportPerItem = function () {
        var $frmSearchReportPerItem = $('#frmSearchReportPerItem');
        $.ajax({
            url: _variables.params.searchReportPerItemUrl,
            type: 'POST',
            data: $frmSearchReportPerItem.serialize(),
            success: function (data) {
                $('#divPerItem').html(data);
                _doBindTblPerItem();
            }
        });
    }

    var initialize = function (options) {
        try {
            _variables.params = options || {};
        } catch (ex) {
            if (typeof console !== "undefined" && console !== null) {
                console.error("Error parsing inline options", ex);
            }
        }
        _doInitializeKendoElements();
        _doInitializeElements();
        var $hdfUserTypeId = $('#frmReportPerItem #hdfUserTypeId');
        var $hdfBranchId = $('#frmReportPerItem #hdfBranchId');

        //if (INFRA.ConvertToInteger($hdfUserTypeId.val()) === 2) {
        //    $('#frmReportPerItem #SearchSOProductBranch').data('kendoDropDownList').value($hdfBranchId.val());
        //}

    }

    return {
        initialize: initialize,
        _variables: _variables
    }

})();


$(document).ready(function () {
    var options = window.inventorySalesOptions;
    REPORTMANAGEMENTPERITEM.initialize(options);
});