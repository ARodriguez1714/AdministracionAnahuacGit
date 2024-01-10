$(document).ready(function () {
    GetAll();
});

function GetAll() {
    $.ajax({
        type: 'GET', 
        url: 'http://localhost:5138/api/Idioma/getidioma',
        success: function (result) {
            $('#tBodyIdioma').empty();

            $.each(result.objects, function (i, idioma) {
                var fila =
                    '<tr>'
                    + '<td class="text-center"><button type="button" class="btn btn-warning bi bi-pencil-fill" id="btnUpdate" onclick="Modal(this)"  value="' + idioma.idIdioma + '"></button></td>'
                    + '<td hidden>' + idioma.idIdioma + '</td>'
                    + '<td class="text-center">' + idioma.nombre + '</td>'
                    + '<td class="text-center"><button type="button" class="btn btn-danger bi bi-trash-fill" onclick="Delete(this)"  value="' + idioma.idIdioma + '"></button></td>'
                    + '</tr>';
                $('#tBodyIdioma').append(fila);
            });
        }
    });
};


function BtnAdd() {
    $("#txtIdioma").val("");
    $("#txtNombre").val("");
    $('#modal').modal('show');
};

function ModalUpdate(boton) {

    $.ajax({
        type: 'GET',
        url: 'http://localhost:5138/api/Idioma/getbyid/' + boton.value,
        success: function (data) {

            $('#txtIdIdioma').val(data.object.idIdioma)
            $('#txtNombre').val(data.object.nombre)
            $('#modal').modal('show');
        },
        error: function () {
            alert('Error en la consulta.');
        }
    });
};
function BtnGuardar() {
    var fd = new FormData(); 
    var idioma = {
        "idIdioma": $('#txtIdIdioma').val(),
        "nombre": $('#txtNombre').val(),
    }
    if (idioma.idIdioma == 0) {
        Add();
    }
    else {
        Update();
    }

} 
function Add(idioma) {
    $.ajax({
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        type: 'POST',
        url: 'http://localhost:5138/api/Idioma/add',
        dataType: 'json',
        data: JSON.stringify(idioma),
        success: function (result) {
            $('#modal').modal('hide');

            $('#tBodyIdioma').empty();

            GetAll();
        },
        error: function (result) {
            alert('Error en la consulta.');
        }
    });
}

function Update(idioma) { 
    $.ajax({
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        type: 'PUT',
        url: 'http://localhost:5138/api/Idioma/update',
        dataType: 'json',
        data: JSON.stringify(idioma),
        success: function (result) {
            $('#modal').modal('hide');

            $('#tBodyIdioma').empty();

            GetAll();
        },
        error: function (result) {
            alert('Error en la consulta.');
        }
    });
} 
function Delete(idIdioma) {
    var checkconfirm = confirm('¿Estas seguro de eliminarlo?');
    if (checkconfirm == true) {
        $.ajax({
            url: 'http://localhost:5138/api/Idioma/delete/' + idIdioma,
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



function Modal(boton) {


    $.ajax({
        url: '@Url.Action("GetByIdIdioma")',

        data: { idIdioma: boton.value },

        type: 'GET',

        dataType: 'json',

        success: function (data) {

            $('#txtIdIdioma').val(data.idIdioma)
            $('#txtNombre').val(data.nombre)

            $('#modal').modal('show');
        },
        error: function () {
            alert('Error en la consulta');
        },
    });
};
