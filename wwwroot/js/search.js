﻿$("#searchInput").autocomplete({
    delay: 100,
    source: function (request, response) {
        $.ajax({
            url: '/Home/GetSearchValue',
            dataType: "json",
            data: { search: request.term },
            success: function (data) {
                response($.map(data, function (item) {
                    return { label: item.name, value: item.ID_City };
                }));
            },
            error: function (xhr, status, error) {
                alert("Error");
            }
        });
    },
    select: function (event, ui) {
        $("#searchInput").val(ui.item.label);
        $("#ID_City").val(ui.item.value);
        $("#searchValue").val($("#searchInput").val());
        return false;
    }
});

function submitForm(event) {
    event.preventDefault();

    var input = document.getElementById("searchInput");
    var cityId = input.value;

    if (cityId != null) {
        var form = event.target;
        form.action = form.action + "?ID_City=" + encodeURIComponent(cityId);
        form.submit();
    }
}

document.getElementById("searchInput").addEventListener("keyup", function (event) {
    if (event.keyCode === 13) {
        if ($("#searchInput").autocomplete("instance").menu.active) {
            event.preventDefault();
        } else {
            submitForm(event);
        }
    }
});