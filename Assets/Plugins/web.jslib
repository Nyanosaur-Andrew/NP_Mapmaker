mergeInto(LibraryManager.library, {

	Hello: function () {
		window.alert("Hello, world!");
	},
  
    AddText: function (textin) {
		var input = document.getElementById('jsonout');
		input.value = UTF8ToString(textin);
	},

});