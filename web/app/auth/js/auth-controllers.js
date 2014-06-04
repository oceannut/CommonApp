'use strict';

define(function (require) {

    require('angular');
    require('../../../static/js/configs');
    require('./auth-services');

    angular.module('auth.controllers', ['configs', 'auth.services'])
        .controller('SignInCtrl', ['$scope', '$location',
            function ($scope, $location) {
                console.log("signin");

                $scope.login = function () {
                    window.location.href = "../cully/index.htm?username=" + $scope.username;
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