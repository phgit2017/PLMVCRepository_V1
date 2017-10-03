var ADMINMANAGEMENTUNIT = (function () {
    var _variables =
    {
        params: null
    };

    var _doBindTblUnit = function () {
        var $gvUnit = $('#gvUnit'), schemaField = {}, columns = [],
            width, field;


        $gvUnit.find('th').each(function () {
            $this = $(this);
            width = INFRA.ReplaceIfNullOrEmpty($this.data('width'), 100);
            field = $this.data('field');

            column = {
                width: width,
                field: field
            }
            columns.push(column);
        });

        $gvUnit.kendoGrid({
            sortable: false,
            scrollable: true,
            pageable: true,
            dataSource: {
                pageSize: 8
            }, columns: columns
        });
    }

    var _doSaveDetails = function (element) {
        var datas = $('#frmUnit').serialize(),
            elementUrl = element.data('url'),
            $form = $('#frmUnit');

        $.validator.unobtrusive.parse($form);
        $form.validate();
        if ($form.valid()) {
            $.ajax({
                url: elementUrl,
                type: 'POST',
                data: datas,
                success: function (data) {
                    $('#divResultContainer').show().html(data.alertMessage);
                    if (INFRA.ConvertToBoolean(data.isSuccess)) {
                        $('#divUnit').html(data.unit);
                        _doBindTblUnit();
                    }
                }
            });

        }
    }

    var _doUpdateDetails = function (element) {
        var datas = $('#frmUpdateUnit').serialize(),
            elementUrl = element.data('url'),
            $form = $('#frmUpdateUnit');

        $.validator.unobtrusive.parse($form);
        $form.validate();

        if ($form.valid()) {
            $.ajax({
                url: elementUrl,
                type: 'POST',
                data: datas,
                success: function (data) {
                    $('#divResultContainer').show().html(data.alertMessage);

                    if (INFRA.ConvertToBoolean(data.isSuccess)) {
                        $('#divUnit').html(data.unit);
                        _doBindTblUnit();
                    }
                    $('#mdleditunit').modal('hide');
                    INFRA.doRemoveOpenedModal();
                }
            });
        }
    }

    var _doSearchDetails = function (element) {
        var datas = $('#frmSearchUnit').serialize(),
            elementUrl = element.data('url'),
            $form = $('#frmSearchUnit');

        $.ajax({
            url: elementUrl,
            type: 'POST',
            data: datas,
            success: function (data) {
                $('#divUnit').html(data);
                _doBindTblUnit();

                $('#mdlsearchunit').modal('hide');
                INFRA.doRemoveOpenedModal();
            }
        });
    }

    var _doDeleteDetails = function (element) {
        var elementUrl = element.data('url'),
            unitid = element.data('unit-id')

        $.ajax({
            url: elementUrl,
            type: 'POST',
            data: { unitId: unitid },
            success: function (data) {
                $('#divResultContainer').show().html(data.alertMessage);

                if (INFRA.ConvertToBoolean(data.isSuccess)) {
                    $('#divUnit').html(data.unit);
                    _doBindTblUnit();
                }
            }
        });
    }

    var _doRefreshDetails = function (element) {
        var elementUrl = element.data('url');

        $.ajax({
            url: elementUrl,
            type: 'POST',
            success: function (data) {
                $('#divUnit').html(data);
                _doBindTblUnit();
            }
        });
    }

    var initialize = function (options) {
        try {
            _variables.params = options || {};
            _doInitializeElements();
            INFRA.doRemoveOpenedModal();
            _doBindTblUnit();
        } catch (e) {
            if (typeof console !== "undefined" && console !== null) {
                console.error("Error parsing inline options", ex);
            }
        }
    }

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
            var $elem = $(this);
            _doSearchDetails($elem);
        });

        $('body').on('click', '[data-action-type="editbutton"]', function () {
            var $elem = $(this);
            $('[data-action-type="editbutton"]').removeClass('selected');
            $elem.addClass('selected');
            $('#mdleditunit').modal('show');
        });

        $('body').on('show.bs.modal', '#mdleditunit', function () {
            var $caller = $('[data-action-type="editbutton"].selected');
            $('#frmUpdateUnit #hdfUnitId').val($caller.data('unit-id'));
            $('#frmUpdateUnit #UnitDesc').val($caller.data('unit-desc'));
        });

        $('#mdleditunit').on('hidden.bs.modal', function () {
            $('#frmUpdateUnit #hdfUnitId').val(null);
            $('#frmUpdateUnit #UnitDesc').val(null);
        });

        $('body').on('click', '#searchbutton', function () {
            var $elem = $(this);
            $('#mdlsearchunit').modal('show');
        });

        $('body').on('click', '[data-action-type="deletebutton"]', function () {
            var $elem = $(this), $caller;

            $('[data-action-type="deletebutton"]').removeClass('selected');
            $elem.addClass('selected');

            $caller = $('[data-action-type="deletebutton"].selected');

            $.alertWindow("Are you sure you want to delete this record?",
            function () {
                _doDeleteDetails($caller);
            },
            function () {
            });
        });

        $('body').on('click', '#refreshbutton', function () {
            var $elem = $(this);
            _doRefreshDetails($elem);
        });
    }

    return {
        initialize: initialize
    }
})();

$(document).ready(function () {
    ADMINMANAGEMENTUNIT.initialize();
});