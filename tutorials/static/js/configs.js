'use strict';

define(function (require) {

    angular.module('configs', [])
        .constant("appName", "演示教程")
        .constant("wcfApp", "http://localhost:2539/tutorials")
        .constant("fileApp", "http://localhost/fileServer");

});