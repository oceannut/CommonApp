'use strict';

define(function (require) {

    require('ng-route');
    require('ng-local-storage');

    require('../../app/auth/js/auth-controllers');
    require('../../app/common/js/client-controllers');
    require('../../app/common/js/index-controllers');
    require('../../app/common/js/home-controllers');
    require('../../app/common/js/category-controllers');
    require('../../app/common/js/user-controllers');
    require('../../app/common/js/notice-controllers');
    require('../../app/common/js/idiom-controllers');
    require('../../app/common/js/schedule-controllers');
    require('../../app/common/js/file-transfer-controllers');

    angular.module('Tutorials', ['ngRoute',
            'LocalStorageModule',
            'auth.controllers',
            'client.controllers',
            'index.controllers',
            'home.controllers',
            'category.controllers',
            'user.controllers',
            'notice.controllers',
            'idiom.controllers',
            'schedule.controllers',
            'fileTransfer.controllers'
        ])
        .config(['$routeProvider', '$httpProvider', 'localStorageServiceProvider',
            function ($routeProvider, $httpProvider, localStorageServiceProvider) {

                $routeProvider
                    .when('/sign-in/', {
                        templateUrl: 'app/auth/partials/sign-in.htm',
                        controller: 'SignInCtrl'
                    })
                    .when('/sign-up/', {
                        templateUrl: 'app/auth/partials/sign-up.htm',
                        controller: 'SignUpCtrl'
                    })
                    .when('/sign-out/', {
                        templateUrl: 'app/auth/partials/sign-out.htm',
                        controller: 'SignOutCtrl'
                    })
                    .when('/password-modify/:username/', {
                        templateUrl: 'app/auth/partials/password-modify.htm',
                        controller: 'PasswordModifyCtrl',
                        access: {
                            loginRequired: true
                        }
                    })
                    .when('/not-authorised/', {
                        templateUrl: 'app/auth/partials/not-authorised.htm'
                    })
                    .when('/not-authenticated/', {
                        templateUrl: 'app/auth/partials/not-authenticated.htm',
                        controller: 'NotAuthenticatedCtrl'
                    })
                    .when('/session-out/', {
                        templateUrl: 'app/auth/partials/session-out.htm',
                        controller: 'SessionOutCtrl'
                    })
                    .when('/home/', {
                        templateUrl: 'app/common/partials/home.htm',
                        controller: 'HomeCtrl',
                        access: {
                            loginRequired: true
                        }
                    })
                    .when('/category-overview/', {
                        templateUrl: 'app/common/partials/category-overview.htm',
                        controller: 'CategoryOverviewCtrl',
                        access: {
                            loginRequired: true,
                            roles: ['admin']
                        }
                    })
                    .when('/category-list/:scope/', {
                        templateUrl: 'app/common/partials/category-list.htm',
                        controller: 'CategoryListCtrl',
                        access: {
                            loginRequired: true,
                            roles: ['admin']
                        }
                    })
                    .when('/category-edit/:scope/:id/', {
                        templateUrl: 'app/common/partials/category-edit.htm',
                        controller: 'CategoryEditCtrl',
                        access: {
                            loginRequired: true,
                            roles: ['admin']
                        }
                    })
                    .when('/user-role-overview/:which/', {
                        templateUrl: 'app/common/partials/user-role-overview.htm',
                        controller: 'UserRoleOverviewCtrl',
                        access: {
                            loginRequired: true,
                            roles: ['admin']
                        }
                    })
                    .when('/user-role-assign/:username/', {
                        templateUrl: 'app/common/partials/user-role-assign.htm',
                        controller: 'UserRoleAssignCtrl',
                        access: {
                            loginRequired: true,
                            roles: ['admin']
                        }
                    })
                    .when('/role-user-assign/:role/', {
                        templateUrl: 'app/common/partials/role-user-assign.htm',
                        controller: 'RoleUserAssignCtrl',
                        access: {
                            loginRequired: true,
                            roles: ['admin']
                        }
                    })
                    .when('/user-setting/:username/', {
                        templateUrl: 'app/common/partials/user-setting.htm',
                        controller: 'UserSettingCtrl',
                        access: {
                            loginRequired: true
                        }
                    })
                    .when('/client-setting/:username/', {
                        templateUrl: 'app/common/partials/client-setting.htm',
                        controller: 'ClientSettingCtrl',
                        access: {
                            loginRequired: true
                        }
                    })
                    .when('/notice-list/', {
                        templateUrl: 'app/common/partials/notice-list.htm',
                        controller: 'NoticeListCtrl',
                        access: {
                            loginRequired: true,
                            roles: ['admin']
                        }
                    })
                    .when('/notice-edit/:id/', {
                        templateUrl: 'app/common/partials/notice-edit.htm',
                        controller: 'NoticeEditCtrl',
                        access: {
                            loginRequired: true,
                            roles: ['admin']
                        }
                    })
                    .when('/notice-details/:id/', {
                        templateUrl: 'app/common/partials/notice-details.htm',
                        controller: 'NoticeDetailsCtrl',
                        access: {
                            loginRequired: true
                        }
                    })
                    .when('/idiom-overview/', {
                        templateUrl: 'app/common/partials/idiom-overview.htm',
                        controller: 'IdiomOverviewCtrl',
                        access: {
                            loginRequired: true,
                            roles: ['admin']
                        }
                    })
                    .when('/idiom-list/:scope/', {
                        templateUrl: 'app/common/partials/idiom-list.htm',
                        controller: 'IdiomListCtrl',
                        access: {
                            loginRequired: true,
                            roles: ['admin']
                        }
                    })
                    .when('/idiom-edit/:scope/:id/', {
                        templateUrl: 'app/common/partials/idiom-edit.htm',
                        controller: 'IdiomEditCtrl',
                        access: {
                            loginRequired: true,
                            roles: ['admin']
                        }
                    })
                    .when('/schedule-list/', {
                        templateUrl: 'app/common/partials/schedule-list.htm',
                        controller: 'ScheduleListCtrl',
                        access: {
                            loginRequired: true,
                            roles: ['admin']
                        }
                    })
                    .when('/file-transfer-overview/', {
                        templateUrl: 'app/common/partials/file-transfer-overview.htm',
                        controller: 'FileTransferOverviewCtrl',
                        access: {
                            loginRequired: true,
                            roles: ['user', 'admin']
                        }
                    })
                    .otherwise({
                        redirectTo: '/sign-in/'
                    });

                $httpProvider.interceptors.push(['$q', '$location', function ($q, $location) {
                    return {
                        'responseError': function (rejection) {
                            console.log(rejection);
                            if (405 === rejection.status) {
                                $location.path("/not-authenticated/").replace();
                            } else if (401 === rejection.status) {
                                $location.path("/session-out/");
                            }
                            return $q.reject(rejection);
                        }
                    };
                } ]);

                localStorageServiceProvider.setPrefix('cully');
                localStorageServiceProvider.setStorageCookieDomain('thinkinbio.com');

            } ])
        .run(['$http', '$rootScope', '$location', 'authorizationType', 'authorization', 'currentUser',
            function ($http, $rootScope, $location, authorizationType, authorization, currentUser) {

                var routeChangeRequiredAfterLogin = false, loginRedirectUrl;
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
                        var userToken = currentUser.getUsername();
                        if (userToken != undefined) {
                            $http.defaults.headers.common.Authorization = 'Basic ' + userToken + ' ' + currentUser.getSignature();
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