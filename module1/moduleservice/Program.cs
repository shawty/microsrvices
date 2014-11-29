using Topshelf;

namespace moduleservice
{
  class Program
  {
    static int Main()
    {
      var exitCode = HostFactory.Run(host =>
      {
        host.Service<ModuleKernel>(service =>
        {
          service.ConstructUsing(() => new ModuleKernel());
          service.WhenStarted(a => a.Start());
          service.WhenStopped(a => a.Stop());
        });
        host.SetDisplayName("Microservices Example Module 1");
        host.SetServiceName("module1service");
        host.SetDescription("Local service to implement service endpoints for Microservices Example Module1 for DDD Scotland presentation");
        host.RunAsNetworkService();
        host.StartAutomaticallyDelayed();
      });

      return (int)exitCode;

    }
  }
}
