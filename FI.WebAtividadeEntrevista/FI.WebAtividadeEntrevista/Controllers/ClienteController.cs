using FI.AtividadeEntrevista.BLL;
using WebAtividadeEntrevista.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FI.AtividadeEntrevista.DML;
using FI.AtividadeEntrevista.Utils;
using FI.WebAtividadeEntrevista.Controllers;
using NLog;
using WebGrease.Activities;
using FI.WebAtividadeEntrevista.Models;
using System.Reflection;
using Microsoft.Ajax.Utilities;

namespace WebAtividadeEntrevista.Controllers
{
    public class ClienteController : BaseController
    {
        public ActionResult Index()
        {
            Session["ListaBeneficiarios"] = null;
            return View();
        }

        public ActionResult Incluir()
        {
            Session["ListaBeneficiarios"] = null;
            return View();
        }

        [HttpPost]
        public JsonResult Incluir(ClienteModel model)
        {
            BoCliente bo = new BoCliente();

            try
            {
                if (!this.ModelState.IsValid)
                {
                    List<string> erros = (from item in ModelState.Values
                                          from error in item.Errors
                                          select error.ErrorMessage).ToList();

                    throw new AtividadeEntrevistaException((string.Join(Environment.NewLine, erros)));
                }

                model.Id = bo.Incluir(new Cliente()
                {
                    CEP = model.CEP,
                    Cidade = model.Cidade,
                    Email = model.Email,
                    Estado = model.Estado,
                    Logradouro = model.Logradouro,
                    Nacionalidade = model.Nacionalidade,
                    Nome = model.Nome,
                    Sobrenome = model.Sobrenome,
                    Telefone = model.Telefone,
                    CPF = model.CPF
                });

                var listaBeneficiarios = Session["ListaBeneficiarios"] is null ? new List<BeneficiarioModel>() : (List<BeneficiarioModel>)Session["ListaBeneficiarios"];

                if (listaBeneficiarios.Any())
                {
                    BoBeneficiario boBeneficiario = new BoBeneficiario();

                    listaBeneficiarios.ForEach(p =>
                    {
                        boBeneficiario.Incluir(new Beneficiario()
                        {
                            CPF = p.CPF.SomenteAlfaNumericos(),
                            Nome = p.Nome,
                            IdCliente = model.Id
                        });
                    });
                }

                return Json("Cadastro efetuado com sucesso.");
            }
            catch (AtividadeEntrevistaException ex)
            {
                Response.StatusCode = 401;
                logger.Info($"{this.ControllerContext.Controller.GetType().Name} -> Incluir -> {ex.Message}");
                return Json(ex.Message);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                logger.Error($"{this.ControllerContext.Controller.GetType().Name} -> Incluir -> {ex}");
                return Json("Não foi possível incluir o cliente, entre em contato com o suporte.");
            }
        }

        [HttpPost]
        public JsonResult Alterar(ClienteModel model)
        {
            BoCliente bo = new BoCliente();

            try
            {
                if (!this.ModelState.IsValid)
                {
                    List<string> erros = (from item in ModelState.Values
                                          from error in item.Errors
                                          select error.ErrorMessage).ToList();

                    throw new AtividadeEntrevistaException((string.Join(Environment.NewLine, erros)));
                }
                else
                {
                    bo.Alterar(new Cliente()
                    {
                        Id = model.Id,
                        CEP = model.CEP,
                        Cidade = model.Cidade,
                        Email = model.Email,
                        Estado = model.Estado,
                        Logradouro = model.Logradouro,
                        Nacionalidade = model.Nacionalidade,
                        Nome = model.Nome,
                        Sobrenome = model.Sobrenome,
                        Telefone = model.Telefone,
                        CPF = model.CPF
                    });

                    return Json("Cadastro alterado com sucesso.");
                }
            }
            catch (AtividadeEntrevistaException ex)
            {
                Response.StatusCode = 401;
                logger.Info($"{this.ControllerContext.Controller.GetType().Name} -> Alterar -> {ex.Message}");
                return Json(ex.Message);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                logger.Error($"{this.ControllerContext.Controller.GetType().Name} -> Alterar -> {ex}");
                return Json("Não foi possível alterar o cliente, entre em contato com o suporte.");
            }
        }

        [HttpGet]
        public ActionResult Alterar(long id)
        {
            Session["ListaBeneficiarios"] = null;
            BoCliente bo = new BoCliente();

            try
            {
                Cliente cliente = bo.Consultar(id);
                Models.ClienteModel model = null;

                if (cliente != null)
                {
                    model = new ClienteModel()
                    {
                        Id = cliente.Id,
                        CEP = cliente.CEP,
                        Cidade = cliente.Cidade,
                        Email = cliente.Email,
                        Estado = cliente.Estado,
                        Logradouro = cliente.Logradouro,
                        Nacionalidade = cliente.Nacionalidade,
                        Nome = cliente.Nome,
                        Sobrenome = cliente.Sobrenome,
                        Telefone = cliente.Telefone,
                        CPF = cliente.CPF
                    };
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }

                return View(model);
            }
            catch (Exception ex)
            {
                logger.Error($"{this.ControllerContext.Controller.GetType().Name} -> Alterar -> {ex}");
                return View();
            }
        }

        [HttpPost]
        public JsonResult ClienteList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                int qtd = 0;
                string campo = string.Empty;
                string crescente = string.Empty;
                string[] array = jtSorting.Split(' ');


                if (array.Length > 0)
                    campo = array[0];

                if (array.Length > 1)
                    crescente = array[1];

                List<Cliente> clientes = new BoCliente().Pesquisa(jtStartIndex, jtPageSize, campo, crescente.Equals("ASC", StringComparison.InvariantCultureIgnoreCase), out qtd);

                //Return result to jTable
                return Json(new { Result = "OK", Records = clientes, TotalRecordCount = qtd });
            }
            catch (Exception ex)
            {
                logger.Error($"{this.ControllerContext.Controller.GetType().Name} -> ClienteList -> {ex}");
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult IncluirBeneficiario(BeneficiarioModel model)
        {
            try
            {
                if (!this.ModelState.IsValid)
                {
                    List<string> erros = (from item in ModelState.Values
                                          from error in item.Errors
                                          select error.ErrorMessage).ToList();

                    throw new AtividadeEntrevistaException((string.Join(Environment.NewLine, erros)));
                }

                var listaBeneficiarios = Session["ListaBeneficiarios"] is null ? new List<BeneficiarioModel>() : (List<BeneficiarioModel>)Session["ListaBeneficiarios"];

                BoBeneficiario boBeneficiario = new BoBeneficiario();

                if (model.IdCliente == 0)
                {
                    if (listaBeneficiarios.Any(p => p.CPF.SomenteAlfaNumericos() == model.CPF.SomenteAlfaNumericos()))
                        throw new AtividadeEntrevistaException("CPF informado já cadastrado para este cliente.");
                }
                else
                {
                    model.Id = boBeneficiario.Incluir(new Beneficiario()
                    {
                        CPF = model.CPF.SomenteAlfaNumericos(),
                        Nome = model.Nome.Trim(),
                        IdCliente = model.IdCliente
                    });

                    listaBeneficiarios.RemoveAll(p => p.IdCliente != model.IdCliente);
                }

                listaBeneficiarios.Add(new BeneficiarioModel()
                {
                    CPF = model.CPF.SomenteAlfaNumericos(),
                    Nome = model.Nome.Trim(),
                    Id = model.IdCliente == 0 ? Utils.RandomId(6) : model.Id,
                    IdCliente = model.IdCliente
                });

                Session["ListaBeneficiarios"] = listaBeneficiarios;

                return PartialView("_BeneficiariosList", listaBeneficiarios);
            }
            catch (AtividadeEntrevistaException ex)
            {
                Response.StatusCode = 401;
                logger.Info($"{this.ControllerContext.Controller.GetType().Name} -> IncluirBeneficiario -> {ex.Message}");
                return Json(ex.Message);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                logger.Error($"{this.ControllerContext.Controller.GetType().Name} -> IncluirBeneficiario -> {ex}");
                return Json("Não foi possível incluir o beneficiário, entre em contato com o suporte.");
            }
        }

        [HttpPost]
        public ActionResult BeneficiarioList(long idCliente = 0)
        {
            List<BeneficiarioModel> listaBeneficiarios = new List<BeneficiarioModel>();

            try
            {
                if (idCliente == 0)
                {
                    return PartialView("_BeneficiariosList", listaBeneficiarios);
                }

                BoBeneficiario bo = new BoBeneficiario();

                var listaBeneficiariosPorCliente = bo.ListarPorCliente(idCliente);

                listaBeneficiariosPorCliente.ForEach(p =>
                {
                    listaBeneficiarios.Add(new BeneficiarioModel()
                    {
                        Id = p.Id,
                        CPF = p.CPF,
                        Nome = p.Nome.Trim(),
                        IdCliente = p.IdCliente
                    }); ;
                });

                Session["ListaBeneficiarios"] = listaBeneficiarios;

                return PartialView("_BeneficiariosList", listaBeneficiarios);
            }
            catch (AtividadeEntrevistaException ex)
            {
                Response.StatusCode = 401;
                logger.Info($"{this.ControllerContext.Controller.GetType().Name} -> BeneficiarioList -> {ex.Message}");
                return Json(ex.Message);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                logger.Error($"{this.ControllerContext.Controller.GetType().Name} -> BeneficiarioList -> {ex}");
                return Json("Não foi possível listar os beneficiários, entre em contato com o suporte.");
            }
        }

        [HttpPost]
        public ActionResult ExcluirBeneficiario(long id)
        {
            BoCliente bo = new BoCliente();

            try
            {
                if (!this.ModelState.IsValid)
                {
                    List<string> erros = (from item in ModelState.Values
                                          from error in item.Errors
                                          select error.ErrorMessage).ToList();

                    throw new AtividadeEntrevistaException((string.Join(Environment.NewLine, erros)));
                }

                var listaBeneficiarios = Session["ListaBeneficiarios"] is null ? new List<BeneficiarioModel>() : (List<BeneficiarioModel>)Session["ListaBeneficiarios"];

                if (listaBeneficiarios.Any())
                {
                    BoBeneficiario boBeneficiario = new BoBeneficiario();

                    listaBeneficiarios.ForEach(p =>
                    {
                        boBeneficiario.Excluir(id);
                    });

                    listaBeneficiarios.RemoveAll(p => p.Id == id);
                }

                return Json(new { Mensagem = "Beneficiário excluído com sucesso.", BeneficiariosList = GetHtmlRazorView("_BeneficiariosList", listaBeneficiarios) });
            }
            catch (AtividadeEntrevistaException ex)
            {
                Response.StatusCode = 401;
                logger.Info($"{this.ControllerContext.Controller.GetType().Name} -> ExcluirBeneficiario -> {ex.Message}");
                return Json(ex.Message);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                logger.Error($"{this.ControllerContext.Controller.GetType().Name} -> ExcluirBeneficiario -> {ex}");
                return Json("Não foi possível excluir o beneficiário, entre em contato com o suporte.");
            }
        }

        [HttpPost]
        public ActionResult AlterarBeneficiario(BeneficiarioModel model)
        {
            try
            {
                var listaBeneficiarios = Session["ListaBeneficiarios"] is null ? new List<BeneficiarioModel>() : (List<BeneficiarioModel>)Session["ListaBeneficiarios"];

                if (listaBeneficiarios.Any())
                {
                    BoBeneficiario boBeneficiario = new BoBeneficiario();

                    if (model.IdCliente == 0)
                    {
                        if (listaBeneficiarios.Any(b => b.CPF.SomenteAlfaNumericos() == model.CPF.SomenteAlfaNumericos() && b.Id != model.Id))
                            throw new AtividadeEntrevistaException("CPF informado já cadastrado para este cliente.");
                    }

                    listaBeneficiarios.ForEach(p =>
                    {
                        if (model.IdCliente > 0)
                        {
                            boBeneficiario.Alterar(new Beneficiario()
                            {
                                CPF = model.CPF.SomenteAlfaNumericos(),
                                Id = model.Id,
                                IdCliente = model.IdCliente,
                                Nome = model.Nome.Trim()
                            });
                        }

                        if (p.Id == model.Id)
                        {
                            p.Nome = model.Nome;
                            p.CPF = model.CPF.SomenteAlfaNumericos();
                        }
                    });
                }

                return Json(new { Mensagem = "Beneficiário alterado com sucesso.", BeneficiariosList = GetHtmlRazorView("_BeneficiariosList", listaBeneficiarios) });
            }
            catch (AtividadeEntrevistaException ex)
            {
                Response.StatusCode = 401;
                logger.Info($"{this.ControllerContext.Controller.GetType().Name} -> AlterarBeneficiario -> {ex.Message}");
                return Json(ex.Message);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                logger.Error($"{this.ControllerContext.Controller.GetType().Name} -> AlterarBeneficiario -> {ex}");
                return Json("Não foi possível alterar o beneficiário, entre em contato com o suporte.");
            }

        }
    }
}