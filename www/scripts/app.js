define('app', ['routes', 'loader', 'jquery', 'angular', 'angular-route', 'angular-resource', 'angular-ui-bootstrap'], function (config, loader) {
    'use strict';

    var app = angular.module('app', ['ngRoute', 'ngResource', 'ui.bootstrap']);
    app.config(configure);

    configure.$inject = ['$routeProvider', '$locationProvider', '$controllerProvider', '$compileProvider', '$filterProvider', '$provide'];

    return app;

    function configure($routeProvider, $locationProvider, $controllerProvider, $compileProvider, $filterProvider, $provide) {
        app.controller = $controllerProvider.register;
        app.directive = $compileProvider.directive;
        app.filter = $filterProvider.register;
        app.factory = $provide.factory;
        app.service = $provide.service;

        $locationProvider.html5Mode(false);
        //$locationProvider.hashPrefix("!");

        if (config.routes != undefined) {
            angular.forEach(config.routes, function(route, path) {
                $routeProvider.when(path, {
                    templateUrl: route.templateUrl,
                    controller: route.controller,
                    resolve: loader(route.dependencies)
                });
            });
        }

        if (config.defaultRoute != undefined) {
            $routeProvider.otherwise({ redirectTo: config.defaultRoute });
        }
    }
});