var plugin = {
    OpenNewTab: function (url) {
        url = Pointer_stringify(url);
        window.open(url, '_blank');
    },
    CloseWindow: function (){
        window.close();
    }
};
mergeInto(LibraryManager.library, plugin);
