var ADMINMANAGEMENTUSER = (function () {
    var _variables =
    {
        params: null,
        gvUserHeight: 200,
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

    var _doInitializeElements = function () {
        $('body').on('click', '#btnnewform', function () {
            _doShowNewForm();
        });

        $('body').on('click', '#searchbutton', function () {
            $('#mdlsearchuser').modal('show');
        });

        $('body').on('click', '#editbutton', function () {
            var $this = $(this);

            _doEditButton($this);
        });

        $('body').on('click', '#updatedetails', function () {
            _doUpdateDetails();
        });

        $('body').on('click', '#updateactive', function () {
            var $elem = $(this);

            $.alertWindow("Are you sure you want to inactive this record?",
            function () {
                _doInactiveDetails($elem);
            },
            function () {
            });
        });

        $('body').on('click', '#refreshbutton', function () {
            _doRefreshDetails();
        });

        $('body').on('click', '#btndiscard', function () {
            _doDiscardNewForm();
        });

        $('body').on('click', '#removefilter', function () {
            //$('#frmSearchUser #UserName').val('');
            //$('#frmSearchUser #SearchUserTypeId').data('kendoDropDownList').value(null);
            //$('#frmSearchUser #SearchUserYesNo').data('kendoDropDownList').value(null);
            //$('#frmSearchUser #SearchBranchID').data('kendoDropDownList').value(null);
            $('#frmSearchUser').deserialize(_variables.defaultSearchValue, true, null);
        });

        $('body').on('click', '#searchdetails', function () {
            _doSearchDetails();
        });

        $('body').on('click', '#savedetails', function () {
            _doSaveDetails();
        });

        $('#mdledituser').on('hidden.bs.modal', function () {
            $('#frmUpdateUser').deserialize(_variables.defaultEditValue, true, null);
        });
    }

    var _doInitializeKendoElements = function () {
        $("#UserTypeId").kendoDropDownList({
            optionLabel: 'Please Select',
            dataTextField: "UserTypeName",
            dataValueField: "UserTypeID",
            dataSource: {
                transport: {
                    read: {
                        dataType: "json",
                        url: _variables.params.userTypeUrl
                    }
                }
            }
        });

        $("#BranchID").kendoDropDownList({
            optionLabel: 'Please Select',
            dataTextField: "BranchName",
            dataValueField: "BranchId",
            dataSource: {
                transport: {
                    read: {
                        dataType: "json",
                        url: _variables.params.getBranchUrl
                    }
                }
            }
        });


        $("#EditUserTypeId").kendoDropDownList({
            optionLabel: 'Please Select',
            dataTextField: "UserTypeName",
            dataValueField: "UserTypeID",
            dataSource: {
                transport: {
                    read: {
                        dataType: "json",
                        url: _variables.params.userTypeUrl
                    }
                }
            }
        });

        $("#EditBranchID").kendoDropDownList({
            optionLabel: 'Please Select',
            dataTextField: "BranchName",
            dataValueField: "BranchId",
            dataSource: {
                transport: {
                    read: {
                        dataType: "json",
                        url: _variables.params.getBranchUrl
                    }
                }
            }
        });

        $("#SearchUserTypeId").kendoDropDownList({
            optionLabel: 'Select All',
            dataTextField: "UserTypeName",
            dataValueField: "UserTypeID",
            dataSource: {
                transport: {
                    read: {
                        dataType: "json",
                        url: _variables.params.userTypeUrl
                    }
                }
            }
        });

        $("#SearchBranchID").kendoDropDownList({
            optionLabel: 'Select All',
            dataTextField: "BranchName",
            dataValueField: "BranchId",
            dataSource: {
                transport: {
                    read: {
                        dataType: "json",
                        url: _variables.params.getBranchUrl
                    }
                }
            }
        });

        $("#SearchUserYesNo").kendoDropDownList({
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

    var _doShowNewForm = function () {
        $('#user-new-form').show();
        $('#divUser').hide();

        $('#searchbutton').hide();
        $('#refreshbutton').hide();
        $('#btnnewform').hide();

        $('#btndiscard').show();

    }

    var _doSearchDetails = function () {

        var $gv = $("#gvUser").data('kendoGrid'), $frmSearch = $('#frmSearchUser');

        doShowTrackerLoader(_variables.loaderMsg);

        $gv.dataSource.read($frmSearch.serializeToJson());

        doRemoveTrackerLoader();
        $('#mdlsearchuser').modal('hide');
        INFRA.doRemoveOpenedModal();
    }

    var _doRefreshDetails = function () {
        _doSearchDetails();
    }

    var _doDiscardNewForm = function () {
        $('#user-new-form').hide();
        $('#divUser').show();

        $('#searchbutton').show();
        $('#refreshbutton').show();
        $('#btnnewform').show();

        $('#btndiscard').hide();

        $('#frmUser').deserialize(_variables.defaultValue, true, null);
    }

    var _doBindTblUser = function () {
        var $gv = $('#gvUser'), schemaField = {}, columns = [],
            width, field;

        $gv.kGridResizeHeight({
            height: _variables.gvUserHeight,
            willRefreshGrid: false,
            isManual: true
        });
    }

    var _doSaveDetails = function () {
        var $form = $('#frmUser'), $UserTypeId = $('#frmUser #UserTypeId'), $BranchID = $('#frmUser #BranchID');
        var isDataReady = true;
        var $hdfUserTypeId = $('#frmUser #hdfUserTypeId');

        doShowTrackerLoader(_variables.loaderMsg);
        $.validator.unobtrusive.parse($form);
        $form.validate();
        if ($form.valid()) {
            if (INFRA.IsNullOrEmpty($UserTypeId.val())) {
                isDataReady = false;
                $('#frmUser [data-valmsg-for="UserTypeID"]').html("<br/>User Type field is a required field").removeClass("field-validation-valid").addClass("error");
            }
            else {
                isDataReady = true;
                $('#frmUser [data-valmsg-for="UserTypeID"]').html("").removeClass("error");
            }
            if (INFRA.ConvertToInteger($UserTypeId.val()) === 2) {
                if (INFRA.IsNullOrEmpty($BranchID.val())) {
                    isDataReady = false;
                    $('#frmUser [data-valmsg-for="BranchId"]').html("<br/>Branch field is a required field").removeClass("field-validation-valid").addClass("error");
                } else {
                    isDataReady = true;
                    $('#frmUser [data-valmsg-for="BranchId"]').html("").removeClass("error");
                }
            }


            if (INFRA.ConvertToBoolean(isDataReady)) {
                $.ajax({
                    url: _variables.params.saveUserDetailsUrl,
                    type: 'POST',
                    data: $form.serialize(),
                    success: function (data) {
                        doRemoveTrackerLoader();
                        $('#divResultContainer').show().html(data.alertMessage);
                        if (INFRA.ConvertToBoolean(data.isSuccess)) {
                            _doSearchDetails();
                            _doDiscardNewForm();
                        } else {

                        }

                    }
                });
            } else {
                doRemoveTrackerLoader();
            }


        } else {
            doRemoveTrackerLoader();
        }

    }

    var _doInactiveDetails = function (element) {
        var $elem = element;
        var userid, usertypeid, branchid, username;

        userid = $elem.data('user-id');
        usertypeid = $elem.data('user-type-id');
        branchid = $elem.data('user-branch-id');
        username = $elem.data('user-name');


        //int userId, int branchId, string userName, int userTypeId

        $.ajax({
            url: _variables.params.updateIsActiveDetailsUrl,
            type: 'POST',
            data: { userId: userid, branchId: branchid, userName: username, userTypeId: usertypeid },
            success: function (data) {
                $('#divResultContainer').show().html(data.alertMessage);
                if (INFRA.ConvertToBoolean(data.isSuccess)) {
                    _doSearchDetails();
                }
            }
        });
    }

    var _doEditButton = function (element) {
        var $elem = element;

        $('#frmUpdateUser #UserName').val($elem.data('user-name'));
        $('#frmUpdateUser #hdfUserId').val($elem.data('user-id'));
        $('#frmUpdateUser #EditUserTypeId').data('kendoDropDownList').value($elem.data('user-type-id'));
        $('#frmUpdateUser #EditBranchID').data('kendoDropDownList').value($elem.data('user-branch-id'));
        $('#mdledituser').modal('show');
    }

    var _doUpdateDetails = function () {
        var $frmUpdateUser = $('#frmUpdateUser');
        var $UserTypeId = $('#frmUpdateUser #EditUserTypeId'), $BranchID = $('#frmUpdateUser #EditBranchID');
        var isDataReady = true;

        debugger;
        doShowTrackerLoader(_variables.loaderMsg);
        $.validator.unobtrusive.parse($frmUpdateUser);
        $frmUpdateUser.validate();
        if ($frmUpdateUser.valid()) {

            if (INFRA.IsNullOrEmpty($UserTypeId.val())) {
                isDataReady = false;
                $('#frmUpdateUser [data-valmsg-for="UserTypeID"]').html("<br/>User Type field is a required field").removeClass("field-validation-valid").addClass("error");
            }
            else {
                isDataReady = true;
                $('#frmUpdateUser [data-valmsg-for="UserTypeID"]').html("").removeClass("error");
            }

            if (INFRA.ConvertToInteger($UserTypeId.val()) === 2) {
                if (INFRA.IsNullOrEmpty($BranchID.val())) {
                    isDataReady = false;
                    $('#frmUpdateUser [data-valmsg-for="BranchId"]').html("<br/>Branch field is a required field").removeClass("field-validation-valid").addClass("error");
                } else {
                    isDataReady = true;
                    $('#frmUpdateUser [data-valmsg-for="BranchId"]').html("").removeClass("error");
                }
            }

            if (INFRA.ConvertToBoolean(isDataReady)) {
                $.ajax({
                    url: _variables.params.updateUserDetailsUrl,
                    type: 'POST',
                    data: $frmUpdateUser.serialize(),
                    success: function (data) {
                        doRemoveTrackerLoader();
                        $('#divResultContainer').show().html(data.alertMessage);
                        if (INFRA.ConvertToBoolean(data.isSuccess)) {
                            _doSearchDetails();

                            $('#mdledituser').modal('hide');
                            INFRA.doRemoveOpenedModal();
                        }
                    }
                });
            } else {
                doRemoveTrackerLoader();
            }
        } else {
            doRemoveTrackerLoader();
        }
    }

    var initialize = function (options) {
        try {
            _variables.params = options || {};
            _doInitializeElements();
            _doInitializeKendoElements();
            _doBindTblUser();
            INFRA.doRemoveOpenedModal();
        } catch (e) {
            if (typeof console !== "undefined" && console !== null) {
                console.error("Error parsing inline options", ex);
            }
        }

        _variables.defaultValue = $('#frmUser').serialize();
        _variables.defaultEditValue = $('#frmUpdateUser').serialize();
        _variables.defaultSearchValue = $('#frmSearchUser').serialize();
    }

    return {
        initialize: initialize,
        _variables: _variables
    }

})();

$(document).ready(function () {
    var options = window.userOptions;
    ADMINMANAGEMENTUSER.initialize(options);

    $(window).resize(function () {
        $('#gvUser').kGridResizeHeight({
            height: ADMINMANAGEMENTUSER._variables.gvUserHeight,
            willRefreshGrid: false,
            isManual: true
        });
    });
});