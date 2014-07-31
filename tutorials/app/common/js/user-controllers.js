'use strict';

define(function (require) {

    require('ng');
    require('bootstrap');
    require('../../../static/js/configs');
    require('./user-services');

    angular.module('user.controllers', ['configs', 'user.services'])
        .controller('UserRoleOverviewCtrl', ['$scope', '$location', '$log', 'UserListService',
            function ($scope, $location, $log, UserListService) {

                $scope.init = function () {
                    UserListService.query()
                        .$promise
                            .then(function (result) {
                                $scope.userList = result;
                            }, function (error) {
                                $log.error(error);
                            });
                }

            } ]);

});