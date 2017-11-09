function submitForm() {
    $("#submit-button").click(function () {
        var from = $("#FromAirport").val();
        var to = $("#ToAirport").val();
        $.ajax({
            url: '/Home/Search',
            method: 'POST',
            cache: false,
            data: { FromAirport: from, ToAirport: to }
        }).done(function (response) {
            $("#flights-results").html(response);
        }).fail(function () {
            alert("failed submitting form");
        });
    });
}