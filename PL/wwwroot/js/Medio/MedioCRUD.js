﻿$(document).ready(function () {
    GetAll();
    GetAllAutor();
    GetAllIdioma();
    GetAllTipoMedio();
    GetAllEditorial();
});
function PreviewImage(event) {
    var output = document.getElementById('imgMedio');
    output.src = URL.createObjectURL(event.target.files[0]);
    output.onload = function () {
        URL.revokeObjectURL(output.src)
    }
};
function GetAll() {
    $.ajax({
        type: 'GET',
        url: 'http://localhost:5138/api/Medio/getall',
        success: function (result) {
            $('#tBodyMedioAdmin').empty();
            $('#tBodyMedioUser').empty();

            var filaUser;

            if (result.objects != null) {
                $.each(result.objects, function (i, medio) {
                    $('#alertBody').empty();
                    $('#myTable').show();
                    var imagen;
                    if (medio.imagen == null) {
                        imagen = '<img src="/Images/medio.png"  width = "50px" height = "50px"/>'
                    }
                    else {
                        imagen = '<img src="data:image/png;base64,' + medio.imagen + '" width = "50px" height = "50px"/>'
                    }

                    var disponibilidad;

                    if (medio.disponibilidad == true) {
                        disponibilidad =
                            '<td>'
                            + '<div class="form-check form-switch">'
                            + '<input class="form-check-input" type="checkbox" id="flexSwitchCheckDefault" checked onchange="ChangeStatus(' + medio.idMedio + ',' + medio.disponibilidad + ')">'
                            + '</div>'
                            + '</td>'
                    } else {
                        disponibilidad =
                            '<td>'
                            + '<div class="form-check form-switch">'
                            + '<input class="form-check-input" type="checkbox" id="flexSwitchCheckDefault" onchange="ChangeStatus(' + medio.idMedio + ',' + medio.disponibilidad + ');">'
                            + '</div>'
                            + '</td>'
                    }
                    var fila =
                        '<tr>'
                        + '<td class="text-center"><button type="button" class="btn btn-warning bi bi-pencil-fill" id="btnUpdate" onclick="Modal(this)"  value="' + medio.idMedio + '"></button></td>'
                        + '<td hidden>' + medio.idMedio + '</td>'
                        + '<td class="text-center">' + medio.nombre + '</td>'
                        + '<td class="text-center">' + medio.archivo + ' </td>'
                        + '<td class="text-center">' + medio.descripcion + ' </td>'
                        + disponibilidad
                        + '<td class="text-center">' + imagen + '</td>'
                        + '<td class="text-center">' + medio.autor.nombre + '</td>'
                        + '<td class="text-center">' + medio.idioma.nombre + '</td>'
                        + '<td class="text-center">' + medio.tipoMedio.nombre + '</td>'
                        + '<td class="text-center">' + medio.editorial.nombre + '</td>'
                        + '<td class="text-center"><button type="button" class="btn btn-danger bi bi-trash-fill" onclick="Delete(this)"  value="' + medio.idMedio + '"></button></td>'
                        + '</tr>';
                    $('#tBodyMedioAdmin').append(fila);
                });
                $.each(result.objects, function (i, medio) {
                    if (medio.disponibilidad == true) {
                        var imagen;
                        if (medio.imagen == null) {
                            imagen = '<img src="/Images/medio.png"  width = "50px" height = "50px"/>'
                        }
                        else {
                            imagen = '<img src="data:image/png;base64,' + medio.imagen + '" width = "50px" height = "50px"/>'
                        }
                        filaUser =
                            '<tr>'
                            + '<td hidden>' + medio.idMedio + '</td>'
                            + '<td class="text-center">' + medio.nombre + '</td>'
                            + '<td class="text-center">' + medio.archivo + ' </td>'
                            + '<td class="text-center">' + medio.descripcion + ' </td>'
                            + '<td class="text-center">' + imagen + '</td>'
                            + '<td class="text-center">' + medio.autor.nombre + '</td>'
                            + '<td class="text-center">' + medio.idioma.nombre + '</td>'
                            + '<td class="text-center">' + medio.tipoMedio.nombre + '</td>'
                            + '<td class="text-center">' + medio.editorial.nombre + '</td>'
                            + '</tr>';
                        $('#tBodyMedioUser').append(filaUser);
                    } else {
                        $('#tableUser').hide();
                        filaUser = '<div class="alert alert-danger" role="alert">No existen medios disponibles.</div>';
                        $('#alertBodyUser').append(filaUser);
                    }
                    

                });
            } else {
                $('#myTable').hide();
                $('#tableUser').hide();
                filaUser = '<div class="alert alert-danger" role="alert">No existen registros.</div>';
                $('#alertBody').append(filaUser);
            }

        }
    });
};
function GetAllAutor() {
    $.ajax({
        type: 'POST',
        url: '/Medio/GetAllAutor',
        dataType: 'json',
        success: function (result) {
            $('#ddlAutor').empty();
            $('#ddlAutor').append('<option value="' + 0 + '">' + 'Seleccione una opción' + '</option>');
            $.each(result.objects, function (i, autor) {
                var select =
                    '<option value="' + autor.idAutor + '">' + autor.nombre + '</option>';
                $("#ddlAutor").append(select);
            });
        },
        error: function (result) {
            alert('Error en la consulta.');
        }
    });
};
function GetAllIdioma() {
    $.ajax({
        type: 'POST',
        url: '/Medio/GetAllIdioma',
        dataType: 'json',
        success: function (result) {
            $('#ddlIdioma').empty();
            $('#ddlIdioma').append('<option value="' + 0 + '">' + 'Seleccione una opción' + '</option>');
            $.each(result.objects, function (i, idioma) {
                var select =
                    '<option value="' + idioma.idIdioma + '">' + idioma.nombre + '</option>';
                $("#ddlIdioma").append(select);
            });
        },
        error: function (result) {
            alert('Error en la consulta.');
        }
    });
};
function GetAllTipoMedio() {
    $.ajax({
        type: 'POST',
        url: '/Medio/GetAllTipoMedio',
        dataType: 'json',
        success: function (result) {
            $('#ddlTipoMedio').empty();
            $('#ddlTipoMedio').append('<option value="' + 0 + '">' + 'Seleccione una opción' + '</option>');
            $.each(result.objects, function (i, tipoMedio) {
                var select =
                    '<option value="' + tipoMedio.idTipoMedio + '">' + tipoMedio.nombre + '</option>';
                $("#ddlTipoMedio").append(select);
            });
        },
        error: function (result) {
            alert('Error en la consulta.');
        }
    });
};
function GetAllEditorial() {
    $.ajax({
        type: 'POST',
        url: '/Medio/GetAllEditorial',
        dataType: 'json',
        success: function (result) {
            $('#ddlEditorial').empty();
            $('#ddlEditorial').append('<option value="' + 0 + '">' + 'Seleccione una opción' + '</option>');
            $.each(result.objects, function (i, editorial) {
                var select =
                    '<option value="' + editorial.idEditorial + '">' + editorial.nombre + '</option>';
                $("#ddlEditorial").append(select);
            });
        },
        error: function (result) {
            alert('Error en la consulta.');
        }
    });
};
function addBtn() {
    $("#txtIdMedio").val("");
    $("#txtNombre").val("");
    $("#txtArchivo").val("");
    $('#txtDescripcion').val("");
    $('#txtDisponibilidad').val("");
    $('#fuImagen').val("");
    $("#ddlAutor").val("0");
    $('#ddlIdioma').val("0");
    $('#ddlTipoMedio').val("0");
    $('#ddlEditorial').val("0");
    $('#imgMedio').attr('src', '/Images/medio.png');
    $('#modal').modal('show');
};
function guardarBtn() {

    var formData = new FormData(document.getElementById('myForm'));

    $.ajax({
        type: 'POST',
        url: '/Medio/Form',
        data: formData,
        processData: false,
        contentType: false,
        success: function () {
            $('#modal').modal('hide');

            GetAll();
        },
        error: function (result) {
            alert('Error en la consulta.');
        }
    });
};
function Modal(boton) {


    $.ajax({
        url: '/Medio/GetByIdMedio',

        data: { idMedio: boton.value },

        type: 'GET',

        dataType: 'json',

        success: function (data) {

            $('#txtIdMedio').val(data.idMedio)
            $('#txtNombre').val(data.nombre)

            $("#txtArchivo").val(data.archivo)
            $('#txtDescripcion').val(data.descripcion)
            $('#txtDisponibilidad').val(data.disponibilidad)
            // $('#fuImagen').val(data.imagen)
            $('#ddlAutor').val(data.autor.idAutor)
            $('#ddlIdioma').val(data.idioma.idIdioma)
            $('#ddlTipoMedio').val(data.tipoMedio.idTipoMedio)
            $('#ddlEditorial').val(data.idEditorial)

            $('#subirImagen').empty();

            var imagen;

            if (data.Imagen == null) {
                imagen = '<img src="/Images/medio.png"  id="imgMedio" width = "50px" height = "50px"/>'
            }
            else {
                imagen = '<img src="data:image/png;base64,' + data.imagen + '"id="imgMedio" width = "50px" height = "50px"/>'
            }
            $('#subirImagen').append(imagen);

            $('#modal').modal('show');

        },
        error: function () {
            alert('Error en la consulta');
        },
    });
};
function Delete(datos) {
    var checkconfirm = confirm('¿Estas seguro que desea elimianr este registro?');
    if (checkconfirm == true) {
        $.ajax({
            url: '/Medio/Delete',
            data: { idMedio: datos.value },
            type: 'GET',
            dataType: 'json',
            success: function () {
                GetAll();
            },
            error: function (result) {
                alert('Error en la consulta.');
            }
        });
    } else {
        return false;
    }
}
function ChangeStatus(idMedio, e) {
    var disponibilidad;

    if (e == true) {
        disponibilidad = false;
    } else {
        disponibilidad = true;
    }
    $.ajax({
        type: 'POST',
        url: '/Medio/CambiarStatus',
        dataType: 'json',
        data: { idMedio, disponibilidad },
        success: {},
        error: function (ex) {
            alert('Failed.' + ex);
        }
    });
}