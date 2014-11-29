var DirectoryEntryViewModel = (function ()
{
  function DirectoryEntryViewModel(inputRecord, parent)
  {
    this.RecordId = ko.observable(0);
    this.EntryName = ko.observable('');
    this.DialedNumber = ko.observable('');
    this.parent = parent;
    ko.mapping.fromJS(inputRecord, {}, this);

  }

  return DirectoryEntryViewModel;
})();

var IndexViewModel = (function ()
{
  function IndexViewModel(targetElement, confirmationDialog, editingDialog)
  {
    this.loadComplete = ko.observable(false);
    this.voipDirectory = ko.observableArray([]);
    this.showAlert = ko.computed(function ()
    {
      if (!this.loadComplete())
        return false;
      if (this.voipDirectory().length < 1)
      {
        return true;
      }
      return false;
    }, this);
    this.showTable = ko.computed(function ()
    {
      if (!this.loadComplete())
        return false;
      if (this.voipDirectory().length > 0)
      {
        return true;
      }
      return false;
    }, this);
    this.deleteConfirmationDialog = confirmationDialog;
    this.editingDialog = editingDialog;

    ko.applyBindings(this, targetElement);
    $.support.cors = true;
  }

  IndexViewModel.prototype.Load = function ()
  {
    var _this = this;
    $.getJSON("/admin/getall", function (data)
    {
      if (data.length > 0)
      {
        _this.voipDirectory(ko.utils.arrayMap(data, function (item)
        {
          return new DirectoryEntryViewModel(item, _this);
        }));
      } else
      {
        _this.voipDirectory([]);
      }
      _this.loadComplete(true);
    });
  };

  IndexViewModel.prototype.AddNew = function()
  {
    var newDirectoryEntry = new DirectoryEntryViewModel({ RecordId: 0, EntryName: '', DialedNumber: '' });
    this.editingDialog.show(this, newDirectoryEntry);
  };

  IndexViewModel.prototype.deleteCallback = function (recordToDelete)
  {
    // Delete confirmation dialog will call back here if delete is confirmed
    this.voipDirectory.remove(recordToDelete);
    //TODO: Make server call to remove the entry from the server too
  };

  IndexViewModel.prototype.btnDeleteClick = function (selectedRecord)
  {
    // Show the confirmation dialog
    // NOTE: this is called from a tablerow click, so this == selectedRecord == DirectoryEntryViewModel
    this.parent.deleteConfirmationDialog.show(this.parent, selectedRecord);
  };

  IndexViewModel.prototype.editCallback = function (recordToUpdate)
  {
    // Edit dialog will call back here if save is clicked
    var existingRecord = ko.utils.arrayFirst(this.voipDirectory(), function(item) {
      return item.RecordId() == recordToUpdate.RecordId();
    });

    existingRecord = recordToUpdate;
    //TODO: Make server call to update the entry from the server too
  };

  IndexViewModel.prototype.btnEditClick = function (selectedRecord)
  {
    // Show the editing dialog
    // NOTE: this is called from a tablerow click, so this == selectedRecord == DirectoryEntryViewModel
    this.parent.editingDialog.show(this.parent, selectedRecord);
  };

  return IndexViewModel;
})();

var ConfirmationDialogViewModel = (function ()
{
  function ConfirmationDialogViewModel(targetElement)
  {
    this.RecordId = ko.observable(0);
    this.ExistingRecord = ko.observable({});

    ko.applyBindings(this, targetElement);
    $.support.cors = true;
    this.DialogDefinition = targetElement;
  }

  ConfirmationDialogViewModel.prototype.show = function (parentViewModel, existingRecord)
  {
    this.ParentViewModel = parentViewModel;
    this.ExistingRecord = existingRecord;
    this.RecordId(existingRecord.RecordId());

    $(this.DialogDefinition).modal('show');
  };

  ConfirmationDialogViewModel.prototype.primaryButtonClick = function ()
  {
    this.ParentViewModel.deleteCallback(this.ExistingRecord);
    $(this.DialogDefinition).modal('hide');
  };

  return ConfirmationDialogViewModel;
})();

var EditDialogViewModel = (function ()
{
  function EditDialogViewModel(targetElement)
  {
    this.DialogTitle = ko.observable('Edit Directory Entry');
    this.ExistingRecord = ko.observable({});
    this.EntryName = ko.observable('');
    this.DialedNumber = ko.observable('');
    this.DialogDefinition = targetElement;

    ko.applyBindings(this, targetElement);
    $.support.cors = true;
  }

  EditDialogViewModel.prototype.show = function (parentViewModel, existingRecord)
  {
    this.ParentViewModel = parentViewModel;
    this.ExistingRecord = existingRecord;
    this.EntryName(existingRecord.EntryName());
    this.DialedNumber(existingRecord.DialedNumber());

    if (existingRecord.RecordId() == 0)
    {
      this.DialogTitle('Add New Directory Entry');
    } else
    {
      this.DialogTitle('Edit Directory Entry');
    }

    $(this.DialogDefinition).modal('show');
  };

  EditDialogViewModel.prototype.primaryButtonClick = function ()
  {
    this.ExistingRecord.EntryName(this.EntryName());
    this.ExistingRecord.DialedNumber(this.DialedNumber());
    this.ParentViewModel.editCallback(this.ExistingRecord);
    $(this.DialogDefinition).modal('hide');
  };

  return EditDialogViewModel;
})();


window.onload = function ()
{
  var myConfirmationDialog = document.getElementById('ConfirmationModal');
  var myConfirmationDialogVM = new ConfirmationDialogViewModel(myConfirmationDialog);

  var myEditDialog = document.getElementById('EditModal');
  var myEditDialogVM = new EditDialogViewModel(myEditDialog);

  var pageView = document.getElementById('directoryList');
  myIndexViewModel = new IndexViewModel(pageView, myConfirmationDialogVM, myEditDialogVM);
  myIndexViewModel.Load();
};
