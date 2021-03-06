﻿'use strict';

define(function (require) {

    angular.module('utils', [])
        .factory('urlUtil', [function () {

            return {
                //reference: http://www.jquery4u.com/snippets/url-parameters-jquery/
                getUrlParam: function (name) {
                    var results = new RegExp('[\\?&]' + name + '=([^&#]*)').exec(window.location.href);
                    if (results == null) {
                        return null;
                    }
                    else {
                        return results[1] || 0;
                    }
                }
            }

        } ])
        .factory('dateUtil', [function () {

            return {
                getDate: function (date) {
                    var now = new Date();
                    var day = now.getDay();
                    if (date == '0') {
                        //today
                    } else if (date == '-1') {
                        //yestoday
                        now.setDate(now.getDate() - 1);
                    } else if (date == '7') {
                        //this week
                        if (day == 0) {
                            now.setDate(now.getDate() - 6);
                        } else {
                            now.setDate(now.getDate() - day + 1);
                        }
                    } else if (date == '-7') {
                        //last week
                        if (day == 0) {
                            now.setDate(now.getDate() - 7 - 6);
                        } else {
                            now.setDate(now.getDate() - now.getDay() - 6);
                        }
                    }
                    return now.getFullYear() + '-' + (now.getMonth() + 1) + '-' + now.getDate();
                },
                getSpan: function (date) {
                    if (date == '0') {
                        //today
                        return '1';
                    } else if (date == '-1') {
                        //yestoday
                        return '1';
                    } else if (date == '7') {
                        //this week
                        var day = new Date().getDay();
                        if (day == 0) {
                            return '7';
                        } else {
                            return day.toString();
                        }
                    } else if (date == '-7') {
                        //last week
                        return '7';
                    }
                    return '0';
                }
            }

        } ]);

});