// Authentication Animation
//
// HOW TO USE
//  1. Just put showBGLoader() to triggers.
//  2. hideBGLoader() is available if you want to manually hide the loader.
//
// REQUIRED MARKUP ELEMENTS
//  <div class="bg-animate">
//      <div class="loader-container">
//            <div class="loader-cell">
//            <div class="spinner"></div>
//            <h4 class="loader-message" id="loaderMessage">Initializing IRIS</h4>
//      </div>
//  </div>

var count = 1,
    prev,
    counterIncrement = 1,
    counter, 
    $body = $('.bg-animate'),
    cur_class = 'bg' + count,
    m_count = 1,
    $msg = $('#loaderMessage'),
    $loaderContainer = $('.bg-animate');

function showBGLoader() {

    $('body').removeClass('login-background');


    $loaderContainer.show();
    //var counter = setInterval(
    //    function () {
    //    }
    //    , 6000);
    counter = setInterval(
    function () {
        if (count >= 4) {
            count = 1;
            prev = count - 1;
            cur_class = 'bg' + count;
            prev_class = 'bg' + prev;
            $body.addClass(cur_class);
            $body.removeClass('bg4');
        }
        else {
            count = count + counterIncrement;
            prev = count - 1;
            cur_class = 'bg' + count;
            prev_class = 'bg' + prev;
            $body.addClass(cur_class);
            $body.removeClass(prev_class);
        }

        m_count = m_count + 1;

        if (m_count == 2) { $msg.html('Building User Experience'); }
        else if (m_count == 3) { $msg.html('Materializing IRIS Modules'); }
        else if (m_count == 4) { $msg.html('Hang in there... Just a little bit more'); }
    }, 4000);
}
function hideBGLoader() {

    $body.attr('class', 'bg-animate');

    $('body').addClass('login-background');


    count = 1;
    clearInterval(counter);
    $loaderContainer.hide();
}