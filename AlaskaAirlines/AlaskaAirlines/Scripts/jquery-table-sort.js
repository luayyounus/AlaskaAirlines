function sort() {
    $("li.sort").click(function () {
        var from = $("#fromCode").attr("value");
        var to = $("#toCode").attr("value");
        var sort = $(this).attr('id');
        $.ajax({
            url: '/Flight/SortTable',
            method: 'POST',
            cache: false,
            data: { fromAirport: from, toAirport: to, sortBy: sort }
        }).done(function (response) {
            $("#table-body").html(response);
        });
    });
}