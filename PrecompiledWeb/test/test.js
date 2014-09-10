
angular.module("TestApp", ['configs', 'services'])
    .controller("TestCtrl", ['$scope', 'DemoService', 'DemoService2', function ($scope, DemoService, DemoService2) {

        $scope.testPost = function () {
            DemoService.save({ 'name': $scope.name })
                .$promise
                    .then(function (result) {
                        console.log(result);
                        $scope.result = result;
                    }, function (error) {
                        console.log("error: " + error);
                    });
        }

        $scope.testGet = function () {
            DemoService.get({ 'name': $scope.name })
                .$promise
                    .then(function (result) {
                        console.log(result);
                        $scope.result = result;
                    }, function (error) {
                        console.log("error: " + error);
                    });
        }

        $scope.testPost2 = function () {
            DemoService2.save($scope.name)
                .then(function (result) {
                    console.log(result);
                    $scope.result2 = result.data;
                }, function (error) {
                    console.log("error: " + error);
                });
        }

        $scope.testGet2 = function () {
            DemoService2.get($scope.name)
                .then(function (result) {
                    console.log(result);
                    $scope.result2 = result.data;
                }, function (error) {
                    console.log("error: " + error);
                });
        }

    } ]);