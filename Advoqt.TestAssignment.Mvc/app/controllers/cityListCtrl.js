var app;
(function (app) {
    var controllers;
    (function (controllers) {
        var CityListCtrl = (function () {
            function CityListCtrl($http) {
                this.$http = $http;
                this.apiKey = "3271c88e42d3ddf385d5d94c8f15df76";
                this.urlPrefix = "http://api.openweathermap.org/data/2.5/weather?units=imperial";
                this.fDegreeHtml = "&#8457;";
            }
            CityListCtrl.prototype.showWeather = function (id) {
                var _this = this;
                var url = this.urlPrefix + "&id=" + id + "&APPID=" + this.apiKey;
                this.$http.get(url)
                    .then(function (response) {
                    _this.displayWeather(response.data);
                })
                    .catch(function (reason) {
                    alert(reason.statusText);
                });
            };
            CityListCtrl.prototype.displayWeather = function (response) {
                var id = "weather" + response.id;
                document.getElementById(id).innerHTML = response.main.temp + this.fDegreeHtml;
            };
            return CityListCtrl;
        }());
        CityListCtrl.$inject = ["$http"];
        angular
            .module("weatherManagement")
            .controller("CityListCtrl", CityListCtrl);
    })(controllers = app.controllers || (app.controllers = {}));
})(app || (app = {}));
