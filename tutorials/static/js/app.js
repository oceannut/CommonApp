'use strict';

define(function (require) {

    require('ng-route');

    require('../../app/common/js/index-controllers');
    require('../../app/auth/js/auth-controllers');
    require('../../app/common/js/category-controllers');
    require('../../app/common/js/user-controllers');

    angular.module('Tutorials', ['ngRoute',
            'index.controllers',
            'auth.controllers',
            'category.controllers',
            'user.controllers'
        ])
        .config(['$routeProvider', '$httpProvider', 
            function ($routeProvider, $httpProvider) {

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
                when('/home/', {
                    templateUrl: 'app/common/partials/home.htm',
                    access: {
                        loginRequired: true,
                        roles: ['user', 'admin']
                    }
                }).
                when('/category-overview/', {
                    templateUrl: 'app/common/partials/category-overview.htm',
                    controller: 'CategoryOverviewCtrl',
                    access: {
                        loginRequired: true,
                        roles: ['admin']
                    }
                }).
                when('/category-list/:scope/', {
                    templateUrl: 'app/common/partials/category-list.htm',
                    controller: 'CategoryListCtrl',
                    access: {
                        loginRequired: true,
                        roles: ['admin']
                    }
                }).
                when('/category-edit/:scope/:id/', {
                    templateUrl: 'app/common/partials/category-edit.htm',
                    controller: 'CategoryEditCtrl',
                    access: {
                        loginRequired: true,
                        roles: ['admin']
                    }
                }).
                when('/user-role-overview/:which/', {
                    templateUrl: 'app/common/partials/user-role-overview.htm',
                    controller: 'UserRoleOverviewCtrl',
                    access: {
                        loginRequired: true,
                        roles: ['admin']
                    }
                }).
                when('/user-role-assign/:username/', {
                    templateUrl: 'app/common/partials/user-role-assign.htm',
                    controller: 'UserRoleAssignCtrl',
                    access: {
                        loginRequired: true,
                        roles: ['admin']
                    }
                }).
                when('/role-user-assign/:role/', {
                    templateUrl: 'app/common/partials/role-user-assign.htm',
                    controller: 'RoleUserAssignCtrl',
                    access: {
                        loginRequired: true,
                        roles: ['admin']
                    }
                }).
                when('/user-setting/:username/', {
                    templateUrl: 'app/common/partials/user-setting.htm',
                    controller: 'UserSettingCtrl',
                    access: {
                        loginRequired: true
                    }
                }).
                otherwise({
                    redirectTo: '/sign-in/'
                });

        } ])
        .run(['$rootScope', '$location', 'authorizationType', 'authorization', 
            function ($rootScope, $location, authorizationType, authorization) {
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
        } ]);

    return {
        init: function () {
            angular.bootstrap(document, ['Tutorials']);
        }
    }

});