define([], function() {
    'use strict';

    angular.module('app').registerController('WelcomeController', WelcomeController);

    WelcomeController.$inject = ['$scope'];
    function WelcomeController($scope) {
        $scope.greeting = 'Welcome to angular js app.';
    }
});