var ORDERMANAGEMENTPURCHASEORDER = (function () {
    var _variables = {
        params: null,
        loaderMsg: 'Please wait while we process your request',
        UpdateMsg: "",
        gvProductHeight: 180,
        defaultCRType: "",
        defaultValue: null,
        defaultEditValue: null,
        defaultSearchValue: null,
        defaultVoidQtyProduct : null
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
        $('body').on('click', '#newbutton', function () {
            _doNewItem();
            _doClearFormField();
            _variables.params.inventoryAction = "save";
        });

        $('body').on('click', '#discardbutton', function () {
            _doDiscardItem();
            _variables.params.inventoryAction = "";
        });

        $('body').on('click', '#savebutton', function () {
            if (_variables.params.inventoryAction == "save") {
                _doSavePurchaseOrderSingle();
            } else {
                _doUpdateProductDetails();
            }

        });

        //$('body').on('click', '#updatebutton', function () {
        //    _doUpdateProductDetails();
        //});

        $('body').on('click', '#addinventory', function () {
            _doAddItemInOrders();
        });

        $('body').on('click', '#editbutton', function () {
            $this = $(this);
            //_doNewItem();

            //$frmEditInventory = $('#frmEditInventory');
            //$frmEditInventory.removeData('validator');
            //$frmEditInventory.removeData('unobtrusiveValidation');
            //$.validator.unobtrusive.parse($frmEditInventory);
            _doUpdateItem();

            //_doClearFormField();
            

            $('#frmEditInventory #hdfEditProductID').val($this.data('product-id'));
            $('#frmEditInventory #EditProductCode').val($this.data('product-code'));
            $('#frmEditInventory #ProductName').val($this.data('product-name'));
            $('#frmEditInventory #ProductDesc').val($this.data('product-desc'));
            $('#frmEditInventory #ProductExtension').val($this.data('product-extension'));
            $('#frmEditInventory #ProductPrice').val($this.data('product-price'));
            $('#frmEditInventory #OriginalPrice').val($this.data('product-originalprice'));
            $('#frmEditInventory #Size').val($this.data('product-size'));
            $('#frmEditInventory #BarCode').val($this.data('product-barcode'));
            $('#frmEditInventory #EditProductCategory').data('kendoDropDownList').value($this.data('product-categoryid').toString());
            $('#frmEditInventory #EditProductBranch').data('kendoDropDownList').value($this.data('product-branchid').toString());
            $('#frmEditInventory #Remarks').val($this.data('product-remarks'));



            _variables.params.inventoryAction = "update";
        });

        $('body').on('click', '#refreshbutton', function () {
            _doRefreshProductList();
        });

        $('body').on('click', '#searchbutton', function () {
            $('#mdlsearchproduct').modal('show');
        });

        $('body').on('click', '#searchdetails', function () {
            _doSearchItem();
        });

        $('body').on('click', '#addqtybutton', function () {
            $this = $(this);
            $('#hdfProductIdAddQty').val($this.data('product-id'));
            $('#mdladdqtyproduct').modal('show');
        });

        $('body').on('click', '#voidqtybutton', function () {
            $this = $(this);
            $('#hdfProductIdVoidQty').val($this.data('product-id'));
            $('#frmVoidQtyProduct #hdfVoidCRType').val('');
            $('#lblCurrQty').text($this.data('product-qty'));
            $('#mdlvoidqtyproduct').modal('show');
        });

        $('body').on('click', '#saveqtydetail', function () {
            _doAddQty();
        });

        $('body').on('click', '#editproductimage', function () {
            $this = $(this);
            $('#hdfProductIdEditProductImage').val($this.data('product-id'));

            _doEditProductImage();
            //$('#mdleditproductimage').modal('show');
        });

        $('body').on('change', '#frmUpdateProductImage #ProductImage', function (e) {
            var files, data, x;
            files = e.target.files;

            if (files.length > 0) {
                if (window.FormData !== undefined) {
                    data = new FormData();
                    for (x = 0; x < files.length; x++) {
                        data.append("file" + x, files[x]);
                    }

                    _doUploadPhoto(data);
                }
            }
        });

        $('body').on('shown.bs.modal', '#mdlsearchproduct', function () {
            $(document).off('focusin.modal');
        });

        $('body').on('click', '#removefilter', function () {
            //$('#SearchProductCode').val('');
            //$('#SearchProductName').val('');
            //$('#SearchProductExtension').val('');
            //$('#frmSearchProduct #SearchProductCategory').data('kendoDropDownList').value(null);
            //$('#frmSearchProduct #SearchProductBranch').data('kendoDropDownList').value(null);
            $('#frmSearchProduct').deserialize(_variables.defaultSearchValue, true, null);

        });

        $('body').on('shown.bs.modal', '#mdladdqtyproduct', function () {
            $(document).off('focusin.modal');
        });

        $('body').on('hidden.bs.modal', '#mdlvoidqtyproduct', function () {
            //$('#frmVoidQtyProduct #VoidQuantity').val('0');
            //$('#frmVoidQtyProduct #VoidRemarks').val('');
            //$('#frmVoidQtyProduct [data-valmsg-for="Remarks"]').html("");
            //$('#frmVoidQtyProduct [data-valmsg-for="Quantity"]').html("");
            $('#lblCRType').text('Please select from (+) / (-)');
            _variables.defaultCRType = "";
            $('#frmVoidQtyProduct').deserialize(_variables.defaultVoidQtyProduct, true, null);
        });

        $('body').on('click', '#refreshcategory', function () {
            $('#ProductCategory').kendoDropDownList({
                filter: "contains",
                optionLabel: 'Select item',
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
        });

        $('body').on('click', '#refreshbranch', function () {
            $('#ProductBranch').kendoDropDownList({
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



        $('body').on('click', '#editrefreshcategory', function () {
            $('#EditProductCategory').kendoDropDownList({
                filter: "contains",
                optionLabel: 'Select item',
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
        });

        $('body').on('click', '#refreshsupplier', function () {
            $('#ProductSupplier').kendoDropDownList({
                filter: "contains",
                optionLabel: 'Select item',
                dataTextField: "SupplierDisplay",
                dataValueField: "SupplierID",
                dataSource: {
                    transport: {
                        read: {
                            dataType: "json",
                            url: _variables.params.supplierGetUrl
                        }
                    }
                }
            });
        });

        $('body').on('click', '#addqtyrefreshsupplier', function () {
            $('#AddQtyProductSupplier').kendoDropDownList({
                filter: "contains",
                optionLabel: 'Select item',
                dataTextField: "SupplierDisplay",
                dataValueField: "SupplierID",
                dataSource: {
                    transport: {
                        read: {
                            dataType: "json",
                            url: _variables.params.supplierGetUrl
                        }
                    }
                }
            });
        });

        $('body').on('click', '#voidqtyadd', function () {
            $('#VoidQuantity').val('0');
            $('#lblCRType').text('+');
            _variables.defaultCRType = "+";
            $('#frmVoidQtyProduct #hdfVoidCRType').val(_variables.defaultCRType);
        });

        $('body').on('click', '#voidqtyminus', function () {
            $('#VoidQuantity').val('0');
            $('#lblCRType').text('-');
            _variables.defaultCRType = "-";
            $('#frmVoidQtyProduct #hdfVoidCRType').val(_variables.defaultCRType);
            
        });

        $('body').on('click', '#voidqtydetail', function () {
            _doVoidQty();
        });

        $('body').on('click', '#extractbutton', function () {
            $frmSearchInventoryReport = $('#frmSearchInventoryReport');
            window.open(_variables.params.extractToExcelUrl + '?' + $frmSearchInventoryReport.serialize());
        });

        //$('body').on('click', '#savebutton', function () {
        //    _doSavePurchaseOrders();
        //});

    }

    var _doInitializeDropDownElements = function () {
        //$("#ProductUnit").kendoDropDownList({
        //    filter: "startswith",
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

        //$('#SearchProductUnit').kendoDropDownList({
        //    filter: "startswith",
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


        $('#ProductCategory').kendoDropDownList({
            filter: "contains",
            optionLabel: 'Select item',
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

        $('#EditProductCategory').kendoDropDownList({
            filter: "contains",
            optionLabel: 'Select item',
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

        $('#ProductSupplier').kendoDropDownList({
            filter: "contains",
            optionLabel: 'Select item',
            dataTextField: "SupplierDisplay",
            dataValueField: "SupplierID",
            dataSource: {
                transport: {
                    read: {
                        dataType: "json",
                        url: _variables.params.supplierGetUrl
                    }
                }
            }
        });

        $('#AddQtyProductSupplier').kendoDropDownList({
            filter: "contains",
            optionLabel: 'Select item',
            dataTextField: "SupplierDisplay",
            dataValueField: "SupplierID",
            dataSource: {
                transport: {
                    read: {
                        dataType: "json",
                        url: _variables.params.supplierGetUrl
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

        $('#ProductBranch').kendoDropDownList({
            filter: "contains",
            optionLabel: 'Select item',
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

        $('#EditProductBranch').kendoDropDownList({
            filter: "contains",
            optionLabel: 'Select item',
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



        //$('#SearchProductSupplier').kendoDropDownList({
        //    filter: "startswith",
        //    optionLabel: 'Select item',
        //    dataTextField: "SupplierDisplay",
        //    dataValueField: "SupplierID",
        //    dataSource: {
        //        transport: {
        //            read: {
        //                dataType: "json",
        //                url: _variables.params.supplierGetUrl
        //            }
        //        }
        //    }
        //});


        //$('#ProductModel').kendoDropDownList({
        //    filter: "startswith",
        //    optionLabel: 'Select item',
        //    dataTextField: "ModelName",
        //    dataValueField: "ModelID",
        //    dataSource: {
        //        transport: {
        //            read: {
        //                dataType: "json",
        //                url: _variables.params.modelGetUrl
        //            }
        //        }
        //    }
        //});

        //$('#SearchProductModel').kendoDropDownList({
        //    filter: "startswith",
        //    optionLabel: 'Select item',
        //    dataTextField: "ModelName",
        //    dataValueField: "ModelID",
        //    dataSource: {
        //        transport: {
        //            read: {
        //                dataType: "json",
        //                url: _variables.params.modelGetUrl
        //            }
        //        }
        //    }
        //});
    }

    var _doBindTblProduct = function () {
        var $gvProduct = $('#gvProduct'), schemaField = {}, columns = [],
            width, field;

        $gvProduct.kGridResizeHeight({
            height: _variables.gvProductHeight,
            willRefreshGrid: true
        });
    }

    var _doAddItemInOrders = function () {
        $frmInventory = $('#frmInventory');
        var rowCount, $tblProductOrder = $('#tblProductOrder'), $$curRowToDelete, $productCodeVal;

        $.ajax({
            url: _variables.params.inventoryAddOrderUrl,
            type: 'POST',
            data: $frmInventory.serialize(),
            success: function (data) {
                if (data.isSuccess == true) {
                    rowCount = $('#ProductOrderRow tr').length;

                    if (rowCount === 1) {
                        $productCodeVal = $tblProductOrder.find('tbody > tr > td').first().find('span').text();
                        if ($productCodeVal === "") {
                            $curRowToDelete = $tblProductOrder.find('tbody > tr');
                            $curRowToDelete.remove();
                        }
                    }

                    $('#ProductOrderRow').append(data.inventory);



                }
            }
        });
    }

    var _doSearchItem = function () {
        //var $frmSearchProduct = $('#frmSearchProduct');

        //doShowTrackerLoader(_variables.loaderMsg);

        //$.ajax({
        //    url: _variables.params.inventorySearchUrl,
        //    type: 'POST',
        //    data: $frmSearchProduct.serialize(),
        //    success: function (data) {
        //        doRemoveTrackerLoader();
        //        $('#divInventory').html(data);
        //        _doBindTblProduct();

        //        $('#mdlsearchproduct').modal('hide');
        //        INFRA.doRemoveOpenedModal();
        //    }
        //});


        var $gvProduct = $("#gvProduct").data('kendoGrid'), $frmSearchProduct = $('#frmSearchProduct');

        doShowTrackerLoader(_variables.loaderMsg);

        $gvProduct.dataSource.read($frmSearchProduct.serializeToJson());

        //if ($gvProduct.pager.page() !== 1) {
        //    $gvProduct.pager.page(1);
        //}

        doRemoveTrackerLoader();
        $('#mdlsearchproduct').modal('hide');
        INFRA.doRemoveOpenedModal();

    }

    var _doSavePurchaseOrders = function () {
        var rowCount, $tblProductOrder = $('#tblProductOrder'), $tds;
        var productCode, productName, productDescription, productExtension, productQuantity, productPrice, productUnit, productCategory, productModel, productSupplier;

        $tblProductOrder.find('tbody > tr').each(function () {
            $tds = $(this).find('td');
            productCode = $tds.eq(0).find('span').text();
            productName = $tds.eq(1).find('span').text();
            productDescription = $tds.eq(2).find('span').text();
            productExtension = $tds.eq(3).find('span').text();
            productQuantity = $tds.eq(4).find('span').text();
            productPrice = $tds.eq(5).find('span').text();
            productSize = $tds.eq(6).find('span').text();
            productUnit = null;
            productCategory = null;
            productModel = null;
            productSupplier = null;
            productRemarks = null;

            $.ajax({
                url: _variables.params.inventorySaveOrderUrl,
                type: 'POST',
                data: {
                    productCode: productCode,
                    productName: productName,
                    productDesc: productDescription,
                    productExtension: productExtension,
                    quantity: productQuantity,
                    price: productPrice,
                    size: productSize,
                    unitId: productUnit,
                    categoryId: productCategory,
                    modelId: productModel,
                    supplierId: productSupplier,
                    remarks: productRemarks
                }, success: function (data) {
                    alert(data.isSuccess);
                }
            });
        });

    }

    var _doSavePurchaseOrderSingle = function () {
        var $frmInventory = $('#frmInventory');
        var $ProductCategory = $('#frmInventory #ProductCategory'), $ProductSupplier = $('#frmInventory #ProductSupplier'), $ProductBranch = $('#frmInventory #ProductBranch');
        var $hdfUserTypeId = $('#frmInventory #hdfUserTypeId');
        doShowTrackerLoader(_variables.loaderMsg);
        var isDataReady = true;
        $.validator.unobtrusive.parse($frmInventory);
        $frmInventory.validate();

        if ($frmInventory.valid()) {
            if (INFRA.ConvertToBoolean(isDataReady)) {
                $.ajax({
                    url: _variables.params.invetnorySaveProductAndPurchaseOrderUrl,
                    type: 'POST',
                    data: $frmInventory.serialize(),
                    success: function (data) {
                        doRemoveTrackerLoader();
                        if (INFRA.ConvertToBoolean(data.isSuccess)) {
                            //$('#divInventory').html(data.inventory);
                            //_doBindTblProduct();
                            _doSearchItem();
                            _doDiscardItem();
                        }
                        $('#divResultContainer').show().html(data.alertMessage);
                    }
                });
            } else {
                doRemoveTrackerLoader();
            }
        } else {
            if (INFRA.IsNullOrEmpty($ProductCategory.val())) {
                isDataReady = false;
                $('#frmInventory [data-valmsg-for="CategoryID"]').html("<br/>Category field is a required field");
            }
            else {
                isDataReady = true;
                $('#frmInventory [data-valmsg-for="CategoryID"]').html("");
            }

            if (INFRA.IsNullOrEmpty($ProductSupplier.val())) {
                isDataReady = false;
                $('#frmInventory [data-valmsg-for="SupplierID"]').html("<br/>Supplier field is a required field");
            }
            else {
                isDataReady = true;
                $('#frmInventory [data-valmsg-for="SupplierID"]').html("");
            }
            if (INFRA.ConvertToInteger($hdfUserTypeId.val()) === 1) {
                if (INFRA.IsNullOrEmpty($ProductBranch.val())) {
                    isDataReady = false;
                    $('#frmInventory [data-valmsg-for="BranchID"]').html("<br/>Branch field is a required field");
                } else {
                    isDataReady = true;
                    $('#frmInventory [data-valmsg-for="BranchID"]').html("");
                }
            }

            doRemoveTrackerLoader();
        }
    }

    var _doRefreshProductList = function () {
        //$frmSearchProduct = $('#frmSearchProduct');
        //doShowTrackerLoader(_variables, loaderMsg);
        //$.ajax({
        //    url: _variables.params.inventoryRefreshUrl,
        //    type: 'POST',
        //    data: $frmSearchProduct.serialize(),
        //    success: function (data) {
        //        doRemoveTrackerLoader();
        //        $('#divInventory').html(data);
        //        _doBindTblProduct();
        //        _doDiscardItem();
        //    }
        //});

        _doSearchItem();
    }

    var _doUpdateProductDetails = function () {
        //inventoryUpdateProductDetailsUrl
        //$frmInventory = $('#frmInventory');
        debugger;
        var $frmEditInventory = $('#frmEditInventory');
        var $ProductCategory = $('#EditProductCategory'), $hdfUserTypeId = $('#editHdfUserTypeId'), $ProductBranch = $('#EditProductBranch');
        var isDataReady = true;

        doShowTrackerLoader(_variables.loaderMsg);
        debugger;
        $.validator.unobtrusive.parse($frmEditInventory);
        $frmEditInventory.validate();
        if ($frmEditInventory.valid()) {

            if (INFRA.IsNullOrEmpty($ProductCategory.val())) {
                isDataReady = false;
                $('#frmEditInventory [data-valmsg-for="CategoryID"]').html("<br/>Category field is a required field");
            }
            else {
                isDataReady = true;
                $('#frmEditInventory [data-valmsg-for="CategoryID"]').html("");
            }
            if (INFRA.ConvertToBoolean(isDataReady)) {
                if (INFRA.ConvertToInteger($hdfUserTypeId.val()) === 1) {
                    if (INFRA.IsNullOrEmpty($ProductBranch.val())) {
                        isDataReady = false;
                        $('#frmEditInventory [data-valmsg-for="BranchID"]').html("<br/>Branch field is a required field");
                    } else {
                        isDataReady = true;
                        $('#frmEditInventory [data-valmsg-for="BranchID"]').html("");
                    }
                }
            }

            if (INFRA.ConvertToBoolean(isDataReady)) {
                $.ajax({
                    url: _variables.params.inventoryUpdateProductDetailsUrl,
                    type: 'POST',
                    data: $frmEditInventory.serialize(),
                    success: function (data) {
                        doRemoveTrackerLoader();
                        if (INFRA.ConvertToBoolean(data.isSuccess)) {
                            //$('#divInventory').html(data.inventory);
                            //_doBindTblProduct();
                            _doSearchItem();
                            _doDiscardItem();
                        }
                        $('#divResultContainer').show().html(data.alertMessage);
                    }
                });
            } else {
                doRemoveTrackerLoader();
            }
        } else {
            doRemoveTrackerLoader();
        }
    }

    var _doAddQty = function () {
        var $frmAddQtyProduct = $('#frmAddQtyProduct'), $AddQtyProductSupplier = $('#frmAddQtyProduct #AddQtyProductSupplier');
        var isDataReady = true;

        $.validator.unobtrusive.parse($frmAddQtyProduct);
        $frmAddQtyProduct.validate();
        if ($frmAddQtyProduct.valid()) {
            if (INFRA.IsNullOrEmpty($AddQtyProductSupplier.val())) {
                isDataReady = false;
                $('#frmAddQtyProduct [data-valmsg-for="SupplierID"]').html("<br/>Supplier field is a required field");
            } else {
                isDataReady = true;
                $('#frmAddQtyProduct [data-valmsg-for="SupplierID"]').html("");
            }

            if (INFRA.ConvertToBoolean(isDataReady)) {
                $.ajax({
                    url: _variables.params.inventoryAddProductUrl,
                    type: 'POST',
                    data: $frmAddQtyProduct.serialize(),
                    success: function (data) {
                        if (INFRA.ConvertToBoolean(data.isSuccess)) {
                            //$('#divInventory').html(data.inventory);
                            //_doBindTblProduct();

                            _doSearchItem();
                            $('#mdladdqtyproduct').modal('hide');
                            INFRA.doRemoveOpenedModal();

                            $('#frmAddQtyProduct #AddQuantity').val('0');
                            $('#frmAddQtyProduct #AddQtyProductSupplier').data('kendoDropDownList').value(null);
                        }

                        $('#divResultContainer').show().html(data.alertMessage);
                    }
                });
            }

        }

    }

    var _doVoidQty = function () {
        var $frmVoidQtyProduct = $('#frmVoidQtyProduct'), $VoidRemarks = $('#frmVoidQtyProduct #VoidRemarks'), $hdfVoidCRType = $('#frmVoidQtyProduct #hdfVoidCRType');
        var isDataReady = true;

        $.validator.unobtrusive.parse($frmVoidQtyProduct);
        $frmVoidQtyProduct.validate();
        if ($frmVoidQtyProduct.valid()) {
            debugger;

            if (INFRA.IsNullOrEmpty($VoidRemarks.val())) {
                isDataReady = false;
                $('#frmVoidQtyProduct [data-valmsg-for="Remarks"]').html("<br/>Remarks field is a required field");
            } else {
                isDataReady = true;
                $('#frmVoidQtyProduct [data-valmsg-for="Remarks"]').html("");
            }

            if (INFRA.ConvertToBoolean(isDataReady)) {
                if (INFRA.IsNullOrEmpty($hdfVoidCRType.val())) {
                    isDataReady = false;
                    $('#frmVoidQtyProduct [data-valmsg-for="Quantity"]').html("<br/>Please select from (+)/(-) field is a required field");
                } else {
                    isDataReady = true;
                    $('#frmVoidQtyProduct [data-valmsg-for="Quantity"]').html("");
                }
            }
            

            if (INFRA.ConvertToBoolean(isDataReady)) {
                $.ajax({
                    url: _variables.params.inventoryVoidProductUrl,
                    type: 'POST',
                    data: $frmVoidQtyProduct.serialize(),
                    success: function (data) {
                        if (INFRA.ConvertToBoolean(data.isSuccess)) {
                            //$('#divInventory').html(data.inventory);
                            //_doBindTblProduct();

                            _doSearchItem();
                            $('#mdlvoidqtyproduct').modal('hide');
                            INFRA.doRemoveOpenedModal();

                            //$('#frmVoidQtyProduct #VoidQuantity').val('0');
                            //$VoidRemarks.val('');
                            //$('#frmVoidQtyProduct [data-valmsg-for="Remarks"]').html("");
                            //$('#frmVoidQtyProduct [data-valmsg-for="Quantity"]').html("");
                            $('#frmVoidQtyProduct').deserialize(_variables.defaultVoidQtyProduct, true, null);
                        }

                        $('#divResultContainer').show().html(data.alertMessage);
                    }
                });
            }

        } else {
            if (INFRA.IsNullOrEmpty($VoidRemarks.val())) {
                isDataReady = false;
                $('#frmVoidQtyProduct [data-valmsg-for="Remarks"]').html("<br/>Remarks field is a required field");
            } else {
                isDataReady = true;
                $('#frmVoidQtyProduct [data-valmsg-for="Remarks"]').html("");
            }
        }

    }

    var _doDiscardItem = function () {
        $('#divInventory').show();
        $('#inventory-form').hide();
        $('#inventory-update-form').hide();
        $('#inventory-form-update-image').hide();

        //buttons
        $('#searchbutton').show();
        $('#refreshbutton').show();
        $('#newbutton').show();
        $('#extractbutton').show();

        $('#discardbutton').hide();
        $('#savebutton').hide();

        _doClearFormField();
    }

    var _doClearFormField = function () {
        //$('#frmInventory #hdfProductID').val(null);
        //$('#frmInventory #ProductCode').val('');
        //$('#frmInventory #ProductName').val('');
        //$('#frmInventory #ProductDesc').val('');
        //$('#frmInventory #ProductExtension').val('');
        //$('#frmInventory #Quantity').val('0');
        //$('#frmInventory #ProductPrice').val('0');
        //$('#frmInventory #OriginalPrice').val('0');
        //$('#frmInventory #Size').val('');
        //$('#frmInventory #Remarks').val('');



        //$('#frmInventory #ProductCategory').data('kendoDropDownList').value(null);
        //$('#frmInventory #ProductSupplier').data('kendoDropDownList').value(null);
        //$('#frmInventory #ProductBranch').data('kendoDropDownList').value(null);
        //$('#frmInventory [data-valmsg-for="BranchID"]').html("");
        //$('#frmInventory [data-valmsg-for="SupplierID"]').html("");
        //$('#frmInventory [data-valmsg-for="CategoryID"]').html("");
        //$('#frmInventory [data-valmsg-for="Price"]').html("");
        //$('#frmInventory [data-valmsg-for="Size"]').html("");
        //$('#frmInventory [data-valmsg-for="CategoryID"]').html("");
        $('#frmInventory').deserialize(_variables.defaultValue, true, null);



        //$('#frmEditInventory #hdfProductID').val(null);
        //$('#frmEditInventory #ProductCode').val('');
        //$('#frmEditInventory #ProductName').val('');
        //$('#frmEditInventory #ProductDesc').val('');
        //$('#frmEditInventory #ProductExtension').val('');
        //$('#frmEditInventory #ProductPrice').val('0');
        //$('#frmEditInventory #OriginalPrice').val('0');
        //$('#frmEditInventory #Size').val('');
        //$('#frmEditInventory #Remarks').val('');
        $('#frmEditInventory').deserialize(_variables.defaultEditValue, true, null);


        //$('#frmEditInventory #EditProductCategory').data('kendoDropDownList').value(null);
        //$('#frmEditInventory #EditProductBranch').data('kendoDropDownList').value(null);
        //$('#frmEditInventory [data-valmsg-for="BranchID"]').html("");
        //$('#frmEditInventory [data-valmsg-for="CategoryID"]').html("");

    }

    var _doNewItem = function () {
        $('#divInventory').hide();
        $('#inventory-form').show();

        //buttons
        $('#searchbutton').hide();
        $('#refreshbutton').hide();
        $('#newbutton').hide();
        $('#extractbutton').hide();

        $('#discardbutton').show();
        $('#savebutton').show();
    }

    var _doUpdateItem = function () {
        $('#divInventory').hide();
        $('#inventory-update-form').show();

        $('#searchbutton').hide();
        $('#refreshbutton').hide();
        $('#newbutton').hide();
        $('#extractbutton').hide();

        $('#discardbutton').show();
        $('#savebutton').show();

    }

    var _doEditProductImage = function () {
        $('#divInventory').hide();
        $('#inventory-form-update-image').show();

        $('#searchbutton').hide();
        $('#refreshbutton').hide();
        $('#newbutton').hide();
        $('#discardbutton').show();
        $('#extractbutton').hide();
    }

    var _doUploadPhoto = function (data) {
        var $frmUpdateProductImage = $('#frmUpdateProductImage');
        var formData = new FormData($frmUpdateProductImage[0]);

        doShowTrackerLoader(_variables.loaderMsg);
        $.ajax({
            type: "POST",
            url: $frmUpdateProductImage.data('url'),
            data: formData,
            processData: false,
            contentType: false,
            success: function (data) {
                doRemoveTrackerLoader();
                if (!INFRA.ConvertToBoolean(data.isSuccess)) {
                    $('#frmUpdateProductImage [data-valmsg-for="ProductImage"]').html("<br/>" + data.alertMessage);
                } else {
                    $('#frmUpdateProductImage #ProductImage').val('');
                    $('#divResultContainer').show().html(data.alertMessage);
                    //$('#divInventory').html(data.inventory);
                    //_doBindTblProduct();
                    _doSearchItem();
                    _doDiscardItem();

                }


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


        _variables.defaultValue = $('#frmInventory').serialize();
        _variables.defaultEditValue = $('#frmEditInventory').serialize();
        _variables.defaultSearchValue = $('#frmSearchProduct').serialize();
        _variables.defaultVoidQtyProduct = $('#frmVoidQtyProduct').serialize();


        _doInitializeDropDownElements();
        _doInitializeElements();
        _doBindTblProduct();
    }

    return {
        initialize: initialize,
        _variables: _variables
    }

})();

$(document).ready(function () {
    var options = window.inventoryOptions;
    ORDERMANAGEMENTPURCHASEORDER.initialize(options);

    //$('#frmEditInventory #ProductPrice').mask("#.##0,00", { reverse: true });
    //$('.money').mask('000.000.000.000.000,00', { reverse: true });

    //$('#frmCustomer #dpBirthDate').mask('00/00/0000');

    $(window).resize(function () {
        $('#gvProduct').kGridResizeHeight({
            height: ORDERMANAGEMENTPURCHASEORDER._variables.gvProductHeight,
            willRefreshGrid: false,
            isManual: true
        });
    });
});