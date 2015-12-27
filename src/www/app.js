define(['app.routes', 'app.loader', 'angular', 'angular-route', 'angular-resource', 'angular-bootstrap'], function (config, loader) {
    'use strict';

    var app = angular.module('app', ['ngRoute', 'ngResource', 'ui.bootstrap']);
    app.config(configure);

    configure.$inject = ['$routeProvider', '$locationProvider', '$controllerProvider', '$compileProvider', '$filterProvider', '$provide'];

    app.run(startup);
    startup.$inject = ['$rootScope', '$location'];

    return app;

    function startup($rootScope, $location) {
        $rootScope.$on('$routeChangeStart', function (evt, next, current) {
            if (angular.isUndefined(next.$$route)) {
                return;
            }
            if (!next.$$route.allowAnonymous) {
                evt.preventDefault();
                $location.path('/login');
            }
        });
    }

    function configure($routeProvider, $locationProvider, $controllerProvider, $compileProvider, $filterProvider, $provide) {
        app.registerController = $controllerProvider.register;
        app.registerDirective = $compileProvider.directive;
        app.registerFilter = $filterProvider.register;
        app.registerFactory = $provide.factory;
        app.registerService = $provide.service;

        $locationProvider.html5Mode(true);
        //$locationProvider.hashPrefix("!");

        if (config.routes != undefined) {
            angular.forEach(config.routes, function(route, path) {
                $routeProvider.when(path, {
                    templateUrl: route.templateUrl,
                    controller: route.controller,
                    resolve: loader(route.dependencies),
                    allowAnonymous: route.allowAnonymous
                });
            });
        }

        if (config.defaultRoute != undefined) {
            $routeProvider.otherwise({ redirectTo: config.defaultRoute });
        }
    }
});