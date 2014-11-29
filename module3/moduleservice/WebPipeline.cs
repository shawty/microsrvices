using System.Net.Http.Formatting;
using System.Web.Http;
using Owin;

namespace moduleservice
{
  public class WebPipeline
  {
    public void Configuration(IAppBuilder application)
    {
      var config = new HttpConfiguration();
      config.MapHttpAttributeRoutes();

      config.Routes.MapHttpRoute(
        name: "DefaultApi",
        routeTemplate: "api/{controller}/{id}",
        defaults: new { id = RouteParameter.Optional });

      config.Formatters.Clear();
      config.Formatters.Add(new JsonMediaTypeFormatter());

      application.UseWebApi(config);

    }
  }
}
