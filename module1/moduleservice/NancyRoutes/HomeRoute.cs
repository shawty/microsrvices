using Nancy;

namespace moduleservice.NancyRoutes
{
  public class HomeRoute : NancyModule
  {
    public HomeRoute()
    {
      Get["/"] = _ => View["index.cshtml"];
    }

  }
}
