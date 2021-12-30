using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace DailyRoutines.Application.Convertors
{
    public static class RenderViewToString
    {
        public static async Task<string> RenderViewAsync(this PageModel pageModel, string pageName)
        {
            var actionContext = new ActionContext(
                pageModel.HttpContext,
                pageModel.RouteData,
                pageModel.PageContext.ActionDescriptor
            );

            await using var sw = new StringWriter();

            IRazorViewEngine razorViewEngine = pageModel.HttpContext.RequestServices.GetService(typeof(IRazorViewEngine)) as IRazorViewEngine;
            IRazorPageActivator activator = pageModel.HttpContext.RequestServices.GetService(typeof(IRazorPageActivator)) as IRazorPageActivator;

            if (razorViewEngine == null) return null;


            var result = razorViewEngine.FindPage(actionContext, pageName);

            if (result.Page == null)
                return null;

            var page = result.Page;

            var view = new RazorView(razorViewEngine,
                activator,
                new List<IRazorPage>(),
                page,
                HtmlEncoder.Default,
                new DiagnosticListener("ViewRenderService"));


            var viewContext = new ViewContext(
                actionContext,
                view,
                pageModel.ViewData,
                pageModel.TempData,
                sw,
                new HtmlHelperOptions()
            );


            var pageNormal = ((Page)result.Page);

            pageNormal.PageContext = pageModel.PageContext;

            pageNormal.ViewContext = viewContext;


            activator?.Activate(pageNormal, viewContext);

            await page.ExecuteAsync();

            return sw.ToString();
        }
    }
}