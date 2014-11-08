define('app/user', ['app'], function(app) {
    'use strict';

    app.controller('UserController', ['$scope',
        function($scope) {
            $scope.greeting = 'User Info';
        }
    ]);

});