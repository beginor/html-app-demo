define([], function() {
    'use strict';

    angular.module('app').registerController('HelpController', HelpController);
    HelpController.$inject = ['$scope'];

    function HelpController($scope) {
        $scope.greeting = 'Help Info';
    }
});