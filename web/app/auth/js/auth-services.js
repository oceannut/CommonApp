﻿'use strict';

define(function (require) {

    require('angular');
    require('resource');
    require('../../../static/js/configs');

    angular.module('auth.services', ['ngResource', 'configs'])
        .constant("signWcfService", "/wcf/SignWcfService.svc")
        .factory('SignUpService', ['$resource', 'wcfApp', 'signWcfService',
            function ($resource, wcfApp, signWcfService) {
                return $resource(wcfApp + signWcfService + 'signup/:username/', {}, {
                    save: { method: 'POST', params: { 'username': '@username', 'pwd': '@pwd', 'name': '@name' }, isArray: false }
                });
            } ]);

});