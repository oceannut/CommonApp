
angular.module("App", ['ngRoute', 'configs', 'services'])
    .config(['$routeProvider', '$httpProvider',
        function ($routeProvider, $httpProvider) {

            $routeProvider
                .when('/partial1/', {
                    templateUrl: 'partial1.htm',
                    controller: 'Partial1Ctrl'
                })
                .when('/partial2/', {
                    templateUrl: 'partial2.htm',
                    controller: 'Partial2Ctrl'
                })
                .when('/partial3/', {
                    templateUrl: 'partial3.htm',
                    controller: 'Partial3Ctrl'
                })
                .otherwise({
                    redirectTo: '/partial1/'
                });

        } ])
    .run(['$http', '$rootScope', function ($http, $rootScope) {
        $rootScope.$on('$routeChangeStart', function (event, next) {
            $http.defaults.headers.common.Authorization = 'Basic me';
        });
    } ])
    .controller("Partial1Ctrl", ['$scope', 'DemoService', function ($scope, DemoService) {

        $scope.init = function () {
            console.log("partial1");
        }

        $scope.testPost = function () {
            DemoService.save({ 'name': $scope.name })
                .$promise
                    .then(function (result) {
                        console.log(result);
                        $scope.result = result;
                    }, function (error) {
                        console.log(error);
                    });
        }

        $scope.testGet = function () {
            DemoService.get({ 'name': $scope.name })
                .$promise
                    .then(function (result) {
                        console.log(result);
                        $scope.result = result;
                    }, function (error) {
                        console.log(error);
                    });
        }

    } ])
    .controller("Partial2Ctrl", ['$scope', 'DemoService2', function ($scope, DemoService2) {

        $scope.init = function () {
            console.log("partial2");
        }

        $scope.testPost = function () {
            DemoService2.save($scope.name)
                .then(function (result) {
                    console.log(result);
                    $scope.result = result.data;
                }, function (error) {
                    console.log(error);
                });
        }

        $scope.testGet = function () {
            DemoService2.get($scope.name)
                .then(function (result) {
                    console.log(result);
                    $scope.result = result.data;
                }, function (error) {
                    console.log(error);
                });
        }

    } ])
    .controller("Partial3Ctrl", ['$scope', 'TimeStampService', 'LocalTimeStampService', function ($scope, TimeStampService, LocalTimeStampService) {

        $scope.init = function () {
            console.log("partial3");
        }

        $scope.testGet = function () {
            TimeStampService.get()
                .then(function (result) {
                    console.log(result);
                    $scope.result = result.data;
                }, function (error) {
                    console.log(error);
                });
        }

        $scope.testGet2 = function () {
            LocalTimeStampService.get()
                .then(function (result) {
                    console.log(result);
                    $scope.result = result.data;
                }, function (error) {
                    console.log(error);
                });
        }

    } ]);