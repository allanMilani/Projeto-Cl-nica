#pragma checksum "C:\Users\Richard\Source\Repos\Projeto-Cl-nica\Projeto Cliínica\Views\Agendamento\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "14f90b82ee9483fec39ab0bc4f0debfa19c29c8d"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Agendamento_Index), @"mvc.1.0.view", @"/Views/Agendamento/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\Richard\Source\Repos\Projeto-Cl-nica\Projeto Cliínica\Views\_ViewImports.cshtml"
using Projeto_Cliínica;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Richard\Source\Repos\Projeto-Cl-nica\Projeto Cliínica\Views\_ViewImports.cshtml"
using Projeto_Cliínica.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"14f90b82ee9483fec39ab0bc4f0debfa19c29c8d", @"/Views/Agendamento/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"bf109c446c1f7c02257efd0ede458e1499c3e6ce", @"/Views/_ViewImports.cshtml")]
    public class Views_Agendamento_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Agendamento>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Create", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary mt-3"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-sm btn-warning"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Edit", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("color: #ffffff; font-size:16px"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", new global::Microsoft.AspNetCore.Html.HtmlString("submit"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-sm btn-danger"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Delete", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("btnRemover"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormActionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "C:\Users\Richard\Source\Repos\Projeto-Cl-nica\Projeto Cliínica\Views\Agendamento\Index.cshtml"
  
    ViewData["Title"] = "Agendamento";

#line default
#line hidden
#nullable disable
            WriteLiteral("<div class=\"row\">\n    <div class=\"col-12 mt-2 mb-3\">\n        <h1>Listagem de Agendamento</h1>\n        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "14f90b82ee9483fec39ab0bc4f0debfa19c29c8d6941", async() => {
                WriteLiteral(" Novo Agendamento ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n    </div>\n</div>\n<div class=\"row\">\n    <div class=\"col-12\">\n        <table class=\"table\">\n            <thead class=\"thead-dark\">\n                <tr>\n                    <th>");
#nullable restore
#line 16 "C:\Users\Richard\Source\Repos\Projeto-Cl-nica\Projeto Cliínica\Views\Agendamento\Index.cshtml"
                   Write(Html.DisplayNameFor(model => model.ID));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\n                    <th>");
#nullable restore
#line 17 "C:\Users\Richard\Source\Repos\Projeto-Cl-nica\Projeto Cliínica\Views\Agendamento\Index.cshtml"
                   Write(Html.DisplayNameFor(model => model.Paciente));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\n                    <th>");
#nullable restore
#line 18 "C:\Users\Richard\Source\Repos\Projeto-Cl-nica\Projeto Cliínica\Views\Agendamento\Index.cshtml"
                   Write(Html.DisplayNameFor(model => model.Medico));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\n                    <th>");
#nullable restore
#line 19 "C:\Users\Richard\Source\Repos\Projeto-Cl-nica\Projeto Cliínica\Views\Agendamento\Index.cshtml"
                   Write(Html.DisplayNameFor(model => model.DataHoraConsulta));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\n                    <th class=\"text-center\">Editar</th>\n                    <th class=\"text-center\">Remover</th>\n                </tr>\n            </thead>\n            <tbody>\n");
#nullable restore
#line 25 "C:\Users\Richard\Source\Repos\Projeto-Cl-nica\Projeto Cliínica\Views\Agendamento\Index.cshtml"
                 if(Model != null)
            {
                

#line default
#line hidden
#nullable disable
#nullable restore
#line 27 "C:\Users\Richard\Source\Repos\Projeto-Cl-nica\Projeto Cliínica\Views\Agendamento\Index.cshtml"
                 foreach(Agendamento agendamento in Model)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <tr>\n                    <td>");
#nullable restore
#line 30 "C:\Users\Richard\Source\Repos\Projeto-Cl-nica\Projeto Cliínica\Views\Agendamento\Index.cshtml"
                   Write(agendamento.ID);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                    <td>");
#nullable restore
#line 31 "C:\Users\Richard\Source\Repos\Projeto-Cl-nica\Projeto Cliínica\Views\Agendamento\Index.cshtml"
                   Write(agendamento.Paciente.Usuario.Nome);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                    <td>");
#nullable restore
#line 32 "C:\Users\Richard\Source\Repos\Projeto-Cl-nica\Projeto Cliínica\Views\Agendamento\Index.cshtml"
                   Write(agendamento.Medico.Usuario.Nome);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                    <td>");
#nullable restore
#line 33 "C:\Users\Richard\Source\Repos\Projeto-Cl-nica\Projeto Cliínica\Views\Agendamento\Index.cshtml"
                   Write(agendamento.DataHoraConsulta);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                    <td class=\"text-center\">\n                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "14f90b82ee9483fec39ab0bc4f0debfa19c29c8d11569", async() => {
                WriteLiteral("\n                            <i class=\"fas fa-edit\"></i>\n                        ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 35 "C:\Users\Richard\Source\Repos\Projeto-Cl-nica\Projeto Cliínica\Views\Agendamento\Index.cshtml"
                                                                              WriteLiteral(agendamento.ID);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n                    </td>\n                    <td class=\"text-center\">\n                        <a class=\"btn btn-sm btn-danger btnAbrirModalRemocao\" data-toggle=\"modal\" data-target=\"#modalRemocao\" data-code=\"");
#nullable restore
#line 40 "C:\Users\Richard\Source\Repos\Projeto-Cl-nica\Projeto Cliínica\Views\Agendamento\Index.cshtml"
                                                                                                                                    Write(agendamento.ID);

#line default
#line hidden
#nullable disable
            WriteLiteral("\" style=\"color: #ffffff; font-size:16px\">\n                            <i class=\"fas fa-trash\"></i>\n                        </a>\n                    </td>\n                </tr>\n");
#nullable restore
#line 45 "C:\Users\Richard\Source\Repos\Projeto-Cl-nica\Projeto Cliínica\Views\Agendamento\Index.cshtml"
                }

#line default
#line hidden
#nullable disable
#nullable restore
#line 45 "C:\Users\Richard\Source\Repos\Projeto-Cl-nica\Projeto Cliínica\Views\Agendamento\Index.cshtml"
                 
            }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"            </tbody>
        </table>
    </div>
</div>
<div class=""modal"" id=""modalRemocao"" tabindex=""-1"" role=""dialog"" aria-hidden=""true"">
    <div class=""modal-dialog modal-dialog-centered"" role=""document"">
        <div class=""modal-content"">
            <div class=""modal-body"">
                <div class=""d-flex justify-content-center"">
                    <div>
                        <i class=""fa fa-4x fa-minus-circle text-danger""></i>
                    </div>
                </div>
                <div class=""d-flex justify-content-center"" style=""padding-top: 12px;"">
                    <span class=""h4"">Tem certeza?</span>
                </div>
                <div class=""d-flex justify-content-center"">
                    <small style=""text-align: center"">Deseja realmente remover este livro do sistema? </small>
                </div>
            </div>
            <div class=""modal-footer d-flex justify-content-center"" style=""border-top: 0px;"">
                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "14f90b82ee9483fec39ab0bc4f0debfa19c29c8d16244", async() => {
                WriteLiteral("\n                    <button type=\"button\" class=\"btn btn-sm btn-secondary\" data-dismiss=\"modal\">Cancelar</button>\n                    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("button", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "14f90b82ee9483fec39ab0bc4f0debfa19c29c8d16642", async() => {
                    WriteLiteral("Remover");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormActionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper.Action = (string)__tagHelperAttribute_7.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_7);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_8);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\n                ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
            </div>
        </div>
    </div>
</div>
<script>
    jQuery(document).ready(function ($) {
        var trReference = null;

        $('.btnAbrirModalRemocao').on('click', function () {
            trReference = $(this).parents(':eq(1)');
        });


        $('#modalRemocao').on('show.bs.modal', function (e) {
            let code = $(e.relatedTarget).data('code');
            let s = document.getElementById('btnRemover').getAttribute('data-id');
            document.getElementById('btnRemover').removeAttribute('data-id');
            document.getElementById('btnRemover').setAttribute('data-id', code);
        });

        $('#btnRemover').on('click', function (event) {
            event.preventDefault();
            event.stopPropagation();
            const id = $(this).attr('data-id');
            const url = $(this).attr('formaction');
            const btn = $(this);

            $.ajax({
                type: 'DELETE',
                url: url + '/' + id,
                beforeSend: funct");
            WriteLiteral(@"ion () {
                    btn.html(
                        `<span class='spinner-border spinner-border-sm' role='status' aria-hidden='true'></span>&nbsp;Salvar`
                    ).attr('disabled', true);
                },
                success: function (response) {
                    $(""#modalRemocao"").modal(""hide"");
                    trReference.hide(300);
                    trReference.remove();
                    Swal.fire({
                        icon: 'success',
                        title: 'Sucesso!',
                        text: 'Agendamento deletado com sucesso!'
                    })
                },
                error: function (response) {
                    $(""#modalRemocao"").modal(""hide"");
                    Swal.fire({
                        icon: 'error',
                        title: 'Ops, ocorreu um erro inesperado na deleção do agendamento',
                        text: response.responseText
                    });
                }
            }).always(functi");
            WriteLiteral("on (response) {\n                btn.html(\'Salvar\').removeAttr(\'disabled\');\n            });\n\n        });\n\n    });\n</script>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Agendamento>> Html { get; private set; }
    }
}
#pragma warning restore 1591
