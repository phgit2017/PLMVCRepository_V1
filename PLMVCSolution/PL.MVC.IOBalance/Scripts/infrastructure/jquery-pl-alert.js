$(document).ready(function () {
    var messages = null, $this;
    messages = $('.js_mvc-alert').find('span');
    if (messages !== null) {

        $.each(messages, function () {
            $this = $(this);
            toastr[$this.data('type')]($this.html());

            toastr.options = {
                "closeButton": true,
                "debug": false,
                "newestOnTop": false,
                "progressBar": false,
                "positionClass": "toast-top-right",
                "preventDuplicates": false,
                "onclick": null,
                "showDuration": "300",
                "hideDuration": "1000",
                "timeOut": "5000",
                "extendedTimeOut": "1000",
                "showEasing": "swing",
                "hideEasing": "linear",
                "showMethod": "fadeIn",
                "hideMethod": "fadeOut"
            };

            $this.remove();
        });
    }
})