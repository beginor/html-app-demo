define(['app'], function(app) {
    'use strict';

    app.controller('WelcomeController', WelcomeController);

    WelcomeController.$inject = ['$scope'];
    
    function WelcomeController($scope) {
        $scope.greeting = 'Welcome to angular js app.';
    }
});