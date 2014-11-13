define(['app'], function (app) {
    'use strict';

    app.registerController('ConfirmController', ConfirmController);
    ConfirmController.$inject = ['$scope', '$modalInstance', 'title', 'message'];

    function ConfirmController($scope, $modalInstance, title, message) {
        $scope.title = title;
        $scope.message = message;

        $scope.ok = ok;
        $scope.cancel = cancel;

        function ok() {
            $modalInstance.close('ok');
        }

        function cancel() {
            $modalInstance.dismiss('cancel');
        }
    }
});