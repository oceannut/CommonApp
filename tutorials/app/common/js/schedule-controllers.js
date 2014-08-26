'use strict';

define(function (require) {

    require('ng');
    require('../../../static/js/filters');
    require('../../../static/js/utils');
    require('./schedule-services');

    angular.module('schedule.controllers', ['filters', 'utils', 'schedule.services'])
        .controller('ScheduleListCtrl', ['$scope', '$location', '$log', 'currentUser', 'ScheduleService', 'dateUtil',
            function ($scope, $location, $log, currentUser, ScheduleService, dateUtil) {

                function load() {
                    $scope.alertMessageVisible = 'hidden';

                    ScheduleService.query()
                        .$promise
                            .then(function (result) {
                                $scope.schedulerList = result;
                            }, function (error) {
                                $scope.alertMessageVisible = 'show';
                                $scope.alertMessage = "提示：获取工作调度失败";
                                $log.error(error);
                            });
                }

                $scope.init = function () {
                    load();
                }

                $scope.refresh = function () {
                    load();
                }

            } ]);

});