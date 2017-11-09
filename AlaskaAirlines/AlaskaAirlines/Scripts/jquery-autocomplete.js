$(document).ready(function () {
    $("#FromAirport, #ToAirport").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: '/Home/AutoComplete',
                type: 'POST',
                dataType: 'JSON',
                data: { Prefix: request.term },
                success: function (data) {
                    $("#FromAirport_validationMessage").remove();
                    $("#ToAirport_validationMessage").remove();
                    response($.map(data, function (item) {
                        return { label: item.Name, value: item.Name };
                    }));
                }
            });
        },
        change: function (event, ui) {
            if (!ui.item) {
                $(this).val('');
                $this.focus();
                $();
            }
        },
        autoFocus: true
    });
});
