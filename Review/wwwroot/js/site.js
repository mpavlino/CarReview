"use strict";

var SiteModule = (function () {

    const loader = document.getElementById('page-loader');
    const overlay = document.getElementById('page-overlay');

    // Show loader when the page starts loading
    function showLoader() {
        if (loader) {
            loader.style.display = 'block';
        }
        if (overlay) {
            overlay.style.display = 'block'; // Show overlay to disable UI
        }
    }

    // Hide loader when the page has fully loaded
    function hideLoader() {
        if (loader) {
            loader.style.display = 'none';
        }
        if (overlay) {
            overlay.style.display = 'none'; // Hide overlay to enable UI
        }
    }
document.addEventListener('DOMContentLoaded', function () {

    // Load the saved selected link from sessionStorage
    const savedLink = sessionStorage.getItem('selectedNavLink');
    const navLinks = document.querySelectorAll('#sidebar .nav-link');

    // Ensure proper navigation link collapsing
    if (savedLink) {
        navLinks.forEach(function (link) {
            link.classList.add('collapsed'); // Add 'collapsed' to all links initially
            if (link.getAttribute('href') === savedLink) {
                link.classList.remove('collapsed'); // Remove 'collapsed' from the saved link
            }
        });
    } else {
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
            navLinks.forEach(function (link) {
                link.classList.add('collapsed');
            });
            navLink.classList.remove('collapsed');
            sessionStorage.setItem('selectedNavLink', navLink.getAttribute('href'));
            window.location.href = navLink.getAttribute('href');
        });
    });

    // Check and handle cookies for consent
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
        const consentButton = document.querySelector("#cookieConsent button[data-cookie-string]");
        if (consentButton) {
            consentButton.addEventListener("click", function (el) {
                document.cookie = el.target.dataset.cookieString;
                document.querySelector("#cookieConsent").classList.add("d-none");
            }, false);
        }
    }

    // Initialize date picker and select2 plugin
    if ($('#datepicker').length) {
        $("#datepicker").datepicker({
            format: "mm/yyyy",
            viewMode: "months",
            minViewMode: "months",
            orientation: "bottom auto"
        });
    }

    $('.select2-input').each(function () {
        var placeholder = $(this).attr('placeholder') || 'Select an option';
        $(this).select2({
            placeholder: placeholder,
            allowClear: true,
            width: 'resolve'
        });
    });

    // Initialize Bootstrap tooltips
    function initializeTooltips() {
        $('[data-bs-toggle="tooltip"]').tooltip();
    }
    initializeTooltips();

    // SweetAlert2 confirmation for delete buttons
    $(document).on('click', '.delete-btn', function (event) {
        event.preventDefault();
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
                window.location.href = deleteUrl;
            }
        });
    });

    // Pagination logic
    var $reviewsList = $('#reviews-list');
    if ($reviewsList.length) {
        var itemsPerPage = 5;
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
        showPage(1);
    }

    // Check if image carousel exists before setting up event listeners
    const imageCarousel = document.querySelector('#imageCarousel');
    if (imageCarousel) {
        const uploadedImagesInput = document.getElementById('uploadedImages');

        // Attach event listener to the carousel container
        imageCarousel.addEventListener('click', function (e) {
            if (e.target && e.target.classList.contains('btn-remove-image')) {
                const imageBase64 = e.target.getAttribute('data-image-base64');
                handleImageRemoval(imageBase64, e);
            }
        });

        // Function to handle image removal
        function handleImageRemoval(imageBase64, e) {
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
                    const carouselItem = e.target.closest('.carousel-item');
                    if (carouselItem) {
                        carouselItem.remove();
                    }
                    const remainingItems = document.querySelectorAll('#imageCarousel .carousel-item');
                    if (remainingItems.length === 0) {
                        imageCarousel.style.display = 'none';
                    } else {
                        remainingItems[0].classList.add('active');
                    }
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
                const src = img.getAttribute('src');
                return src.split(',')[1];
            });
            uploadedImagesInput.value = base64Strings.join(';');
        }
    }

    $("#syncCars").on('click', function () {
        showLoader();
    });

    //// Show the loader immediately
    //showLoader();

    //// Hide the loader when the window fully loads
    //window.addEventListener('load', function () {
    //    hideLoader();
    //});



});

    function bindAjax () {
        // Show / Hide loader on AJAX requests
        $(document).bind("ajaxStart.mine", function (e) {
            showLoader();
        }).bind("ajaxComplete.mine", function () {
            hideLoader();
        }).bind("ajaxError.mine", function () {
            hideLoader();
        });
    }

    bindAjax();

    return ({
        showLoader: showLoader,
        hideLoader: hideLoader
    });

})();
