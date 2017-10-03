(function (jQuery, undefined) {

    var push = Array.prototype.push,
        rcheck = /^(?:radio|checkbox)$/i,
        rplus = /\+/g,
        rselect = /^(?:option|select-one|select-multiple)$/i,
        rvalue = /^(?:button|color|date|datetime|datetime-local|email|hidden|month|number|password|range|reset|search|submit|tel|text|textarea|time|url|week)$/i;

    function getElements(elements) {
        return elements.map(function () {
            return this.elements ? jQuery.makeArray(this.elements) : this;
        }).filter(":input:not(:disabled)").get();
    }

    function getElementsByName(elements) {
        var current,
            elementsByName = {};

        jQuery.each(elements, function (i, element) {
            current = elementsByName[element.name];
            elementsByName[element.name] = current === undefined ? element :
                (jQuery.isArray(current) ? current.concat(element) : [current, element]);
        });

        return elementsByName;
    }

    jQuery.fn.deserialize = function (data, willResetValidation, options) {
        var i, length,
            elements = getElements(this),
            normalized = [];
        $(this).formReset(willResetValidation);

        if (!data || !elements.length) {
            return this;
        }

        if (jQuery.isArray(data)) {
            normalized = data;

        } else if (jQuery.isPlainObject(data)) {
            var key, value;

            for (key in data) {
                jQuery.isArray(value = data[key]) ?
                    push.apply(normalized, jQuery.map(value, function (v) {
                        return { name: key, value: v };
                    })) : push.call(normalized, { name: key, value: value });
            }

        } else if (typeof data === "string") {
            var parts;

            data = data.split("&");

            for (i = 0, length = data.length; i < length; i++) {
                parts = data[i].split("=");
                push.call(normalized, {
                    name: decodeURIComponent(parts[0].replace(rplus, "%20")),
                    value: decodeURIComponent(parts[1].replace(rplus, "%20"))
                });
            }
        }

        if (!(length = normalized.length)) {
            return this;
        }

        var current, element, j, len, name, property, type, value,
            change = jQuery.noop,
            complete = jQuery.noop,
            names = {}, $element, elementParent, ddl, selectedValue,
            mvcChkBox, mvcChkBoxHidden, ismvcCheck, kendoNumeric, kendoNumericVal, kendoCurrent, kendoCurrentWidth, kendoDatePicker, kendoDatePickerVal;

        options = options || {};
        elements = getElementsByName(elements);

        // Backwards compatible with old arguments: data, callback
        if (jQuery.isFunction(options)) {
            complete = options;

        } else {
            change = jQuery.isFunction(options.change) ? options.change : change;
            complete = jQuery.isFunction(options.complete) ? options.complete : complete;
        }

        for (i = 0; i < length; i++) {
            current = normalized[i];

            name = current.name;
            value = current.value;

            if (!(element = elements[name])) {
                continue;
            }

            if (i > 0 && name === normalized[i - 1].name) {
                continue;
            }

            type = (len = element.length) ? element[0] : element;
            type = (type.type || type.nodeName).toLowerCase();

            property = null;
            if (rvalue.test(type)) {
                if (len) {
                    j = names[name];
                    element = element[names[name] = (j == undefined) ? 0 : ++j];
                }

                change.call(element, (element.value = value));
                element.setAttribute('value', value);

                $element = $(element);
                elementParent = $element.parent();

                if (elementParent.parent().hasClass('k-datepicker')) {
                    kendoCurrent = $('#' + $element.get(0).id);
                    kendoDatePicker = kendoCurrent.data('kendoDatePicker');
                    kendoDatePickerVal = kendoDatePicker.value();

                    kendoDatePicker._old = new Date(value);
                    kendoDatePicker._value = new Date(value);

                }

                if (elementParent.hasClass('k-dropdown')) {
                    ddl = $element.data("kendoDropDownList");
                    if (value === "") {
                        ddl.select(0);
                    }
                    else {
                        selectedValue = value === "" ? 0 : parseInt(value);
                        ddl.value(selectedValue);
                    }
                }

                if (elementParent.parent().hasClass('k-numerictextbox')) {
                    kendoCurrent = $('#' + $element.get(0).id);
                    kendoCurrentWidth = kendoCurrent.css('width');
                    kendoNumeric = kendoCurrent.data('kendoNumericTextBox');
                    kendoNumericVal = $element.val();

                    kendoNumeric._old = kendoNumericVal;
                    kendoNumeric._value = null;
                    kendoNumeric._text.val(kendoNumericVal);
                    kendoNumeric.element.val(kendoNumericVal);

                    //kendoCurrent.kendoNumericTextBox({
                    //    format: "c"
                    //});

                    elementParent.parent().css('width', kendoCurrentWidth);

                }


                //if (type === "hidden") {
                //    mvcChkBoxHidden = $("input[type='hidden'][name='" + current.name + "']");
                //    mvcChkBox = $("input[type='checkbox'][name='" + current.name + "']");

                //    if (mvcChkBox.length !== 0 && mvcChkBoxHidden.length !== 0) {
                //        mvcChkBox.prop('checked', mvcChkBoxHidden.val())
                //    }
                //}
            } else if (rcheck.test(type)) {
                property = "checked";

                mvcChkBoxHidden = $("input[type='hidden'][name='" + current.name + "']");
                mvcChkBox = $("input[type='checkbox'][name='" + current.name + "']");

                if (mvcChkBox.length !== 0 && mvcChkBoxHidden.length !== 0) {

                    ismvcCheck = mvcChkBoxHidden.val();

                    if (typeof ismvcCheck === "undefined") {
                        ismvcCheck = false;
                    }
                    else {
                        ismvcCheck = ismvcCheck.toLowerCase() === "true";
                    }

                    mvcChkBox.prop('checked', ismvcCheck)
                }

            } else if (rselect.test(type)) {
                property = "selected";
            }

            if (property) {
                if (!len) {
                    element = [element];
                    len = 1;
                }

                for (j = 0; j < len; j++) {
                    current = element[j];

                    if (current.value == value) {
                        change.call(current, (current[property] = true) && value);
                    }
                }
            }
        }

        complete.call(this);

        return this;
    };

})(jQuery);

(function ($) {
    //re-set all client validation given a jQuery selected form or child
    $.fn.resetValidation = function () {
        var $form = this.closest('form');

        //reset jQuery Validate's internals
        $form.validate().resetForm();

        //reset unobtrusive validation summary, if it exists
        $form.find("[data-valmsg-summary=true]")
            .removeClass("validation-summary-errors")
            .addClass("validation-summary-valid")
            .find("ul").empty();

        //reset unobtrusive field level, if it exists
        $form.find("[data-valmsg-replace]")
            .removeClass("field-validation-error")
            .addClass("field-validation-valid")
            .empty();

        return $form;
    };

    //reset a form given a jQuery selected form or a child
    //by default validation is also reset
    $.fn.formReset = function (resetValidation) {
        var $form = this.closest('form');

        $form.find(".input-validation-error")
            .removeClass("input-validation-error");

        // $form.get(0).reset();

        if (resetValidation == undefined || resetValidation) {
            $form.resetValidation();
        }

        return $form;
    }
})(jQuery);
