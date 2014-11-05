define('app/user', ['angular'],
    function(angular) {
        'use strict';

        var user = angular.module('user', []);

        user.controller('UserController', ['$scope',
            function($scope) {
                $scope.greeting = 'User Info';
            }
        ]);

    }
);