define(['app'], function(app) {
    'use strict';

    app.controller('WelcomeController', ['$scope',
        function($scope) {
            $scope.greeting = 'Welcome to angular js app.';
        }
    ]);

});