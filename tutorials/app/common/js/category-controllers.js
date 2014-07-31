'use strict';

define(function (require) {

    require('ng');
    require('../../../static/js/configs');
    require('./category-services');

    angular.module('category.controllers', ['configs', 'category.services'])
        .factory("scopes", function () {

            var list = [];

            return {
                init: function (scopeCatalog) {
                    if (scopeCatalog != undefined && scopeCatalog != null) {
                        for (var i = 0; i < scopeCatalog.length; i++) {
                            var scopes = scopeCatalog[i].scopes;
                            for (var j = 0; j < scopes.length; j++) {
                                list.push(scopes[j]);
                            }
                        }
                    }
                },
                get: function (key) {
                    if (key != null && key != "") {
                        for (var i in list) {
                            var item = list[i];
                            if (key == item.code) {
                                return item;
                            }
                        }
                    }
                    return null;
                }
            }

        })
        .controller('CategoryOverviewCtrl', ['$scope', '$location', '$log', 'CategoryConfigService', 'scopes',
            function ($scope, $location, $log, CategoryConfigService, scopes) {

                $scope.init = function () {
                    CategoryConfigService.query()
                        .$promise
                            .then(function (result) {
                                $scope.scopeCatalog = result;
                                scopes.init($scope.scopeCatalog);
                            }, function (error) {
                                $log.error(error);
                            });
                }

            } ])
        .controller('CategoryListCtrl', ['$scope', '$location', '$log', '$routeParams', 'currentUser', 'CategoryListService', 'scopes',
            function ($scope, $location, $log, $routeParams, currentUser, CategoryListService, scopes) {

                $scope.init = function () {

                    var scope = scopes.get($routeParams.scope);
                    $scope.scopeName = scope == null ? $routeParams.scope : scope.name;

                    CategoryListService.query({ "scope": $routeParams.scope })
                        .$promise
                            .then(function (result) {
                                $scope.categoryList = result;
                            }, function (error) {
                                $log.error(error);
                            });

                }

            } ]);

});