#pragma checksum "C:\Users\Mirko\Documents\CarReview\Review\Views\Car\ReviewerDetails.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "19a4ca1da34714cc38868a8eecd9032f3e68e06a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Car_ReviewerDetails), @"mvc.1.0.view", @"/Views/Car/ReviewerDetails.cshtml")]
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
#line 1 "C:\Users\Mirko\Documents\CarReview\Review\Views\_ViewImports.cshtml"
using Review;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Mirko\Documents\CarReview\Review\Views\_ViewImports.cshtml"
using Review.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Mirko\Documents\CarReview\Review\Views\_ViewImports.cshtml"
using Review.Model;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"19a4ca1da34714cc38868a8eecd9032f3e68e06a", @"/Views/Car/ReviewerDetails.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"cc3eed6a05fa337a284c781f9d236e0ec921c21c", @"/Views/_ViewImports.cshtml")]
    public class Views_Car_ReviewerDetails : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Review.Model.Reviewer>
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
#line 2 "C:\Users\Mirko\Documents\CarReview\Review\Views\Car\ReviewerDetails.cshtml"
  
    ViewData["Title"] = "Details";

#line default
#line hidden
#nullable disable
            WriteLiteral("<h2>Details</h2>\r\n");
#nullable restore
#line 6 "C:\Users\Mirko\Documents\CarReview\Review\Views\Car\ReviewerDetails.cshtml"
 if (Model != null)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <ol class=\"breadcrumb\">\r\n        <li>");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "19a4ca1da34714cc38868a8eecd9032f3e68e06a4011", async() => {
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
            WriteLiteral("</li>\r\n        <li class=\"active\">Reviewer / ");
#nullable restore
#line 10 "C:\Users\Mirko\Documents\CarReview\Review\Views\Car\ReviewerDetails.cshtml"
                                 Write(Model.FullName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</li>\r\n    </ol>\r\n");
            WriteLiteral("    <div>\r\n        <h4>Reviewer</h4>\r\n        <hr />\r\n        <dl class=\"dl-horizontal\">\r\n            <dt>\r\n                ID\r\n            </dt>\r\n            <dd>\r\n                ");
#nullable restore
#line 21 "C:\Users\Mirko\Documents\CarReview\Review\Views\Car\ReviewerDetails.cshtml"
           Write(Html.DisplayFor(model => model.ID));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </dd>\r\n            <dt>\r\n                First Name\r\n            </dt>\r\n            <dd>\r\n                ");
#nullable restore
#line 27 "C:\Users\Mirko\Documents\CarReview\Review\Views\Car\ReviewerDetails.cshtml"
           Write(Html.DisplayFor(model => model.FirstName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </dd>\r\n            <dt>\r\n                Last Name\r\n            </dt>\r\n            <dd>\r\n                ");
#nullable restore
#line 33 "C:\Users\Mirko\Documents\CarReview\Review\Views\Car\ReviewerDetails.cshtml"
           Write(Html.DisplayFor(model => model.LastName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </dd>\r\n            <dt>\r\n                Date of birth\r\n            </dt>\r\n            <dd>\r\n                ");
#nullable restore
#line 39 "C:\Users\Mirko\Documents\CarReview\Review\Views\Car\ReviewerDetails.cshtml"
           Write(Html.DisplayFor(model => model.DateOfBirth.Date));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </dd>\r\n            <dt>\r\n                Gender\r\n            </dt>\r\n            <dd>\r\n                ");
#nullable restore
#line 45 "C:\Users\Mirko\Documents\CarReview\Review\Views\Car\ReviewerDetails.cshtml"
           Write(Html.DisplayFor(model => model.Gender));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </dd>\r\n            <dt>\r\n                About\r\n            </dt>\r\n            <dd>\r\n                ");
#nullable restore
#line 51 "C:\Users\Mirko\Documents\CarReview\Review\Views\Car\ReviewerDetails.cshtml"
           Write(Html.DisplayFor(model => model.About));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </dd>\r\n        </dl>\r\n    </div>\r\n");
#nullable restore
#line 55 "C:\Users\Mirko\Documents\CarReview\Review\Views\Car\ReviewerDetails.cshtml"

}
else
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div class=\"alert alert-danger\">\r\n        Nije odabran ispravni ID reviewera.\r\n    </div>\r\n");
#nullable restore
#line 62 "C:\Users\Mirko\Documents\CarReview\Review\Views\Car\ReviewerDetails.cshtml"

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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Review.Model.Reviewer> Html { get; private set; }
    }
}
#pragma warning restore 1591
