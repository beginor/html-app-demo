define([], function() {
    'use strict';

    angular.module('app').registerController('UserController', UserController);

    UserController.$inject = ['$scope'];
    
    function UserController($scope) {
        $scope.greeting = 'User Info';
    }

});