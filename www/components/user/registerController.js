define(['app'], function (app) {
    'use strict';

    app.registerController('RegisterController', RegisterController);

    RegisterController.$inject = ['$scope'];

    function RegisterController($scope) {
        $scope.greeting = 'User Info';
    }

});