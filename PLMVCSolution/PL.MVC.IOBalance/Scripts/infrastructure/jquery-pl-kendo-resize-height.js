(function ($) {
    $.fn.kGridResizeHeight = function (options) {
        var gridElement, dataArea, lockedArea, newGridHeight, newDataAreaHeight, $this, $thisKendoData, isPageable, isGroupable, hasLockColumns, st,
            basedHeight, willRefreshGrid, isManual, hasExtraHeader, isAngular;

        if (typeof options !== "object") {
            alert('kGridResizeHeight: Error parsing options.');
            return;
        }

        willRefreshGrid = (typeof (options.willRefreshGrid) === "undefined" || options.willRefreshGrid === "") ? false : options.willRefreshGrid;
        isManual = (typeof (options.isManual) === "undefined" || options.isManual === "") ? false : options.isManual;
        hasExtraHeader = (typeof (options.hasExtraHeader) === "undefined" || options.hasExtraHeader === "") ? false : options.hasExtraHeader;
        isAngular = (typeof (options.isAngular) === "undefined" || options.isAngular === "") ? false : options.isAngular;
        basedHeight = options.height;

        if (!(typeof (basedHeight) === "undefined" || basedHeight === "")) {
            try {
                basedHeight = parseInt(basedHeight);
            } catch (e) {
                alert('kGridResizeHeight: Missing or Invalid based height');
                return;
            }
        }

        if (!isManual) {
            hasExtraHeader = false;
        }

        $this = $(this);

        gridElement = $this;
        $thisKendoData = $this.data('kendoGrid');

        isPageable = $thisKendoData.options.pageable;
        isGroupable = $thisKendoData.options.groupable;

        if (isManual) {
            gridElement = $(gridElement.data('kendoGrid').wrapper);
        }

        dataArea = gridElement.find(".k-grid-content").eq(0);
        lockedArea = gridElement.find('.k-grid-content-locked').eq(0);

        hasLockColumns = lockedArea != null;

        //newGridHeight = $(document).height() - basedHeight;
        newGridHeight = $(window).height() - basedHeight;
        newDataAreaHeight = newGridHeight - 65;

        if (!isPageable) {
            newDataAreaHeight = newDataAreaHeight + 40;
        }

        dataArea.height(newDataAreaHeight);

        if (hasLockColumns) {
            if (isAngular) {
                lockedArea.height(newDataAreaHeight + 15);
                //dataArea.width(dataArea.width() + 17);
            }
            else {
                lockedArea.height(newDataAreaHeight);
            }
        }

        if (isManual) {
            if (hasExtraHeader) {
                newGridHeight = newGridHeight + 50;
            }

            newGridHeight = newGridHeight + 13;
        }

        if (isAngular) {
            if (isGroupable && isPageable) {
                newGridHeight = 29
            }
            else if (!isGroupable && isPageable) {
                newGridHeight = 17;
            }
            else if (isGroupable && !isPageable) {
                newGridHeight = 0;
            }
            else if (!isGroupable && !isPageable) {
                newGridHeight = 0;
                newDataAreaHeight = newDataAreaHeight + 32;
                dataArea.height(newDataAreaHeight);
            }

            newGridHeight = newGridHeight + (isGroupable ? gridElement.find('.k-grouping-header').eq(0).height() : 0) + gridElement.find('.k-grid-header').eq(0).height() + gridElement.find('.k-grid-content').eq(0).height() + (isPageable ? gridElement.find('.k-grid-pager').eq(0).height() : 0);
        }

        gridElement.height(newGridHeight);

        if (willRefreshGrid) {
            $thisKendoData.refresh();
        }
        return this;
    };

    $.fn.kGridFixHeight = function () {
        $(this).data('kendoGrid').refresh();
        return this;
    };

}(jQuery));