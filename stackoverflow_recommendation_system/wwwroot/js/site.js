// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(function() {
    var pageNumber = 1;
    var inProgress = false;

    function loadData() {
        if (!inProgress) {
            inProgress = true;
            $('#loading').show();
            
            $.ajax({
                url: 'Home/FetchData/',
                data: { pageNumber: pageNumber },
                type: 'GET',
                dataType: 'json',
                success: function(data) {
                    pageNumber++;
                    for (var i = 0; i < data.length; i += 2) {
                        // Prepare the first item
                        var item1 = data[i];
                        var content1  = item1 ? '<div class="col-6">' +
                            '<div class="rounded-4 border border-2 border-secondary rounded-3">' +
                            '<p class="text-uppercase fw-bold fs-3">' + item1 + '</p>' +
                            '<p class="fs-6">' + item1 + '</p>' +
                            '<div class="d-flex justify-content-evenly">' +
                            '<div class="">' +
                            '<p class="fs-6">' + item1 + '</p>' +
                            '<p class="fs-6">' + item1 + '</p>' +
                            '</div>' +
                            '<div class="d-flex justify-content-center flex-column text-start " style="padding-right: 1rem;">' +
                            '<p class="fs-6 fw-bold fst-italic" style="margin-bottom: 0.2rem;">' + item1 + '</p>' +
                            '<p class="lh-lg text-success" style="margin-bottom: 0.2rem;">' + item1 + '</p>' +
                            '<p class="lh-lg text-success" style="margin-bottom: 0.2rem;">' + item1 + '</p>' +
                            '</div>' +
                            '</div>' +
                            '</div>' +
                            '</div>': '';
                        // Prepare the second item, if it exists
                        var item2 = data[i + 1];
                        var content2  = item2 ? '<div class="col-6">' +
                            '<div class="rounded-4 border border-2 border-secondary rounded-3">' +
                            '<p class="text-uppercase fw-bold fs-3">' + item2 + '</p>' +
                            '<p class="fs-6">' + item2 + '</p>' +
                            '<div class="d-flex justify-content-evenly">' +
                            '<div class="d-flex justify-content-center flex-column">' +
                            '<p class="fs-6">' + item2 + '</p>' +
                            '<p class="fs-6">' + item2 + '</p>' +
                            '</div>' +
                            '<div class="d-flex justify-content-center flex-column text-start " style="padding-right: 1rem;">' +
                            '<p class="fs-6 fw-bold fst-italic" style="margin-bottom: 0.2rem;">' + item2 + '</p>' +
                            '<p class="lh-lg text-success" style="margin-bottom: 0.2rem;">' + item2 + '</p>' +
                            '<p class="lh-lg text-success" style="margin-bottom: 0.2rem;">' + item2 + '</p>' +
                            '</div>' +
                            '</div>' +
                            '</div>' +
                            '</div>': '';
                        // Combine both items into a single row
                        var newRow = '<div class="row pt-4 ">' + content1 + content2 + '</div>';

                        $('#data-container').append(newRow);
                    }
                    $('#loading').hide();
                    inProgress = false;
                },
                error: function() {
                    $('#loading').hide();
                    inProgress = false;
                    // Handle error
                }
            });
        }
    }

    $(window).scroll(function() {
        if ($(window).scrollTop() + $(window).height() > $(document).height() - 100) {
            loadData();
        }
    });

    loadData(); // Initial load
});