define([], function () {
    'use strict';

    angular.module('app').registerController('LoginController', LoginController);

    LoginController.$inject = ['$scope'];

    function LoginController($scope) {
        $scope.greeting = 'User Info';
    }

});