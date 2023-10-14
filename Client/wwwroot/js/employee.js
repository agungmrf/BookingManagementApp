$("h1").html("Employee");

$(document).ready(function () {
    $('#employeeTable').DataTable({
        ordering: true,
        ajax: {
            url: 'http://localhost:5032/api/Employee',
            dataSrc: "data",
            dataType: "JSON"

        },
        columns: [
            {
                data: null,
                render: function (data, type, row, meta) {
                    return meta.row + 1;
                }
            },
            {data: "nik"},
            {
                data: 'null',
                render: function (data, type, row) {
                    return row.firstName + ' ' + row.lastName;
                }
            },
            {
                data: "birthDate",
                render: function (data, type) {
                    if (type === 'display' || type === 'filter') {
                        return moment(data).format('ddd, DD-MM-YYYY');
                    }
                    return data;
                }
            },
            {
                data: "gender",
                render: function (data) {
                    return data === 0 ? "Female" : "Male";
                }
            },
            {
                data: "hiringDate",
                render: function (data, type) {
                    if (type === 'display' || type === 'filter') {
                        return moment(data).format('ddd, DD-MM-YYYY');
                    }
                    return data;
                }
            },
            {data: "email"},
            {data: "phoneNumber"},
            {
                data: '',
                render: function (data, type, row) {
                    return `<button onclick="Update('${row.url}')" data-bs-toggle="modal" data-bs-target="" class="btn btn-warning btn-circle btn-sm"><i class="fas fa-edit"></i> </button>
                    <button onclick="Delete('${row.guid}')" data-bs-toggle="modal" data-bs-target="" class="btn btn-danger btn-circle btn-sm"><i class="fas fa-trash"></i> </button>`;
                }
            }
        ],

        dom:
            "<'row mb-3'<'col-sm-12 col-md-5 d-flex align-items-center justify-content-start'f><'col-sm-12 col-md-7 d-flex align-items-center justify-content-end'B>>" +
            "<'row'<'col-sm-12'tr>>" +
            "<'row'<'col-sm-12 col-md-5'i><'col-sm-12 col-md-7'p>>",
        buttons: [
            {
                extend: 'copyHtml5',
                exportOptions: {
                    columns: [0, ':visible']
                }
            },
            {
                extend: 'csvHtml5',
                exportOptions: {
                    columns: ':visible'
                }
            },
            {
                extend: 'excelHtml5',
                exportOptions: {
                    columns: ':visible'
                }
            },
            {
                extend: 'pdfHtml5',
                exportOptions: {
                    columns: [0, 1, 2, 5]
                }
            },
            {
                extend: 'print',
                exportOptions: {
                    columns: ':visible'
                }
            },
            {
                extend: 'colvis',
                collectionLayout: 'fixed columns',
                collectionTitle: 'Column visibility control'
            }
        ],
        initComplete: function () {
            var btns = $('.dt-button');
            btns.addClass('d-none d-sm-inline-block btn btn-sm btn-primary shadow-sm');
            btns.removeClass('dt-button');

        }
    });
});

$('#addEmployeeButton').click(function () {
    // Untuk mengosongkan form
    $('#employeeForm')[0].reset();
    // Untuk mengubah judul modal
    $('#employeeModal').modal('show');
});

$('#saveEmployeeButton').click(function () {
    // Ambil data dari form dan panggil fungsi Insert
    Insert();
    // Untuk reload table
    $('#employeeModal').modal('hide');
});

function Insert() {
    var employee = {};
    employee.FirstName = $("#firstname").val();
    employee.LastName = $("#lastname").val();
    employee.BirthDate = $("#birthdate").val();
    employee.Gender = parseInt($("#gender").val());
    employee.HiringDate = $("#hiringdate").val();
    employee.Email = $("#email").val();
    employee.PhoneNumber = $("#phonenumber").val();
    $.ajax({
        url: "http://localhost:5032/api/Employee",
        type: "POST",
        data: JSON.stringify(employee),
        contentType: "application/json",
        dataType: "json"
    }).done((result) => {
        alert("Success add Employee");
    }).fail((error) => {
        alert("Error add Employee");
    })
}

function Delete(guid) {
    if (confirm("Apakah anda yakin ingin menghapus data ini?")) {
        $.ajax({
            url: "http://localhost:5032/api/Employee/?guid=" + guid,
            type: "DELETE",
            contentType: "application/json",
            dataType: "json"
        }).done((result) => {
            alert("Data employee berhasil dihapus.");
            // Reload the table
            $('#employeeTable').DataTable().ajax.reload();
        }).fail((error) => {
            alert("Data employee gagal dihapus.");
        });
    }
}

function Update(url) {
    $.ajax({
        url: url,
        type: "GET",
        dataType: "json"
    }).done((employee) => {
        $("#firstname").val(employee.FirstName);
        $("#lastname").val(employee.LastName);
        $("#birthdate").val(employee.BirthDate);
        $("#gender").val(employee.Gender);
        $("#hiringdate").val(employee.HiringDate);
        $("#email").val(employee.Email);
        $("#phonenumber").val(employee.PhoneNumber);

        // Show the modal for editing
        $('#employeeModal').modal('show');
    }).fail((error) => {
        alert("Error fetching employee data for update.");
    });
}