$(document).ready(function () {
    GetAll();
    Usuarios();
});

function GetAll() {
    $.ajax({
        type: 'GET',
        url: '/Rol/GetAllRoles',
        success: function (result) {
            $('#tBodyRol').empty();

            $.each(result, function (i, rol) {
                var fila =
                    '<tr>'
                    + '<td style="text-align: center; vertical-align: middle;"><button type="button" class="btn btn-info" id="btnAsignar" onclick="Asignar(this)" value="' + rol.id + '">Asignar</button></td>'
                    // + '<td style="display: none;"></td>'
                    + '<td style="text-align: center;">' + rol.name + '</td>'
                    + '<td style="text-align: center; vertical-align: middle;"><button type="button" class="btn btn-warning" id="btnUpdate" onclick="Actualizar(this)"  value="' + rol.id + '">Editar</button></td>'
                    // + '<td style="display: none;"></td>'
                    + '<td style="text-align: center; vertical-align: middle;"><button type="button" class="btn btn-danger" onclick="Delete(this)"  value="' + rol.id + '">Eliminar</button></td>'
                    + '</tr>';
                $('#tBodyRol').append(fila);
            });
        },
        error: function (result) {
            alert('Error en la consulta.');
        }
    });
};

function Usuarios() {
    $.ajax({
        type: 'GET',
        url: '/Rol/GetAllUsuarios',
        success: function (result) {
            $('#ddlAsignar').append('<option value="' + 0 + '">' + 'Seleccione una opción' + '</option>');
            $.each(result.objects, function (i, user) {
                var select =
                    '<option id="idUsuario" value="' + user.idUsuario + '">' + user.userName + '</option>';
                $("#ddlAsignar").append(select);
            });
        },
        error: function (result) {
            alert('Error en la consulta.');
        }
    });
};

function BtnAdd() {
    $("#txtIdRol").val("");
    $("#txtNombre").val("");
    $('#modalAgregar').modal('show');
};

function BtnGuardar() {
    $.ajax({
        type: 'POST',
        url: '/Rol/Form',
        data: $('#formAgregar').serialize(),
        dataType: 'json',
        success: function (result) {
            $('#modalAgregar').modal('hide');

            $('#tBodyRol').empty();

            GetAll();
        },
        error: function (result) {
            alert('Error en la consulta.');
        }
    });
};

function Actualizar(idRol) {
    $.ajax({
        url: '/Rol/GetByIdRol',
        data: { IdRole: idRol.value },
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            $('#txtIdRol').val(data.object.roleId);
            $('#txtNombre').val(data.object.name);
            $('#modalAgregar').modal('show');

        },
        error: function () {
            alert('Disculpe, existió un problema');
        }
    });
};

function Delete(idRol) {
    var checkconfirm = confirm('¿Estas seguro de eliminarlo?');
    if (checkconfirm == true) {
        $.ajax({
            url: '/Rol/Delete',
            data: { IdRole: idRol.value },
            type: 'POST',
            dataType: 'json',
            success: function () {
                $('#tBodyRol').empty();
                GetAll();
            },
            error: function () {
                alert('Disculpe, existió un problema');
            }
        });
    } else {
        return false;
    }

};

function Asignar(idRol) {
    $.ajax({
        url: '/Rol/Asignar',
        data: { IdRole: idRol.value },
        type: 'GET',
        dataType: 'json',
        success: function (result) {
            $('#txtIdRole').val(result.rol.roleId);
            $('#ddlAsignar').val('0');
            $('#modalAsignar').modal('show');
        },
        error: function () {
            alert('Disculpe, existió un problema');
        }
    });
};

function BtnAsignar() {
    $.ajax({
        type: 'POST',
        url: '/Rol/Asignar',
        data: $('#formAsignar').serialize(),
        dataType: 'json',
        success: function (result) {
            $('#modalAsignar').modal('hide');

            $('#modalAsignado').modal('show');
        },
        error: function (result) {
            alert('Error en la consulta.');
        }
    });
};