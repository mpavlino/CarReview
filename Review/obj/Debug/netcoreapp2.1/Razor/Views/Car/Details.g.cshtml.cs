#pragma checksum "C:\Users\Mirko\source\repos\Review\Review\Views\Car\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e067ffb321603a525e61104730e78c9df4d7eb9e"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Car_Details), @"mvc.1.0.view", @"/Views/Car/Details.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Car/Details.cshtml", typeof(AspNetCore.Views_Car_Details))]
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
#line 1 "C:\Users\Mirko\source\repos\Review\Review\Views\_ViewImports.cshtml"
using Review;

#line default
#line hidden
#line 2 "C:\Users\Mirko\source\repos\Review\Review\Views\_ViewImports.cshtml"
using Review.Models;

#line default
#line hidden
#line 3 "C:\Users\Mirko\source\repos\Review\Review\Views\_ViewImports.cshtml"
using Review.Model;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e067ffb321603a525e61104730e78c9df4d7eb9e", @"/Views/Car/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"cc3eed6a05fa337a284c781f9d236e0ec921c21c", @"/Views/_ViewImports.cshtml")]
    public class Views_Car_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Review.Model.Car>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
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
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "C:\Users\Mirko\source\repos\Review\Review\Views\Car\Details.cshtml"
  
    ViewData["Title"] = "Details";

#line default
#line hidden
            BeginContext(68, 18, true);
            WriteLiteral("<h2>Details</h2>\r\n");
            EndContext();
#line 6 "C:\Users\Mirko\source\repos\Review\Review\Views\Car\Details.cshtml"
 if (Model != null)
{

#line default
#line hidden
            BeginContext(110, 41, true);
            WriteLiteral("    <ol class=\"breadcrumb\">\r\n        <li>");
            EndContext();
            BeginContext(151, 35, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "747bd0c7a5d642d0b98fa751aa1e08b2", async() => {
                BeginContext(173, 9, true);
                WriteLiteral("Cars list");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(186, 34, true);
            WriteLiteral("</li>\r\n        <li class=\"active\">");
            EndContext();
            BeginContext(221, 17, false);
#line 10 "C:\Users\Mirko\source\repos\Review\Review\Views\Car\Details.cshtml"
                      Write(Model.Brand?.Name);

#line default
#line hidden
            EndContext();
            BeginContext(238, 18, true);
            WriteLiteral("</li>\r\n    </ol>\r\n");
            EndContext();
            BeginContext(258, 176, true);
            WriteLiteral("    <div>\r\n        <h4>Car</h4>\r\n        <hr />\r\n        <dl class=\"dl-horizontal\">\r\n            <dt>\r\n                ID\r\n            </dt>\r\n            <dd>\r\n                ");
            EndContext();
            BeginContext(435, 34, false);
#line 21 "C:\Users\Mirko\source\repos\Review\Review\Views\Car\Details.cshtml"
           Write(Html.DisplayFor(model => model.ID));

#line default
#line hidden
            EndContext();
            BeginContext(469, 115, true);
            WriteLiteral("\r\n            </dd>\r\n            <dt>\r\n                Brand\r\n            </dt>\r\n            <dd>\r\n                ");
            EndContext();
            BeginContext(585, 42, false);
#line 27 "C:\Users\Mirko\source\repos\Review\Review\Views\Car\Details.cshtml"
           Write(Html.DisplayFor(model => model.Brand.Name));

#line default
#line hidden
            EndContext();
            BeginContext(627, 115, true);
            WriteLiteral("\r\n            </dd>\r\n            <dt>\r\n                Model\r\n            </dt>\r\n            <dd>\r\n                ");
            EndContext();
            BeginContext(743, 37, false);
#line 33 "C:\Users\Mirko\source\repos\Review\Review\Views\Car\Details.cshtml"
           Write(Html.DisplayFor(model => model.Model));

#line default
#line hidden
            EndContext();
            BeginContext(780, 120, true);
            WriteLiteral("\r\n            </dd>\r\n            <dt>\r\n                Model Year\r\n            </dt>\r\n            <dd>\r\n                ");
            EndContext();
            BeginContext(901, 46, false);
#line 39 "C:\Users\Mirko\source\repos\Review\Review\Views\Car\Details.cshtml"
           Write(Html.DisplayFor(model => model.ModelYear.Year));

#line default
#line hidden
            EndContext();
            BeginContext(947, 116, true);
            WriteLiteral("\r\n            </dd>\r\n            <dt>\r\n                Engine\r\n            </dt>\r\n            <dd>\r\n                ");
            EndContext();
            BeginContext(1064, 38, false);
#line 45 "C:\Users\Mirko\source\repos\Review\Review\Views\Car\Details.cshtml"
           Write(Html.DisplayFor(model => model.Engine));

#line default
#line hidden
            EndContext();
            BeginContext(1102, 120, true);
            WriteLiteral("\r\n            </dd>\r\n            <dt>\r\n                Max. speed\r\n            </dt>\r\n            <dd>\r\n                ");
            EndContext();
            BeginContext(1223, 40, false);
#line 51 "C:\Users\Mirko\source\repos\Review\Review\Views\Car\Details.cshtml"
           Write(Html.DisplayFor(model => model.TopSpeed));

#line default
#line hidden
            EndContext();
            BeginContext(1263, 127, true);
            WriteLiteral(" km/h\r\n            </dd>\r\n            <dt>\r\n                Acceleration\r\n            </dt>\r\n            <dd>\r\n                ");
            EndContext();
            BeginContext(1391, 44, false);
#line 57 "C:\Users\Mirko\source\repos\Review\Review\Views\Car\Details.cshtml"
           Write(Html.DisplayFor(model => model.Acceleration));

#line default
#line hidden
            EndContext();
            BeginContext(1435, 129, true);
            WriteLiteral(" sec\r\n            </dd>\r\n            <dt>\r\n                Summary (About)\r\n            </dt>\r\n            <dd>\r\n                ");
            EndContext();
            BeginContext(1565, 43, false);
#line 63 "C:\Users\Mirko\source\repos\Review\Review\Views\Car\Details.cshtml"
           Write(Html.DisplayFor(model => model.Description));

#line default
#line hidden
            EndContext();
            BeginContext(1608, 117, true);
            WriteLiteral("\r\n            </dd>\r\n            <dt>\r\n                Country\r\n            </dt>\r\n            <dd>\r\n                ");
            EndContext();
            BeginContext(1726, 20, false);
#line 69 "C:\Users\Mirko\source\repos\Review\Review\Views\Car\Details.cshtml"
           Write(Model?.Country?.Name);

#line default
#line hidden
            EndContext();
            BeginContext(1746, 118, true);
            WriteLiteral("\r\n            </dd>\r\n            <dt>\r\n                Reviewer\r\n            </dt>\r\n            <dd>\r\n                ");
            EndContext();
            BeginContext(1865, 25, false);
#line 75 "C:\Users\Mirko\source\repos\Review\Review\Views\Car\Details.cshtml"
           Write(Model?.Reviewer?.FullName);

#line default
#line hidden
            EndContext();
            BeginContext(1890, 48, true);
            WriteLiteral("\r\n            </dd>\r\n        </dl>\r\n    </div>\r\n");
            EndContext();
#line 79 "C:\Users\Mirko\source\repos\Review\Review\Views\Car\Details.cshtml"

}
else
{

#line default
#line hidden
            BeginContext(1952, 96, true);
            WriteLiteral("    <div class=\"alert alert-danger\">\r\n        Nije odabran ispravni ID automobila.\r\n    </div>\r\n");
            EndContext();
#line 86 "C:\Users\Mirko\source\repos\Review\Review\Views\Car\Details.cshtml"

}

#line default
#line hidden
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Review.Model.Car> Html { get; private set; }
    }
}
#pragma warning restore 1591
