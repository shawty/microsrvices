$(document).ready(function() {

  $('#btnReload').on('click', function() {

    myIndexViewModel.Load();

  });

  $('#btnAddNew').on('click', function ()
  {

    myIndexViewModel.AddNew();

  });

});