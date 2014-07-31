'use strict';

define(function (require) {

    angular.module('auth.models', [])
        .factory("currentUser", function () {

            var username = '';
            var name = '';
            var login = false;

            return {
                sign_in: function (e) {
                    username = e;
                    login = true;
                },
                sign_out: function () {
                    username = '';
                    name = '';
                    login = false;
                },
                getUsername: function () {
                    return username;
                },
                getName: function () {
                    return name;
                },
                setName: function (e) {
                    name = e;
                },
                isLogin: function () {
                    return login;
                }
            }

        });

});