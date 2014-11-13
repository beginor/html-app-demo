define(['app'], function(app) {
    'use strict';

    app.registerController('UserController', UserController);

    UserController.$inject = ['$scope'];
    
    function UserController($scope) {
        $scope.greeting = 'User Info';
    }

});