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
        .controller('SignUpCtrl', ['$scope', '$location', 'currentUser',
            function ($scope, $location, currentUser) {

                console.log("signup");

            } ]);

});