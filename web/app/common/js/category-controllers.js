'use strict';

define(function (require) {

    require('angular');
    require('../../../static/js/configs');
    require('../../../static/js/filters');
    require('./category-services');

    angular.module('category.controllers', ['configs', 'filters', 'category.services'])
        .controller('CategoryCtrl', ['$scope', '$location', 'currentUser', 'CategoryService',
            function ($scope, $location, currentUser, CategoryService) {

                $scope.query = function () {
                    CategoryService.query({ 'scope': $scope.scope })
                        .$promise
                            .then(function (result) {
                                $scope.categoryList = result;
                            }, function (error) {
                                console.log("error: " + error);
                            });
                }

            } ]);

});