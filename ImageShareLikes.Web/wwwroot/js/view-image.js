$(() => {
    const id = $("#id").val();
    refreshLikes();

    setInterval(function () {
        console.log("This function will get called every 1000 milliseconds (or 1 second)");
        refreshLikes();
    }, 1000);

    $("#btn-like").on('click', function () {

        $("#btn-like").prop("disabled", true);

        $.post('/home/UpdateImageLikes', { id },
            function () {
                refreshLikes();
            });
    })

    function refreshLikes() {

        $.get('/home/GetLikes', { id }, function ({ likes }) {
            $("#current-likes").text(likes)
        });
    }
})