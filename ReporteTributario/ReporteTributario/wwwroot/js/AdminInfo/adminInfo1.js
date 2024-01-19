
function LoadListing() {
    var empdata = [];
    let urlConsulta = "/AdminInformacion/GetAll";
    $.ajax({
        type: "POST",
        url: urlConsulta ,
        async: false,
        success: function (data) {
            $.each(data, function (key, value) {
                var editbtn = "<a onclick='FunEdit(this)' class='btn btn-primary'>Editar</a>";
                var removebtn = "<a onclick='FunRemove(this)' class='btn btn-danger'>Remover</a>";
                var hdn = "<input type='hidden' value=" + value.idImpuesto + " />";
                var action = editbtn + " | " + removebtn + hdn;
                empdata.push([value.idImpuesto,
                                value.impuesto,
                                value.ciudad,
                                value.departamento,
                                value.fechaLimite,
                                value.responsable,
                                value.periodo,
                                value.periodicidad,
                                action
                ])
            })                 
        },
        failure: function (err) {

        }
    });
    

    js('#tbllist').DataTable({
        data: empdata,
        lengthMenu: [5, 10, 15, 20, 100, 200, 500],
        columnDefs: [
            { className: "centered", targets: [0, 1, 2, 3, 4, 5, 6,7, 8] },
            { orderable: false, targets: [5, 6] },
            { searchable: false, targets: [1] }
            //{ width: "50%", targets: [0] }
        ],
        pageLength: 10,
        destroy: true,
        language: {
            lengthMenu: "Mostrar _MENU_ registros por p&aacutegina",
            zeroRecords: "Ningún usuario encontrado",
            info: "Mostrando de _START_ a _END_ de un total de _TOTAL_ registros",
            infoEmpty: "Ningún usuario encontrado",
            infoFiltered: "(filtrados desde _MAX_ registros totales)",
            search: "Buscar:",
            loadingRecords: "Cargando...",
            paginate: {
                first: "Primero",
                last: "Último",
                next: "Siguiente",
                previous: "Anterior"
            }
        }

    });
}
