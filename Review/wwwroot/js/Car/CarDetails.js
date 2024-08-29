"use strict";
document.addEventListener('DOMContentLoaded', function () {

    let currentImageIndex = 0;
    let imageList = [];

    // Function to open the modal and show an image
    function openImageModal(imageSrc, index) {
        currentImageIndex = index;
        document.getElementById('modalImage').src = imageSrc;
        var imageModal = new bootstrap.Modal(document.getElementById('imageModal'));
        imageModal.show();
        updateModalButtons();
    }

    // Event listener for image clicks in carousel
    $(document).on('click', '.carousel-item img', function () {
        const clickedImageSrc = $(this).attr('src');
        const carouselItems = $(this).closest('.carousel-inner').find('.carousel-item img');

        // Update the list of images and the current index
        imageList = carouselItems.map(function () {
            return $(this).attr('src');
        }).get();
        currentImageIndex = imageList.indexOf(clickedImageSrc);

        openImageModal(clickedImageSrc, currentImageIndex);
    });

    // Update state of the Next and Previous buttons
    function updateModalButtons() {
        document.getElementById('prevImageBtn').disabled = (currentImageIndex === 0);
        document.getElementById('nextImageBtn').disabled = (currentImageIndex === imageList.length - 1);
    }

    const nextBtn = document.querySelector('#nextImageBtn');
    const prevBtn = document.querySelector('#prevImageBtn');

    if (nextBtn && prevBtn) {
        // Next button event listener
        document.getElementById('nextImageBtn').addEventListener('click', function () {
            if (currentImageIndex < imageList.length - 1) {
                currentImageIndex++;
                document.getElementById('modalImage').src = imageList[currentImageIndex];
                updateModalButtons();
            }
        });

        // Previous button event listener
        document.getElementById('prevImageBtn').addEventListener('click', function () {
            if (currentImageIndex > 0) {
                currentImageIndex--;
                document.getElementById('modalImage').src = imageList[currentImageIndex];
                updateModalButtons();
            }
        });
    }
});
