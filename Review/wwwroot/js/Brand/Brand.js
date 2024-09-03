"use strict";

var BrandsModule = (function () {
    // Define functions outside of DOMContentLoaded for accessibility in the return

    const loader = document.getElementById('page-loader');
    const tableLoader = document.getElementById('table-loader');
    //const overlay = document.getElementById('page-overlay');
    const datatable = document.getElementById('tbl-brands');
    function showLoader() {
        if (loader) {
            loader.style.display = 'block';
        }
        //if (overlay) {
        //    overlay.style.display = 'block'; // Show overlay to disable UI
        //}
    }

    function hideLoader() {
        if (loader) {
            loader.style.display = 'none';
        }
        //if (overlay) {
        //    overlay.style.display = 'none'; // Hide overlay to enable UI
        //}
    }

    //if (overlay) {
    //    overlay.style.display = 'block'; // Show overlay to disable UI
    //}

    document.addEventListener('DOMContentLoaded', () => {
        // Show the loader initially if found
        //showLoader();

        try {
            // Initialize DataTable
            //const table = new simpleDatatables.DataTable("#tbl-brands");
            const table = new simpleDatatables.DataTable("#tbl-brands", {
                perPage: 10,
                searchable: true,
                sortable: true,
                deferRender: true, // If supported or consider similar options
            });

            // Hide loader once DataTable initialization is complete
            table.on('datatable.init', () => {
                tableLoader.style.display = 'none';
                //overlay.style.display = 'none';
                //datatable.attributes.removeNamedItem("style");
            });
        } catch (error) {
            console.error("Error initializing DataTable:", error);
            hideLoader();
            tableLoader.style.display = 'none';
        }

        // Example event for a button with id 'syncBrands'
        $("#syncBrands").on('click', function () {
            showLoader();
        });
    });

    // Return functions for external access
    return {
        showLoader: showLoader,
        hideLoader: hideLoader
    };

})();
