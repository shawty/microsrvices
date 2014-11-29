using System.Collections.Generic;
using System.Linq;
using moduledata;
using moduledata.Entites;
using moduleservice.ViewModels;
using Nancy;
using Omu.ValueInjecter;

namespace moduleservice.NancyRoutes
{
  public class HomeRoute : NancyModule
  {
    public HomeRoute()
    {
      Get["/"] = _ =>
      {
        List<DummyEntry> names = new List<DummyEntry>();
        foreach(DummyDbEntry dbrec in DummyData.GetAllEntries())
        {
          DummyEntry vmrec = new DummyEntry();
          vmrec.InjectFrom(dbrec);
          names.Add(vmrec);
        }

        ViewBag.RelativeBase = string.Empty;
        return View["index.cshtml", names];
      };

      Get["/detail/{id}"] = _ =>
      {
        DummyEntry record = new DummyEntry();
        DummyDbEntry dbRecord = DummyData.GetEntryById(_.id);
        record.InjectFrom(dbRecord);

        ViewBag.RelativeBase = "../";
        return View["detail.cshtml", record];
      };

    }

  }
}
