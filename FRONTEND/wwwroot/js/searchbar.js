window.isSuggestionFocused = function () {
    const active = document.activeElement;
    const list = document.querySelector('.autocomplete-list');
    const clear = document.querySelector('.clear-search-btn');
    if (clear && clear.contains(active)) return true;
    if (list && list.contains(active)) return true;
    return false;
};