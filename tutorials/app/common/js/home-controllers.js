'use strict';

define(function (require) {

    require('ng');
    require('bootstrap');
    require('../../auth/js/auth-models');

    require('../../../static/css/carousel.css');

    angular.module('home.controllers', ['auth.models'])
        .controller('HomeCtrl', ['$scope', '$location', 'currentUser',
            function ($scope, $location, currentUser) {

                $scope.init = function () {

                }

            } ]);


});