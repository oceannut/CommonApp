'use strict';

define(function (require) {

    require('ng');
    require('ng-resource');
    require('../../../static/js/configs');

    angular.module('user.services', ['ngResource', 'configs'])
        .constant("userWcfService", "/wcf/UserWcfService.svc")
        .factory('UserService', ['$resource', 'wcfApp', 'userWcfService',
            function ($resource, wcfApp, userWcfService) {
                return $resource(wcfApp + userWcfService + '/user/:username/', {}, {
                    update: { method: 'PUT', params: { 'username': '@username', 'name': '@name', 'group': '@group', 'roles': '@roles'} },
                    get: { method: 'GET', params: { 'username': '@username' } }
                });
            } ])
        .factory('UserListService', ['$resource', 'wcfApp', 'userWcfService',
            function ($resource, wcfApp, userWcfService) {
                return $resource(wcfApp + userWcfService + '/user/', {}, {
                    query: { method: 'GET', params: { }, isArray: true }
                });
            } ])
        .factory('RoleConfigService', ['$resource',
            function ($resource) {
                return $resource('config/roles.json/', {}, {
                    query: { method: 'GET', params: {}, isArray: true }
                });
            } ]);

});