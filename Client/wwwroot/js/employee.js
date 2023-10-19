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
                data: null,
                render: function (data, type, row) {
                    return `<button onclick="UpdateGet('${row.url}')" data-bs-toggle="modal" data-bs-target="#employeeModalUpdate" class="btn btn-warning btn-circle btn-sm"><i class="fas fa-edit"></i> </button>
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
        Swal.fire(
            'Successfull',
            'Employee data was added successfully.',
            'success'
        );
        // Reload the table
        $('#employeeTable').DataTable().ajax.reload();
    }).fail((error) => {
        Swal.fire(
            'Error',
            'Failed to add employee data. Please try again.',
            'error'
        );
    });
}

function Delete(guid) {
    Swal.fire({
        title: 'Are you sure?',
        text: 'You won\'t be able to revert this!',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            // Lakukan penghapusan
            $.ajax({
                url: "http://localhost:5032/api/Employee/?guid=" + guid,
                type: "DELETE",
                contentType: "application/json",
                dataType: "json"
            }).done((result) => {
                Swal.fire(
                    'Successfull',
                    'Employee data was successfully deleted.',
                    'success'
                );
                // Reload the table
                $('#employeeTable').DataTable().ajax.reload();
            }).fail((error) => {
                Swal.fire(
                    'Error',
                    'Employee data failed to be deleted, please try again.',
                    'error'
                );
            });
        }
    });
}

function UpdateGet(data) {
    $.ajax({
        url: "http://localhost:5032/api/Employee/" + data, // Sesuaikan URL dengan endpoint API yang benar
        type: "GET",
        contentType: "application/json",
        dataType: "json"
    }).done(res => {
        $('#UGuid').val(res.data.guid);
        $('#UNik').val(res.data.nik);
        $('#update_firstname').val(res.data.firstName);
        $('#update_lastname').val(res.data.lastName);
        $('#update_birthdate').val(moment(res.data.birthDate).format('DD-MM-YYYY'));
        $('#update_gender').val(res.data.gender === 0 ? "Female" : "Male");
        $('#update_hiringdate').val(moment(res.data.hiringDate).format('DD-MM-YYYY'));
        $('#update_email').val(res.data.email);
        $('#update_phonenumber').val(res.data.phoneNumber);
    }).fail(error => {
        alert("Insert failed")
    });
}

function UpdateEmployee() {
    var obj = {
        guid: $('#UGuid').val(),
        nik: $('#UNik').val(),
        firstName: $("#update_firstname").val(),
        lastName: $("#update_lastname").val(),
        birthDate: $("#update_birthdate").val(),
        gender: ($('#update_gender').val() === "Female") ? 0 : 1,
        hiringDate: $("#update_hiringdate").val(),
        email: $("#update_email").val(),
        phoneNumber: $("#update_phonenumber").val()
    };

    $.ajax({
        url: "http://localhost:5032/api/Employee/", // Sesuaikan URL dengan endpoint API yang benar
        type: "PUT",
        data: JSON.stringify(obj),
        contentType: "application/json",
        //dataType: "json"
    }).done(result => {
        // Tambahkan kode untuk menampilkan pemberitahuan jika berhasil
        Swal.fire(
            'Good job!',
            'Data has been successfuly updated!',
            'success'
        ).then(() => {
            location.reload(); // Mereset form
        });

    }).fail(error => {
        // Tambahkan kode untuk menampilkan pemberitahuan jika gagal
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Failed to update data! Please try again.'
        })
    });
}
