using System.Collections.Generic;
using System.Linq;
using moduledata.Entites;

namespace moduledata
{
  public class DirectoryData
  {
    private static readonly List<DbDirectoryEntry> Directory = new List<DbDirectoryEntry>
    {
      new DbDirectoryEntry {Pkid = 1, Name = "Peter Fixed", TelephoneNumber = "01234567890"},
      new DbDirectoryEntry {Pkid = 2, Name = "Peter Mobile", TelephoneNumber = "07234567890"},
      new DbDirectoryEntry {Pkid = 3, Name = "Peter Skype", TelephoneNumber = "shawty_ds"},
      new DbDirectoryEntry {Pkid = 4, Name = "Peter Office", TelephoneNumber = "100"},
    };

    public static IEnumerable<DbDirectoryEntry> GetAllEntries()
    {
      return Directory.AsEnumerable();
    }

    public static DbDirectoryEntry GetEntryById(int pkid)
    {
      return Directory.FirstOrDefault(x => x.Pkid.Equals(pkid));
    }

    public static DbDirectoryEntry GetEntryByName(string name)
    {
      return Directory.FirstOrDefault(x => x.Name.Equals(name));
    }

    public static DbDirectoryEntry GetUserByTelephone(string telephoneNumber)
    {
      return Directory.FirstOrDefault(x => x.TelephoneNumber.Equals(telephoneNumber));
    }

    public static bool DeleteEntryById(int id)
    {
      DbDirectoryEntry userToRemove = GetEntryById(id);
      if (userToRemove == null) return false;
      Directory.Remove(userToRemove);
      return true;
    }

    public static bool AddNewEntry(DbDirectoryEntry newEntryToAdd, out AddDirectoryEntryFailReason result)
    {
      DbDirectoryEntry possiblyExistingEntry = GetEntryByName(newEntryToAdd.Name);
      if (possiblyExistingEntry != null)
      {
        result = AddDirectoryEntryFailReason.allreadyExistsDuplicateName;
        return false;
      }

      possiblyExistingEntry = GetUserByTelephone(newEntryToAdd.TelephoneNumber);
      if (possiblyExistingEntry != null)
      {
        result = AddDirectoryEntryFailReason.allreadyExistsDuplicatePhoneNumber;
        return false;
      }

      var newid = Directory.Max(x => x.Pkid);
      newEntryToAdd.Pkid = newid;
      Directory.Add(newEntryToAdd);

      result = AddDirectoryEntryFailReason.noErrorEntryAdded;
      return true;
    }

    public static bool UpdateEntry(DbDirectoryEntry userToUpdate)
    {
      var existingId = userToUpdate.Pkid;
      DbDirectoryEntry existingUser = GetEntryById(existingId);
      if (existingUser == null) return false;
      Directory.Remove(existingUser); //NOTE: this is only until we build a read DB!!! DO NOT DO THIS IN PROD
      Directory.Add(userToUpdate);
      return true;
    }

  }
}
