"use strict";

if (typeof DataProvider !== "object") { throw new Error("DataProvider.js is required"); }

var module = (function (dataProvider) {

    var init = function() {

        $.blockUI({ message: "<h4> Just a moment...</h4>" });
        $.when(dataProvider.loadDays(), dataProvider.loadLanguages()).then(function (days, languages) {
            ko.applyBindings(new MessageViewModel(days[0], languages[0]));
        }).then(function () { $.unblockUI(); });
    };

    function MessageViewModel(days, languages) {

        var self = this;
        
        self.languages = ko.observableArray(languages);
        self.days = ko.observableArray(days);

        //define current day of week to set as a default
        var today = new Date();
        var currentDayOfWeek = today.getDay() === 0 ? 7 : today.getDay();

        self.message = ko.mapping.fromJS({Id:0, Text: "", Day:""});

        self.selectedDay = ko.observable(currentDayOfWeek);
        self.selectedLanguage = ko.observable(1);
        self.selectedLanguageImagePath = ko.computed(function () {
            return "/Content/Images/" + self.languages().filter(
                function (item) {
                    if (item.Id === self.selectedLanguage()) return item;
                })[0].PartialFlagPath;
        });

        self.selectDefaultDay = function (option, item) {
            if (item.Id === currentDayOfWeek) {
                ko.applyBindingsToNode(option.parentElement, { value: item.Id }, item);
            }
        };

        self.dayChanged = function () {
            self.isEditMode(false);
            loadMessage();
        };

        self.languageChanged = function () {
            self.isEditMode(false);
            loadMessage();
        };

        loadMessage();

        function loadMessage() {
            $.when(dataProvider.loadMessage(self.selectedDay(), self.selectedLanguage())).then(function (message) {
                ko.mapping.fromJS(message, self.message);
            });
        }

        //Edit message area
        var tempvalue;

        self.isEditMode = ko.observable(false);
        self.errorMessage = ko.observable();

        self.Edit = function() {
            tempvalue = self.message.Text();
            self.isEditMode(true);
        }

        self.Save = function() {
            $.post("/api/Messages/SetMessage/", { id: self.message.Id, text: self.message.Text })
            .success(function () {
                clearErrorMessage();
                self.isEditMode(false);
            })
            .error(function (xhr) {
                var err = $.parseJSON(xhr.responseText);
                if (typeof(err.ModelState) == "object")
                    self.errorMessage(err.ModelState["message.Text"][0]);
                else
                    self.errorMessage(err.Message);
            });
        }

        function clearErrorMessage() {
            self.errorMessage("");
        }

        self.Cancel = function() {
            clearErrorMessage();
            self.message.Text(tempvalue);
            self.isEditMode(false);
        }
        //Edit message area End
    }

    return {
        Init: init
    }

})(DataProvider);