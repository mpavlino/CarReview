Dropzone.autoDiscover = false;

var $form = $('#carForm');
var $dropzone = $('#dropzone');
var dropzoneUrl = '/Car/UploadImageCarReview'; // Your controller action endpoint

var myDropzone = new Dropzone($dropzone[0], {
    url: dropzoneUrl,
    autoProcessQueue: false,
    uploadMultiple: true,
    paramName: "files", // This will automatically add [0], [1], etc. for multiple files
    addRemoveLinks: true,
    acceptedFiles: 'image/*',
    successmultiple: function (files, response) {
        var uploadedImages = $('#uploadedImages').val();
        response.forEach(function (image) {
            uploadedImages += image.imageData + ";"; // Assuming the response includes image paths
        });
        $('#uploadedImages').val(uploadedImages);
    },
    error: function (file, response) {
        alert('Upload failed: ' + response);
    }
});

$form.on('submit', function (e) {
    e.preventDefault(); // Prevent default form submission

    if (myDropzone.getQueuedFiles().length > 0) {
        myDropzone.processQueue(); // Process files in Dropzone
    } else {
        submitForm(); // Submit form if no files are queued
    }
});

myDropzone.on('queuecomplete', function () {
    submitForm(); // Submit form after files are processed
});

function submitForm() {
    $form.off('submit').submit(); // Remove the handler and submit the form
}
