$(document).ready(function () {
    GetAll();
    GetTipoAutor();
});

function PreviewImage(event) {
    var output = document.getElementById('imgAutor');
    output.src = URL.createObjectURL(event.target.files[0]);
    output.onload = function () {
        URL.revokeObjectURL(output.src) // free memory
    }
};

function GetAll() {
    $.ajax({
        type: 'GET',
        url: 'http://localhost:5138/api/Autor/getautores',
        success: function (result) {
            $('#cardAutor').empty();

            $('#errorAlert').empty();

            if (result.objects === null) {
                var alert =
                    '<div class="alert alert-danger" role="alert">' + result.message + '</div>';
                $('#errorAlert').append(alert);
            } else {
                $.each(result.objects, function (i, autor) {
                    var imagen;
                    if (autor.foto == null) {
                        imagen = '<img src="/Images/userDefault.jpg" width = "50px" height = "50px"/>'
                    }
                    else {
                        imagen = '<img src="data:image/png;base64,' + autor.foto + '" width = "100%" height = "100%"/>'
                    }
                    var fila =
                        '<div class="col-7 col-md-4 col-lg-3 ">'
                        + '<a id="btnUpdate" onclick="ModalUpdate(' + autor.idAutor + ')">'
                        + '<div class="card border-primary border-3 border-opacity-50">'
                        + '<div class="image">' + imagen + '</div>'
                        + '<span class="title">' + autor.nombre + ' ' + autor.apellidoPaterno + ' ' + autor.apellidoMaterno + '</span>'
                        + '<span class="tipoAutor">' + autor.tipoAutor.nombre + '</span>'
                        + '</div>'
                        + '</a>'
                        + '</div>';
                    $('#cardAutor').append(fila);
                });
            }
        },
        error: function (result) {
            alert('Error en la consulta.');
        }
    });
};

function GetTipoAutor() {
    $.ajax({
        type: 'GET',
        url: '/Autor/GetAllTipoAutor',
        success: function (result) {
            $('#ddlTipoAutor').empty();
            $('#ddlTipoAutor').append('<option value="' + 0 + '">' + 'Seleccione una opción' + '</option>');
            $.each(result.objects, function (i, autor) {
                var select =
                    '<option value="' + autor.idTipoAutor + '">' + autor.nombre + '</option>';
                $("#ddlTipoAutor").append(select);
            });
        },
        error: function (result) {
            alert('Error en la consulta.');
        }
    });
};

function BtnAdd() {
    $("#txtIdAutor").val("");
    $("#txtNombre").val("");
    $("#txtApellidoPaterno").val("");
    $('#txtApellidoMaterno').val("");
    $('#ddlTipoAutor').val("0");
    $('#fuImagen').val("");
    $('#txtUpdateFoto').val("");
    $('#cargarFoto').empty();

    var imagen = '<img src="/Images/userDefault.jpg" id="imgAutor" width = "150px" height = "150px"/>';

    $('#cargarFoto').append(imagen);
    $('#modal').modal('show');
};

function ModalUpdate(boton) {

    $.ajax({
        type: 'GET',
        url: 'http://localhost:5138/api/Autor/getbyid/' + boton,
        success: function (data) {

            $('#txtIdAutor').val(data.object.idAutor)
            $('#txtNombre').val(data.object.nombre)
            $('#txtApellidoPaterno').val(data.object.apellidoPaterno)
            $('#txtApellidoMaterno').val(data.object.apellidoMaterno)
            $('#ddlTipoAutor').val(data.object.tipoAutor.idTipoAutor)
            $('#fuImagen').val("")
            $('#txtUpdateFoto').val(data.object.foto)

            $('#txtUpdateFoto').hide();
            $("#txtUpdateFoto").prop("readonly", true);

            $('#cargarFoto').empty();

            var imagen;


            if (data.object.foto == null) {
                imagen = '<img src="/Images/userDefault.jpg" id="imgAutor" width = "150px" height = "150px"/>'
            }
            else {
                imagen = '<img src="data:image/png;base64,' + data.object.foto + '" id="imgAutor" width = "150px" height = "150px"/>'
            }

            $('#cargarFoto').append(imagen);

            $('#modal').modal('show');
        },
        error: function () {
            alert('Disculpe, existió un problema');
        },
    });
};

function BtnGuardarAutor() {
    var fd = new FormData();
    var files;
    var imagen = document.getElementById('fuImagen');
    var imagenUpdate = document.getElementById('txtUpdateFoto');

    if (imagen.value == "" && imagenUpdate.value == "") {
        fetch('/Images/userDefault.jpg')
            .then(response => response.blob())
            .then(blob => {
                fd.append('fuImagen', blob, 'userDefault.jpg');

                AjaxGuardar(fd);
            });
    } else {

        if (imagen.value != "") {
            $('#txtUpdateFoto').val("");
            var files = $('#fuImagen')[0].files[0];
            fd.append('fuImagen', files);
            AjaxGuardar(fd);
        } else {
            AjaxGuardarUpdate(imagenUpdate.value);
        }
    }

    function AjaxGuardar(formData) {
        $.ajax({
            type: 'POST',
            url: 'http://localhost:5138/api/Autor/convertimagen',
            data: formData,
            processData: false,
            contentType: false,
            success: function (foto) {
                var byteFoto = foto;
                var autor =
                {
                    "idAutor": Number($('#txtIdAutor').val()),
                    "nombre": $('#txtNombre').val(),
                    "apellidoPaterno": $('#txtApellidoPaterno').val(),
                    "apellidoMaterno": $('#txtApellidoMaterno').val(),
                    "foto": byteFoto,
                    "autores": null,
                    "tipoAutor": {
                        "idTipoAutor": Number($('#ddlTipoAutor').val()),
                        "nombre": "string",
                        "tipoAutores": null
                    }
                }
                if (autor.idAutor == 0) {
                    Add(autor);
                } else {
                    Update(autor);
                }

            },
            error: function (result) {
                alert('Error en la consulta.');
            }

        });
    }

    function AjaxGuardarUpdate(imagenUpdate) {

        var autor =
        {
            "idAutor": Number($('#txtIdAutor').val()),
            "nombre": $('#txtNombre').val(),
            "apellidoPaterno": $('#txtApellidoPaterno').val(),
            "apellidoMaterno": $('#txtApellidoMaterno').val(),
            "foto": imagenUpdate,
            "autores": null,
            "tipoAutor": {
                "idTipoAutor": Number($('#ddlTipoAutor').val()),
                "nombre": "string",
                "tipoAutores": null
            }
        }
        if (autor.idAutor == 0) {
            Add(autor);
        } else {
            Update(autor);
        }
    }

}

function Add(autor) {
    $.ajax({
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        type: 'POST',
        url: 'http://localhost:5138/api/Autor/add',
        dataType: 'json',
        data: JSON.stringify(autor),
        success: function (result) {
            $('#modal').modal('hide');

            $('#tBodyAutor').empty();

            GetAll();
        },
        error: function (result) {
            alert('Error en la consulta.');
        }
    });
}

function Update(autor) {
    $.ajax({
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        type: 'PUT',
        url: 'http://localhost:5138/api/Autor/update',
        dataType: 'json',
        data: JSON.stringify(autor),
        success: function (result) {
            $('#modal').modal('hide');

            $('#tBodyAutor').empty();

            GetAll();
        },
        error: function (result) {
            alert('Error en la consulta.');
        }
    });
}