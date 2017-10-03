var ORDERMANAGEMENTINVENTORY = (function () {
    var _variables =
    {
        params: null,
        defaultSaveValue: null,
        defaultEditValue: null,
        defaultSearchValue: null,
        defaultEditQtyValue: null,
        qtyType: "",
        gvHeight: 180,
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


                }
            });

            $('#mdledit').modal('hide');
        }
        else {

        }


    }

    var _doUpdateQtyForm = function () {
        var $frm = $('#frmEditQty');
        _variables.qtyType = $('#frmEditQty #qtyType').data('kendoDropDownList').value();

        if (_variables.qtyType == "") {
            toastr.error('Please specify the Quantity type you want to do');
        } else {
            $.validator.unobtrusive.parse($frm);
            $frm.validate();
            if ($frm.valid()) {
                debugger;
                var editQtyData = $frm.serialize() + '&qtyType=' + _variables.qtyType;

                $.ajax({
                    url: _variables.params.updateQtyUrl,
                    type: 'POST',
                    data: editQtyData,
                    success: function (data) {
                        if (data.isSuccess == true) {
                            _doSearchDetails();

                        }
                        debugger;
                        $('#divResultContainer').show().html(data.alertMessage);
                    }
                });

                $('#mdleditqty').modal('hide');
            }
            else {

            }
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
        $('#frmEdit #Quantity').removeClass('disabled');

    }

    var _doSearchDetails = function () {
        var $gv = $("#gvProducts").data('kendoGrid'), $frm = $('#frmSearch');

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
            $('#mdlsearch').modal('show');
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
            $('#mdledit').modal('show');
        });

        $('body').on('click', '#editqtyitem', function () {
            $this = $(this);
            $('#mdleditqty').modal('show');
        });

        $('body').on('shown.bs.modal', '#mdlsearch', function () {
            $(document).off('focusin.modal');
        });

        $('body').on('shown.bs.modal', '#mdledit', function () {
            $(document).off('focusin.modal');

            $('#frmEdit #hdfProductId').val($this.data('product-id'));
            $('#frmEdit #ProductCode').val($this.data('product-code'));
            $('#frmEdit #ProductName').val($this.data('product-name'));
            $('#frmEdit #ProductDescription').val($this.data('product-description'));
            $('#frmEdit #ProductSize').val($this.data('product-size'));
            $('#frmEdit #CurrentNum').val($this.data('product-currentnum'));
            $('#frmEdit #DRNum').val($this.data('product-drnum'));
            $('#frmEdit #CartonNum').val($this.data('product-cartonnum'));
            $('#frmEdit #hdfQuantity').val($this.data('product-quantity'));

            $('#frmEdit #CategoryId').data('kendoDropDownList').value($this.data('category-id'));
            $('#frmEdit #QuantityUnitId').data('kendoDropDownList').value($this.data('quantity-unit-id'));

        });

        $('body').on('hidden.bs.modal', '#mdledit', function () {
            $('#frmEdit').deserialize(_variables.defaultEditValue, true, null);
        });

        $('body').on('click', '#updatedetails', function () {
            _doUpdateForm();
        });

        $('body').on('shown.bs.modal', '#mdleditqty', function () {
            $(document).off('focusin.modal');
            
            $('#frmEditQty #hdfProductId').val($this.data('product-id'));
            $('#frmEditQty #OldQuantity').val($this.data('product-quantity'));
            $('#frmEditQty #SupplierId').data('kendoDropDownList').value($this.data('product-supplierid'));



            $('#frmEditQty #ProductCode').val($this.data('product-code'));
            $('#frmEditQty #ProductName').val($this.data('product-name'));
            $('#frmEditQty #ProductDescription').val($this.data('product-description'));
            $('#frmEditQty #ProductSize').val($this.data('product-size'));
            $('#frmEditQty #CurrentNum').val($this.data('product-currentnum'));
            $('#frmEditQty #DRNum').val($this.data('product-drnum'));
            $('#frmEditQty #CartonNum').val($this.data('product-cartonnum'));

            $('#frmEditQty #CategoryId').data('kendoDropDownList').value($this.data('category-id'));
            $('#frmEditQty #QuantityUnitId').data('kendoDropDownList').value($this.data('quantity-unit-id'));

            _variables.qtyType = "";

        });

        $('body').on('hidden.bs.modal', '#mdleditqty', function () {
            $('#frmEditQty').deserialize(_variables.defaultEditQtyValue, true, null);
        });

        //$('body').on('click', '#plusbutton', function () {
        //    _variables.qtyType = "add";
        //    $('#frmEditQty #Quantity').val('0');
        //});

        //$('body').on('click', '#minusbutton', function () {
        //    _variables.qtyType = "minus";
        //    $('#frmEditQty #Quantity').val('0');
        //});

        $('body').on('click', '#updateqtydetails', function () {
            _doUpdateQtyForm();
        });



    }

    var _doInitializeKendoElements = function () {
        $("#CategoryId").kendoDropDownList({
            filter: 'contains',
            optionLabel: '- Select One -',
            dataTextField: "CategoryName",
            dataValueField: "CategoryId",
            dataSource: {
                transport: {
                    read: {
                        dataType: "json",
                        url: _variables.params.categoriesUrl
                    }
                }
            }
        });

        $("#SupplierId").kendoDropDownList({
            filter: 'contains',
            optionLabel: '- Select One -',
            dataTextField: "SupplierCode",
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
        

        $("#frmEditQty #SupplierId").kendoDropDownList({
            filter: 'contains',
            optionLabel: '- Select One -',
            dataTextField: "SupplierCode",
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

        

        $("#QuantityUnitId").kendoDropDownList({
            optionLabel: '- Select One -',
            dataTextField: "UnitName",
            dataValueField: "QuantityUnitID",
            dataSource: {
                transport: {
                    read: {
                        dataType: "json",
                        url: _variables.params.quanityUnitUrl
                    }
                }
            }
        });

        $("#frmEdit #CategoryId").kendoDropDownList({
            filter: 'contains',
            optionLabel: '- Select One -',
            dataTextField: "CategoryName",
            dataValueField: "CategoryId",
            dataSource: {
                transport: {
                    read: {
                        dataType: "json",
                        url: _variables.params.categoriesUrl
                    }
                }
            }
        });

        $("#frmEdit #SupplierId").kendoDropDownList({
            filter: 'contains',
            optionLabel: '- Select One -',
            dataTextField: "SupplierCode",
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

        $("#frmEdit #QuantityUnitId").kendoDropDownList({
            optionLabel: '- Select One -',
            dataTextField: "UnitName",
            dataValueField: "QuantityUnitID",
            dataSource: {
                transport: {
                    read: {
                        dataType: "json",
                        url: _variables.params.quanityUnitUrl
                    }
                }
            }
        });


        $("#frmEditQty #CategoryId").kendoDropDownList({
            filter:'contains',
            optionLabel: '- Select One -',
            dataTextField: "CategoryName",
            dataValueField: "CategoryId",
            dataSource: {
                transport: {
                    read: {
                        dataType: "json",
                        url: _variables.params.categoriesUrl
                    }
                }
            }
        });

        $("#frmEditQty #qtyType").kendoDropDownList({
            optionLabel: '- Select One -',
            dataTextField: "QuantityTypeName",
            dataValueField: "QuantityTypeName",
            dataSource: {
                transport: {
                    read: {
                        dataType: "json",
                        url: _variables.params.qtyTypeUrl
                    }
                }
            }
        });

        

        $("#frmEditQty #QuantityUnitId").kendoDropDownList({
            optionLabel: '- Select One -',
            dataTextField: "UnitName",
            dataValueField: "QuantityUnitID",
            dataSource: {
                transport: {
                    read: {
                        dataType: "json",
                        url: _variables.params.quanityUnitUrl
                    }
                }
            }
        });

        

        $("#SearchCategoryId").kendoDropDownList({
            filter:'contains',
            optionLabel: '- Select All -',
            dataTextField: "CategoryName",
            dataValueField: "CategoryId",
            dataSource: {
                transport: {
                    read: {
                        dataType: "json",
                        url: _variables.params.categoriesUrl
                    }
                }
            }
        });
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
        _variables.defaultEditQtyValue = $('#frmEditQty').serialize();
        _variables.defaultSearchValue = $('#frmSearch').serialize();
        _variables.qtyType = "";
    }

    return {
        initialize: initialize,
        _variables: _variables
    }
})();

$(document).ready(function () {
    var options = window.orderOptions;
    ORDERMANAGEMENTINVENTORY.initialize(options);
    var $gv = $('#gvProducts');
    

    $gv.kGridResizeHeight({
        height: ORDERMANAGEMENTINVENTORY._variables.gvHeight,
        willRefreshGrid: false,
        isManual: true
    });

    $(window).resize(function () {
        $gv.kGridResizeHeight({
            height: ORDERMANAGEMENTINVENTORY._variables.gvHeight,
            willRefreshGrid: false,
            isManual: true
        });
    });

});