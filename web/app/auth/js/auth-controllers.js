'use strict';

define(function (require) {

    require('angular');
    require('cookies');
    require('../../../static/js/configs');
    require('./auth-services');

    angular.module('auth.controllers', ['ngCookies', 'configs', 'auth.services'])
        .controller('SignInCtrl', ['$scope', '$location', '$cookieStore', 'currentUser', 'SignInService',
            function ($scope, $location, $cookieStore, currentUser, SignInService) {

                console.log("signin");

                $scope.signin = function () {
                    SignInService.save({
                        'username': $scope.username,
                        'pwd': $scope.pwd
                    })
                        .$promise
                            .then(function (result) {
                                if (result.Code == 0) {
                                    //$cookieStore.put('username', $scope.username);
                                    //console.log("hello " + $cookieStore.get('username'));
                                    window.location.href = "../common/index.htm?username=" + $scope.username;
                                    //window.location.href = "../common/index.htm";
                                } else {
                                    alert(result.Message);
                                }
                            }, function (error) {
                                console.log(error);
                            });
                }

            } ])
        .controller('SignUpCtrl', ['$scope', '$location', 'currentUser', 'SignUpService',
            function ($scope, $location, currentUser, SignUpService) {

                console.log("signup");

                $scope.signup = function () {
                    SignUpService.save({
                        'username': $scope.username,
                        'pwd': $scope.pwd,
                        'name': $scope.name
                    })
                        .$promise
                            .then(function (result) {
                                console.log(result);
                            }, function (error) {
                                console.log(error);
                            });
                }

            } ]);

});