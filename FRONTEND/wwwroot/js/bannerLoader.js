window.attachBannerLoadedHandler = function(bannerClass, dotnetRef) {
    var banner = document.getElementsByClassName(bannerClass)[0];
    if (banner) {
        var img = banner.querySelector('img');
        if (img) {
            img.addEventListener('bannerLoaded', function() {
                dotnetRef.invokeMethodAsync('OnBannerLoaded');
            });
        }
    }
}
