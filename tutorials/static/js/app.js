'use strict';

define(function (require) {

    require('ng-route');

    require('./configs');
    require('../../app/auth/js/auth-controllers');
    require('../../app/common/js/category-controllers');

    angular.module('Tutorials', ['ngRoute',
        'configs',
        'auth.controllers',
        'category.controllers'])
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
                when('/sign-out/', {
                    templateUrl: 'app/auth/partials/sign-out.htm',
                    controller: 'SignOutCtrl'
                }).
                when('/category-overview/', {
                    templateUrl: 'app/common/partials/category-overview.htm',
                    controller: 'CategoryOverviewCtrl'
                }).
                when('/category-list/:scope/', {
                    templateUrl: 'app/common/partials/category-list.htm',
                    controller: 'CategoryListCtrl'
                }).
                when('/user-role-overview/', {
                    templateUrl: 'app/common/partials/user-role-overview.htm'
                }).
                otherwise({
                    redirectTo: '/sign-in/'
                });

        } ])
        .controller('IndexCtrl', ['$scope', '$location', 'currentUser', 'appName',
            function ($scope, $location, currentUser, appName) {

                var categoryNav = {
                    "name": "信息分类",
                    "url": "/category-overview/",
                    "active": ""
                };
                var userRoleNav = {
                    "name": "用户/角色",
                    "url": "/user-role-overview/",
                    "active": ""
                };

                $scope.init = function () {

                    $scope.appName = appName;
                    $scope.makeNavbarVisible();
                    $scope.navList = [categoryNav, userRoleNav];
                }

                $scope.changeUrl = function (nav) {
                    for (var i = 0; i < $scope.navList.length; i++) {
                        var item = $scope.navList[i];
                        if (item.name == nav.name) {
                            item.active = "active";
                            $location.path(item.url);
                        } else {
                            item.active = "";
                        }
                    }
                }

                $scope.makeNavbarVisible = function () {
                    if (currentUser.isLogin()) {
                        $scope.navbarVisible = '';
                    } else {
                        $scope.navbarVisible = 'none';
                    }
                }

            } ]);

    return {
        init: function () {
            angular.bootstrap(document, ['Tutorials']);
        }
    }

});