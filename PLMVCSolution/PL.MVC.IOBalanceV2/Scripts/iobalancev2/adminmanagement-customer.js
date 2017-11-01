var ADMINMANAGEMENTCUSTOMER = (function () {
    var _variables =
    {
        params: null,
        defaultSaveValue: null,
        defaultEditValue: null,
        defaultSearchValue: null,
        gvHeight: 180
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

        $('.loader-mask').show();
        $.validator.unobtrusive.parse($frm);
        $frm.validate();
        if ($frm.valid()) {
            $.ajax({
                url: _variables.params.saveUrl,
                type: 'POST',
                data: $frm.serialize(),
                success: function (data) {
                    $('.loader-mask').hide();
                    if (data.isSuccess == true) {
                        _doSearchDetails();
                        _discardSaveForm();
                        toastr.success(data.alertMessage);
                    } else {
                        toastr.error(data.alertMessage);
                    }
                }
            });
        } else {
            $('.loader-mask').hide();
            toastr.error('An error occured during the process. Please check the details or the required fields.');
        }

        


    }

    var _doUpdateForm = function () {
        var $frm = $('#frmEdit');
        $('.loader-mask').show();
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

                        toastr.success(data.alertMessage);
                    } else {
                        toastr.error(data.alertMessage);
                    }

                    $('.loader-mask').hide();
                }
            });
            
            $('#mdledit').modal('hide');
        }
        else {
            $('.loader-mask').hide();
            toastr.error('An error occured during the process. Please check the details or the required fields.');
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
        var $gv = $("#gvCustomer").data('kendoGrid'), $frm = $('#frmSearch');

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
                'show':true,
                'backdrop': 'static',
                'keyboard': false
            });
        });

        $('body').on('shown.bs.modal', '#mdledit', function () {
            $(document).off('focusin.modal');

            $('#frmEdit #hdfCustomerId').val($this.data('customer-id'));
            $('#frmEdit #CustomerCode').val($this.data('customer-code'));
            $('#frmEdit #CustomerName').val($this.data('customer-name'));
            $('#frmEdit #CustomerAddress').val($this.data('customer-address'));
            $('#frmEdit #hdfDateCreated').val($this.data('customer-datecreated'));
            $('#frmEdit #hdfCreatedBy').val($this.data('customer-createdby'));
            $('#frmEdit #hdfIsActive').val($this.data('customer-isactive'));

            //$('#EditBranchID').data('kendoDropDownList').value($this.data('discount-branchid'));
        });

        $('body').on('hidden.bs.modal', '#mdledit', function () {
            $('#frmEdit').deserialize(_variables.defaultEditValue, true, null);
        });

        $('body').on('click', '#updatedetails', function () {
            _doUpdateForm();
        });


        $('body').on('click', '#updateactive', function () {
            var $this = $(this), dto = {};
            dto = {
                CustomerId: $this.data('customer-id'),
                CustomerCode: $this.data('customer-code'),
                CustomerName: $this.data('customer-name'),
                CustomerAddress: $this.data('customer-address'),
                DateCreated: $this.data('customer-datecreated'),
                CreatedBy: $this.data('customer-createdby'),
                IsActive: $this.data('customer-isactive'),
            };

            $.alertWindow("Are you sure you want to change the status of this record?",
            function () {
                $('.loader-mask').show();
                $.ajax({
                    url: _variables.params.activateUrl,
                    type: 'POST',
                    data: { 'dto': dto },
                    success: function (data) {
                        
                        if (data.isSuccess == true) {
                            _doSearchDetails();
                            toastr.success(data.alertMessage);
                        } else {
                            toastr.error(data.alertMessage);
                        }
                        $('.loader-mask').hide();
                    }

                });
            },
            function () {
            });
        });


    }

    var _doInitializeKendoElements = function ()
    {
        $("#SearchCustomerYesNo").kendoDropDownList({
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
    ADMINMANAGEMENTCUSTOMER.initialize(options);
    var $gv = $('#gvCustomer');


    $gv.kGridResizeHeight({
        height: ADMINMANAGEMENTCUSTOMER._variables.gvHeight,
        willRefreshGrid: false,
        isManual: true
    });

    $(window).resize(function () {
        $gv.kGridResizeHeight({
            height: ADMINMANAGEMENTCUSTOMER._variables.gvHeight,
            willRefreshGrid: false,
            isManual: true
        });
    });

});