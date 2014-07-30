'use strict';

define(function (require) {

    require('route');
    require('cookies');
    require('../../../static/js/utils');
    require('../../../static/js/configs');
    require('./biz-notification-controllers');
    require('./category-controllers');

    angular.module('CommonApp', ['ngRoute', 'ngCookies', 'utils', 'configs', 'bizNotification.controllers', 'category.controllers'])
        .config(['$routeProvider', '$httpProvider', function ($routeProvider, $httpProvider) {

            $routeProvider.
                when('/overview/', {
                    templateUrl: 'partials/overview.htm'
                }).
                when('/notification/', {
                    templateUrl: 'partials/notification.htm',
                    controller: 'BizNotificationCtrl'
                }).
                when('/category/', {
                    templateUrl: 'partials/category.htm',
                    controller: 'CategoryCtrl'
                }).
                otherwise({
                    redirectTo: '/overview/'
                });

            $httpProvider.defaults.headers.common["Authorization"] = 'fangxing/123';

        } ])
        .controller('MainCtrl', ['$scope', '$cookieStore', 'currentUser', 'urlUtil',
            function ($scope, $cookieStore, currentUser, urlUtil) {

                currentUser.username = urlUtil.getUrlParam('username');
//                console.log($cookieStore.get('username'));
//                currentUser.username = $cookieStore.get('username');
//                console.log("bingo " + currentUser.username);

            } ]);

    return {
        init: function () {
            angular.bootstrap(document, ['CommonApp']);
        }
    }

});