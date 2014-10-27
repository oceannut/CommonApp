
angular.module("services", ['ngResource', 'configs'])
    .factory('DemoService', ['$resource', 'wcfApp',
        function ($resource, wcfApp) {
            return $resource(wcfApp + '/DemoWcfService.svc/demo/echo/:name/', {}, {
                save: { method: 'POST', params: { 'name': '@name'} },
                get: { method: 'GET', params: { 'name': '@name'} }
            });
        } ])
    .factory('DemoService2', ['$http', 'wcfApp',
        function ($http, wcfApp) {
            return {
                save: function (name) {
                    return $http({ method: 'POST', url: wcfApp + '/DemoWcfService.svc/demo/echo/' + name + '/' });
                },
                get: function (name) {
                    return $http({ method: 'GET', url: wcfApp + '/DemoWcfService.svc/demo/echo/' + name + '/' });
                }
            }
        } ])
    .factory('DemoService3', ['$resource', 'wcfApp',
        function ($resource, wcfApp) {
            return $resource(wcfApp + '/DemoWcfService.svc/demo/echo/:name/', { name: '@name' });
        } ])
    .factory('PostService', ['$resource', 'wcfApp',
        function ($resource, wcfApp) {
            return $resource(wcfApp + '/DemoWcfService.svc/demo/test/:name/', {}, {
                save: { method: 'POST', params: { 'name': '@name', 'what': '@what'} }
            });
        } ])
    .factory('PostService2', ['$http', 'wcfApp',
        function ($http, wcfApp) {
            return {
                save: function (name, what) {
                    return $http({
                        method: 'POST',
                        url: wcfApp + '/DemoWcfService.svc/demo/test/' + name + '/',
                        data: { 'what': what }
                    });
                }
            }
        } ])
    .factory('PostService3', ['$http', 'wcfApp',
        function ($http, wcfApp) {
            return {
                save: function (formData) {
                    return $http({
                        method: 'POST',
                        url: wcfApp + '/DemoWcfService.svc/demo/test/' + formData.name + '/',
                        data: formData
                    });
                }
            }
        } ])
    .factory('PostService4', ['$resource', 'wcfApp',
        function ($resource, wcfApp) {
            return $resource(wcfApp + '/DemoWcfService.svc/demo/test/:name/', { name: '@name' }, {
                save: { method: 'POST', params: { what: '@what' } }
            });
        } ])
    .factory('TimeStampService', ['$http', 'wcfApp',
        function ($http, wcfApp) {
            return {
                get: function () {
                    return $http({ method: 'GET', url: wcfApp + '/TimeStampWcfService.svc/timestamp/next/' });
                }
            }
        } ])
    .factory('LocalTimeStampService', ['$http', 'wcfApp',
        function ($http, wcfApp) {
            return {
                get: function () {
                    return $http({ method: 'GET', url: wcfApp + '/TimeStampWcfService.svc/timestamp/local/next/' });
                }
            }
        } ])
    .factory('SignInService', ['$resource', 'wcfApp',
        function ($resource, wcfApp) {
            return $resource(wcfApp + '/SignWcfService.svc/signin/:username/', {}, {
                save: { method: 'POST', params: { 'username': '@username', 'pwd': '@pwd'} }
            });
        } ])
    .factory('SignInService2', ['$http', 'wcfApp',
        function ($http, wcfApp) {
            return {
                save: function (formData) {
                    return $http({
                        method: 'POST',
                        url: wcfApp + '/SignWcfService.svc/signin/' + formData.username + '/',
                        data: formData
                    });
                }
            }
        } ]);