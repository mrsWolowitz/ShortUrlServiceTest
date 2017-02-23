app.service("APIService", function ($http) {
    this.getHistory = function () {
        return $http.get("api/records")
    }
    this.generateUrl = function (longUrl) {
        return $http(
        {
            method: 'post',
            data: longUrl,
            url: 'api/records'
        });
    }
});