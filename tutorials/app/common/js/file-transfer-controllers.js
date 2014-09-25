'use strict';

define(function (require) {

    require('jquery');
    require('ng');
    require('underscore');

    require('../../../lib/jquery-file-upload/js/vendor/jquery.ui.widget');
    require('../../../lib/jquery-file-upload/js/jquery.iframe-transport');
    require('../../../lib/jquery-file-upload/js/jquery.fileupload');
    require('./file-transfer-services');

    require('../../../lib/jquery-file-upload/css/style.css');
    require('../../../lib/jquery-file-upload/css/jquery.fileupload.css');

    angular.module('fileTransfer.controllers', ['fileTransfer.services'])
        .controller('FileTransferOverviewCtrl', ['$scope', '$log', '$location', '$http', 'currentUser', 'UploadService', 'UploadLogService',
            function ($scope, $log, $location, $http, currentUser, UploadService, UploadLogService) {

                $scope.init = function () {

                    $scope.events = {
                        isBusy: false,
                        fileList: []
                    };

                    $('#fileupload').fileupload({
                        url: 'upload',
                        dataType: 'json',
                        start: function (e) {
                            $scope.$apply(function () {
                                $scope.events.isBusy = true;
                            });
                        },
                        stop: function (e) {
                            $scope.$apply(function () {
                                $scope.events.isBusy = false;
                            });
                        },
                        done: function (e, data) {
                            if (angular.isArray(data.result)) {
                                $scope.$apply(function () {
                                    _.each(data.result, function (item) {
                                        item.fileSizeUnit = "KB"
                                        item.fileSize = Math.ceil(item.Size / 1024);
                                        if (item.fileSize > 1024) {
                                            item.fileSizeUnit = "MB"
                                            item.fileSize = Math.round(item.fileSize / 1024 * 100) / 100;
                                        }
                                        $scope.events.fileList.push(item);
                                    });
                                });
                            }
                        },
                        fail: function (e, data) {
                            console.log(data.errorThrown);
                        },
                        progressall: function (e, data) {
                            var progress = parseInt(data.loaded / data.total * 100, 10);
                            $('#progress .progress-bar').css(
                                                    'width',
                                                    progress + '%'
                                                );
                        }
                    }).prop('disabled', !$.support.fileInput)
                                        .parent().addClass($.support.fileInput ? undefined : 'disabled');

                }

                //                $scope.upload = function () {
                //                    console.log(document.getElementById("fileupload2"));
                //                    var fileData = document.getElementById("fileupload2").files[0];
                //                    $.ajax({
                //                        url: 'http://localhost:2539/tutorials/wcf/FileTransferWcfService.svc/upload/',
                //                        type: 'POST',
                //                        data: fileData,
                //                        cache: false,
                //                        dataType: 'json',
                //                        processData: false, // Don't process the files
                //                        contentType: "application/octet-stream", // Set content type to false as jQuery will tell the server its a query string request
                //                        success: function (data) {
                //                            console.log('successful..');
                //                        },
                //                        error: function (data) {
                //                            console.log("error");
                //                            console.log(data);
                //                        }
                //                    });
                //                }

                $scope.save = function () {
                    UploadLogService.save({
                        "user": currentUser.getUsername(),
                        "uploadFiles": $scope.events.fileList
                    })
                    .$promise
                        .then(function (result) {
                            console.log("success");
                        }, function (error) {

                            $log.error(error);
                        });
                }

                $scope.remove = function (file) {
                    UploadService.remove({
                        "uploadFile": file
                    })
                    .$promise
                        .then(function (result) {
                            for (var i in $scope.events.fileList) {
                                if (file.Path === $scope.events.fileList[i].Path) {
                                    $scope.events.fileList.splice(i, 1);
                                    break;
                                }
                            }
                        }, function (error) {
                            $log.error(error);
                        });
                }

            } ]);


});