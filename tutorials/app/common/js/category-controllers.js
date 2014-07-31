'use strict';

define(function (require) {

    require('ng');
    require('../../../static/js/configs');
    require('./category-services');

    angular.module('category.controllers', ['configs', 'category.services'])
        .controller('CategoryOverviewCtrl', ['$scope', '$location', '$log', 'CategoryConfigService',
            function ($scope, $location, $log, CategoryConfigService) {

                $scope.init = function () {
                    CategoryConfigService.query()
                        .$promise
                            .then(function (result) {
                                $scope.scopeCatalog = result;
                            }, function (error) {
                                $log.error(error);
                            });
                }

            } ])
        .controller('CategoryListCtrl', ['$scope', '$location', '$log', '$routeParams', 'currentUser', 'CategoryListService',
            function ($scope, $location, $log, $routeParams, currentUser, CategoryListService) {

                $scope.init = function () {

                    console.log($routeParams.scope);

                }

            } ]);

});