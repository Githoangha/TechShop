$('body').on('click', '#btn-load', loadContent);
function loadContent() {
    $.ajax({
        type: 'post',
        url: '/home/studentPartial',
        success: function (data) {
            $('#cover').html(data)

        }
    });
}