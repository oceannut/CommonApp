
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
        } ]);