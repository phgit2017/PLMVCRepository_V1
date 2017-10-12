﻿var ADMINMANAGEMENTMODEL = (function () {
    var _variables = {
        params: null,
        loaderMsg: "",
        UpdateMsg: ""
    }

    var _doBindTblModel = function () {
        var $gvModel = $('#gvModel'), schemaField = {}, columns = [],
            width, field;


        $gvModel.find('th').each(function () {
            $this = $(this);
            width = INFRA.ReplaceIfNullOrEmpty($this.data('width'), 100);
            field = $this.data('field');

            column = {
                width: width,
                field: field
            }
            columns.push(column);
        });

        $gvModel.kendoGrid({
            sortable: false,
            scrollable: true,
            pageable: true,
            dataSource: {
                pageSize: 8
            }, columns: columns
        });
    }

    var _doInitializeElements = function ()
    {
        $('body').on('click', '#newbutton', function () {
            _doNewItem();
            _variables.params.modelAction = "save";
        });

        $('body').on('click', '#discardbutton', function () {
            _doDiscardItem();
            _variables.params.modelAction = "";
        });

        $('body').on('click', '#savebutton', function () {
            _doSaveItem();
        });

        $('body').on('click', '#editbutton', function () {
            var $this = $(this);
            _doNewItem();
            _doClearItem();

            $('#frmModel #ModelName').val($this.data('model-name'));
            $('#frmModel #hdfModelId').val($this.data('model-id'));
            _variables.params.modelAction = "update";
        });

        $('body').on('click', '#searchbutton', function () {
            $('#mdlsearchmodel').modal('show');
        });

        $('body').on('click', '#searchdetails', function () {
            _doSearchItem();
        });

        $('body').on('click', '#refreshbutton', function () {
            _doRefreshItem();
        });
    }

    var _doNewItem = function() {
        $('#divModel').hide();
        $('#model-form').show();

        //buttons
        $('#searchbutton').hide();
        $('#refreshbutton').hide();
        $('#newbutton').hide();

        $('#discardbutton').show();
        $('#savebutton').show();
    }

    var _doDiscardItem = function () {
        $('#divModel').show();
        $('#model-form').hide();

        //buttons
        $('#searchbutton').show();
        $('#refreshbutton').show();
        $('#newbutton').show();

        $('#discardbutton').hide();
        $('#savebutton').hide();
    }

    var _doClearItem = function ()
    {
        $('#frmModel #ModelName').val(null);

    }

    var _doSaveItem = function ()
    {
        var elementUrl = "", $frmModel = $('#frmModel');

        if (_variables.params.modelAction === "save") {
            elementUrl = _variables.params.modelSaveUrl;
        }
        else if (_variables.params.modelAction === "update") {
            elementUrl = _variables.params.modelUpdateUrl;
        }

        if (_variables.params.modelAction != '') {
            $.ajax({
                url: elementUrl,
                type: 'POST',
                data: $frmModel.serialize(),
                success: function (data) {
                    $('#divResultContainer').show().html(data.alertMessage);
                    if (INFRA.ConvertToBoolean(data.isSuccess)) {
                        $('#divModel').html(data.model);
                        _doBindTblModel();
                    }
                }
            });
        }

        _doDiscardItem();
    }

    var _doSearchItem = function ()
    {
        var $frmSearchModel = $('#frmSearchModel');
        
        $.ajax({
            url: _variables.params.modelSearchUrl,
            type: 'POST',
            data: $frmSearchModel.serialize(),
            success: function (data) {
                $('#divModel').html(data);
                _doBindTblModel();

                $('#mdlsearchmodel').modal('hide');
                INFRA.doRemoveOpenedModal();
            }
        });
    }

    var _doRefreshItem = function () {
        var $frmSearchModel = $('#frmSearchModel');

        $.ajax({
            url: _variables.params.modelRefreshUrl,
            type: 'POST',
            success: function (data) {
                $('#divModel').html(data);
            }
        });
    }

    var initialize = function (options)
    {
        try {
            _variables.params = options || {};
        } catch (ex) {
            if (typeof console !== "undefined" && console !== null) {
                console.error("Error parsing inline options", ex);
            }
        }

        _doInitializeElements();
        _doBindTblModel();
    }
    
    
    return {
        initialize : initialize
    }


})();

$(document).ready(function () {
    var options = window.modelOptions;
    ADMINMANAGEMENTMODEL.initialize(options);
});