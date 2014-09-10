'use strict';

define(function (require) {

    require('ng');
    require('../../../static/js/configs');
    require('../../../static/js/events');
    require('../../auth/js/auth-models');

    angular.module('index.controllers', ['configs', 'events', 'auth.models'])
        .controller('IndexCtrl', ['$scope', '$location', 'currentUser', 'eventbus', 'appName',
            function ($scope, $location, currentUser, eventbus, appName) {

                var homeNav = {
                    "name": "首页",
                    "url": "/home/",
                    "active": "active"
                };
                var categoryNav = {
                    "name": "信息分类",
                    "url": "/category-overview/",
                    "active": ""
                };
                var userRoleNav = {
                    "name": "用户/角色",
                    "url": "/user-role-overview/user/",
                    "active": ""
                };
                var noticeNav = {
                    "name": "公告管理",
                    "url": "/notice-list/",
                    "active": ""
                };
                var idiomNav = {
                    "name": "常用短语",
                    "url": "/idiom-overview/",
                    "active": ""
                };
                var scheduleNav = {
                    "name": "工作调度",
                    "url": "/schedule-list/",
                    "active": ""
                };

                $scope.init = function () {

                    eventbus.subscribe("userSignIn", function (e, data) {
                        $scope.makeNavbarVisible();
                        $scope.loginUser = {};
                        $scope.loginUser.username = data;
                        $scope.loginUser.name = currentUser.getName();
                    });
                    eventbus.subscribe("userSignOut", function (e, data) {
                        $scope.makeNavbarVisible();
                        $scope.loginUser = undefined;
                    });
                    eventbus.subscribe("userModified", function (e, data) {
                        $scope.loginUser.name = currentUser.getName();
                    });

                    $scope.appName = appName;
                    $scope.makeNavbarVisible();
                    $scope.navList = [homeNav, categoryNav, userRoleNav, noticeNav, idiomNav, scheduleNav];

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


});