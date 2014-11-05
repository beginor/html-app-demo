define('app/dialogs', ['angular'],
    function(angular) {
        'use strict';

        var dialogs = angular.module('dialogs', []);

        dialogs.controller('DialogsController', ['$scope',
            //
            function ($scope) {
                $scope.greeting = 'Hello';

                //
                $scope.$on('$destroy', function(evt, next, current) {
                    console.log('dialog controller destroy.');
                });
            }
        ]);
    }
);