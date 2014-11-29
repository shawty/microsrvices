using Nancy.ViewEngines.Razor;

namespace moduleservice
{
  public static class HtmlHelperExtensions
  {
    public static IHtmlString PathCombiner<T>(this HtmlHelpers<T> helpers, string part1, string part2)
    {
      var markup = string.Format("{0}{1}", part1, part2);
      return new NonEncodedHtmlString(markup);
    }

  }
}
