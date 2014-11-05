define('app/help', ['angular'],
    function(angular) {
        'use strict';

        var help = angular.module('help', []);

        help.controller('HelpController', ['$scope',
            function($scope) {
                $scope.greeting = 'Help Info';
            }
        ]);
    }
);