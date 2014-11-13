define(['app'], function (app) {
    'use strict';

    app.registerController('ModalController', ModalController);

    ModalController.$inject = ['$scope', '$modalInstance', 'items'];

    function ModalController($scope, $modalInstance, items) {
        $scope.items = items;
        $scope.selected = {
            item: $scope.items[0]
        };

        $scope.ok = function () {
            $modalInstance.close($scope.selected.item);
        };

        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };
    }
});