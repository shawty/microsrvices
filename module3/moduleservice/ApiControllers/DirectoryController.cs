using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using moduledata;
using moduleservice.DataModels;

namespace moduleservice.ApiControllers
{
  public class PhoneDirectoryController : ApiController
  {
    [Route("directory")]
    [HttpGet]
    public List<DirectoryEntry> Directory()
    {
      var databaseEntries = DirectoryData.GetAllEntries();

      return databaseEntries.Select(dbDirectoryEntry => new DirectoryEntry
      {
        Pkid = dbDirectoryEntry.Pkid,
        Name = dbDirectoryEntry.Name,
        TelephoneNumber = dbDirectoryEntry.TelephoneNumber
      }).ToList();
    }

  }
}
