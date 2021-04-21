stpaulsApp.controller("SlideController", ["$scope", "Config", "CommonService", function ($scope, Config, CommonService) {

    $scope.myInterval = 3000;



    $scope.slides = [
        {
            image: 'http://lorempixel.com/400/200/'
        },
        {
            image: 'http://lorempixel.com/400/200/food'
        },
        {
            image: 'http://lorempixel.com/400/200/sports'
        },
        {
            image: 'http://lorempixel.com/400/200/people'
        }
    ];
    $scope.GetDataForDashboard = function () {
        $scope.ImageList = [];
        var lst = {
            title: "PhotoGalleries",
            fields: ["PhotoPath"]
            
        };
        if ($scope.searchAlbumName !== '') {
            lst.filter = lst.filter + " and indexof(Album" + $scope.searchAlbumName + "') gt -1";
        }


        //$scope.showSpinner();
        CommonService.GetListItems(lst).then(function (response) {
            if (response && response.data.d.results.length > 0) {
                $scope.slides = response.data.d.results;
                //angular.forEach($scope.slides, function (value,key) {
                //    value.PhotoPath = Config.ServiceBaseURL + value.PhotoPath

                //})
            }
            else {
                $scope.slides = [];

            }
        });
    };
}]);