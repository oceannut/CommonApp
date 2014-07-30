'use strict';

define(function (require) {

    require('route');

    angular.module('Demo', ['ngRoute'])
        .config(['$routeProvider', '$httpProvider', function ($routeProvider, $httpProvider) {

            $routeProvider.
                when('/auth/', {
                    templateUrl: 'app/auth/partials/index.htm'
                }).
                otherwise({
                    redirectTo: '/auth/'
                });

        } ]);

    return {
        init: function () {
            angular.bootstrap(document, ['Demo']);
        }
    }

});