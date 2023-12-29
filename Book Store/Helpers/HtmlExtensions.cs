using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace Book_Store.Helpers
{
	public static class HtmlExtensions
	{
		public static IHtmlContent GetBackgroundImageUrl(this IHtmlHelper html)
		{
			string backgroundImageUrl = "https://www.pixground.com/wp-content/uploads/2023/07/Abstract-Gradient-Blue-Layers-AI-Generated-4K-Wallpaper-jpg.webp";
			return new HtmlString(backgroundImageUrl);
		}
	}
}
