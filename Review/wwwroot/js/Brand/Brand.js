"use strict";


var BrandsModule = (function () {
document.addEventListener('DOMContentLoaded', () => {
    const loader = document.getElementById('page-loader');

    function showLoader() {
        loader.style.display = 'block';
    }

    // Hide loader when the page has fully loaded
    function hideLoader() {
        loader.style.display = 'none';
    }

    // Show the loader initially if found
    if (loader) {
        showLoader();
    }

    //var spinner = new Spin.Spinner().spin($("#page-loader"));

    try {
        // Initialize DataTable
        const table = new simpleDatatables.DataTable("#tbl-brands");

        // Hide loader once DataTable initialization is complete
        table.on('datatable.init', () => {
            if (loader) {
                hideLoader();
            }
        });
    } catch (error) {
        console.error("Error initializing DataTable:", error);
        if (loader) {
            hideLoader();
        }
    }

    $("#syncBrands").on('click', function () {
        showLoader();
    });
});
    return ({
        showLoader: showLoader,
        hideLoader: hideLoader
    });

})();