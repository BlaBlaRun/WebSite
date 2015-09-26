$(document).ready(function () {
    // Setup - add a text input to each footer cell
    $('#table tfoot th').each(function () {
        var title = $('#table thead th').eq($(this).index()).text();
        $(this).html('<input type="text" placeholder="Search ' + title + '" />');
    });


    // DataTable
    var table = $('#table').DataTable();


    // Apply the search
    table.columns().every(function () {
        var that = this;

        $('input', this.footer()).on('keyup change', function () {
            if (that.search() !== this.value) {
                that
                    .search(this.value)
                    .draw();
            }
        });
    });
});

