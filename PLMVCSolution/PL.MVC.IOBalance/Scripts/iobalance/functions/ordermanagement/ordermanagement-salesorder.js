var ORDERMANAGEMENTSALESORDER = (function () {
    var _variables = {
        params: null,
        loaderMsg: 'Please wait while we process your request',
        UpdateMsg: "",
        gvProductHeight: 240,
        gvSavedSalesOrderHeight: 230,
        defaultSalesReceiptForm: null,
        defaultProductSearchForm: null
    }

    var _doInitializeElements = function () {
        $('body').on('click', '#btnaddsalesorder', function () {
            _doAddSalesOrder();
        });

        $('body').on('click', '#btnsavesalesorder', function () {
            _doSaveSalesOrder();
        });

        $('body').on('click', '#btnclickaddsalesorder', function () {
            var $elem = $(this);
            if (_doCountExistingProduct()) {
                _clickAddSalesOrder($elem);
            }



        });

        $('body').on('click', '#searchproduct', function () {
            $('#mdlsearchproduct').modal('show');
        });

        $('body').on('click', '#btnfiltersalesorder', function () {
            $('#mdlsearchsavedsalesorder').modal('show');
        });

        $('body').on('shown.bs.modal', '#mdlsearchproduct', function () {
            $(document).off('focusin.modal');
        });

        $('body').on('shown.bs.modal', '#mdlsearchsavedsalesorder', function () {
            $(document).off('focusin.modal');
        });

        $('body').on('click', '#searchdetails', function () {
            _doSearchItem();
        });

        $('body').on('click', '#removeitem', function () {
            var $this = $(this);
            _doRemoveSalesOrderItem($this);
        });

        $('body').on('blur', '#changeqty', function () {
            var $this = $(this);
            _doChangeQtyInSalesOrder($this);
        });

        $('body').on('keypress', '#changeqty', function (e) {
            var $this = $(this);

            if (e.keyCode == 13) {
                _doChangeQtyInSalesOrder($this);
            }
        });

        $('body').on('click', '#btnlistofsalesorder', function (e) {

            _doShowSavedSalesOrder();
            _doBindTblSavedSalesOrder();
        });

        $('body').on('click', '#btnsalesorderform', function () {
            _doShowSalesOrderForm();
        });

        $('body').on('click', '#refreshproduct', function () {
            _doRefreshItem();
        });

        $('body').on('click', '#searchsavedsalesorder', function () {
            _doSearchSavedSalesOrder();
        });

        $('body').on('click', '#btnrefreshsalesorder', function () {
            _doRefreshSavedSalesOrder();
        });

        $('body').on('click', '#removeproductfilter', function () {
            //$('#SearchProductCode').val('');
            //$('#SearchProductName').val('');
            //$('#SearchProductExtension').val('');
            //$('#frmSearchProduct #SearchProductCategory').data('kendoDropDownList').value(null);
            //$('#frmSearchProduct #SearchProductBranch').data('kendoDropDownList').value(null);
            $('#frmSearchProduct').deserialize(_variables.defaultProductSearchForm, true, null);
        });

        $('body').on('click', '#removesofilter', function () {
            $('#dpFrom').val('');
            $('#dpTo').val('');
            $('#ReceiptNumber').val('');
            $('#mdlsearchsavedsalesorder #SearchSOProductBranch').data('kendoDropDownList').value(null);
        });

        $('body').on('click', '#refreshcustomer', function () {
            $("#SalesCustomer").kendoDropDownList({
                filter: "contains",
                optionLabel: 'Select Customer',
                dataTextField: "DropdownDisplay",
                dataValueField: "CustomerID",
                dataSource: {
                    transport: {
                        read: {
                            dataType: "json",
                            url: _variables.params.customerGetUrl
                        }
                    }
                }
            });
        });

        $('body').on('click', '#refreshbranch', function () {
            $('#SalesBranch').kendoDropDownList({
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
        });

        $('body').on('keypress', '#txtBarCode', function (e) {
            if (e.which == 13) {
                if (INFRA.IsNullOrEmpty($('#txtBarCode').val())) {
                    toastr.error('Product bar code should not be blank/empty');
                } else {
                    _doSearchBarCode();
                }
            }
        });

        $('body').on('click', '#btnBarCode', function () {
            if (INFRA.IsNullOrEmpty($('#txtBarCode').val())) {
                toastr.error('Product bar code should not be blank/empty');
            } else {
                _doSearchBarCode();
            }

        });
    }

    var _doInitializeKendoElements = function () {
        $("#SalesCustomer").kendoDropDownList({
            filter: "contains",
            optionLabel: 'Select Customer',
            dataTextField: "DropdownDisplay",
            dataValueField: "CustomerID",
            dataSource: {
                transport: {
                    read: {
                        dataType: "json",
                        url: _variables.params.customerGetUrl
                    }
                }
            }
        });

        $('#SalesBranch').kendoDropDownList({
            filter: "contains",
            optionLabel: 'Select Branch',
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

        $("#dpFrom").kendoDatePicker({
            format: "MM/dd/yyyy"
        });
        $("#dpTo").kendoDatePicker({
            format: "MM/dd/yyyy"
        });

        //$(".SalesUnit").kendoDropDownList({
        //    optionLabel: 'Select item',
        //    dataTextField: "UnitDesc",
        //    dataValueField: "UnitID",
        //    dataSource: {
        //        transport: {
        //            read: {
        //                dataType: "json",
        //                url: _variables.params.unitGetUrl
        //            }
        //        }
        //    }
        //});

        //$('#ProductCategory').kendoDropDownList({
        //    filter: "startswith",
        //    optionLabel: 'Select item',
        //    dataTextField: "CategoryDisplay",
        //    dataValueField: "CategoryID",
        //    change: doChangeProductCategory,
        //    dataSource: {
        //        serverFiltering: true,
        //        transport: {
        //            read: {
        //                dataType: "json",
        //                url: _variables.params.categoryGetUrl
        //            }
        //        }
        //    }
        //});

        //$("#SalesProduct").kendoDropDownList({
        //    filter: "startswith",
        //    optionLabel: 'Select item',
        //    dataTextField: "DropDownDisplay",
        //    dataValueField: "ProductID",
        //    dataSource: {
        //        transport: {
        //            read: {
        //                dataType: "json",
        //                url: _variables.params.productGetUrl
        //            }
        //        }
        //    }
        //});

        $('#SearchProductModel').kendoDropDownList({
            filter: "startswith",
            optionLabel: 'Select All',
            dataTextField: "ModelName",
            dataValueField: "ModelID",
            dataSource: {
                transport: {
                    read: {
                        dataType: "json",
                        url: _variables.params.modelGetUrl
                    }
                }
            }
        });

        $('#SearchProductCategory').kendoDropDownList({
            filter: "startswith",
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

        $('#SearchProductBranch').kendoDropDownList({
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

    var doShowTrackerLoader = function (msg) {
        $('body').showLoader({
            type: "page",
            message: msg
        });
    }

    var doRemoveTrackerLoader = function () {
        $('body').hideLoader();
    }

    //#Region Private Methods
    var _doAddSalesOrder = function () {
        $frmSalesOrderDetails = $('#frmSalesOrderDetails');
        $gvSalesOrderDetails = $('#gvSalesOrderDetails tbody');
        var tempTable, curRow;

        $.ajax({
            url: _variables.params.addSalesOrderUrl,
            type: 'POST',
            data: $frmSalesOrderDetails.serialize(),
            success: function (data) {
                if (INFRA.ConvertToBoolean(data.isSuccess)) {
                    tempTable = $('<table></table>');
                    tempTable.html(data.salesOrder);
                    curRow = tempTable.find('tbody > tr');
                    if ($gvSalesOrderDetails.find('tr').length === 0) {
                        $gvSalesOrderDetails.html(curRow);

                    } else {
                        $(curRow).appendTo($gvSalesOrderDetails);
                    }
                    //_doBindTblProduct();
                }
                $('#divResultContainer').show().html(data.alertMessage);
            }
        });
    }
    var _clickAddSalesOrder = function (element) {
        var productId = element.data('product-id');
        var branchId = null;
        var $gvSalesOrderDetails = $('#gvSalesOrderDetails tbody');
        var tempTable, curRow, isAddSuccess;

        $hdfUserTypeId = $('#hdfUserTypeId');
        if (INFRA.ConvertToInteger($hdfUserTypeId.val()) === 1) {
            branchId = $('#SalesBranch').val();

        }


        if (INFRA.IsNullOrEmpty(branchId) && INFRA.ConvertToInteger($hdfUserTypeId.val()) === 1) {
            $('#frmSalesOrder [data-valmsg-for="BranchID"]').html("<br/>Branch field is a required field");
        } else {
            $('#frmSalesOrder [data-valmsg-for="BranchID"]').html("");

            $.ajax({
                url: _variables.params.addClickSalesOrderUrl,
                type: 'POST',
                data: { productId: productId, branchId: branchId },
                success: function (data) {
                    if (INFRA.ConvertToBoolean(data.isSuccess)) {
                        tempTable = $('<table></table>');
                        tempTable.html(data.salesOrder);
                        curRow = tempTable.find('tbody > tr');
                        if ($gvSalesOrderDetails.find('tr').length === 0) {
                            $gvSalesOrderDetails.html(curRow);

                        } else {
                            isAddSuccess = _doCheckExistingProduct(productId);
                            if (INFRA.ConvertToBoolean(isAddSuccess)) {
                                $(curRow).appendTo($gvSalesOrderDetails);
                            }

                        }
                        //_doBindTblProduct();
                        _doCountTotalSalesOrder();
                        _doComputeTotalAmount();
                    }
                    $('#divResultContainer').show().html(data.alertMessage);
                }
            });
        }



    }
    var _clickAddSalesOrderPerProductId = function (productId) {
        //var productId = element.data('product-id');
        var branchId = null;
        var $gvSalesOrderDetails = $('#gvSalesOrderDetails tbody');
        var tempTable, curRow, isAddSuccess;

        $hdfUserTypeId = $('#hdfUserTypeId');
        if (INFRA.ConvertToInteger($hdfUserTypeId.val()) === 1) {
            branchId = $('#SalesBranch').val();

        }


        if (INFRA.IsNullOrEmpty(branchId) && INFRA.ConvertToInteger($hdfUserTypeId.val()) === 1) {
            $('#frmSalesOrder [data-valmsg-for="BranchID"]').html("<br/>Branch field is a required field");
        } else {
            $('#frmSalesOrder [data-valmsg-for="BranchID"]').html("");

            $.ajax({
                url: _variables.params.addClickSalesOrderUrl,
                type: 'POST',
                data: { productId: productId, branchId: branchId },
                success: function (data) {
                    if (INFRA.ConvertToBoolean(data.isSuccess)) {
                        tempTable = $('<table></table>');
                        tempTable.html(data.salesOrder);
                        curRow = tempTable.find('tbody > tr');
                        if ($gvSalesOrderDetails.find('tr').length === 0) {
                            $gvSalesOrderDetails.html(curRow);

                        } else {
                            isAddSuccess = _doCheckExistingProduct(productId);
                            if (INFRA.ConvertToBoolean(isAddSuccess)) {
                                $(curRow).appendTo($gvSalesOrderDetails);
                            }

                        }
                        //_doBindTblProduct();
                        _doCountTotalSalesOrder();
                        _doComputeTotalAmount();
                    }
                    $('#divResultContainer').show().html(data.alertMessage);
                }
            });
        }
    }

    var _doCountExistingProduct = function () {
        $gvSalesOrderDetails = $('#gvSalesOrderDetails');

        var count = $gvSalesOrderDetails.find('tbody > tr');
        if (count.length >= 16) {
            toastr.error('Product should be only 16 items');
            return false;
        } else {
            return true;
        }

    }
    var _doCheckExistingProduct = function (productId) {
        var productid = productId;
        var $gvSalesOrderDetails = $('#gvSalesOrderDetails');
        var productRowId = 0;
        var isSuccess = true;


        $gvSalesOrderDetails.find('tbody > tr').each(function () {
            $this = $(this);
            productRowId = $this.data('product-id');

            if (productRowId == productid) {
                toastr.error('this product is already in the order');
                isSuccess = false;
                return isSuccess;
            } else {
                return isSuccess;
            }

        });

        return isSuccess;
    }
    var _doComputeTotalAmount = function () {
        var total = 0;
        var $gvSalesOrderDetails = $('#gvSalesOrderDetails');

        $gvSalesOrderDetails.find('tbody > tr').each(function () {
            //debugger;
            $tds = $(this).find('td');
            var totalPricePerItemQty;
            totalPricePerItemQty = INFRA.ConvertToFloat($tds.eq(4).find('span').text());

            total = total + totalPricePerItemQty
        });
        $('#salesordertotal').html(total);
    }
    var _doCountTotalSalesOrder = function () {
        var count = 0;
        var $gvSalesOrderDetails = $('#gvSalesOrderDetails');


        $gvSalesOrderDetails.find('tbody > tr').each(function () {
            $tds = $(this).find('td');
            count = count + 1;
        });
        $('#salesordercount').html(count);
    }
    var _doRemoveSalesOrderItem = function (element) {
        var $elem = (element);
        var productid;

        productid = $elem.data('product-id');
        $elem.parent().parent().parent().parent().remove();

        _doComputeTotalAmount();
        _doCountTotalSalesOrder();
    }
    var _doChangeQtyInSalesOrder = function (element) {
        var $elem = element;
        var productid, quantity, unitprice, totalpriceperitemqty;

        productid = $elem.data('product-id');
        quantity = $elem.val();
        unitprice = $elem.data('product-unit-price');


        if (!INFRA.IsNullOrEmpty(quantity)) {
            totalpriceperitemqty = quantity * unitprice;
            $elem.parent().parent().parent().parent().find('td').eq(4).find('span').text(INFRA.ConvertToFloat(totalpriceperitemqty).toFixed(2));
        }

        _doComputeTotalAmount();
    }
    var _doClearItemsInSalesOrder = function () {
        $gvSalesOrderDetails.find('tbody > tr').each(function () {
            $this = $(this);
            $this.remove();
        });

        //$('#SalesCustomer').data('kendoDropDownList').value(null);
        //$('[data-valmsg-for="CustomerId"]').html("").removeClass("error");
        debugger;
        $('#frmSalesOrder').deserialize(_variables.defaultSalesReceiptForm, true, null);

        _doComputeTotalAmount();
        _doCountTotalSalesOrder();
    }

    var _doShowSavedSalesOrder = function () {
        $('#divsalesorderform').hide();
        $('#divlistofsalesorders').show();

        //hideshowbuttons
        $('#btnlistofsalesorder').hide();
        $('#btnsavesalesorder').hide();
        $('#btnsalesorderform').show();
    }
    var _doShowSalesOrderForm = function () {
        $('#divlistofsalesorders').hide();
        $('#divsalesorderform').show();

        //hideshowbuttons
        $('#btnsalesorderform').hide();
        $('#btnsavesalesorder').show();
        $('#btnlistofsalesorder').show();
    }
    var _doSaveSalesOrder = function () {
        debugger;
        var salesOrderDetail = [], salesOrder = [], newSalesOrder, newItem, $tds;
        var salesOrders = [];
        var isDataReady = true;

        doShowTrackerLoader(_variables.loaderMsg);

        $frmSalesOrder = $('#frmSalesOrder');
        $gvSalesOrderDetails = $('#gvSalesOrderDetails');
        $ddlCustomer = $('#SalesCustomer');
        $SalesBranch = $('#SalesBranch');

        $gvSalesOrderDetails.find('tbody > tr').each(function () {
            $tds = $(this).find('td');
            var productId,
                quantity,
                unitPrice,
                unitText,
                totalPricePerItemQty;

            productId = $tds.eq(0).find('input:hidden').eq(0).val();
            quantity = $tds.eq(1).find('span').find('input').val();
            unitText = $tds.eq(2).find('span').find('select').val();
            unitPrice = $tds.eq(3).find('span').text();
            totalPricePerItemQty = $tds.eq(4).find('span').text();

            newItem = {
                ProductID: productId,
                Quantity: quantity,
                UnitDesc: unitText,
                UnitPrice: unitPrice,
                TotalPricePerItemQty: totalPricePerItemQty
            };
            salesOrderDetail.push(newItem);
        });

        if (INFRA.IsNullOrEmpty($ddlCustomer.val())) {
            isDataReady = false;
            $('[data-valmsg-for="CustomerId"]').html("<br/>Customer field is a required field").removeClass("field-validation-valid").addClass("error");
            doRemoveTrackerLoader();
        }
        else {

            $hdfUserTypeId = $('#hdfUserTypeId');
            if (INFRA.ConvertToInteger($hdfUserTypeId.val()) === 1) {
                if (INFRA.IsNullOrEmpty($SalesBranch.val())) {
                    isDataReady = false;
                    $('#frmSalesOrder [data-valmsg-for="BranchID"]').html("<br/>Branch field is a required field").removeClass("field-validation-valid").addClass("error");
                    doRemoveTrackerLoader();
                } else {
                    $('#frmSalesOrder [data-valmsg-for="BranchID"]').html("").removeClass("error");
                }


            }


            if (INFRA.ConvertToBoolean(isDataReady)) {
                salesOrders = JSON.stringify({
                    'salesOrderDetailDto': salesOrderDetail,
                    'customerId': $ddlCustomer.val()
                });


                $.ajax({
                    contentType: 'application/json; charset=utf-8',
                    type: "POST",
                    dataType: 'json',
                    url: _variables.params.saveSalesOrderUrl,
                    data: salesOrders,
                    success: function (data) {
                        //$('body').hideLoader();
                        doRemoveTrackerLoader();
                        if (data.isSuccess) {
                            _doRefreshItem();
                            $('#divSavedSalesOrder').html(data.salesOrder);
                            //_doBindTblSavedSalesOrder();
                            _doSearchSavedSalesOrder();
                            _doClearItemsInSalesOrder();
                        }
                        $('#divResultContainer').show().html(data.alertMessage);
                    }
                });
            } else {
                doRemoveTrackerLoader();
            }





        }




    }
    var _doSearchSavedSalesOrder = function () {

        //$frmSearchSavedSalesOrder = $('#frmSearchSavedSalesOrder');
        //doShowTrackerLoader(_variables.loaderMsg);
        //$.ajax({
        //    url: _variables.params.searchSavedSalesOrderUrl,
        //    type: 'POST',
        //    data: $frmSearchSavedSalesOrder.serialize(),
        //    success: function (data) {
        //        doRemoveTrackerLoader();
        //        $('#divSavedSalesOrder').html(data);
        //        _doBindTblSavedSalesOrder();
        //        $('#mdlsearchsavedsalesorder').modal('hide');
        //    }
        //});
        //INFRA.doRemoveOpenedModal();


        var $gvSavedSalesOrder = $("#gvSavedSalesOrder").data('kendoGrid'), $frmSearchSavedSalesOrder = $('#frmSearchSavedSalesOrder');

        //doShowTrackerLoader(_variables.loaderMsg);

        $gvSavedSalesOrder.dataSource.read($frmSearchSavedSalesOrder.serializeToJson());

        //if ($gvSavedSalesOrder.pager.page() !== 1) {
        //    $gvSavedSalesOrder.pager.page(1);
        //}

        //doRemoveTrackerLoader();
        $('#mdlsearchsavedsalesorder').modal('hide');
        INFRA.doRemoveOpenedModal();
    }
    var _doRefreshSavedSalesOrder = function () {
        //$frmSearchSavedSalesOrder = $('#frmSearchSavedSalesOrder');
        //doShowTrackerLoader(_variables.loaderMsg);
        //$.ajax({
        //    url: _variables.params.savedSalesOrderRefreshUrl,
        //    type: 'POST',
        //    data: $frmSearchSavedSalesOrder.serialize(),
        //    success: function (data) {
        //        doRemoveTrackerLoader();
        //        $('#divSavedSalesOrder').html(data);
        //        _doBindTblSavedSalesOrder();
        //    }
        //});
        _doSearchSavedSalesOrder();
    }
    //#EndRegion Private Methods





    //#Region Private Methods for product
    var _doSearchItem = function () {

        var $gvProduct = $("#gvProduct").data('kendoGrid'), $frmSearchProduct = $('#frmSearchProduct');

        doShowTrackerLoader(_variables.loaderMsg);

        $gvProduct.dataSource.read($frmSearchProduct.serializeToJson(), false);


        doRemoveTrackerLoader();
        $('#mdlsearchproduct').modal('hide');
        INFRA.doRemoveOpenedModal();
    }

    var _doSearchBarCode = function () {
        //var $gvProduct = $("#gvProduct").data('kendoGrid');
        //var searchModel = [], branchId;
        //doShowTrackerLoader(_variables.loaderMsg);

        //$hdfUserTypeId = $('#hdfUserTypeId');
        //if (INFRA.ConvertToInteger($hdfUserTypeId.val()) === 1) {
        //    branchId = $('#SalesBranch').val();

        //}

        //if (INFRA.IsNullOrEmpty(branchId) && INFRA.ConvertToInteger($hdfUserTypeId.val()) === 1) {
        //    $('#frmSalesOrder [data-valmsg-for="BranchID"]').html("<br/>Branch field is a required field");
        //} else {
        //    $('#frmSalesOrder [data-valmsg-for="BranchID"]').html("");

        //    if (INFRA.ConvertToInteger($hdfUserTypeId.val()) === 1) {
        //        searchModel = {
        //            BarCode: $('#txtBarCode').val(),
        //            BranchId: branchId
        //        };
        //    } else {
        //        searchModel = {
        //            BarCode: $('#txtBarCode').val()
        //        };
        //    }

        //    debugger;
        //    var dataItem = $gvProduct.dataSource.read(searchModel);

        //    if (dataItem.promise.length > 0) {
        //        var selectedItem = $gvProduct.table.find('tbody> tr').find('a[data-product-barcode=' + searchModel.BarCode + ']');
        //        _clickAddSalesOrder(selectedItem);

        //    } else {

        //    }
        //    $('#txtBarCode').val("");
        //}


        //doRemoveTrackerLoader();

        var barCode, $branchId;
        var $hdfUserTypeId = $('#hdfUserTypeId');
        barCode = $('#txtBarCode').val();

        if (INFRA.ConvertToInteger($hdfUserTypeId.val()) === 1) {
            $branchId = $('#SalesBranch').val();
        }

        if (INFRA.IsNullOrEmpty($branchId) && INFRA.ConvertToInteger($hdfUserTypeId.val()) === 1) {
            $('#frmSalesOrder [data-valmsg-for="BranchID"]').html("<br/>Branch field is a required field");
        } else {
            $('#frmSalesOrder [data-valmsg-for="BranchID"]').html("");

            doShowTrackerLoader(_variables.loaderMsg);

            $.ajax({
                url: _variables.params.searchProductPerBarCodeUrl,
                type: 'POST',
                data: { barCode: barCode, branchIdAdmin: $branchId },
                success: function (data) {
                    doRemoveTrackerLoader();
                    if (INFRA.ConvertToBoolean(data.hasProduct)) {
                        if (_doCountExistingProduct()) {
                            _clickAddSalesOrderPerProductId(data.list.ProductID);
                        }
                    } else {
                        toastr.error('Product maybe not registered');
                    }

                    $('#txtBarCode').val("");
                }
            });
        }


    }
    var _doRefreshItem = function () {
        //$frmSearchProduct = $('#frmSearchProduct');
        //doShowTrackerLoader(_variables.loaderMsg);

        //$.ajax({
        //    url: _variables.params.inventoryRefreshUrl,
        //    type: 'POST',
        //    data: $frmSearchProduct.serialize(),
        //    success: function (data) {
        //        doRemoveTrackerLoader();
        //        //if (INFRA.ConvertToBoolean(data.isSuccess)) {
        //        $('#divproducts').html(data);
        //        _doBindTblProduct();

        //        //}
        //    }
        //});

        _doSearchItem();
    }
    //#EndRegion Private Methods for product


    var doChangeProductCategory = function () {
        var $category = $('#ProductCategory').data('kendoDropDownList');

        if (!INFRA.IsNullOrEmpty($category.value())) {
            $("#SalesProduct").kendoDropDownList({
                filter: "startswith",
                optionLabel: 'Select item',
                dataTextField: "DropDownDisplay",
                dataValueField: "ProductID",
                dataSource: {
                    transport: {
                        read: {
                            dataType: "json",
                            url: _variables.params.productByCategoryIdUrl,
                            data: {
                                categoryId: $category.value()
                            }
                        }
                    }
                }
            });
        } else {
            $("#SalesProduct").kendoDropDownList({
                filter: "startswith",
                optionLabel: 'Select item',
                dataTextField: "DropDownDisplay",
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

    //#Region Private Methods for KendoGrid
    var _doBindTblProduct = function () {
        var $gvProduct = $('#gvProduct'), schemaField = {}, columns = [],
            width, field;


        //$gvProduct.find('th').each(function () {
        //    $this = $(this);
        //    width = INFRA.ReplaceIfNullOrEmpty($this.data('width'), 100);
        //    field = $this.data('field');

        //    column = {
        //        width: width,
        //        field: field,
        //        attributes: {
        //            "class": "text-center"
        //        }
        //    }
        //    columns.push(column);
        //});

        //$gvProduct.kendoGrid({
        //    sortable: false,
        //    scrollable: true,
        //    pageable: true,
        //    dataSource: {
        //        pageSize: 50
        //    },
        //    columns: columns,
        //    dataBound: function () {
        //        $('th').each(function () {
        //            $this = $(this);
        //            $this.addClass('strong text-center');
        //        });

        //        $('td').each(function () {
        //            var $this = $(this);
        //            var identifier = $this.find("i").eq(0);
        //            var id, cssClass;

        //            if (!INFRA.IsNullOrEmpty(identifier)) {
        //                id = identifier.data('id');
        //                cssClass = identifier.data('css-class');

        //                if (!INFRA.IsNullOrEmpty(id)) {
        //                    $this.prop('id', id);
        //                }

        //                if (!INFRA.IsNullOrEmpty(cssClass)) {
        //                    $this.addClass(cssClass);
        //                }

        //                identifier.remove();
        //            }
        //        });
        //    }
        //});

        $gvProduct.kGridResizeHeight({
            height: _variables.gvProductHeight,
            willRefreshGrid: false,
            isManual: true
        });
    }
    var _doBindTblSavedSalesOrder = function () {
        var $gvSavedSalesOrder = $('#gvSavedSalesOrder'), schemaField = {}, columns = [],
            width, field;

        $gvSavedSalesOrder.kGridResizeHeight({
            height: _variables.gvSavedSalesOrderHeight,
            willRefreshGrid: true
        });

        //$gvSavedSalesOrder.find('th').each(function () {
        //    $this = $(this);
        //    width = INFRA.ReplaceIfNullOrEmpty($this.data('width'), 100);
        //    field = $this.data('field');

        //    column = {
        //        width: width,
        //        field: field,
        //        attributes: {
        //            "class": "text-center"
        //        }
        //    }
        //    columns.push(column);
        //});

        //$gvSavedSalesOrder.kendoGrid({
        //    sortable: false,
        //    scrollable: true,
        //    pageable: true,
        //    dataSource: {
        //        pageSize: 50
        //    },
        //    columns: columns,
        //    dataBound: function () {
        //        $('th').each(function () {
        //            $this = $(this);
        //            $this.addClass('strong text-center');
        //        });

        //        $('td').each(function () {
        //            var $this = $(this);
        //            var identifier = $this.find("i").eq(0);
        //            var id, cssClass;

        //            if (!INFRA.IsNullOrEmpty(identifier)) {
        //                id = identifier.data('id');
        //                cssClass = identifier.data('css-class');

        //                if (!INFRA.IsNullOrEmpty(id)) {
        //                    $this.prop('id', id);
        //                }

        //                if (!INFRA.IsNullOrEmpty(cssClass)) {
        //                    $this.addClass(cssClass);
        //                }

        //                identifier.remove();
        //            }
        //        });
        //    }
        //});


    }
    //#EndRegion Private Methods for KendoGrid


    var initialize = function (options) {
        try {
            _variables.params = options || {};
        } catch (ex) {
            if (typeof console !== "undefined" && console !== null) {
                console.error("Error parsing inline options", ex);
            }
        }

        _variables.defaultSalesReceiptForm = $('#frmSalesOrder').serialize();
        _variables.defaultProductSearchForm = $('#frmSearchProduct').serialize();

        _doInitializeKendoElements();
        _doInitializeElements();
        _doBindTblProduct();
        //_doBindTblSavedSalesOrder();

    }

    return {
        initialize: initialize,
        _variables: _variables,
        BindTblSavedSalesOrder: _doBindTblSavedSalesOrder
    }

})();

$(document).ready(function () {
    var options = window.salesOptions;
    ORDERMANAGEMENTSALESORDER.initialize(options);
    ORDERMANAGEMENTSALESORDER.BindTblSavedSalesOrder();

    $(window).resize(function () {
        $('#gvProduct').kGridResizeHeight({
            height: ORDERMANAGEMENTSALESORDER._variables.gvProductHeight,
            willRefreshGrid: false,
            isManual: true
        });


        $('#gvSavedSalesOrder').kGridResizeHeight({
            height: ORDERMANAGEMENTSALESORDER._variables.gvSavedSalesOrderHeight,
            willRefreshGrid: true
        });
    });
});