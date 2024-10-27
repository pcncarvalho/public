$(document).ready(function () {

    $('#formCadastroBeneficiario #CPF').mask('999.999.999-99');

});

function IncluirBeneficiario() {

    var urlPost = '/Cliente/IncluirBeneficiario';

    $.ajax({
        url: urlPost,
        method: "POST",
        data: {
            "Nome": $("#formCadastroBeneficiario #Nome").val(),
            "CPF": $("#formCadastroBeneficiario #CPF").val(),
            "IdCliente": $("#formCadastroBeneficiario #IdCliente").val()
        },
        error:
            function (r) {
                if (r.status == 401)
                    ModalDialog("Atenção", r.responseJSON);
                else if (r.status == 400)
                    ModalDialog("Ocorreu um erro", r.responseJSON);
                else if (r.status == 500)
                    ModalDialog("Ocorreu um erro", "Ocorreu um erro interno no servidor.");
            },
        success:
            function (r) {
                $("#formCadastroBeneficiario #Nome").val('');
                $("#formCadastroBeneficiario #CPF").val('');
                $('#pnlBeneficiarios').html(r);
            }
    });
}

function AlterarBeneficiario(id) {
    alert(id);
}

function ExcluirBeneficiario(id) {
    alert(id);
}

function ModalDialog(titulo, texto) {
    var random = Math.random().toString().replace('.', '');
    var texto = '<div id="' + random + '" class="modal fade">                                                               ' +
        '        <div class="modal-dialog">                                                                                 ' +
        '            <div class="modal-content">                                                                            ' +
        '                <div class="modal-header">                                                                         ' +
        '                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>         ' +
        '                    <h4 class="modal-title">' + titulo + '</h4>                                                    ' +
        '                </div>                                                                                             ' +
        '                <div class="modal-body">                                                                           ' +
        '                    <p>' + texto + '</p>                                                                           ' +
        '                </div>                                                                                             ' +
        '                <div class="modal-footer">                                                                         ' +
        '                    <button type="button" class="btn btn-default" data-dismiss="modal">Fechar</button>             ' +
        '                                                                                                                   ' +
        '                </div>                                                                                             ' +
        '            </div><!-- /.modal-content -->                                                                         ' +
        '  </div><!-- /.modal-dialog -->                                                                                    ' +
        '</div> <!-- /.modal -->                                                                                        ';

    $('body').append(texto);
    $('#' + random).modal('show');
}
