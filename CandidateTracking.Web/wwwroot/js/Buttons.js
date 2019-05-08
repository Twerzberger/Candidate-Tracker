$(() => {

    $("#confirmed-btn").on('click', function () {
        const id = $('#confirmed-btn').data('id');
        $.post('/home/confirmed', { id }, function (ids) {

            document.getElementById('pending-count').textContent = ids.pending;
            document.getElementById('confirmed-count').textContent = ids.confirmed;
            document.getElementById('declined-count').textContent = ids.declined;

            removeButtons();
        });
    });

    $("#refused-btn").on('click', function () {
        const id = $('#refused-btn').data('id');
        $.post('/home/declined', { id }, function (ids) {

            document.getElementById('pending-count').textContent = ids.pending;
            document.getElementById('confirmed-count').textContent = ids.confirmed;
            document.getElementById('declined-count').textContent = ids.declined;

            removeButtons();

        });
    });

    const removeButtons = () => {
        $("#confirmed-btn").remove();
        $("#refused-btn").remove();
    }
});