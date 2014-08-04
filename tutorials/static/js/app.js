'use strict';

define(function (require) {

    require('ng-route');

    require('./configs');
    require('./events');
    require('../../app/auth/js/auth-controllers');
    require('../../app/common/js/category-controllers');
    require('../../app/common/js/user-controllers');

    angular.module('Tutorials', ['ngRoute',
            'configs',
            'events',
            'auth.controllers',
            'category.controllers',
            'user.controllers'
        ])
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
                when('/not-authorised/', {
                    templateUrl: 'app/auth/partials/not-authorised.htm'
                }).
                when('/category-overview/', {
                    templateUrl: 'app/common/partials/category-overview.htm',
                    controller: 'CategoryOverviewCtrl',
                    access: {
                        loginRequired: true
                    }
                }).
                when('/category-list/:scope/', {
                    templateUrl: 'app/common/partials/category-list.htm',
                    controller: 'CategoryListCtrl',
                    access: {
                        loginRequired: true
                    }
                }).
                when('/category-edit/:scope/:id/', {
                    templateUrl: 'app/common/partials/category-edit.htm',
                    controller: 'CategoryEditCtrl',
                    access: {
                        loginRequired: true
                    }
                }).
                when('/user-role-overview/', {
                    templateUrl: 'app/common/partials/user-role-overview.htm',
                    controller: 'UserRoleOverviewCtrl',
                    access: {
                        loginRequired: true
                    }
                }).
                otherwise({
                    redirectTo: '/sign-in/'
                });

        } ])
        .run(['$rootScope', '$location', 'authorizationType', 'authorization', function ($rootScope, $location, authorizationType, authorization) {
            var routeChangeRequiredAfterLogin = false,
            loginRedirectUrl;
            $rootScope.$on('$routeChangeStart', function (event, next) {
                var authorised;
                if (routeChangeRequiredAfterLogin && next.originalPath !== "/sign-in/") {
                    routeChangeRequiredAfterLogin = false;
                    $location.path(loginRedirectUrl).replace();
                } else if (next.access !== undefined) {
                    authorised = authorization.authorize(next.access.loginRequired,
                                                     next.access.roles);
                    if (authorised === authorizationType.loginRequired) {
                        routeChangeRequiredAfterLogin = true;
                        loginRedirectUrl = next.originalPath;
                        $location.path("/sign-in/");
                    } else if (authorised === authorizationType.notAuthorised) {
                        $location.path("/not-authorised/").replace();
                    }
                }
            });
        } ])
        .controller('IndexCtrl', ['$scope', '$location', 'currentUser', 'eventbus', 'userCache', 'appName',
            function ($scope, $location, currentUser, eventbus, userCache, appName) {

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

                    eventbus.subscribe("userSignIn", function (e, data) {
                        $scope.makeNavbarVisible();
                        $scope.loginUser = {};
                        $scope.loginUser.username = data;
                        userCache.getAsync(data, function (e) {
                            if (e != null) {
                                currentUser.setName(e.Name);
                                $scope.loginUser.name = e.Name;
                            } else {
                                $scope.loginUser.name = data;
                            }
                        });
                    });
                    eventbus.subscribe("userSignOut", function (e, data) {
                        $scope.makeNavbarVisible();
                        $scope.loginUser = undefined;
                    });
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