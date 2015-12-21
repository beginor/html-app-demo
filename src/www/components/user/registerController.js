define([], function () {
    'use strict';

    angular.module('app').registerController('RegisterController', RegisterController);

    RegisterController.$inject = ['$scope'];

    function RegisterController($scope) {
        $scope.greeting = 'User Info';
    }

});