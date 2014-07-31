'use strict';

define(function (require) {

    require('ng');
    require('ng-resource');
    require('../../../static/js/configs');

    angular.module('category.services', ['ngResource', 'configs'])
        .constant("categoryWcfService", "/wcf/CategoryWcfService.svc")
        .factory('CategoryConfigService', ['$resource',
            function ($resource) {
                return $resource('config/categories.json/', {}, {
                    query: { method: 'GET', params: { }, isArray: true }
                });
            } ])
        .factory('CategoryListService', ['$resource', 'wcfApp', 'categoryWcfService',
            function ($resource, wcfApp, categoryWcfService) {
                return $resource(wcfApp + categoryWcfService + '/category/:scope/', {}, {
                    query: { method: 'GET', params: { 'scope': '@scope' }, isArray: true }
                });
            } ]);

});