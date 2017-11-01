var ORDERMANAGEMENTSALESORDER = (function () {
    var _variables =
    {
        params: null,
        defaultSaveValue: null,
        defaultSalesOrderValue: null,
        defaultSalesOrderDetailValue: null,
        defaultEditValue: null,
        defaultSearchValue: null,
        defaultEditQtyValue: null,
        qtyType: "",
        gvHeight: 180,
        gvDataHolder: null
    };



    var _doAddOrderInGrid = function () {
        var grid = $("#gvSalesOderList").data("kendoGrid");
        var salesNo = "", product = "", errMsg = "";
        var productId, originalPrice, price, quantity = 0, gridItemsCnt = 0, errCnt = 0;



        productId = $('#frmSalesOrderDetail #ProductId').data('kendoDropDownList').value();
        salesNo = $('#frmSalesOrder #SalesNo').val();
        product = $('#frmSalesOrderDetail #ProductId').data('kendoDropDownList').text();
        originalPrice = $('#frmSalesOrderDetail #SalesPrice').val();
        quantity = $('#frmSalesOrderDetail #Quantity').val();


        if (productId == "" || productId == 0) {
            errMsg += "please select product";
            errCnt++;
        }
        else if (quantity == "" || quantity == 0) {
            if (errCnt == 0) {
                errMsg += "quantity should be not 0";
            } else {
                errMsg += "<br/>quantity should be not 0";
            }

            errCnt++;
        } else if (originalPrice == "") {
            if (errCnt == 0) {
                errMsg += "price should be not blank";
            } else {
                errMsg += "<br/>price should be not blank";
            }

            errCnt++;
        } else {
            var isSameProductId = false;


            if (!INFRA.IsNullOrEmpty(grid._data)) {
                $.each(grid._data, function () {
                    var gridDataItem = $(this)[0];
                    var gridProductId;


                    gridProductId = gridDataItem.ProductId;

                    if (gridProductId == productId) {
                        isSameProductId = true;
                    }
                    gridItemsCnt++;
                });

                if (isSameProductId) {
                    if (errCnt == 0) {
                        errMsg += "product was already added in list";
                    } else {
                        errMsg += "<br/>product was already added in list";
                    }

                    errCnt++;
                }
                
                if (gridItemsCnt >= 16) {
                    if (errCnt == 0) {
                        errMsg += "product list is at maximum numbers in receipt";
                    } else {
                        errMsg += "<br/>product list is at maximum numbers in receipt";
                    }

                    errCnt++;
                }
            }

        }
        
        if (errCnt >= 1) {
            toastr.error(errMsg);
        } else {
            price = (originalPrice * quantity);
            gridItemsCnt = gridItemsCnt + 1;

            grid.dataSource.add(
                {
                    SalesOrderOrder: gridItemsCnt,
                    SalesNo: salesNo,
                    ProductId: productId,
                    ProductDisplay: product,
                    SalesPrice: price.toFixed(2),
                    UnitPrice: originalPrice,
                    Quantity: quantity
                });

            doClearWhenAdd();
            doComputeTotalAndCount();
        }



    }

    var _doSaveOderInGrid = function () {
        var gridData = $("#gvSalesOderList").data("kendoGrid");
        var salesNo = "";
        var customerId = 0;
        var salesOrderDetails = [], newItem;

        salesNo = $('#frmSalesOrder #SalesNo').val();
        customerId = $('#frmSalesOrder #CustomerId').data('kendoDropDownList').value();



        if (INFRA.IsNullOrEmpty(gridData._data)) {
            toastr.error('please add order details first');
        }
        else if(customerId == 0 || salesNo == "") {
            toastr.error('Customer and Sales Number field is required');
        }
        else {
            $.each(gridData._data, function () {
                var item = $(this)[0];

                newItem = {
                    SalesNo: item.SalesNo,
                    CustomerId: item.CustomerId,
                    ProductId: item.ProductId,
                    ProductDisplay: item.ProductDisplay,
                    SalesPrice: item.SalesPrice,
                    Quantity: item.Quantity,
                    UnitPrice: item.UnitPrice

                };
                salesOrderDetails.push(newItem);
            });

            $('.loader-mask').show();
            $.ajax({
                url: _variables.params.saveOrderUrl,
                type: 'POST',
                dataType: 'json',
                data: {
                    dto: salesOrderDetails,
                    SalesNo: salesNo,
                    CustomerId: customerId
                },
                success: function (data) {
                    $('.loader-mask').hide();
                    if (data.isSuccess) {
                        _doGetSalesNum();
                        _doClearAll();
                        doComputeTotalAndCount();

                        window.open(_variables.params.exportUrl + '?salesOrderId=' + data.salesOrderId + '&salesNo=' + data.salesNo + '&customerId=' + data.customerId);
                    }

                    toastr.success(data.alertMsg);
                }
            });
        }




    }

    var _doSaveQueueOrder = function () {
        var gridData = $("#gvSalesOderList").data("kendoGrid");
        var salesNo = "";
        var customerId = 0;
        var salesOrderDetails = [], newItem;
        salesNo = $('#frmSalesOrder #SalesNo').val();
        customerId = $('#frmSalesOrder #CustomerId').data('kendoDropDownList').value();


        if (INFRA.IsNullOrEmpty(gridData._data)) {
            toastr.error('please fill in the details first');
        }
        else if (customerId == 0 || salesNo == "") {
            toastr.error('Customer and Sales Number field is required');
        }
        else {
            $.each(gridData._data, function () {
                var item = $(this)[0];
                newItem = {
                    SalesNo: item.SalesNo,
                    CustomerId: item.CustomerId,
                    ProductId: item.ProductId,
                    ProductDisplay: item.ProductDisplay,
                    SalesPrice: item.SalesPrice,
                    Quantity: item.Quantity,
                    UnitPrice: item.UnitPrice

                };
                salesOrderDetails.push(newItem);
            });

            $('.loader-mask').show();
            $.ajax({
                url: _variables.params.queueOrderUrl,
                type: 'POST',
                dataType: 'json',
                data: {
                    dto: salesOrderDetails,
                    SalesNo: salesNo,
                    CustomerId: customerId
                },
                success: function (data) {
                    $('.loader-mask').hide();
                    if (data.isSuccess) {
                        _doGetSalesNum();
                        _doClearAll();
                        doComputeTotalAndCount();
                    }

                    toastr.success(data.alertMsg);
                }
            });
        }



    }

    var _doClearAll = function () {
        $('#frmSalesOrder').deserialize(_variables.defaultSalesOrderValue, true, null);
        doClearWhenAdd();
        $("#gvSalesOderList").data("kendoGrid").dataSource.data([]);

    }

    var _doGetSalesNum = function () {
        $.ajax({
            url: _variables.params.getSalesNumUrl,
            type: 'POST',
            success: function (data) {
                if (data.isSuccess) {
                    $('#frmSalesOrder #SalesNo').val(data.SalesNum);
                }
            }
        });
    }

    var _doGetPrice = function (customerId, productId) {
        $.ajax({
            url: _variables.params.getPriceUrl,
            type: 'POST',
            data: { customerId: customerId, productId: productId },
            success: function (data) {
                if (data != null) {
                    $('#frmSalesOrderDetail #SalesPrice').val(data.price);
                }
            }
        });
        doGetProductDetails(productId);
    }

    var doComputeTotalAndCount = function () {
        var gridData = $("#gvSalesOderList").data("kendoGrid");
        var cnt = 0, salesPrice = 0;

        if (!INFRA.IsNullOrEmpty(gridData._data)) {
            $.each(gridData._data, function () {
                var item = $(this)[0];

                var p = INFRA.ConvertToInteger(item.SalesPrice);
                salesPrice = (salesPrice + p);
                cnt++;
            });


        }
        $('#divList #gridTotal').html(INFRA.CommaAdding(salesPrice.toFixed(2)));
        $('#divList #gridCount').html(cnt);
    }

    var FixPriceToStringDisplay = function (data) {
        return INFRA.CommaAdding(data.SalesPrice);
    }

    var doClearWhenAdd = function () {
        $('#frmSalesOrderDetail #currProdQty').html('');
        $('#frmSalesOrderDetail').deserialize(_variables.defaultSalesOrderDetailValue, true, null);
    }

    var doGetProductDetails = function (productId) {
        $.ajax({
            url: _variables.params.productByIdUrl,
            type: 'GET',
            data: { productId: productId },
            success: function (data) {
                if (data.Quantity <= 0) {
                    $('#frmSalesOrderDetail #currProdQty').html("Current Quantity: <span style='color:red'>" + data.Quantity + "</span>");
                } else {
                    $('#frmSalesOrderDetail #currProdQty').html("Current Quantity: <span>" + data.Quantity + "</span>");
                }
                
                $('*[data-toggle="tooltip"]').tooltip({ 'container': 'body', 'placement': 'right' });

            }
        });
    }



    var _doInitializeElements = function () {

        $('body').on('click', '#btnnewform', function () {
            _doShowSaveForm();
        });

        $('body').on('click', '#btnAddSalesDetails', function () {
            _doAddOrderInGrid();
        });

        $('body').on('click', '#edititem', function () {
            $this = $(this);

            $('[data-edit-selected="true"]').removeClass('selected');
            $this.addClass('selected');

            $('#mdleditorderlist').modal({
                'show': true,
                'backdrop': 'static',
                'keyboard': false
            });
        });

        $('body').on('click', '#removeitem', function () {
            $this = $(this);
            $this.removeClass('selected');

            var grid = $("#gvSalesOderList").data("kendoGrid");
            grid.removeRow($this);
            grid.refresh();

            doComputeTotalAndCount();
        });

        $('body').on('shown.bs.modal', '#mdleditorderlist', function () {
            $(document).off('focusin.modal');

            $('#frmEditOrderList #EditQuantity').val($this.data('orderlist-quantity'));
            $('#frmEditOrderList #EditPrice').val($this.data('orderlist-unitprice'));
            $('#mdleditorderlist #EditProductName').html($this.data('orderlist-productname'));
            

            //$('#EditBranchID').data('kendoDropDownList').value($this.data('discount-branchid'));
        });

        $('body').on('hidden.bs.modal', '#mdleditorderlist', function () {
            $('#frmEditOrderList').deserialize(_variables.defaultEditValue, true, null);
        });

        $('body').on('change', '#frmSalesOrderDetail #ProductId', function () {
            var customerId = $('#frmSalesOrder #CustomerId').data('kendoDropDownList').value();
            var productId = $('#frmSalesOrderDetail #ProductId').data('kendoDropDownList').value();


            if (customerId == "" || customerId == 0) {
                toastr.error('please select customer first');
            } else {
                _doGetPrice(customerId, productId);
            }

        });

        $('body').on('click', '#btnSaveSalesDetails', function () {
            _doSaveOderInGrid();
        });

        $('body').on('click', '#btnSaveQueueDetails', function () {
            _doSaveQueueOrder();
        });

        $('body').on('click', '#btnEditOrderList', function () {
            var grid = $("#gvSalesOderList").data("kendoGrid");
            var caller = $('[data-edit-selected="true"].selected');
            var $frm = $('#frmEditOrderList');

            $.validator.unobtrusive.parse($frm);
            $frm.validate();
            if ($frm.valid()) {


                var dataItems = grid.dataSource._data;
                var editedQty = 0, editedPrice = 0, salesPrice = 0, errCnt = 0;
                var errMsg = "";
                var productIdSelected = caller.data('orderlist-productid');

                editedQty = $('#frmEditOrderList #EditQuantity').val();
                editedPrice = $('#frmEditOrderList #EditPrice').val();
                salesPrice = editedPrice * editedQty;

                if (editedQty == "" || editedQty == 0) {
                    if (errMsg == 0) {
                        errMsg += "quantity should be not 0";
                    } else {
                        errMsg += "<br/>quantity should be not 0";
                    }

                    errCnt++;
                } else if (editedPrice == "") {
                    if (errMsg == 0) {
                        errMsg += "price should be not blank";
                    } else {
                        errMsg += "<br/>price should be not blank";
                    }

                    errCnt++;
                }

                if (errCnt >= 1) {
                    toastr.error(errMsg);
                } else {
                    $.each(dataItems, function () {
                        var gridDataItem = $(this)[0];
                        var gridProductId;


                        gridProductId = gridDataItem.ProductId;


                        if (gridProductId == productIdSelected) {

                            gridDataItem.Quantity = editedQty;
                            gridDataItem.SalesPrice = salesPrice.toFixed(2);
                            gridDataItem.UnitPrice = editedPrice;
                        }


                    });
                    grid.refresh();
                    $('#mdleditorderlist').modal('hide');
                    INFRA.doRemoveOpenedModal();
                }

            } else {
                $('.loader-mask').hide();
                toastr.error('An error occured during the process. Please check the details or the required fields.');
            }

            
            

        });

    }

    var _doInitializeKendoElements = function () {
        $("#frmSalesOrder #CustomerId").kendoDropDownList({
            filter: 'contains',
            optionLabel: '- Select One -',
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

        $("#frmSalesOrderDetail #ProductId").kendoDropDownList({
            filter: 'contains',
            optionLabel: '- Select One -',
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
        _variables.defaultEditValue = $('#frmEditOrderList').serialize();
        _variables.defaultSalesOrderDetailValue = $('#frmSalesOrderDetail').serialize();
        _variables.defaultSalesOrderValue = $('#frmSalesOrder').serialize();

        _variables.defaultEditQtyValue = $('#frmEditQty').serialize();
        _variables.defaultSearchValue = $('#frmSearch').serialize();
        _variables.qtyType = "";
        _doGetSalesNum();
        doComputeTotalAndCount();

    }

    return {
        initialize: initialize,
        _variables: _variables,
        FixPriceToStringDisplay: FixPriceToStringDisplay
    }
})();

$(document).ready(function () {
    var options = window.salesOrderOptions;
    ORDERMANAGEMENTSALESORDER.initialize(options);

    var $gv = $('#gvSalesOderList');


    $gv.kGridResizeHeight({
        height: ORDERMANAGEMENTSALESORDER._variables.gvHeight,
        willRefreshGrid: false,
        isManual: true
    });

    $(window).resize(function () {
        $gv.kGridResizeHeight({
            height: ORDERMANAGEMENTSALESORDER._variables.gvHeight,
            willRefreshGrid: false,
            isManual: true
        });
    });
});