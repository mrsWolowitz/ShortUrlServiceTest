var app;
(function () {
    app = angular.module("APIModule", []);
})();

//app.service("APIService", function ($http) {
//    this.getSubs = function () {
//        return $http.get("api/records")
//    }
//});

//app.controller('APIController', function ($scope, APIService) {
//    getAll();

//    function getAll() {
//        var servCall = APIService.getSubs();
//        servCall.then(function (d) {
//            $scope.subscriber = d.data;
//        }, function (error) {
//            $log.error('Oops! Something went wrong while fetching the data.')
//        })
//    }
//})