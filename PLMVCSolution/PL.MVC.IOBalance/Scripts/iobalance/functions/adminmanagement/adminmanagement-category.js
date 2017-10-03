var ADMINMANAGEMENTCATEGORY = (function () {
    var _variables =
    {
        params: null,
        defaultModalUpdateValue: null,
        gvCategoryHeight: 200,
        loaderMsg: 'Please wait while we process your request',
        defaultValue: null,
        defaultEditValue: null,
        defaultSearchValue: null
    };

    var doShowTrackerLoader = function (msg) {
        $('body').showLoader({
            type: "page",
            message: msg
        });
    }

    var doRemoveTrackerLoader = function () {
        $('body').hideLoader();
    }



    var _doBindTblCategory = function () {
        var $gv = $('#divCategory');

        $frm = $('#frmSearchCategory');

        $("#divCategory").kendoGrid({
            dataSource: {
                type: "aspnetmvc-ajax",
                transport: {
                    read: _variables.params.getKendoUrl,
                    datatype: "json",
                    data: $frm.serialize()
                },
                pageSize: 50,
                serverPaging: true,
                serverSorting: true,
                serverFiltering: true,
                schema: {
                    total: function (data) {
                        return data.Total;
                    },
                    data: function (data) {
                        return data.Data;
                    }
                }
            },
            sortable: true,
            filterable: false,
            pageable: true,
            scrollable: true,
            groupable: false,
            columnMenu: {
                messages: {
                    filter: "Apply filter"
                }
            },
            columns: [
                {
                    field: "",
                    title: "Action",
                    width: 300,
                    locked: true,
                    template: function (e) {
                        return _doBindGVAction(e);
                    },
                    headerAttributes: {
                        "class": "bos-grid-text-center"
                    },
                    attributes: {
                        "class": "bos-grid-text-center"
                    }
                },
                {
                    field: "CategoryCode",
                    title: "Category Code",
                    width: 300,
                    locked: false,
                    sortable: true,
                    headerAttributes: {
                        "class": "bos-grid-text-center"
                    },
                    attributes: {
                        "class": "bos-grid-text-center"
                    }
                },
                {
                    field: "CategoryName",
                    title: "Category Name",
                    width: 300,
                    locked: false,
                    headerAttributes: {
                        "class": "bos-grid-text-center"
                    },
                    attributes: {
                        "class": "bos-grid-text-center"
                    }
                },
                {
                    field: "IsActive",
                    title: "Active?",
                    width: 210,
                    locked: false,
                    headerAttributes: {
                        "class": "bos-grid-text-center"
                    },
                    attributes: {
                        "class": "bos-grid-text-center"
                    }
                }]
        });

        $gv.kGridResizeHeight({
            height: _variables.gvCategoryHeight,
            willRefreshGrid: false,
            isManual: true
        });
    }

    var _doBindGVAction = function (e) {
        var editbutton, deletebutton;
        editbutton = "<a href='javascript:void(0)' type='button' class='selected' data-button-type='actionbuttons' data-action-type='editbutton' data-category-id='" + e.CategoryID + "' data-category-code='" + e.CategoryCode + "' data-category-name='" + e.CategoryName + "'> Edit Details</a>";
        deletebutton = "<a href='javascript:void(0)' type='button' class='selected' data-button-type='actionbuttons' data-action-type='deletebutton' data-category-id='" + e.CategoryID + "' data-category-code='" + e.CategoryCode + "' data-category-name='" + e.CategoryName + "' > Deactivate/Activate </a>";

        return editbutton + " | " + deletebutton;
    }

    var _doBindDropDownCategory = function () {
        var datas = [
            { text: 'True', value: 'true' },
            { text: 'False', value: 'false' }
        ];

        $('#frmSearchCategory #isActive').kendoDropDownList({
            dataTextField: 'text',
            dataValueField: 'value',
            optionLabel: '- Please select -',
            dataSource: datas,
            index: 0
        });
    }

    var _doSaveDetails = function (element) {
        var datas = $('#frmCategory').serialize(),
            elementUrl = element.data('url'),
            $form = $('#frmCategory');

        doShowTrackerLoader(_variables.params.loaderMsg);

        $.validator.unobtrusive.parse($form);
        $form.validate();
        if ($form.valid()) {
            $.ajax({
                url: elementUrl,
                type: 'POST',
                data: datas,
                success: function (data) {
                    doRemoveTrackerLoader();
                    $('#divResultContainer').show().html(data.alertMessage);
                    if (INFRA.ConvertToBoolean(data.isSuccess)) {
                        //$('#divCategory').html(data.categories);
                        //_doBindTblCategory();
                        _doSearchDetails();
                        _doDiscardNewForm();
                    } else {

                    }

                }
            });

        } else {
            doRemoveTrackerLoader();
        }
    }

    var _doUpdateDetails = function (element) {
        var datas = $('#frmUpdateCategory').serialize(),
            elementUrl = element.data('url'),
            $form = $('#frmUpdateCategory');

        doShowTrackerLoader(_variables.params.loaderMsg);

        $.validator.unobtrusive.parse($form);
        $form.validate();

        if ($form.valid()) {
            $.ajax({
                url: elementUrl,
                type: 'POST',
                data: datas,
                success: function (data) {
                    doRemoveTrackerLoader();
                    $('#divResultContainer').show().html(data.alertMessage);

                    if (INFRA.ConvertToBoolean(data.isSuccess)) {
                        //$('#divCategory').html(data.categories);
                        //_doBindTblCategory();
                        _doSearchDetails();
                        $('#mdleditcategory').modal('hide');
                        INFRA.doRemoveOpenedModal();

                    } else {

                    }

                }
            });
        } else {
            doRemoveTrackerLoader();
        }
    }

    var _doSearchDetails = function () {
        //var datas = $('#frmSearchCategory').serialize(),
        //    elementUrl = _variables.params.searchItemUrl,
        //    $form = $('#frmSearchCategory');
        //doShowTrackerLoader(_variables.params.loaderMsg);

        //$.ajax({
        //    url: elementUrl,
        //    type: 'POST',
        //    data: datas,
        //    success: function (data) {
        //        doRemoveTrackerLoader();
        //        //$('#divCategory').html(data);
        //        //_doBindTblCategory();

        //        $('#mdlsearchcategory').modal('hide');
        //        INFRA.doRemoveOpenedModal();
        //    }
        //});
        var $gv = $("#divCategory").data('kendoGrid'), $frmSearch = $('#frmSearchCategory');

        doShowTrackerLoader(_variables.loaderMsg);

        $gv.dataSource.read($frmSearch.serializeToJson());

        doRemoveTrackerLoader();
        $('#mdlsearchcategory').modal('hide');
        INFRA.doRemoveOpenedModal();
    }

    var _doRefreshDetails = function () {
        //var elementUrl = element.data('url'), $frmSearchCategory = $('#frmSearchCategory');

        //doShowTrackerLoader(_variables.params.loaderMsg);
        //$.ajax({
        //    url: elementUrl,
        //    type: 'POST',
        //    data: $frmSearchCategory.serialize(),
        //    success: function (data) {
        //        doRemoveTrackerLoader();
        //        $('#divCategory').html(data);
        //        _doBindTblCategory();
        //    }
        //});
        _doSearchDetails();
    }

    var _doInactiveDetails = function (element) {
        var elementUrl = _variables.params.updateActiveItemUrl,
            categoryid = element.data('category-id'), categorycode = element.data('category-code'), categoryname = element.data('category-name');

        doShowTrackerLoader(_variables.params.loaderMsg);
        $.ajax({
            url: elementUrl,
            type: 'POST',
            data: { categoryId: categoryid, categoryCode: categorycode, categoryName: categoryname },
            success: function (data) {
                $('#divResultContainer').show().html(data.alertMessage);
                if (INFRA.ConvertToBoolean(data.isSuccess)) {
                    //$('#divCategory').html(data.categories);
                    //_doBindTblCategory();
                    _doSearchDetails();
                    doRemoveTrackerLoader();
                } else {
                    doRemoveTrackerLoader();
                }
            }
        });
    }

    var _doShowNewForm = function () {
        $('#category-new-form').show();
        $('#divCategory').hide();

        $('#searchbutton').hide();
        $('#refreshbutton').hide();
        $('#btnnewform').hide();

        $('#btndiscard').show();

    }

    var _doDiscardNewForm = function () {
        $('#category-new-form').hide();
        $('#divCategory').show();

        $('#searchbutton').show();
        $('#refreshbutton').show();
        $('#btnnewform').show();

        $('#btndiscard').hide();

        //$('#frmCategory #CategoryCode').val('');
        //$('#frmCategory #CategoryName').val('');
        $('#frmCategory').deserialize(_variables.defaultValue, true, null);

    }

    var initialize = function (options) {
        try {
            _variables.params = options || {};
            _doBindTblCategory();
            //_doBindCategory();
            _doInitializeElements();
            _doInitializeKendoElements();
            _variables.defaultModalUpdateValue = $('#frmUpdateCategory').serialize();
            INFRA.doRemoveOpenedModal();
            _doBindDropDownCategory();
        } catch (ex) {
            if (typeof console !== "undefined" && console !== null) {
                console.error("Error parsing inline options", ex);
            }
        }

        _variables.defaultValue = $('#frmCategory').serialize();
        _variables.defaultEditValue = $('#frmUpdateCategory').serialize();
        _variables.defaultSearchValue = $('#frmSearchCategory').serialize();
    };

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

        $('body').on('click', '#searchbutton', function () {
            var $elem = $(this);
            $('#mdlsearchcategory').modal('show');
        });

        $('body').on('click', '#refreshbutton', function () {
            _doRefreshDetails();
        });

        $('body').on('click', '[data-action-type="editbutton"]', function () {
            var $elem = $(this);
            $('[data-action-type="editbutton"]').removeClass('selected');
            $elem.addClass('selected');
            $('#mdleditcategory').modal('show');
        });

        $('body').on('click', '[data-action-type="deletebutton"]', function () {
            var $elem = $(this), $caller;

            $('[data-action-type="deletebutton"]').removeClass('selected');
            $elem.addClass('selected');

            $caller = $('[data-action-type="deletebutton"].selected');

            $.alertWindow("Are you sure you want to update this record?",
            function () {
                debugger;
                _doInactiveDetails($caller);
            },
            function () {
            });
        });

        $('body').on('show.bs.modal', '#mdleditcategory', function () {
            var $caller = $('[data-action-type="editbutton"].selected');
            $('#frmUpdateCategory #hdfCategoryId').val($caller.data('category-id'));
            $('#frmUpdateCategory #CategoryCode').val($caller.data('category-code'));
            $('#frmUpdateCategory #CategoryName').val($caller.data('category-name'));

        });

        $('#mdleditcategory').on('hidden.bs.modal', function () {
            //$('#frmUpdateCategory #hdfCategoryId').val(null);
            //$('#frmUpdateCategory #CategoryCode').val(null);
            //$('#frmUpdateCategory #CategoryName').val(null);
            $('#frmUpdateCategory').deserialize(_variables.defaultEditValue, true, null);
        });

        $('body').on('click', '#btnnewform', function () {
            _doShowNewForm();
        });

        $('body').on('click', '#btndiscard', function () {
            _doDiscardNewForm();
        });

        $('body').on('click', '#removefilter', function () {
            //$('#frmSearchCategory #CategoryCode').val('');
            //$('#frmSearchCategory #CategoryName').val('');
            //$('#frmSearchCategory #SearchCategoryYesNo').data('kendoDropDownList').value(null);
            $('#frmSearchCategory').deserialize(_variables.defaultSearchValue, true, null);
        });
    }

    var _doInitializeKendoElements = function () {
        $("#SearchCategoryYesNo").kendoDropDownList({
            optionLabel: 'Select All',
            dataTextField: "text",
            dataValueField: "value",
            dataSource: {
                transport: {
                    read: {
                        dataType: "json",
                        url: _variables.params.isActiveYesNoUrl
                    }
                }
            }
        });
        //isActiveYesNoUrl
    }

    return {
        initialize: initialize,
        _variables: _variables
    };

})();

$(document).ready(function () {
    var options = window.categoryOptions;
    ADMINMANAGEMENTCATEGORY.initialize(options);
    $(window).resize(function () {
        $('#gvCategory').kGridResizeHeight({
            height: ADMINMANAGEMENTCATEGORY._variables.gvCategoryHeight,
            willRefreshGrid: false,
            isManual: true
        });
    });
});