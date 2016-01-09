//if (typeof ($) != object)
//    throw new Error("Dependencies to jquery are not loaded to the MessageOfTheDay module");
//if (typeof (ko) != object)
//    throw new Error("Dependencies to knockout are not loaded to the MessageOfTheDay module");

var module = (function () {
    
    var module = {};

    // Load days list from server, then populate self.Days
    function LoadDays() {
        return $.getJSON("/api/Messages/getDays", function (data) {
            module.days = data;
        });
    }

    // Load Languages list from server, then populate self.Languages
    function LoadLanguages() {
       return $.getJSON("/api/Messages/getLanguages", function (data) {
            module.languages = data;
        });
    }

    var init = function Init() {
        //define current day of week to set as a default
        var today = new Date();
        var currentDayOfWeek = today.getDay() == 0 ? 7 : today.getDay();

        module.initDay = currentDayOfWeek;
        module.initLanguage = 1;
        
        $.blockUI({ message: '<h4> Just a moment...</h4>' });
        LoadDays().then(LoadLanguages)
            .then(function () { ko.applyBindings(new MessageViewModel()); })
            .then(function () { $.unblockUI(); });
    };

    function MessageViewModel() {

        var self = this;

        self.message = ko.observable();
        self.languages = ko.observableArray(module.languages);
        self.days = ko.observableArray(module.days);

        self.selectedDay = ko.observable(module.initDay);
        self.selectedLanguage = ko.observable(module.initLanguage);
        self.selectedLanguageImagePath = ko.computed(function () {
            return "/Content/Images/" + self.languages().filter(
                function (item) {
                    if (item.Id == self.selectedLanguage()) return item;
                })[0].PartialFlagPath;
        });

        self.selectDefaultDay = function (option, item) {
            if (item.Id == module.initDay) {
                ko.applyBindingsToNode(option.parentElement, { value: item.Id }, item);
            }
        };

        self.dayChanged = function (element) {
            self.isEditMode(false);
            LoadMessage();
        };

        self.languageChanged = function (element) {
            self.isEditMode(false);
            LoadMessage();
        };
        
        function LoadMessage() {
            return $.getJSON("/api/Messages/getMessage?dayId=" + self.selectedDay() + "&languageId=" + self.selectedLanguage(),
                function (message) {
                    self.message(message);
                });
        }

        LoadMessage();

        //Edit message area
        var tempvalue;
        self.isEditMode = ko.observable(false);

        self.Edit = function Edit() {
            tempvalue = self.message().Text;
            self.isEditMode(true);
        }

        self.Error = ko.observable();

        self.Save = function Save() {
            $.post("/api/Messages/SetMessage/" + self.message().Id, { text: self.message().Text })
            .success(function (data) {
                self.message(data);
                self.isEditMode(false);
            })
            .error(function (data) {
                self.Error(data.responseText.Message);
            });
        }

        self.Cancel = function Cancel() {
            $("#messageText").val(tempvalue);
            self.message().Text = tempvalue;
            self.isEditMode(false);
        }
        //Edit message area End
    }

    return {
        InitModule: init
    }

})();