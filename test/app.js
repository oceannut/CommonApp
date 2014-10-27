
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
                .when('/partial4/', {
                    templateUrl: 'partial4.htm',
                    controller: 'Partial4Ctrl'
                })
                .when('/partial5/', {
                    templateUrl: 'partial5.htm',
                    controller: 'Partial5Ctrl'
                })
                .when('/partial6/', {
                    templateUrl: 'partial6.htm',
                    controller: 'Partial6Ctrl'
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

    } ])
    .controller("Partial4Ctrl", ['$scope', '$log', 'SignInService', 'SignInService2',
        function ($scope, $log, SignInService, SignInService2) {

            $scope.init = function () {
                console.log("partial4");
            }

            $scope.signin = function () {
                var hashObj = new jsSHA($scope.pwd, 'TEXT');
                var hashPwd = hashObj.getHash(
        		'SHA-1',
        		'B64',
        		parseInt(1, 10)
            );
                SignInService.save({
                    'username': $scope.username,
                    'pwd': hashPwd
                })
            .$promise
                .then(function (result) {
                    $scope.result = result;
                }, function (error) {
                    $log.error(error);
                });
            }

            $scope.signin2 = function () {
                var hashObj = new jsSHA($scope.pwd, 'TEXT');
                var hashPwd = hashObj.getHash(
                'SHA-1',
                'B64',
                parseInt(1, 10)
            );
                SignInService2.save({
                    'username': $scope.username,
                    'pwd': hashPwd
                })
                .then(function (result) {
                    $scope.result = result.data;
                }, function (error) {
                    $log.error(error);
                });
            }

        } ])
    .controller("Partial5Ctrl", ['$scope', '$log', 'PostService', 'PostService2', 'PostService3',
        function ($scope, $log, PostService, PostService2, PostService3) {

            $scope.init = function () {
                console.log("partial5");
            }

            $scope.save = function () {
                PostService.save({
                    'name': $scope.name,
                    'what': $scope.what
                })
                .$promise
                    .then(function (result) {
                        $scope.result = result;
                    }, function (error) {
                        $log.error(error);
                    });
            }

            $scope.save2 = function () {
                PostService2.save($scope.name, $scope.what)
                    .then(function (result) {
                        $scope.result = result.data;
                    }, function (error) {
                        $log.error(error);
                    });

            }

            $scope.save3 = function () {
                PostService3.save({
                    'name': $scope.name,
                    'what': $scope.what
                })
                .then(function (result) {
                    $scope.result = result.data;
                }, function (error) {
                    $log.error(error);
                });
            }

        } ])
    .controller("Partial6Ctrl", ['$scope', 'DemoService3', 'PostService4', function ($scope, DemoService3, PostService4) {

        $scope.init = function () {
            console.log("partial6");
        }

        $scope.testPost = function () {
            DemoService3.save({ 'name': $scope.name })
            .$promise
                .then(function (result) {
                    console.log(result);
                    $scope.result = result;
                }, function (error) {
                    console.log(error);
                });
        }

        $scope.testGet = function () {
            DemoService3.get({ 'name': $scope.name })
            .$promise
                .then(function (result) {
                    console.log(result);
                    $scope.result = result;
                }, function (error) {
                    console.log(error);
                });
        }

        $scope.save = function () {
            PostService4.save({
                'name': $scope.name,
                'what': $scope.what
            })
            .$promise
                .then(function (result) {
                    $scope.result = result;
                }, function (error) {
                    $log.error(error);
                });
        }

    } ]);