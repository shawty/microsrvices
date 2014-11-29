using System.Collections.Generic;
using System.Linq;
using moduledata.Entites;

namespace moduledata
{
  public class DummyData
  {
    private static readonly List<DummyDbEntry> _dummyData = new List<DummyDbEntry>
    {
      new DummyDbEntry {Pkid = 1, Name = "Peter Shaw (1)", EMail = "peter1@example.com"},
      new DummyDbEntry {Pkid = 2, Name = "Peter Shaw (2)", EMail = "peter2@example.com"},
      new DummyDbEntry {Pkid = 3, Name = "Peter Shaw (3)", EMail = "peter3@example.com"},
      new DummyDbEntry {Pkid = 4, Name = "Peter Shaw (4)", EMail = "peter4@example.com"}
    };

    public static IEnumerable<DummyDbEntry> GetAllEntries()
    {
      return _dummyData.AsEnumerable();
    }

    public static DummyDbEntry GetEntryById(int pkid)
    {
      return _dummyData.FirstOrDefault(x => x.Pkid.Equals(pkid));
    }

  }
}
