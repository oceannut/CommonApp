'use strict';

define(function (require) {

    require('ng');
    require('../../../static/js/configs');
    require('./auth-services');
    require('../../common/js/user-services');
    require('../../common/js/common-cache');

    require('../../../static/css/sign.css');

    angular.module('auth.controllers', ['configs', 'auth.services', 'user.services', 'common.cache'])
        .controller('SignInCtrl', ['$scope', '$location', '$log', 'currentUser', 'SignInService', 'userCache',
            function ($scope, $location, $log, currentUser, SignInService, userCache) {

                $scope.init = function () {
                    $scope.alertMessageVisible = 'hidden';
                    $scope.isLoging = false;
                    $scope.login = {};
                }

                $scope.signin = function () {
                    $scope.alertMessageVisible = 'hidden';
                    $scope.isLoging = true;
                    SignInService.save({
                        'username': $scope.login.username,
                        'pwd': $scope.login.pwd
                    })
                    .$promise
                        .then(function (result) {
                            currentUser.username = $scope.login.username;
                            userCache.getAsync(currentUser.username, function (e) {
                                currentUser.name = e.Name;
                            });
                            $location.path('/common-overview/');
                        }, function (error) {
                            $scope.alertMessageVisible = 'show';
                            if (error.status == '400') {
                                $scope.alertMessage = "提示：无效的请求，" + error.data;
                            } else if (error.status == '403') {
                                $scope.alertMessage = "提示：登录密码错误，请重新输入密码";
                            } else if (error.status == '404') {
                                $scope.alertMessage = "提示：用户名" + $scope.login.username + "不存在";
                            } else {
                                $scope.alertMessage = "提示：登录操作失败";
                            }
                            $log.error(error);
                        })
                        .then(function () {
                            $scope.isLoging = false;
                        });
                }

            } ])
        .controller('SignUpCtrl', ['$scope', '$location',
            function ($scope, $location) {

                $scope.init = function () {
                    console.log("sign-up");

                }

                $scope.signup = function () {
                    console.log($scope.username);
                }

            } ]);

});