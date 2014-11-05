(function(angular) {
    'use strict';

    var welcome = angular.module('welcome', []);

    welcome.controller('WelcomeController', ['$scope',
        function($scope) {
            $scope.greeting = 'Welcome to angular js app.';
        }
    ]);

}(window.angular));