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

});