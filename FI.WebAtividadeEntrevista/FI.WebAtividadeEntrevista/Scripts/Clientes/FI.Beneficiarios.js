$(document).ready(function () {

    $('#formCadastroBeneficiario #CPF').mask('999.999.999-99');
    $('#formAlterarBeneficiario #CPF').mask('999.999.999-99');

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

function ModalAlterarBeneficiario(id, idCliente, cpf, nome) {

    $('#formAlterarBeneficiario #Id').val(id);
    $('#formAlterarBeneficiario #IdCliente').val(idCliente);
    $('#formAlterarBeneficiario #CPF').val(cpf);
    $('#formAlterarBeneficiario #Nome').val(nome);

    $('#modal-alterar-beneficiario').modal('show');
}

function AlterarBeneficiario() {

    var id = $('#formAlterarBeneficiario #Id').val();
    var idCliente = $('#formAlterarBeneficiario #IdCliente').val();
    var cpf = $('#formAlterarBeneficiario #CPF').val();
    var nome = $('#formAlterarBeneficiario #Nome').val();

    var urlPost = '/Cliente/AlterarBeneficiario';

    $.ajax({
        url: urlPost,
        method: "POST",
        data: {
            "Id": id,
            "IdCliente": idCliente,
            "CPF": cpf,
            "Nome": nome
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
                $('#pnlBeneficiarios').html(r.BeneficiariosList);
                $('#modal-alterar-beneficiario').modal('hide');
                LimparDadosBeneficiario();
                ModalDialog('Alteração beneficiário', r.Mensagem);
            }
    });
}

function LimparDadosBeneficiario() {
    $('#formAlterarBeneficiario #Id').val(0);
    $('#formAlterarBeneficiario #IdCliente').val(0);
    $('#formAlterarBeneficiario #CPF').val('');
    $('#formAlterarBeneficiario #Nome').val('');
}

function ExcluirBeneficiario(id, idCliente) {

    var urlPost = '/Cliente/ExcluirBeneficiario';

    ShowConfirmation('Deseja realmente excluir o beneficiário?', function () {

        $.ajax({
            url: urlPost,
            method: "POST",
            data: {
                "id": id
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
                    $('#pnlBeneficiarios').html(r.BeneficiariosList);
                    ModalDialog('Exclusão beneficiário', r.Mensagem);
                }
        });
    });

}
