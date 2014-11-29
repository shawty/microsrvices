var DirectoryEntryViewModel = (function ()
{
  function DirectoryEntryViewModel(inputRecord)
  {
    this.Pkid = ko.observable(0);
    this.Name = ko.observable('');
    this.TelephoneNumber = ko.observable('');
    ko.mapping.fromJS(inputRecord, {}, this);

  }

  return DirectoryEntryViewModel;
})();

var IndexViewModel = (function ()
{
  function IndexViewModel(targetElement)
  {
    this.loadComplete = ko.observable(false);
    this.phoneDirectory = ko.observableArray([]);
    this.showAlert = ko.computed(function ()
    {
      if (!this.loadComplete())
        return false;
      if (this.phoneDirectory().length < 1)
      {
        return true;
      }
      return false;
    }, this);

    this.showTable = ko.computed(function ()
    {
      if (!this.loadComplete())
        return false;
      if (this.phoneDirectory().length > 0)
      {
        return true;
      }
      return false;
    }, this);

    ko.applyBindings(this, targetElement);
    $.support.cors = true;
  }

  IndexViewModel.prototype.Load = function ()
  {
    var _this = this;
    $.getJSON("/module3/directory", function (data)
    {
      if (data.length > 0)
      {
        _this.phoneDirectory(ko.utils.arrayMap(data, function (item)
        {
          return new DirectoryEntryViewModel(item, _this);
        }));
      } else
      {
        _this.phoneDirectory([]);
      }
      _this.loadComplete(true);
    });
  };

  return IndexViewModel;
})();

window.onload = function ()
{
  var pageView = document.getElementById('directoryList');
  myIndexViewModel = new IndexViewModel(pageView);
  myIndexViewModel.Load();
};
