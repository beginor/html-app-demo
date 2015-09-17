define(['app'], function (app) {
    'use strict';

    app.registerController('LoginController', LoginController);

    LoginController.$inject = ['$scope'];

    function LoginController($scope) {
        $scope.greeting = 'User Info';
    }

});