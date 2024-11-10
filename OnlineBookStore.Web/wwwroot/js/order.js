var dataTable;
$(document).ready(function () {
    var url = window.location.search;
    if (url.includes('inProcess')) {
        loadDataTable('inProcess');
    }
    else {
        if (url.includes('completed')) {
            loadDataTable('completed');
        }
        else {
            if (url.includes('pending')) {
                loadDataTable('pending');
            }
            else {
                if (url.includes('approved')) {
                    loadDataTable('approved');
                }
                else {
                    loadDataTable('all');
                }
            }
        }
    }
    //loadDataTable();
    console.log("Order.js called");
});

function loadDataTable(status) {
    console.log($('#tblData').length);  // Should output 1 if the table exists

    dataTable = $('#tblData').DataTable({
        "ajax":
            { url: '/admin/order/getall?status=' + status },
        "columns": [
            {data : 'id', "width": "5%"},
            { data: 'name', "width": "15%" },
            { data: 'phoneNumber', "width": "20%" },
            { data: 'applicationUser.email', "width": "10%" },
            { data: 'orderStatus', "width": "10%" },
            { data: 'orderTotal', "width": "10%" },
            {
                data: 'id',
                "render": function (data) {
                    //return '<div>Hello</div>'
                    return `<div class="w-75 btn-group" role="group"><a href = "/admin/order/details?orderId=${data}" class="btn btn-primary mx-2" > <i class="bi bi-pencil-square"></i></a></div > `
                    //return
                    //`<div class="w-75 btn-group" role="group">
                    //    <a href="" class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i>Edit</a>
                    //    <a href="" class="btn btn-danger mx-2"> <i class="bi bi-trash-fill"></i>Delete</a>
                    //</div>`
                },
                "width": "25%"
            },
        ]
    });
    console.log("Datatable: ", dataTable.columns().data());
}

function Delete(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }
            })
        }
    });
}