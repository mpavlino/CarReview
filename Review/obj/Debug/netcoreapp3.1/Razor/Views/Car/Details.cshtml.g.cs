#pragma checksum "C:\Users\MirkoP\OneDrive - Teched\Desktop\Stuff\CarReview-master\Review\Views\Car\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2109e110408ace46dc4bb579f9478ee0b6b2b322"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Car_Details), @"mvc.1.0.view", @"/Views/Car/Details.cshtml")]
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
#line 1 "C:\Users\MirkoP\OneDrive - Teched\Desktop\Stuff\CarReview-master\Review\Views\_ViewImports.cshtml"
using Review;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\MirkoP\OneDrive - Teched\Desktop\Stuff\CarReview-master\Review\Views\_ViewImports.cshtml"
using Review.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\MirkoP\OneDrive - Teched\Desktop\Stuff\CarReview-master\Review\Views\_ViewImports.cshtml"
using Review.Model;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2109e110408ace46dc4bb579f9478ee0b6b2b322", @"/Views/Car/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"86bcf7ed508de77e608b2e68afaeab6bf5bef957", @"/Views/_ViewImports.cshtml")]
    public class Views_Car_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Review.Model.Car>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\MirkoP\OneDrive - Teched\Desktop\Stuff\CarReview-master\Review\Views\Car\Details.cshtml"
  
    ViewData["Title"] = "Details";

#line default
#line hidden
#nullable disable
            WriteLiteral("<h2>Details</h2>\n");
#nullable restore
#line 6 "C:\Users\MirkoP\OneDrive - Teched\Desktop\Stuff\CarReview-master\Review\Views\Car\Details.cshtml"
 if (Model != null)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <ol class=\"breadcrumb\">\n        <li>");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "2109e110408ace46dc4bb579f9478ee0b6b2b3224122", async() => {
                WriteLiteral("Cars list");
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
            WriteLiteral("</li>\n        <li class=\"active\">");
#nullable restore
#line 10 "C:\Users\MirkoP\OneDrive - Teched\Desktop\Stuff\CarReview-master\Review\Views\Car\Details.cshtml"
                      Write(Model.Brand?.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</li>\n    </ol>\n");
            WriteLiteral("    <div>\n        <h4>Car</h4>\n        <hr />\n        <dl class=\"dl-horizontal\">\n            <dt>\n                ID\n            </dt>\n            <dd>\n                ");
#nullable restore
#line 21 "C:\Users\MirkoP\OneDrive - Teched\Desktop\Stuff\CarReview-master\Review\Views\Car\Details.cshtml"
           Write(Html.DisplayFor(model => model.ID));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            </dd>\n            <dt>\n                Brand\n            </dt>\n            <dd>\n                ");
#nullable restore
#line 27 "C:\Users\MirkoP\OneDrive - Teched\Desktop\Stuff\CarReview-master\Review\Views\Car\Details.cshtml"
           Write(Html.DisplayFor(model => model.Brand.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            </dd>\n            <dt>\n                Model\n            </dt>\n            <dd>\n                ");
#nullable restore
#line 33 "C:\Users\MirkoP\OneDrive - Teched\Desktop\Stuff\CarReview-master\Review\Views\Car\Details.cshtml"
           Write(Html.DisplayFor(model => model.Model));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            </dd>\n            <dt>\n                Model Year\n            </dt>\n            <dd>\n                ");
#nullable restore
#line 39 "C:\Users\MirkoP\OneDrive - Teched\Desktop\Stuff\CarReview-master\Review\Views\Car\Details.cshtml"
           Write(Html.DisplayFor(model => model.ModelYear.Year));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            </dd>\n            <dt>\n                Engine\n            </dt>\n            <dd>\n                ");
#nullable restore
#line 45 "C:\Users\MirkoP\OneDrive - Teched\Desktop\Stuff\CarReview-master\Review\Views\Car\Details.cshtml"
           Write(Html.DisplayFor(model => model.Engine));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            </dd>\n            <dt>\n                Max. speed\n            </dt>\n            <dd>\n                ");
#nullable restore
#line 51 "C:\Users\MirkoP\OneDrive - Teched\Desktop\Stuff\CarReview-master\Review\Views\Car\Details.cshtml"
           Write(Html.DisplayFor(model => model.TopSpeed));

#line default
#line hidden
#nullable disable
            WriteLiteral(" km/h\n            </dd>\n            <dt>\n                Acceleration\n            </dt>\n            <dd>\n                ");
#nullable restore
#line 57 "C:\Users\MirkoP\OneDrive - Teched\Desktop\Stuff\CarReview-master\Review\Views\Car\Details.cshtml"
           Write(Html.DisplayFor(model => model.Acceleration));

#line default
#line hidden
#nullable disable
            WriteLiteral(" sec\n            </dd>\n            <dt>\n                Summary (About)\n            </dt>\n            <dd>\n                ");
#nullable restore
#line 63 "C:\Users\MirkoP\OneDrive - Teched\Desktop\Stuff\CarReview-master\Review\Views\Car\Details.cshtml"
           Write(Html.DisplayFor(model => model.Description));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            </dd>\n            <dt>\n                Country\n            </dt>\n            <dd>\n                ");
#nullable restore
#line 69 "C:\Users\MirkoP\OneDrive - Teched\Desktop\Stuff\CarReview-master\Review\Views\Car\Details.cshtml"
           Write(Model?.Country?.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            </dd>\n            <dt>\n                Reviewer\n            </dt>\n            <dd>\n                ");
#nullable restore
#line 75 "C:\Users\MirkoP\OneDrive - Teched\Desktop\Stuff\CarReview-master\Review\Views\Car\Details.cshtml"
           Write(Model?.Reviewer?.FullName);

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            </dd>\n        </dl>\n    </div>\n");
#nullable restore
#line 79 "C:\Users\MirkoP\OneDrive - Teched\Desktop\Stuff\CarReview-master\Review\Views\Car\Details.cshtml"

}
else
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div class=\"alert alert-danger\">\n        Nije odabran ispravni ID automobila.\n    </div>\n");
#nullable restore
#line 86 "C:\Users\MirkoP\OneDrive - Teched\Desktop\Stuff\CarReview-master\Review\Views\Car\Details.cshtml"

}

#line default
#line hidden
#nullable disable
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
