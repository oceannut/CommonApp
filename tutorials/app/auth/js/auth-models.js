'use strict';

define(function (require) {

    angular.module('auth.models', [])
        .constant("authorizationType", { authorised: 0,
            loginRequired: 1,
            notAuthorised: 2
        })
        .factory("currentUser", function () {

            var username = '';
            var name = '';
            var roles;
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
                getRoles: function () {
                    return roles;
                },
                setRoles: function (e) {
                    roles = e;
                },
                isLogin: function () {
                    return login;
                }
            }

        })
        .factory("authorization", ['authorizationType', 'currentUser', function (authorizationType, currentUser) {

            return {
                authorize: function (loginRequired, requiredRoles) {

                    var result = authorizationType.authorised,
                    loweredRoles = [],
                    isInRole = true,
                    role, i;

                    if (loginRequired === true && !currentUser.isLogin()) {
                        result = authorizationType.loginRequired;
                    } else if ((loginRequired === true && currentUser.isLogin()) && (requiredRoles === undefined || requiredRoles.length === 0)) {
                        // Login is required but no specific permissions are specified.
                        result = authorizationType.authorised;
                    } else if (requiredRoles) {
                        loweredRoles = [];
                        angular.forEach(currentUser.getRoles(), function (role) {
                            loweredRoles.push(role.toLowerCase());
                        });

                        for (i = 0; i < requiredRoles.length; i += 1) {
                            role = requiredRoles[i].toLowerCase();

                            isInRole = loweredRoles.indexOf(role) > -1;
                            // if we only need one of the permissions and we have it there is no point carrying on
                            if (isInRole) {
                                break;
                            }
                        }

                        result = isInRole ? authorizationType.authorised : authorizationType.notAuthorised;
                    }

                    return result;
                }
            }

        } ]);

});