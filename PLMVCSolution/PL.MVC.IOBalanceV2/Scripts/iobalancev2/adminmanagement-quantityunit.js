var ADMINMANAGEMENTQUANTITYUNIT = (function () {
    var _variables =
    {
        params: null,
        defaultSaveValue: null,
        defaultEditValue: null,
        defaultSearchValue: null,
        gvHeight:180
    };

    var _doShowSaveForm = function () {

        $('#searchbutton').hide();
        $('#refreshbutton').hide();
        $('#btnnewform').hide();
        $('#divList').hide();

        $('#new-form').show();
        $('#btndiscard').show();
    }

    var _doSaveForm = function () {
        var $frm = $('#frmSave');


        $.validator.unobtrusive.parse($frm);
        $frm.validate();
        if ($frm.valid()) {
            $.ajax({
                url: _variables.params.saveUrl,
                type: 'POST',
                data: $frm.serialize(),
                success: function (data) {
                    if (data.isSuccess == true) {
                        _doSearchDetails();
                        _discardSaveForm();
                    }
                    $('#divResultContainer').show().html(data.alertMessage);

                }
            });
        } else {

        }




    }

    var _doUpdateForm = function () {
        var $frm = $('#frmEdit');

        $.validator.unobtrusive.parse($frm);
        $frm.validate();
        if ($frm.valid()) {

            $.ajax({
                url: _variables.params.updateUrl,
                type: 'POST',
                data: $frm.serialize(),
                success: function (data) {
                    if (data.isSuccess == true) {
                        _doSearchDetails();

                    }

                    $('#divResultContainer').show().html(data.alertMessage);
                }
            });

            $('#mdledit').modal('hide');
        }
        else {

        }


    }

    var _discardSaveForm = function () {
        $('#searchbutton').show();
        $('#refreshbutton').show();
        $('#btnnewform').show();
        $('#divList').show();

        $('#new-form').hide();
        $('#btndiscard').hide();

        $('#frmSave').deserialize(_variables.defaultSaveValue, true, null);

    }

    var _doSearchDetails = function () {
        var $gv = $("#gvQuantityUnit").data('kendoGrid'), $frm = $('#frmSearch');

        $gv.dataSource.read($frm.serializeToJson());
        $('#mdlsearch').modal('hide');
    }



    var _doInitializeElements = function () {
        $('body').on('click', '#btnnewform', function () {
            _doShowSaveForm();
        });

        $('body').on('click', '#btndiscard', function () {
            _discardSaveForm();
        });

        $('body').on('click', '#btnSaveform', function () {
            _doSaveForm();
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

        $('body').on('click', '#removefilter', function () {
            $('#frmSearch').deserialize(_variables.defaultSearchValue, true, null);
        });

        $('body').on('click', '#refreshbutton', function () {
            _doSearchDetails();
        });

        $('body').on('click', '#edititem', function () {
            $this = $(this);
            $('#mdledit').modal({
                'show': true,
                'backdrop': 'static',
                'keyboard': false
            });
        });

        $('body').on('shown.bs.modal', '#mdledit', function () {
            $(document).off('focusin.modal');

            $('#frmEdit #hdfQuantityUnitId').val($this.data('quantity-id'));
            $('#frmEdit #UnitName').val($this.data('unit-name'));
        });

        $('body').on('hidden.bs.modal', '#mdledit', function () {
            $('#frmEdit').deserialize(_variables.defaultEditValue, true, null);
        });

        $('body').on('click', '#updatedetails', function () {
            _doUpdateForm();
        });



    }

    var _doInitializeKendoElements = function () {

    }


    var initialize = function (options) {
        try {
            _variables.params = options || {};
            _doInitializeElements();
            _doInitializeKendoElements();
        }
        catch (e) {
            if (typeof console !== "undefined" && console !== null) {
                console.error("Error parsing inline options", ex);
            }
        }

        _variables.defaultSaveValue = $('#frmSave').serialize();
        _variables.defaultEditValue = $('#frmEdit').serialize();
        _variables.defaultSearchValue = $('#frmSearch').serialize();
    }

    return {
        initialize: initialize,
        _variables: _variables
    }
})();

$(document).ready(function () {
    var options = window.adminOptions;
    ADMINMANAGEMENTQUANTITYUNIT.initialize(options);


    var $gv = $('#gvQuantityUnit');


    $gv.kGridResizeHeight({
        height: ADMINMANAGEMENTQUANTITYUNIT._variables.gvHeight,
        willRefreshGrid: false,
        isManual: true
    });

    $(window).resize(function () {
        $gv.kGridResizeHeight({
            height: ADMINMANAGEMENTQUANTITYUNIT._variables.gvHeight,
            willRefreshGrid: false,
            isManual: true
        });
    });
});