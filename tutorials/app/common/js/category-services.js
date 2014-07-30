'use strict';

define(function (require) {

    require('ng');
    require('ng-resource');
    require('../../../static/js/configs');

    angular.module('category.services', ['ngResource', 'configs'])
        .constant("categoryWcfService", "/wcf/CategoryWcfService.svc")
        .factory('CategoryListService', ['$resource', 'wcfApp', 'categoryWcfService',
            function ($resource, wcfApp, categoryWcfService) {
                return $resource(wcfApp + categoryWcfService + '/category/:scope/', {}, {
                    query: { method: 'GET', params: { 'scope': '@scope' }, isArray: true }
                });
            } ]);

});