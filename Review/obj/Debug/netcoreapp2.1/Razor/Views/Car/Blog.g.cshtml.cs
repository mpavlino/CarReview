#pragma checksum "C:\Users\Mirko\source\repos\Review\Review\Views\Car\Blog.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "bb8ef4e027290999f2c335f9cf3a06bd71a861ad"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Car_Blog), @"mvc.1.0.view", @"/Views/Car/Blog.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Car/Blog.cshtml", typeof(AspNetCore.Views_Car_Blog))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"bb8ef4e027290999f2c335f9cf3a06bd71a861ad", @"/Views/Car/Blog.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"cc3eed6a05fa337a284c781f9d236e0ec921c21c", @"/Views/_ViewImports.cshtml")]
    public class Views_Car_Blog : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Review.Model.Car>
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
#line 2 "C:\Users\Mirko\source\repos\Review\Review\Views\Car\Blog.cshtml"
  
    ViewData["Title"] = "Blog";

#line default
#line hidden
            BeginContext(65, 18, true);
            WriteLiteral("<h2>Details</h2>\r\n");
            EndContext();
#line 6 "C:\Users\Mirko\source\repos\Review\Review\Views\Car\Blog.cshtml"
 if (Model != null)
{



#line default
#line hidden
            BeginContext(111, 41, true);
            WriteLiteral("    <ol class=\"breadcrumb\">\r\n        <li>");
            EndContext();
            BeginContext(152, 35, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7df8147d0d7b42bd9372a45ad6c39980", async() => {
                BeginContext(174, 9, true);
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
            BeginContext(187, 34, true);
            WriteLiteral("</li>\r\n        <li class=\"active\">");
            EndContext();
            BeginContext(222, 17, false);
#line 12 "C:\Users\Mirko\source\repos\Review\Review\Views\Car\Blog.cshtml"
                      Write(Model.Brand?.Name);

#line default
#line hidden
            EndContext();
            BeginContext(239, 18, true);
            WriteLiteral("</li>\r\n    </ol>\r\n");
            EndContext();
            BeginContext(259, 72, true);
            WriteLiteral("    <div class=\"row\">\r\n        <div class=\"col-md-10\">\r\n            <h2>");
            EndContext();
            BeginContext(332, 17, false);
#line 17 "C:\Users\Mirko\source\repos\Review\Review\Views\Car\Blog.cshtml"
           Write(Model.Brand?.Name);

#line default
#line hidden
            EndContext();
            BeginContext(349, 1, true);
            WriteLiteral(" ");
            EndContext();
            BeginContext(351, 11, false);
#line 17 "C:\Users\Mirko\source\repos\Review\Review\Views\Car\Blog.cshtml"
                              Write(Model.Model);

#line default
#line hidden
            EndContext();
            BeginContext(362, 68, true);
            WriteLiteral("</h2>\r\n            <ul>\r\n                <li>Engine of this car is: ");
            EndContext();
            BeginContext(431, 12, false);
#line 19 "C:\Users\Mirko\source\repos\Review\Review\Views\Car\Blog.cshtml"
                                      Write(Model.Engine);

#line default
#line hidden
            EndContext();
            BeginContext(443, 48, true);
            WriteLiteral("</li>\r\n                <li>It has top speed of: ");
            EndContext();
            BeginContext(492, 14, false);
#line 20 "C:\Users\Mirko\source\repos\Review\Review\Views\Car\Blog.cshtml"
                                    Write(Model.TopSpeed);

#line default
#line hidden
            EndContext();
            BeginContext(506, 74, true);
            WriteLiteral(" km/h</li>\r\n                <li>Acceleration time in seconds (0-100km/h): ");
            EndContext();
            BeginContext(581, 18, false);
#line 21 "C:\Users\Mirko\source\repos\Review\Review\Views\Car\Blog.cshtml"
                                                         Write(Model.Acceleration);

#line default
#line hidden
            EndContext();
            BeginContext(599, 42, true);
            WriteLiteral("</li>\r\n                <li>Year of model: ");
            EndContext();
            BeginContext(642, 20, false);
#line 22 "C:\Users\Mirko\source\repos\Review\Review\Views\Car\Blog.cshtml"
                              Write(Model.ModelYear.Year);

#line default
#line hidden
            EndContext();
            BeginContext(662, 36, true);
            WriteLiteral("</li>\r\n                <li>Country: ");
            EndContext();
            BeginContext(699, 19, false);
#line 23 "C:\Users\Mirko\source\repos\Review\Review\Views\Car\Blog.cshtml"
                        Write(Model.Country?.Name);

#line default
#line hidden
            EndContext();
            BeginContext(718, 97, true);
            WriteLiteral("</li>\r\n            </ul>\r\n        </div>\r\n    </div>\r\n    <div class=\"range\">\r\n        <h3>About ");
            EndContext();
            BeginContext(816, 11, false);
#line 28 "C:\Users\Mirko\source\repos\Review\Review\Views\Car\Blog.cshtml"
             Write(Model.Model);

#line default
#line hidden
            EndContext();
            BeginContext(827, 32, true);
            WriteLiteral("</h3>\r\n        <p>\r\n            ");
            EndContext();
            BeginContext(860, 17, false);
#line 30 "C:\Users\Mirko\source\repos\Review\Review\Views\Car\Blog.cshtml"
       Write(Model.Description);

#line default
#line hidden
            EndContext();
            BeginContext(877, 28, true);
            WriteLiteral("\r\n        </p>\r\n    </div>\r\n");
            EndContext();
#line 33 "C:\Users\Mirko\source\repos\Review\Review\Views\Car\Blog.cshtml"


}
else
{

#line default
#line hidden
            BeginContext(921, 96, true);
            WriteLiteral("    <div class=\"alert alert-danger\">\r\n        Nije odabran ispravni ID automobila.\r\n    </div>\r\n");
            EndContext();
#line 41 "C:\Users\Mirko\source\repos\Review\Review\Views\Car\Blog.cshtml"

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
