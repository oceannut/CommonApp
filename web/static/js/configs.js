'use strict';

define(function (require) {

    angular.module('configs', [])
        .constant("wcfApp", "http://localhost:2258/web")
        .value('currentUser', { username: '' });

});