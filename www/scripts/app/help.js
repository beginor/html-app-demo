define('app/help', ['app'], function(app) {
    'use strict';

    app.controller('HelpController', ['$scope',
        function($scope) {
            $scope.greeting = 'Help Info';
        }
    ]);
});