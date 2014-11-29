using System.Net.Http.Formatting;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
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

      application.UseFileServer(new FileServerOptions
      {
        FileSystem = new PhysicalFileSystem("Content"),
        RequestPath = new PathString("/Content")
      });

      application.UseWebApi(config);
      application.UseNancy();

    }
  }
}
