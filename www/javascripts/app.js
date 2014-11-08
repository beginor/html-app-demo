define('app', ['routes', 'loader', 'jquery', 'angular', 'angular-route', 'angular-resource', 'angular-ui-bootstrap'], function (config, loader) {
    'use strict';

    var app = angular.module('app', ['ngRoute', 'ngResource', 'ui.bootstrap']);

    app.config([
        '$routeProvider',
        '$locationProvider',
        '$controllerProvider',
        '$compileProvider',
        '$filterProvider',
        '$provide',
        function ($routeProvider, $locationProvider, $controllerProvider, $compileProvider, $filterProvider, $provide) {
            app.controller = $controllerProvider.register;
            app.directive = $compileProvider.directive;
            app.filter = $filterProvider.register;
            app.factory = $provide.factory;
            app.service = $provide.service;

            //$locationProvider.html5Mode(true);

            if (config.routes != undefined) {
                angular.forEach(config.routes, function(route, path) {
                    $routeProvider.when(path, {
                        templateUrl: route.templateUrl,
                        resolve: loader(route.dependencies)
                    });
                });
            }

            if (config.defaultRoutePath != undefined) {
                $routeProvider.otherwise({ redirectTo: config.defaultRoutePath });
            }
        }
    ]);

    return app;
});