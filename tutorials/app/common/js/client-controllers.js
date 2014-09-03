'use strict';

define(function (require) {

    require('ng');
    //require('jquery');
    //require('bootstrap');
    require('../../../static/js/utils');

    angular.module('client.controllers', ['filters', 'utils'])
        .controller('ClientSettingCtrl', ['$scope', '$location', '$log', '$anchorScroll', 'dateUtil',
            function ($scope, $location, $log, $anchorScroll, dateUtil) {

                $scope.init = function () {

                    $('#menu').affix();

                }

                $scope.gotoAnchor = function (anchor) {
                    $location.hash(anchor);
                    $anchorScroll();
                }

            } ]);

});