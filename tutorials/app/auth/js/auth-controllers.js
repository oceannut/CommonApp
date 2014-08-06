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
        .controller('SignInCtrl', ['$scope', '$location', '$log', 'currentUser', 'eventbus', 'SignInService', 'currentUserDetails',
            function ($scope, $location, $log, currentUser, eventbus, SignInService, currentUserDetails) {

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
                            currentUserDetails.getAsync($scope.login.username, function (e) {
                                currentUser.setDetails({ "name": e.Name, "roles": e.Roles });
                                eventbus.broadcast("userSignIn", $scope.login.username);
                                //$location.path('/home/');
                            });
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
        .controller('SignUpCtrl', ['$scope', '$location', '$log', 'UserService', 'SignUpService',
            function ($scope, $location, $log, UserService, SignUpService) {

                var lastUsername;

                $scope.init = function () {
                    $scope.alertMessageVisible = 'hidden';
                    $scope.user = {};
                    $scope.isUserExisted = undefined;
                    lastUsername = $scope.user.username;
                    $scope.isSamePwd = true;
                    $scope.isBusy = false;
                }

                $scope.usernameChanged = function () {
                    if (lastUsername != $scope.user.username && $scope.user.username != undefined && $scope.user.username != '') {
                        lastUsername = $scope.user.username;
                        $scope.usernameDisabled = true;
                        $scope.usernameStatus = "";
                        $scope.usernameFeedback = "fa-spin fa-spinner";
                        $scope.usernameFeedbackText = "";

                        UserService.get({ "username": $scope.user.username })
                            .$promise
                                .then(function (result) {
                                    if (result.Username === undefined) {
                                        $scope.usernameStatus = "has-success";
                                        $scope.usernameFeedback = "fa-check";
                                    } else {
                                        $scope.usernameStatus = "has-error";
                                        $scope.usernameFeedback = "fa-exclamation-triangle";
                                        $scope.usernameFeedbackText = "此用户名已被使用，请换一个。";
                                    }
                                }, function (error) {
                                    $log.error(error);
                                })
                                .then(function () {
                                    $scope.usernameDisabled = false;
                                });

                    } else if ($scope.user.username == undefined || $scope.user.username == "") {
                        $scope.usernameStatus = "";
                        $scope.usernameFeedback = "";
                        $scope.usernameFeedbackText = "";
                    }
                }

                $scope.pwd2Changed = function () {
                    $scope.isSamePwd = $scope.user.pwd != undefined & $scope.user.pwd2 != undefined
                                        & $scope.user.pwd2 == $scope.user.pwd;
                }

                $scope.signup = function () {
                    $scope.alertMessageVisible = 'hidden';
                    $scope.isBusy = true;
                    SignUpService.save({
                        'username': $scope.user.username,
                        'pwd': $scope.user.pwd,
                        'name': $scope.user.name
                    })
                    .$promise
                        .then(function (result) {
                            $scope.isSuccess = true;
                            $scope.alertMessageVisible = 'show';
                            $scope.alertMessage = "提示：注册成功";
                        }, function (error) {
                            $scope.isSuccess = false;
                            $scope.alertMessageVisible = 'show';
                            $scope.alertMessage = "提示：注册失败";
                            $log.error(error);
                        })
                        .then(function () {
                            $scope.isBusy = false;
                        });
                }

            } ])
        .controller('SignOutCtrl', ['$scope', 'currentUser', 'eventbus', 'currentUserDetails',
            function ($scope, currentUser, eventbus, currentUserDetails) {

                $scope.init = function () {
                    currentUser.sign_out();
                    currentUserDetails.clear();
                    eventbus.broadcast("userSignOut", currentUser.username);
                }

            } ]);

});