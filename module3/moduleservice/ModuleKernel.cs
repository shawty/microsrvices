using System;
using Microsoft.Owin.Hosting;

namespace moduleservice
{
  public class ModuleKernel
  {
    private IDisposable _moduleApplication;

    public void Start()
    {
      StartOptions startOptions = new StartOptions();
      startOptions.Urls.Add("http://127.0.0.1:7003");
      startOptions.Urls.Add("http://localhost:7003");

      _moduleApplication = WebApp.Start<WebPipeline>(startOptions);

    }

    public void Stop()
    {
      _moduleApplication.Dispose();
    }

  }
}
