$(document).ready(function () {
    GetAll();
});

function GetAll() {
    $.ajax({
        url: '/Restaurante/GetAllRestaurante',
        type: 'GET',
        dataType: 'json',
        success: function (result) {

            let filas = '';

            $.each(result, function (i, item) {

                let direccionCompleta = `
                ${item.direccion.calle ?? ''} #${item.direccion.numeroExterior ?? ''}
                Int ${item.direccion.numeroInterior ?? ''}<br>
                ${item.direccion.colonia.nombre ?? ''}, 
                ${item.direccion.colonia.municipio.nombre ?? ''}<br>
                ${item.direccion.colonia.municipio.estado.nombre ?? ''} 
                C.P. ${item.direccion.colonia.codigoPostal ?? ''}
`;

                filas += `
                    <tr>
                        <td>${item.nombre}</td>
                        <td><img src="${item.logo ?? ''}" width="50"/></td>
                        <td>${item.fechaApertura}</td>
                        <td>${item.fechaCierre}</td>
                        <td>${direccionCompleta}</td>
                        <td>
                        <button class = "btn bton-danger" onclick "Delete(${item.idRestaurante})">
                        Eliminar </button>
                       
                        </td>
                    </tr>
                `;
            });

            $('#restauranteTabla').html(filas);
        },
        error: function () {
            alert('Error al obtener los datos');
        }
    });
}


function Delete(id) {
    if (!confirm("Estas seguro de que quieres elminar el restaurante?")) {
        return;
    }
    $.ajax({
        url: '/Restaurante/Delete',
        type: 'Post',
        data: { id: id },
        success: function (result) {

            if (result.success) {
                alert('Eliminado correctamente');
                GetAll();
            } else {
                alert(result.error || 'Error al eliminar');
            }
        },
        error: function () {
            alert('Error ');
        }
    });
}