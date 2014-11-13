define('app/help', ['app'], function(app) {
    'use strict';

    app.controller('HelpController', HelpController);
    HelpController.$inject = ['$scope'];

    function HelpController($scope) {
        $scope.greeting = 'Help Info';
    }
});