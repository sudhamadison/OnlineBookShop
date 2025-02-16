$(document).ready(function () {
    $("#categoryTable").DataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        "ajax": {
            "url": "Category/GetAllCategoriesOld",
            "type": "POST",
            "datatype": "json"
        },
        "columnDefs": [{
            "targets": [0],
            "visbile": false,
            "searchable": false
        }],
        "columns": [
            { "data": "categoryId", "name": "Id", "autowidth": true },
            { "data": "name", "name": "Name", "autowidth": true },
            { "data": "description", "name": "Description", "autowidth": true }
        ]
    });
});