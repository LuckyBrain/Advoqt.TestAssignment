module app.controllers {
    interface ICityListModel {
        showWeather(id: number): void;
    }

    interface IWeatherData {
        id: number;
        main: {
            temp: number;
            pressure: number;
            humidity: number;
            temp_min: number;
            temp_max: number;
        };
    }

    class CityListCtrl implements ICityListModel {
        apiKey = "3271c88e42d3ddf385d5d94c8f15df76";
        urlPrefix = "http://api.openweathermap.org/data/2.5/weather?units=imperial";
        fDegreeHtml = "&#8457;";

        static $inject = ["$http"];
        constructor(private $http: ng.IHttpService) { }

        showWeather(id: number): void {
            var url = this.urlPrefix + "&id=" + id + "&APPID=" + this.apiKey;
            this.$http.get(url)
                .then(response => {
                    this.displayWeather(<IWeatherData>response.data);
                })
                .catch(reason => {
                    alert(reason.statusText);
                });
        }

        displayWeather(response: IWeatherData): void {
            var id = "weather" + response.id;
            document.getElementById(id).innerHTML = response.main.temp + this.fDegreeHtml;
        }
    }

    angular
        .module("weatherManagement")
        .controller("CityListCtrl", CityListCtrl);
}