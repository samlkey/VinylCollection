window.attachIframeLoadedHandler = function(iframeId, dotnetRef) {
    var iframe = document.getElementById(iframeId);
    if (iframe) {
        iframe.addEventListener('iframeLoaded', function() {
            dotnetRef.invokeMethodAsync('OnIframeLoaded');
        });
    }
}
