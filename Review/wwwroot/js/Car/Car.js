"use strict";

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