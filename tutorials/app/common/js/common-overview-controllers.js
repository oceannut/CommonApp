'use strict';

define(function (require) {

    require('ng');
    require('../../../static/js/configs');

    angular.module('common.overview.controllers', ['configs'])
        .controller('OverviewCtrl', ['$scope', '$location', '$log', 'appName', 'currentUser',
            function ($scope, $location, $log, appName, currentUser) {

                $scope.init = function () {
                    $scope.appName = appName;
                    $scope.user = currentUser;
                }

            } ]);

});