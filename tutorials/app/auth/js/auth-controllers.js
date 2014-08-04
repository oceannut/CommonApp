'use strict';

define(function (require) {

    require('ng');
    require('../../../static/js/configs');
    require('../../../static/js/events');
    require('./auth-models');
    require('./auth-services');
    require('../../common/js/user-services');
    require('../../common/js/common-cache');

    require('../../../static/css/sign.css');

    angular.module('auth.controllers', ['configs', 'events', 'auth.models', 'auth.services', 'user.services', 'common.cache'])
        .controller('SignInCtrl', ['$scope', '$location', '$log', 'currentUser', 'eventbus', 'SignInService',
            function ($scope, $location, $log, currentUser, eventbus, SignInService) {

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
                            currentUser.sign_in($scope.login.username);
                            eventbus.broadcast("userSignIn", $scope.login.username);
                            $location.path('/category-overview/');
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

            } ])
        .controller('SignOutCtrl', ['$scope', 'currentUser', 'eventbus',
            function ($scope, currentUser, eventbus) {

                $scope.init = function () {
                    currentUser.sign_out();
                    eventbus.broadcast("userSignOut", currentUser.username);
                }

            } ]);

});