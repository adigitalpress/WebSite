using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace aDigitalWebSite.Web.Infrastructure.Filters
{
	public class CommonViewBagInitializerActionFilter : ActionFilterAttribute
	{
		public override void OnResultExecuting(ResultExecutingContext context)
		{
#if DEBUG
			((BaseController)context.Controller).ViewBag.RootBlobURL = "";
#else
			((BaseController)context.Controller).ViewBag.RootBlobURL = "http://blob.adigital.press";
#endif

		}
	}
}
