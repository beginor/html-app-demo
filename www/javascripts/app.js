(function(angular) {
    'use strict';

    var app = angular.module('app', ['ngRoute', 'welcome', 'dialogs', 'list', 'user', 'help']);

    app.config([
        '$routeProvider',
        function ($rootProvider) {
            $rootProvider
                .when('/welcome', { templateUrl: 'views/welcome.html', controller: 'WelcomeController' })
                .when('/dialogs', { templateUrl: 'views/dialogs.html', controller: 'DialogsController' })
                .when('/list', { templateUrl: 'views/list.html', controller: 'ListController' })
                .when('/user', { templateUrl: 'views/user.html', controller: 'UserController' })
                .when('/help', { templateUrl: 'views/help.html', controller: 'HelpController' })
                .otherwise({ redirectTo: '/welcome' });
        }
    ]);
}(window.angular));