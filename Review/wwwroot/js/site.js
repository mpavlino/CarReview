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
});