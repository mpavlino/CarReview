
// Prevent Dropzone from auto discovering this element
Dropzone.autoDiscover = false;

var url = $('form').attr('action');

// Initialize Dropzone
var myDropzone = new Dropzone("#dropzone", {
    url: $('form').attr('action'), // Get the form action URL
    autoProcessQueue: false,
    addRemoveLinks: true,
    init: function () {
        var dz = this;

        // Override the form submit to process Dropzone queue first
        $('form').on('submit', function (e) {
            e.preventDefault();
            e.stopPropagation();

            if (dz.getQueuedFiles().length > 0) {
                dz.processQueue(); // Process Dropzone queue
            } else {
                this.submit(); // Submit form if no files are queued
            }
        });

        dz.on("success", function (file, response) {
            // Submit the form after successful upload
            $('form')[0].submit();
        });

        dz.on("error", function (file, response) {
            // Handle the error response
            alert(response);
        });
    },
    accept: function (file, done) {
        done();
    }
});
