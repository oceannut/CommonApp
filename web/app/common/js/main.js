﻿'use strict';

define(function (require) {

    require('route');
    require('../../../static/js/utils');
    require('../../../static/js/configs');
    require('./biz-notification-controllers');
    require('./category-controllers');

    angular.module('CommonApp', ['ngRoute', 'utils', 'configs', 'bizNotification.controllers', 'category.controllers'])
        .config(['$routeProvider', function ($routeProvider) {

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

        } ])
        .controller('MainCtrl', ['$scope', 'currentUser', 'urlUtil',
            function ($scope, currentUser, urlUtil) {

                currentUser.username = urlUtil.getUrlParam('username');

            } ]);

    return {
        init: function () {
            angular.bootstrap(document, ['CommonApp']);
        }
    }

});