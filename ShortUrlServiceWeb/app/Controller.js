app.controller('APIController', function ($scope, APIService) {
    getAll();

     function getAll() {
        var servCall = APIService.getHistory();
         servCall.then(function (d) {
             $scope.items = d.data;
         }, function (error) {
             console.error("Произошла ошибка при получении данных")
         })
    }

     $scope.generate = function () {
        var longUrl = {
            UrlLong: $scope.urlLong            
        };
        var generate = APIService.generateUrl(longUrl);
         generate.success(function (d) {
             $scope.success = "Ссылка добавлена";
             $scope.error = null;
             $scope.urlLong = d.UrlShort;
        }).error (function (error) {
            $scope.error = "Произошла ошибка при генерации короткой ссылки";
            $scope.success = null;
        });
    };
})



