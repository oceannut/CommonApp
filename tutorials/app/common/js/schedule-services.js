'use strict';

define(function (require) {

    require('ng');
    require('ng-resource');
    require('../../../static/js/configs');

    angular.module('schedule.services', ['ngResource', 'configs'])
        .constant("scheduleWcfService", "/wcf/ScheduleWcfService.svc")
        .factory('ScheduleService', ['$resource', 'wcfApp', 'scheduleWcfService',
            function ($resource, wcfApp, scheduleWcfService) {
                return $resource(wcfApp + scheduleWcfService + '/schedule/', {}, {
                    query: { method: 'GET', params: {}, isArray: true }
                });
            } ]);

});