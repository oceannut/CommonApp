'use strict';

define(function (require) {

    require('ng-route');

    require('./configs');
    require('../../app/auth/js/auth-controllers');
    require('../../app/common/js/common-overview-controllers');

    angular.module('Tutorials', ['ngRoute', 'configs', 'auth.controllers', 'common.overview.controllers'])
        .config(['$routeProvider', '$httpProvider', function ($routeProvider, $httpProvider) {

            $routeProvider.
                when('/sign-in/', {
                    templateUrl: 'app/auth/partials/sign-in.htm',
                    controller: 'SignInCtrl'
                }).
                when('/sign-up/', {
                    templateUrl: 'app/auth/partials/sign-up.htm',
                    controller: 'SignUpCtrl'
                }).
                when('/common-overview/', {
                    templateUrl: 'app/common/partials/overview.htm'
                }).
                otherwise({
                    redirectTo: '/sign-in/'
                });

        } ])
        .controller('IndexCtrl', ['$scope', 'appName',
            function ($scope, appName) {

                $scope.appName = appName;

            } ]); ;

    return {
        init: function () {
            angular.bootstrap(document, ['Tutorials']);
        }
    }

});