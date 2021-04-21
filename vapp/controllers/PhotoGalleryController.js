stpaulsApp.controller("PhotoGalleriesController", ['$timeout','Config','Upload','GlobalVariableService', '$scope', '$filter', '$http', '$location', '$routeParams', 'toaster', 'CommonService',
    function ($timeout,Config,Upload,GlobalVariableService, $scope, $filter, $http, $location, $routeParams, toaster, CommonService) {
         
        $scope.jcp = "";

        $scope.CropAndSave = function () {
            console.log($scope.jcp.active.pos);

            var AttachedFile = document.getElementById('ProfileImage').files;
            if (AttachedFile && AttachedFile.length) {
                var splitfile = AttachedFile[0].name;
                var fileNameWithoutExt = splitfile.substring(0, splitfile.lastIndexOf("."));
                var fileType = splitfile.substring(splitfile.lastIndexOf(".") + 1);
                var date = $filter('date')(new Date(), "ddMMyyyyHHmmsss");
                var filename = fileNameWithoutExt + "_" + date + "." + fileType;

                
                ////CommonService.GetListItems(lstBill).then(function (response) {
                ////    if (response && response.data.d.results.length > 0) {

                ////    }
                ////});

                Upload.upload({
                    url: Config.ServiceBaseURL + "/api/PhotoGalleryAPI/CropAndSaveImage?moduleName=" + $scope.Album +"&fileName=" + filename + "&x=" + $scope.jcp.active.pos.x + "&y=" + $scope.jcp.active.pos.y + "&w=" + $scope.jcp.active.pos.w + "&h=" + $scope.jcp.active.pos.h,
                    //url: Config.ServiceBaseURL + "/api",
                    //data:values
                    data: {

                        files: AttachedFile
                    }
                }).then(function (response) {
                    $timeout(function () {
                        $scope.status = response.status;
                        if ($scope.status === 200) {
                            alert("Image Saved")
                            
                        }
                    });
                }, function (response) {
                    if (response.status > 0) {
                        var errorMsg = response.status + ': ' + JSON.stringify(response.data);
                        console.log(errorMsg);
                        //alert(errorMsg);
                    }
                });
            }
        };

        $scope.init = function () {
            Jcrop.load('widePhoto').then(img => {
                $scope.jcp = Jcrop.attach(img, {
                    multi: false,
                });

                $scope.jcp.setOptions({
                    shadeOpacity: 0.8,
                });

                const rect = Jcrop.Rect.sizeOf($scope.jcp.el);
                $scope.jcp.newWidget(rect.scale(.7, .5).center(rect.w, rect.h));
                $scope.jcp.focus();
            });

        };

        $scope.init();


    }]);