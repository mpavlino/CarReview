// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
"use strict"

document.addEventListener('DOMContentLoaded', function () {
    // Load the saved selected link from sessionStorage
    const savedLink = sessionStorage.getItem('selectedNavLink');
    const navLinks = document.querySelectorAll('#sidebar .nav-link');

    if (savedLink) {
        navLinks.forEach(function (link) {
            link.classList.add('collapsed'); // Add 'collapsed' to all links initially
            if (link.getAttribute('href') === savedLink) {
                link.classList.remove('collapsed'); // Remove 'collapsed' from the saved link
            }
        });
    } else {
        // If no link is saved, ensure the first link is not collapsed
        navLinks.forEach(function (link, index) {
            if (index === 0) {
                link.classList.remove('collapsed');
            } else {
                link.classList.add('collapsed');
            }
        });
    }

    // Add click event listeners to all nav links
    navLinks.forEach(function (navLink) {
        navLink.addEventListener('click', function (event) {
            event.preventDefault(); // Prevent the default link click behavior

            // Add 'collapsed' class to all links
            navLinks.forEach(function (link) {
                link.classList.add('collapsed');
            });

            // Remove 'collapsed' class from the clicked link
            navLink.classList.remove('collapsed');

            // Save the selected link to sessionStorage
            sessionStorage.setItem('selectedNavLink', navLink.getAttribute('href'));

            // Navigate to the link destination
            window.location.href = navLink.getAttribute('href');
        });
    });

    function checkCookie(cookieName, cookieValue) {
        const cookies = document.cookie.split(';');
        for (let i = 0; i < cookies.length; i++) {
            const cookie = cookies[i].trim();
            if (cookie.startsWith(cookieName + '=' + cookieValue)) {
                return true;
            }
        }
        return false;
    }

    var cookieExists = checkCookie('.AspNet.Consent', 'yes');

    if (!cookieExists) {
        document.querySelector("#cookieConsent button[data-cookie-string]").addEventListener("click", function (el) {
            document.cookie = el.target.dataset.cookieString;
            document.querySelector("#cookieConsent").classList.add("d-none");
        }, false);
    }


    $("#datepicker").datepicker({
        format: "mm/yyyy",
        viewMode: "months",
        minViewMode: "months",
        orientation: "bottom auto"
    });
    $('.select2-input').each(function () {
        var placeholder = $(this).attr('placeholder') || 'Select an option'; // Default placeholder if none provided
        $(this).select2({
            placeholder: placeholder,
            allowClear: true,
            width: 'resolve' // Ensure it takes the width of the parent container
        });
    });

    // Initialize Bootstrap tooltips
    function initializeTooltips() {
        $('[data-bs-toggle="tooltip"]').tooltip();
    }

    // Call the function on page load
    initializeTooltips();

    // SweetAlert2 confirmation for delete buttons
    $(document).on('click', '.delete-btn', function (event) {
        event.preventDefault(); // Prevent the default action

        var deleteUrl = $(this).attr('href');

        Swal.fire({
            title: 'Are you sure?',
            text: "You won't be able to revert this!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, delete it!'
        }).then((result) => {
            if (result.isConfirmed) {
                // Redirect to the delete URL
                window.location.href = deleteUrl;
            }
        });
    });

    var itemsPerPage = 5; // Number of items to show per page
    var $reviewsList = $('#reviews-list');
    var $paginationControls = $('#pagination-controls');
    var $items = $reviewsList.children('.review-item');

    function showPage(page) {
        $items.hide();
        $items.slice((page - 1) * itemsPerPage, page * itemsPerPage).show();
    }

    function setupPagination() {
        var totalPages = Math.ceil($items.length / itemsPerPage);
        var $pagination = $paginationControls.find('.pagination');
        $pagination.empty();

        for (var i = 1; i <= totalPages; i++) {
            var $pageItem = $('<li class="page-item"></li>');
            var $pageLink = $('<a class="page-link" href="#">' + i + '</a>');
            if (i === 1) $pageItem.addClass('active');
            $pageLink.click(function (e) {
                e.preventDefault();
                var pageNum = $(this).text();
                showPage(parseInt(pageNum));
                $pagination.find('.page-item').removeClass('active');
                $(this).parent().addClass('active');
            });
            $pageItem.append($pageLink);
            $pagination.append($pageItem);
        }
    }

    setupPagination();
    showPage(1); // Show the first page initially

    const uploadedImagesInput = document.getElementById('uploadedImages');
    // Attach event listener to the carousel container
    // Function to handle image removal
    function handleImageRemoval(imageBase64, e) {
        // Use SweetAlert2 for confirmation
        Swal.fire({
            title: 'Are you sure?',
            text: "Do you want to remove this image?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, remove it!',
            cancelButtonText: 'Cancel'
        }).then((result) => {
            if (result.isConfirmed) {
                // Remove the image element from the DOM
                const carouselItem = e.target.closest('.carousel-item');
                if (carouselItem) {
                    carouselItem.remove();
                }

                // If no items are left, hide the carousel
                const remainingItems = document.querySelectorAll('#imageCarousel .carousel-item');
                if (remainingItems.length === 0) {
                    document.getElementById('imageCarousel').style.display = 'none';
                } else {
                    // Recalculate the active class to avoid carousel breaking
                    remainingItems[0].classList.add('active');
                }

                // Update hidden input with remaining images
                updateUploadedImages();

                Swal.fire(
                    'Removed!',
                    'The image has been removed.',
                    'success'
                );
            }
        });
    }

    // Function to update uploadedImages hidden input
    function updateUploadedImages() {
        const remainingItems = document.querySelectorAll('#imageCarousel .carousel-item img');
        const base64Strings = Array.from(remainingItems).map(img => {
            // Extract base64 from src attribute
            const src = img.getAttribute('src');
            return src.split(',')[1]; // Remove the data URL part (everything before the comma)
        });

        // Update the hidden input field with base64 strings
        uploadedImagesInput.value = base64Strings.join(';');
    }

    // Attach event listener to the carousel container
    document.querySelector('#imageCarousel').addEventListener('click', function (e) {
        if (e.target && e.target.classList.contains('btn-remove-image')) {

            // Get the base64 string from the data attribute
            const imageBase64 = e.target.getAttribute('data-image-base64');

            // Call function to handle image removal
            handleImageRemoval(imageBase64, e);
        }
    });

});