"use strict";

let dataTable;

document.addEventListener('DOMContentLoaded', function () {
    const tableElement = document.querySelector('#tbl-cars');
    dataTable = new simpleDatatables.DataTable(tableElement, {
        searchable: true,  // Disable client-side search (since you're doing server-side search)
        fixedHeight: true,  // Example option to ensure a fixed height
        paging: true        // Enable client-side paging
    });
    window.datatable = dataTable
    dataTable.on("datatable.init", () => {
        dataTable.initialized = true
    })
});

function performSearch(sender) {
    var formValues = $(sender).closest('form').serialize();

    $.ajax({
        url: '/Car/IndexAjax',
        data: formValues,
        method: "POST",
        success: function (html) {
            // Destroy the existing DataTable instance
            const tableElement = document.querySelector('#tbl-cars');
            var table = dataTable;
            //dataTable = new simpleDatatables.DataTable(tableElement);
            table.destroy();  // Remove the current table

            // Replace table body with new data
            $('#tbl-cars tbody').html(html);

            table = new simpleDatatables.DataTable(tableElement, {
                searchable: true,  // Disable client-side search (since you're doing server-side search)
                fixedHeight: true,  // Example option to ensure a fixed height
                paging: true        // Enable client-side paging
            });
        }
    });
}



$(document).ready(function () {
    $('#BrandID').change(function () {
        var brandId = $(this).val();

        // Clear existing options in the Model dropdown
        $('#ModelID').empty().append('<option value="">Select Model</option>');

        // If a valid brand is selected, fetch models
        if (brandId) {
            $.ajax({
                url: '/Car/GetModelsByBrand', // Assuming you have an action method for this
                type: 'GET',
                data: { brandId: brandId },
                success: function (data) {
                    // Populate the Model dropdown with fetched data
                    $.each(data, function (index, item) {
                        $('#ModelID').append($('<option>', {
                            value: item.value,
                            text: item.text
                        }));
                    });
                },
                error: function (e) {
                    alert('Failed to fetch models. Please try again.');
                }
            });
        }
    });


    $('#Brand').change(function () {
        var brandId = $(this).val();

        // Clear existing options in the Model dropdown
        $('#Model').empty().append('<option value="">Select Model</option>');

        // If a valid brand is selected, fetch models
        if (brandId) {
            $.ajax({
                url: '/Car/GetModelsByBrand', // Assuming you have an action method for this
                type: 'GET',
                data: { brandId: brandId },
                success: function (data) {
                    // Populate the Model dropdown with fetched data
                    $.each(data, function (index, item) {
                        $('#Model').append($('<option>', {
                            value: item.value,
                            text: item.text
                        }));
                    });
                },
                error: function () {
                    alert('Failed to fetch models. Please try again.');
                }
            });
        }
    });
});