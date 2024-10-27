$(document).ready(function () {

    var urlPost = '/Cliente/BeneficiarioList';

    $.ajax({
        url: urlPost,
        method: "POST",
        data: {
            "idCliente": $("#formCadastroBeneficiario #IdCliente").val()
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
                $('#pnlBeneficiarios').html(r);
            }
    });
});