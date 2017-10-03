$('body').on('shown.bs.modal', function () {
    $('body').css('overflow-y', '');
});

var INFRA = {
    IsNull: function (obj) {
        return obj == null || typeof obj === "undefined";
    },
    IsEmpty: function (str) {
        return str === "";
    },
    IsZeroLength: function (obj) {
        if (typeof obj === "function") {
            return false;
        }

        return obj.length === 0;
    },
    IsNullOrEmpty: function (obj) {
        return INFRA.IsNull(obj) || INFRA.IsEmpty(obj) || INFRA.IsZeroLength(obj);
    },
    ReplaceIfNullOrEmpty: function (valueToTest, valueToReplace) {
        if (INFRA.IsNullOrEmpty(valueToTest)) {
            return valueToReplace;
        }
        else {
            return valueToTest;
        }
    },
    ConvertToBoolean: function (obj) {
        if (INFRA.IsNullOrEmpty(obj)) {
            return false;
        }
        else if (typeof obj === "boolean") {
            return obj;
        }

        return obj.toLowerCase() === "true";
    },
    ConvertToInteger: function (obj) {
        return parseInt(INFRA.ReplaceIfNullOrEmpty(obj, 0));
    },
    IsDate: function (date) {
        return !isNaN(Date.parse(date));
    },
    doRemoveOpenedModal: function () {
        $('body').removeClass('modal-open');
        $('.modal-backdrop').remove();
    },
    ConvertToBoolean: function (obj) {
        if (INFRA.IsNullOrEmpty(obj)) {
            return false;
        }
        else if (typeof obj === "boolean") {
            return obj;
        }

        return obj.toLowerCase() === "true";
    },
    ConvertToFloat: function (obj) {
        return parseFloat(INFRA.ReplaceIfNullOrEmpty(obj, 0));
    }
};

$.fn.serializeToJson = function () {
    var result = {};
    var data = this.serializeArray();
    $.each(data, function () {
        result[this.name] = this.value;
    });

    return result;
}