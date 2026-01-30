window.carouselGestures = {
    init: function (element, dotNetHelper) {
        let startY = 0;
        let endY = 0;
        element.addEventListener('touchstart', function (e) {
            if (e.touches.length === 1) {
                startY = e.touches[0].clientY;
            }
        });
        element.addEventListener('touchend', function (e) {
            if (e.changedTouches.length === 1) {
                endY = e.changedTouches[0].clientY;
                let deltaY = endY - startY;
                if (Math.abs(deltaY) > 50) { // threshold
                    if (deltaY < 0) {
                        dotNetHelper.invokeMethodAsync('NextAlbum'); // swipe up
                    } else {
                        dotNetHelper.invokeMethodAsync('PreviousAlbum'); // swipe down
                    }
                }
            }
        });
    }
};
