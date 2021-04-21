//Define an angular module for our app 
var stpaulsApp = angular.module("stpaulsApp", ["ngFileUpload","ngRoute", "ngAnimate", "ui.bootstrap", "toaster", "ui.grid", "ui.grid.edit", "ui.grid.cellNav", "ui.grid.validate",
    "ui.grid.pagination", "ui.grid.autoResize", "ui.grid.selection", "ui.grid.resizeColumns", "ui.grid.grouping", "mgcrea.ngStrap"]);//;.run(init);
//for autocomplete dropdown
//function init($rootScope, GlobalVariableService) {
//    //ngRoute
//    $rootScope.$on('$routeChangeStart', function (angularEvent, next, current) {
//        GlobalVariableService.validateUrl(current);
//    });

stpaulsApp.constant("Config", {
    "ServiceBaseURL": "http://localhost:8070",
    
});//http://ephraim.ttpsolutions.in //http://localhost:50503



stpaulsApp.directive("select2", function ($timeout, $parse) {
    return {
        require: 'ngModel',
        restrict: 'A',
        link: function (scope, element, attrs) {
            $timeout(function () {
                element.select2();
                element.select2Initialized = true;
            });

            var recreateSelect = function () {
                if (!element.select2Initialized) {
                    return;
                }
                $timeout(function () {
                    element.select2('destroy');
                    element.select2();
                });
            };

            scope.$watch(attrs.ngModel, recreateSelect);

            if (attrs.ngOptions) {
                var list = attrs.ngOptions.match(/ in ([^ ]*)/)[1];
                // watch for option list change
                scope.$watch(list, recreateSelect);
            }

            if (attrs.ngDisabled) {
                scope.$watch(attrs.ngDisabled, recreateSelect);
            }
        }
    };
});

stpaulsApp.directive('validFile', function () {
    return {
        require: 'ngModel',
        link: function (scope, el, attrs, ngModel) {
            //change event is fired when file is selected
            el.bind('change', function () {
                scope.$apply(function () {
                    ngModel.$setViewValue(el.val());
                    ngModel.$render();
                });
            });
        }
    }
});

stpaulsApp.directive('myUpload', function () {
    return {
        restrict: 'A',
        link: function (scope, elem, attrs) {
            var reader = new FileReader();
            reader.onload = function (e) {
                scope.image = e.target.result;
                scope.$apply();
            }
            elem.on('change', function () {
                reader.readAsDataURL(elem[0].files[0]);
            });
        }
    };
});

//Define Routing for app
stpaulsApp.config(['$routeProvider', '$locationProvider',
    function ($routeProvider, $locationProvider) {
        $locationProvider.hashPrefix('');
        $locationProvider.html5Mode({
            enabled: false,
            requireBase: true
        });

        $routeProvider.when("/", {
            templateUrl: '/vtemplate/uploadfile.html',
            controller: 'PhotoGalleriesController'
        }).when("/Slide", {
            templateUrl: '/vtemplate/slide.html',
            controller: 'SlideController'        
        }).when("/EditPage", {
            templateUrl: '/vtemplate/EditPage.html',
            controller: 'PagesController'
        }).when("/EditPage/:ID", {
            templateUrl: '/vtemplate/EditPage.html',
            controller: 'PagesController'
        }).when("/invalid", {
            templateUrl: '/vtemplate/InvalidPage.html',
            controller: 'sss'
        }).when("/valid", {
            templateUrl: '/vtemplate/InvalidPage.html',
            controller: 'ddd'
        });
    }]);


