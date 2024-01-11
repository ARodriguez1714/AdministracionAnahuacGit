$(document).ready(function () {
    GetAll();

    $("#ddlPais").change(function () {
        $("#ddlEstado").empty();
        $("#ddlMunicipio").empty();
        $("#ddlColonia").empty();
        $.ajax({
            type: 'POST',
            url: '/Editorial/GetEstado',
            dataType: 'json',
            data: { IdPais: $("#ddlPais").val() },
            success: function (estados) {
                $("#ddlEstado").append('<option value="0">' + 'Seleccione una opción' + '</option>');
                $("#ddlMunicipio").append('<option value="0">' + 'Seleccione una opción' + '</option>');
                $("#ddlColonia").append('<option value="0">' + 'Seleccione una opción' + '</option>');
                $.each(estados, function (i, estados) {
                    $("#ddlEstado").append('<option value ="'
                        + estados.idEstado + '">'
                        + estados.nombre + '</option>');
                });
            },
            error: function (ex) {
                alert('Failed. ' + ex)
            }
        });
    });

    $("#ddlEstado").change(function () {
        $("#ddlMunicipio").empty();
        $("#ddlColonia").empty();
        $.ajax({
            type: 'POST',
            url: '/Editorial/GetMunicipio',
            dataType: 'json',
            data: { IdEstado: $("#ddlEstado").val() },
            success: function (municipios) {
                $("#ddlMunicipio").append('<option value="0">' + 'Seleccione una opción' + '</option>');
                $("#ddlColonia").append('<option value="0">' + 'Seleccione una opción' + '</option>');
                $.each(municipios, function (i, municipios) {
                    $("#ddlMunicipio").append('<option value ="'
                        + municipios.idMunicipio + '">'
                        + municipios.nombre + '</option>');
                });
            },
            error: function (ex) {
                alert('Failed. ' + ex)
            }
        });
    });

    $("#ddlMunicipio").change(function () {
        $("#ddlColonia").empty();
        $.ajax({
            type: 'POST',
            url: '/Editorial/GetColonia',
            dataType: 'json',
            data: { IdMunicipio: $("#ddlMunicipio").val() },
            success: function (colonias) {
                $("#ddlColonia").append('<option value="0">' + 'Seleccione una opción' + '</option>');
                $.each(colonias, function (i, colonias) {
                    $("#ddlColonia").append('<option value ="'
                        + colonias.idColonia + '">'
                        + colonias.nombre + '</option>');
                });
            },
            error: function (ex) {
                alert('Failed. ' + ex)
            }
        });
    });
});

function GetAll() {
    $.ajax({
        type: 'GET',
        url: 'http://localhost:5138/api/Editorial/getalleditorial',
        success: function (result) {
            $('#tBodyEditorial').empty();
            $('#errorAlert').empty();

            if (result.objects.length === 0) {
                var alert =
                    '<div class="alert alert-danger" role="alert">'+result.message+'</div>';
                $('#errorAlert').append(alert);
            } else {
                $.each(result.objects, function (i, editorial) {
                    var fila =
                        '<tr>'
                        + '<td class="text-center"><button type="button" class="btn btn-warning" id="btnUpdate" onclick="Modal(' + editorial.idEditorial + ')"><i class="bi bi-pencil"></i></button></td>'
                        + '<td hidden>' + editorial.idEditorial + '</td>'
                        + '<td class="text-center">' + editorial.nombre + '</td>'
                        + '<td class="text-center">' + editorial.direccion.calle + ', ' + editorial.direccion.numeroInterior
                        + ', ' + editorial.direccion.colonia.nombre + ', ' + editorial.direccion.colonia.codigoPostal + '</td>'
                        + '<td class="text-center"><button type="button" class="btn btn-danger" onclick="Delete(' + editorial.idEditorial + ')"><i class="bi bi-trash3"></i></button></td>'
                        + '</tr>';
                    $('#tBodyEditorial').append(fila);
                });
            }
        }
    });
};

function btnAdd() {
    $("#txtIdEditorial").val("");
    $("#txtNombre").val("");
    $("#txtIdDireccion").val("");
    $('#ddlPais').val("");
    $("#ddlEstado").empty();
    $("#ddlMunicipio").empty();
    $("#ddlColonia").empty();
    $.ajax({
        type: 'POST',
        url: '/Editorial/GetEstado',
        dataType: 'json',
        data: { IdPais: $("#ddlPais").val() },
        success: function (estados) {
            $("#ddlEstado").append('<option value="0">' + 'Seleccione un Estado' + '</option>');
            $("#ddlMunicipio").append('<option value="0">' + 'Seleccione un Municipio' + '</option>');
            $("#ddlColonia").append('<option value="0">' + 'Seleccione una Colonia' + '</option>');
            $.each(estados, function (i, estados) {
                $("#ddlEstado").append('<option value ="'
                    + estados.idEstado + '">'
                    + estados.nombre + '</option>');
            });

            $("#ddlMunicipio").empty();
            $("#ddlColonia").empty();
            $.ajax({
                type: 'POST',
                url: '/Editorial/GetMunicipio',
                dataType: 'json',
                data: { IdEstado: $("#ddlEstado").val() },
                success: function (municipios) {
                    $("#ddlMunicipio").append('<option value="0">' + 'Seleccione un Municipio' + '</option>');
                    $("#ddlColonia").append('<option value="0">' + 'Seleccione una Colonia' + '</option>');
                    $.each(municipios, function (i, municipios) {
                        $("#ddlMunicipio").append('<option value ="'
                            + municipios.idMunicipio + '">'
                            + municipios.nombre + '</option>');
                    });

                    $("#ddlColonia").empty();
                    $.ajax({
                        type: 'POST',
                        url: '/Editorial/GetColonia',
                        dataType: 'json',
                        data: { IdMunicipio: $("#ddlMunicipio").val() },
                        success: function (colonias) {
                            $("#ddlColonia").append('<option value="0">' + 'Seleccione una Colonia' + '</option>');
                            $.each(colonias, function (i, colonias) {
                                $("#ddlColonia").append('<option value ="'
                                    + colonias.idColonia + '">'
                                    + colonias.nombre + '</option>');
                            });
                        },
                        error: function (ex) {
                            alert('Failed. ' + ex)
                        }
                    });
                },
                error: function (ex) {
                    alert('Failed. ' + ex)
                }
            });
        },
        error: function (ex) {
            alert('Failed. ' + ex)
        }
    });


    $('#ddlMunicipio').val("0");
    $('#ddlColonia').val("0");
    $('#txtCalle').val("");
    $('#txtNumeroInterior').val("");
    $('#txtNumeroExterior').val("");
    $('#modal').modal('show');
};

function BtnGuardar() {

    var editorial =
    {
        "idEditorial": Number($('#txtIdEditorial').val()),
        "nombre": $('#txtNombre').val(),
        "direccion": {
            "idDireccion": Number($('#txtIdDireccion').val()),
            "calle": $('#txtCalle').val(),
            "numeroInterior": $('#txtNumeroInterior').val(),
            "numeroExterior": $('#txtNumeroExterior').val(),
            "colonia": {
                "idColonia": Number($('#ddlColonia').val()),
                "nombre": "string",
                "codigoPostal": "string",
                "municipio": {
                    "idMunicipio": Number($('#ddlMunicipio').val()),
                    "nombre": "string",
                    "estado": {
                        "idEstado": Number($('#ddlEstado').val()),
                        "nombre": "string",
                        "pais": {
                            "idPais": Number($('#ddlPais').val()),
                            "nombre": "string",
                            "paises": [
                                "string"
                            ]
                        },
                        "estados": [
                            "string"
                        ]
                    },
                    "municipios": [
                        "string"
                    ]
                },
                "colonias": [
                    "string"
                ]
            }
        },
        "editoriales": [
            "string"
        ]
    }

    if (editorial.idEditorial == 0) {
        Add(editorial);
    } else {
        Update(editorial);
    }
    //var formData = new FormData(document.getElementById('myForm'));

    //var formData = $('#myForm').serialize();

    //$.ajax({
    //    type: 'POST',
    //    url: 'http://localhost:5138/api/Editorial/guardar',
    //    data: formData,
    //    processData: false,
    //    contentType: false,
    //    success: function () {
    //        $('#modal').modal('hide');

    //        $('#tBodyEditorial').empty();

    //        GetAll();

    //    }
    //});
};

function Add(editorial) {
    $.ajax({
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        type: 'POST',
        url: 'http://localhost:5138/api/Editorial/add',
        dataType: 'json',
        data: JSON.stringify(editorial),
        success: function (result) {
            $('#modal').modal('hide');

            GetAll();
        },
        error: function (result) {
            alert(result.responseJSON.errors);
        }
    });
};
function Update(editorial) {
    $.ajax({
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        type: 'PUT',
        url: 'http://localhost:5138/api/Editorial/update',
        dataType: 'json',
        data: JSON.stringify(editorial),
        success: function (result) {
            $('#modal').modal('hide');

            GetAll();
        },
        error: function (result) {
            alert('Error en la consulta.');
        }
    });
};

function Modal(idEditorial) {


    $.ajax({
        url: 'http://localhost:5138/api/Editorial/getbyideditorial/' + idEditorial,

        data: { idEditorial: idEditorial },

        type: 'GET',

        success: function (data) {

            $('#txtIdEditorial').val(data.idEditorial)
            $('#txtNombre').val(data.nombre)
            $('#ddlPais').val(data.direccion.colonia.municipio.estado.pais.idPais);
            $("#ddlEstado").empty();
            $("#ddlMunicipio").empty();
            $("#ddlColonia").empty();
            $.ajax({
                type: 'POST',
                url: '/Editorial/GetEstado',
                dataType: 'json',
                data: { IdPais: $("#ddlPais").val() },
                success: function (estados) {
                    $("#ddlEstado").append('<option value="0">' + 'Seleccione un Estado' + '</option>');
                    $.each(estados, function (i, estados) {
                        $("#ddlEstado").append('<option value ="'
                            + estados.idEstado + '">'
                            + estados.nombre + '</option>');
                    });
                    $('#ddlEstado').val(data.direccion.colonia.municipio.estado.idEstado);

                    $.ajax({
                        type: 'POST',
                        url: '/Editorial/GetMunicipio',
                        dataType: 'json',
                        data: { IdEstado: $("#ddlEstado").val() },
                        success: function (municipios) {
                            $("#ddlMunicipio").append('<option value="0">' + 'Seleccione un Municipio' + '</option>');
                            $.each(municipios, function (i, municipios) {
                                $("#ddlMunicipio").append('<option value ="'
                                    + municipios.idMunicipio + '">'
                                    + municipios.nombre + '</option>');
                            });

                            $('#ddlMunicipio').val(data.direccion.colonia.municipio.idMunicipio);
                            $.ajax({
                                type: 'POST',
                                url: '/Editorial/GetColonia',
                                dataType: 'json',
                                data: { IdMunicipio: $("#ddlMunicipio").val() },
                                success: function (colonias) {
                                    $("#ddlColonia").append('<option value="0">' + 'Seleccione una Colonia' + '</option>');
                                    $.each(colonias, function (i, colonias) {
                                        $("#ddlColonia").append('<option value ="'
                                            + colonias.idColonia + '">'
                                            + colonias.nombre + '</option>');
                                    });

                                    $('#ddlColonia').val(data.direccion.colonia.idColonia);
                                },
                                error: function (ex) {
                                    alert('Failed. ' + ex)
                                }
                            });
                        },
                        error: function (ex) {
                            alert('Failed. ' + ex)
                        }
                    });
                },
                error: function (ex) {
                    alert('Failed. ' + ex)
                }
            });
            $('#txtCalle').val(data.direccion.calle);
            $('#txtNumeroInterior').val(data.direccion.numeroInterior);
            $('#txtNumeroExterior').val(data.direccion.numeroExterior);
            $('#txtIdDireccion').val(data.direccion.idDireccion);

            $('#modal').modal('show');
        },
        error: function () {
            alert('Disculpe, existió un problema');
        },
    });
};

function Delete(idEditorial) {
    var checkconfirm = confirm('¿Estas seguro de eliminarlo?');
    if (checkconfirm == true) {
        $.ajax({
            url: 'http://localhost:5138/api/Editorial/delete/' + idEditorial,
            type: 'DELETE',
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
