(function(angular) {
    'use strict';

    var help = angular.module('help', []);

    help.controller('HelpController', ['$scope',
        function($scope) {
            $scope.greeting = 'User Info';
        }
    ]);

}(window.angular));