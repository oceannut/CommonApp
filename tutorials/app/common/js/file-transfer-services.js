'use strict';

define(function (require) {

    require('ng');
    require('ng-resource');
    require('../../../static/js/configs');

    angular.module('fileTransfer.services', ['ngResource', 'configs'])
        .constant("fileTransferWcfService", "/wcf/FileTransferWcfService.svc")
        .factory('UploadService', ['$resource', 'wcfApp', 'fileTransferWcfService',
            function ($resource, wcfApp, fileTransferWcfService) {
                return $resource(wcfApp + fileTransferWcfService + '/upload/', {}, {
                    remove: { method: 'PUT', params: { 'uploadFile': '@uploadFile'} }
                });
            } ])
        .factory('UploadLogService', ['$resource', 'wcfApp', 'fileTransferWcfService',
            function ($resource, wcfApp, fileTransferWcfService) {
                return $resource(wcfApp + fileTransferWcfService + '/uploadLog/:user/:id/', {}, {
                    save: { method: 'POST', params: { 'user': '@user', 'id': '0', 'uploadFiles': '@uploadFiles'} }
                });
            } ]);

});