#pragma checksum "C:\Users\Mirko\source\repos\Review\Review\Views\Car\ReviewerDetails.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "19a4ca1da34714cc38868a8eecd9032f3e68e06a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Car_ReviewerDetails), @"mvc.1.0.view", @"/Views/Car/ReviewerDetails.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Car/ReviewerDetails.cshtml", typeof(AspNetCore.Views_Car_ReviewerDetails))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"19a4ca1da34714cc38868a8eecd9032f3e68e06a", @"/Views/Car/ReviewerDetails.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"cc3eed6a05fa337a284c781f9d236e0ec921c21c", @"/Views/_ViewImports.cshtml")]
    public class Views_Car_ReviewerDetails : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Review.Model.Reviewer>
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
#line 2 "C:\Users\Mirko\source\repos\Review\Review\Views\Car\ReviewerDetails.cshtml"
  
    ViewData["Title"] = "Details";

#line default
#line hidden
            BeginContext(73, 18, true);
            WriteLiteral("<h2>Details</h2>\r\n");
            EndContext();
#line 6 "C:\Users\Mirko\source\repos\Review\Review\Views\Car\ReviewerDetails.cshtml"
 if (Model != null)
{

#line default
#line hidden
            BeginContext(115, 41, true);
            WriteLiteral("    <ol class=\"breadcrumb\">\r\n        <li>");
            EndContext();
            BeginContext(156, 35, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "bb6cf3ed078d40d79180583d6852366b", async() => {
                BeginContext(178, 9, true);
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
            BeginContext(191, 45, true);
            WriteLiteral("</li>\r\n        <li class=\"active\">Reviewer / ");
            EndContext();
            BeginContext(237, 14, false);
#line 10 "C:\Users\Mirko\source\repos\Review\Review\Views\Car\ReviewerDetails.cshtml"
                                 Write(Model.FullName);

#line default
#line hidden
            EndContext();
            BeginContext(251, 18, true);
            WriteLiteral("</li>\r\n    </ol>\r\n");
            EndContext();
            BeginContext(271, 181, true);
            WriteLiteral("    <div>\r\n        <h4>Reviewer</h4>\r\n        <hr />\r\n        <dl class=\"dl-horizontal\">\r\n            <dt>\r\n                ID\r\n            </dt>\r\n            <dd>\r\n                ");
            EndContext();
            BeginContext(453, 34, false);
#line 21 "C:\Users\Mirko\source\repos\Review\Review\Views\Car\ReviewerDetails.cshtml"
           Write(Html.DisplayFor(model => model.ID));

#line default
#line hidden
            EndContext();
            BeginContext(487, 120, true);
            WriteLiteral("\r\n            </dd>\r\n            <dt>\r\n                First Name\r\n            </dt>\r\n            <dd>\r\n                ");
            EndContext();
            BeginContext(608, 41, false);
#line 27 "C:\Users\Mirko\source\repos\Review\Review\Views\Car\ReviewerDetails.cshtml"
           Write(Html.DisplayFor(model => model.FirstName));

#line default
#line hidden
            EndContext();
            BeginContext(649, 119, true);
            WriteLiteral("\r\n            </dd>\r\n            <dt>\r\n                Last Name\r\n            </dt>\r\n            <dd>\r\n                ");
            EndContext();
            BeginContext(769, 40, false);
#line 33 "C:\Users\Mirko\source\repos\Review\Review\Views\Car\ReviewerDetails.cshtml"
           Write(Html.DisplayFor(model => model.LastName));

#line default
#line hidden
            EndContext();
            BeginContext(809, 123, true);
            WriteLiteral("\r\n            </dd>\r\n            <dt>\r\n                Date of birth\r\n            </dt>\r\n            <dd>\r\n                ");
            EndContext();
            BeginContext(933, 48, false);
#line 39 "C:\Users\Mirko\source\repos\Review\Review\Views\Car\ReviewerDetails.cshtml"
           Write(Html.DisplayFor(model => model.DateOfBirth.Date));

#line default
#line hidden
            EndContext();
            BeginContext(981, 116, true);
            WriteLiteral("\r\n            </dd>\r\n            <dt>\r\n                Gender\r\n            </dt>\r\n            <dd>\r\n                ");
            EndContext();
            BeginContext(1098, 38, false);
#line 45 "C:\Users\Mirko\source\repos\Review\Review\Views\Car\ReviewerDetails.cshtml"
           Write(Html.DisplayFor(model => model.Gender));

#line default
#line hidden
            EndContext();
            BeginContext(1136, 115, true);
            WriteLiteral("\r\n            </dd>\r\n            <dt>\r\n                About\r\n            </dt>\r\n            <dd>\r\n                ");
            EndContext();
            BeginContext(1252, 37, false);
#line 51 "C:\Users\Mirko\source\repos\Review\Review\Views\Car\ReviewerDetails.cshtml"
           Write(Html.DisplayFor(model => model.About));

#line default
#line hidden
            EndContext();
            BeginContext(1289, 48, true);
            WriteLiteral("\r\n            </dd>\r\n        </dl>\r\n    </div>\r\n");
            EndContext();
#line 55 "C:\Users\Mirko\source\repos\Review\Review\Views\Car\ReviewerDetails.cshtml"

}
else
{

#line default
#line hidden
            BeginContext(1351, 95, true);
            WriteLiteral("    <div class=\"alert alert-danger\">\r\n        Nije odabran ispravni ID reviewera.\r\n    </div>\r\n");
            EndContext();
#line 62 "C:\Users\Mirko\source\repos\Review\Review\Views\Car\ReviewerDetails.cshtml"

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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Review.Model.Reviewer> Html { get; private set; }
    }
}
#pragma warning restore 1591
