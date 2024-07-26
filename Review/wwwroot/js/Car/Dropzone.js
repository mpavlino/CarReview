Dropzone.autoDiscover = false;

var $form = $('#carForm');
var $dropzone = $('#dropzone');
var dropzoneUrl = '/Car/UploadImage'; // Endpoint for file uploads

var myDropzone = new Dropzone($dropzone[0], {
    url: dropzoneUrl,
    autoProcessQueue: false,
    addRemoveLinks: true,
    uploadMultiple: false,
    acceptedFiles: 'image/*',
    success: function (file, response) {
        // Store the file data in a hidden field
        $('#ImageData').val(response.imageData); // Change according to your response
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
        submitForm(); // Submit form if no files are to be uploaded
    }
});

myDropzone.on('queuecomplete', function () {
    submitForm(); // Submit form after files are processed
});

function submitForm() {
    $form.off('submit').submit(); // Remove the handler and submit the form
}