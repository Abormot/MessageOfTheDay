"use strict";

//Load data from api
var DataProvider = (function () {

    // Load days list from server
    function loadDays() {
        return $.getJSON("/api/Messages/getDays");
    }

    // Load Languages list from server
    function loadLanguages() {
        return $.getJSON("/api/Messages/getLanguages");
    }

    // Load Message from server by params DayId LanguageId
    function loadMessage(dayId, languageId) {
        return $.getJSON("/api/Messages/getMessage?dayId=" + dayId + "&languageId=" + languageId);
    }

    return {
        loadDays: loadDays,
        loadLanguages: loadLanguages,
        loadMessage: loadMessage
    }
})();