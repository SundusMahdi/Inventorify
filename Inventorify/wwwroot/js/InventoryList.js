var dataTable;
var restock = 0;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "home/getall",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "name", "width": "15%" },
            { "data": "group", "width": "15%" },
            {
                "data": "count",
                "render": function (data) {
                    if (data < 100) {
                        restock += .25;
                        return `<div class='text-danger'>${data}</div>`;
                    }
                    else {
                        return `${data}`;
                    }
                }, "width": "15%"
            },
            { "data": "unitPrice", "width": "15%" },
            { "data": "totalPrice", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                        <a href="/home/Upsert?id=${data}" class='btn btn-success text-white m-1' style='cursor:pointer; width:70px;'>
                            Edit
                        </a>
                        <a class='btn btn-danger text-white m-1' style='cursor:pointer; width:70px;'
                            onclick=Delete('/home/Delete?id='+${data})>
                            Delete
                        </a>
                        </div>`;
                }, "width": "25%"
            }
        ],
        "language": {
            "emptyTable": "no data found"
        },
        "width": "100%"
    });
}

function Delete(url) {
    swal({
        title: "Are you sure?",
        text: "Once deleted, you will not be able to recover",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}

function displayWarnings() {
    var e = document.getElementById("restockWarning");
    if (restock == 0) {
        e.innerHTML = null;
    } else if (restock == 1) {
        e.style.diplay = "block";
        e.innerHTML = '<img class="mr-1 mb-1" src="danger.png" height="20"/> 1 item needs to be restocked';
    } else if (restock > 1) {
        e.style.diplay = "block";
        e.innerHTML = '<img class="mr-1 mb-1" src="danger.png" height="20"/>' + restock + ' items need to be restocked';
    }
}