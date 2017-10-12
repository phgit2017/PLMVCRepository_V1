(function ($) {

    var loaderclass = ".loader-mask";

    $.fn.showLoader = function (options) {
        var loaderType, message, loader;

        if (typeof options !== "object") {
            alert('showLoader: Error parsing options.');
            return;
        }

        loaderType = ((typeof (options.type) === "undefined" || options.type === "") ? 'section' : options.type);
        message = ((typeof (options.message) === "undefined" || options.message === "") ? "Loading..." : options.message);


        if (loaderType === "page") {
            loaderType = "loader-page";
        }
        else {
            loaderType = "loader-section";
        }

        loader = "<div class='loader-mask " + loaderType + "' style='display:none'>";
        loader += "<div class='loader-container'>";
        loader += "<div class='loader-cell'>";
        loader += "<div class='spinner'></div>";
        loader += "<span class='loader-message'>" + message + "</span>";
        loader += "</div>";
        loader += "</div>";
        loader += "</div>";

        this.find(loaderclass).remove();

        this.append(loader);

        this.find(loaderclass).toggle();

        return this;
    };

    $.fn.hideLoader = function () {

        this.find(loaderclass).remove();

        return this;
    };

}(jQuery));