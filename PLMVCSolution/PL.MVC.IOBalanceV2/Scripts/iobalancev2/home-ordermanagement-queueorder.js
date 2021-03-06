﻿var HOMEQUEUEMANAGEMENT = (function () {
    var _variables =
    {
        params: null,
        defaultSaveValue: null,
        defaultEditValue: null,
        defaultSearchValue: null,
        gvHeight: 200,
        defaultSalesOrderDetailValue: null,
        _salesOrderId: 0,
        _selectedRowHtml: ""
    };

    var _doShowSaveDetails = function () {

        $('#divBtnList').show();
        $('#divListQueueOrderDetail').show();
        $('#divListQueue').hide();
    }

    var _doDiscardSaveDetails = function () {
        $('#divBtnList').hide();
        $('#divListQueueOrderDetail').hide();
        $('#divListQueue').show();
    }

    var _doSearchOrderListDetails = function (salesOrderId) {
        var $gv = $("#gvQueueOrderDetailList").data('kendoGrid');


        $gv.dataSource.read({
            salesOrderId: salesOrderId
        });


        
    }


    var doRefreshOrderNumber = function (data) {
        var $gv = $("#gvQueueOrderDetailList").data('kendoGrid');


        var gridItemsCnt = 0;
        var dataItems = $gv.dataSource._data;
        $.each(dataItems, function () {
            var gridDataItem = $(this)[0];
            gridItemsCnt = gridItemsCnt + 1;
            gridDataItem.SalesOrderOrder = gridItemsCnt;

        });

        doComputeTotalAndCount();

        return data.SalesOrderOrder;

    }

    var FixPriceToStringDisplay = function (data) {
        return INFRA.CommaAdding(data.SalesPrice);
    }

    var FixQuantityToStringDisplay = function (data) {
        return INFRA.CommaAdding(data.Quantity);
    }

    var FixCommaStringDisplay = function (obj) {
        return INFRA.CommaAdding(obj.toFixed(2));
    }

    var _doSearchOrders = function () {
        var $gv = $("#gvQueueOrders").data('kendoGrid');

        $gv.dataSource.read();

    }

    var _doGetOrderAndOrderDetails = function (salesOrderId) {
        $.ajax({
            url: _variables.params.getOrderAndOrderDetailsUrl,
            type: 'POST',
            data: { salesOrderId: salesOrderId },
            success: function (data) {
                if (!INFRA.IsNullOrEmpty(data.order)) {
                    $('#frmSalesOrder #SalesNo').val(data.order.SalesNo);
                    $('#frmSalesOrder #CustomerId').val(data.order.customer.CustomerDropDownDisplay);
                    $('#frmSalesOrder #hdfCustomerId').val(data.order.customer.CustomerId);
                    $('*[data-toggle="tooltip"]').tooltip({ 'container': 'body', 'placement': 'right' });

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
                //$('#frmSalesOrderDetail #currProdQty').html("Current Quantity: " + data.Quantity);
            }
        });
    }

    var _doAddOrderInGrid = function () {
        var grid = $("#gvQueueOrderDetailList").data("kendoGrid");
        var salesNo = "", product = "", errMsg = "";
        var productId, originalPrice, price, quantity = 0, errCnt = 0;

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
            if (errMsg == 0) {
                errMsg += "quantity should be not 0";
            } else {
                errMsg += "<br/>quantity should be not 0";
            }

            errCnt++;
        } else if (originalPrice == "") {
            if (errMsg == 0) {
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
                });

                if (isSameProductId) {
                    if (errMsg == 0) {
                        errMsg += "product was already added in list";
                    } else {
                        errMsg += "<br/>product was already added in list";
                    }

                    errCnt++;
                }
            }

        }

        if (errCnt >= 1) {
            toastr.error(errMsg);
        } else {
            price = (originalPrice * quantity);


            grid.dataSource.add(
                {
                    SalesNo: salesNo,
                    ProductId: productId,
                    ProductDisplay: product,
                    SalesPrice: price.toFixed(2),
                    UnitPrice: originalPrice,
                    Quantity: quantity,
                    product: {
                        ProductDropDownDisplay: product
                    }
                });

            doClearWhenAdd();
            doComputeTotalAndCount();
        }



    }

    var doComputeTotalAndCount = function () {
        var gridData = $("#gvQueueOrderDetailList").data("kendoGrid");
        var cnt = 0, salesPrice = 0;
        if (!INFRA.IsNullOrEmpty(gridData._data)) {
            $.each(gridData._data, function () {
                var item = $(this)[0];

                var p = INFRA.ConvertToInteger(item.SalesPrice);
                salesPrice = (salesPrice + p);
                cnt++;
            });


        }
        $('#divListQueueOrderDetail #gridTotal').html(INFRA.CommaAdding(salesPrice.toFixed(2)));
        $('#divListQueueOrderDetail #gridCount').html(cnt);
    }

    var doClearWhenAdd = function () {
        $('#frmSalesOrderDetail #currProdQty').html('');
        $('#frmSalesOrderDetail').deserialize(_variables.defaultSalesOrderDetailValue, true, null);
    }

    var _doSaveOderInGrid = function () {
        var gridData = $("#gvQueueOrderDetailList").data("kendoGrid");
        var customerId = 0;

        var salesOrderDetails = [], newItem;

        customerId = $('#frmSalesOrder #hdfCustomerId').val();



        if (INFRA.IsNullOrEmpty(gridData._data)) {
            toastr.error('please fill in the details first');
        }
        else if (customerId == 0) {
            toastr.error('Customer field is required');
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
                    CustomerId: customerId,
                    salesOrderId: _variables._salesOrderId
                },
                success: function (data) {
                    $('.loader-mask').hide();
                    if (data.isSuccess) {
                        //_doClearAll();
                        doComputeTotalAndCount();
                    }

                    toastr.success(data.alertMsg);
                    _doDiscardSaveDetails();
                    _doSearchOrders();

                }
            });
        }




    }

    var _doSaveQueueOrder = function () {
        var gridData = $("#gvQueueOrderDetailList").data("kendoGrid");
        var salesNo = "";
        var customerId = 0;
        var salesOrderDetails = [], newItem;


        customerId = $('#frmSalesOrder #hdfCustomerId').val();


        if (INFRA.IsNullOrEmpty(gridData._data)) {
            toastr.error('please fill in the details first');
        }
        else if (customerId == 0) {
            toastr.error('Customer field is required');
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
                    CustomerId: customerId,
                    salesOrderId: _variables._salesOrderId
                },
                success: function (data) {
                    $('.loader-mask').hide();
                    if (data.isSuccess) {
                        //_doClearAll();
                        doComputeTotalAndCount();
                    }

                    toastr.success(data.alertMsg);
                    _doDiscardSaveDetails();
                    _doSearchOrders();
                }
            });
        }



    }

    var _doInitializeElements = function () {

        $('body').on('click', '#divListQueue #edititem', function () {
            $this = $(this);
            _doShowSaveDetails();
            _variables._salesOrderId = $this.data('queue-orderid');

            _doSearchOrderListDetails(_variables._salesOrderId);
            _doGetOrderAndOrderDetails(_variables._salesOrderId);

            
        });

        $('body').on('click', '#btnDiscard', function () {
            _doDiscardSaveDetails();
        });

        $('body').on('change', '#frmSalesOrderDetail #ProductId', function () {
            var customerId = $('#frmSalesOrder #hdfCustomerId').val();
            var productId = $('#frmSalesOrderDetail #ProductId').data('kendoDropDownList').value();
            _doGetPrice(customerId, productId);
        });

        $('body').on('click', '#btnAddSalesDetails', function () {
            _doAddOrderInGrid();
        });

        $('body').on('click', '#removeitem', function () {
            $this = $(this);
            $this.removeClass('selected');

            var grid = $("#gvQueueOrderDetailList").data("kendoGrid");

            grid.removeRow($this);
            grid.refresh();

            doComputeTotalAndCount();
        });

        $('body').on('click', '#divListQueueOrderDetail #edititem', function () {
            $this = $(this);
            $('[data-edit-selected="true"]').removeClass('selected');
            $this.addClass('selected');


            $('#mdleditorderlist').modal('show');
        });


        $('body').on('shown.bs.modal', '#mdleditorderlist', function () {


            $(document).off('focusin.modal');

            $('#frmEditOrderList #EditQuantity').val($this.data('orderlist-quantity'));
            $('#frmEditOrderList #EditPrice').val($this.data('orderlist-unitprice'));
            $('#EditProductName').val($this.data('orderlist-productname'));


        });

        $('body').on('hidden.bs.modal', '#mdleditorderlist', function () {



            $('#frmEditOrderList').deserialize(_variables.defaultEditValue, true, null);
        });

        $('body').on('click', '#btnSaveSalesDetails', function () {
            _doSaveOderInGrid();
        });

        $('body').on('click', '#btnEditOrderList', function () {
            var grid = $("#gvQueueOrderDetailList").data("kendoGrid");
            var caller = $('[data-edit-selected="true"].selected');

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




                $('#mdleditorderlist').modal('hide');
                INFRA.doRemoveOpenedModal();

                grid.refresh();
            }






        });

        $('body').on('click', '#btnSaveQueueDetails', function () {
            _doSaveQueueOrder();
        });
    }

    var _doInitializeKendoElements = function () {
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

        //_variables.defaultSaveValue = $('#frmSave').serialize();
        //_variables.defaultEditValue = $('#frmEdit').serialize();
        //_variables.defaultSearchValue = $('#frmSearch').serialize();

        _variables.defaultSalesOrderDetailValue = $('#frmSalesOrderDetail').serialize();
        _variables.defaultEditValue = $('#frmEditOrderList').serialize();

        INFRA.doRemoveOpenedModal();
    }

    return {
        initialize: initialize,
        _variables: _variables,
        doRefreshOrderNumber: doRefreshOrderNumber,
        FixPriceToStringDisplay: FixPriceToStringDisplay,
        FixQuantityToStringDisplay: FixQuantityToStringDisplay,
        FixCommaStringDisplay: FixCommaStringDisplay
    }
})();

$(document).ready(function () {
    var options = window.queueOptions;
    HOMEQUEUEMANAGEMENT.initialize(options);


    var $gvQueueList = $('#gvQueueOrders');
    var $gvQueueOrderList = $('#gvQueueOrderDetailList');


    $gvQueueList.kGridResizeHeight({
        height: HOMEQUEUEMANAGEMENT._variables.gvHeight,
        willRefreshGrid: false,
        isManual: true
    });

    $gvQueueOrderList.kGridResizeHeight({
        height: HOMEQUEUEMANAGEMENT._variables.gvHeight,
        willRefreshGrid: false,
        isManual: true
    });

    $(window).resize(function () {

        $gvQueueList.kGridResizeHeight({
            height: HOMEQUEUEMANAGEMENT._variables.gvHeight,
            willRefreshGrid: false,
            isManual: true
        });

        $gvQueueOrderList.kGridResizeHeight({
            height: HOMEQUEUEMANAGEMENT._variables.gvHeight,
            willRefreshGrid: false,
            isManual: true
        });
    });


});