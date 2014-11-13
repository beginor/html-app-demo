define(['app'], function(app) {
    'use strict';

    app.registerController('HelpController', HelpController);
    HelpController.$inject = ['$scope'];

    function HelpController($scope) {
        $scope.greeting = 'Help Info';
    }
});