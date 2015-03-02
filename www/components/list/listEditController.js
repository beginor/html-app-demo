define(['app', 'components/list/listFactory'], function(app) {
    'use strict';

    app.registerController('ListEditController', ListEditController);

    ListEditController.$inject = ['$scope', '$modalInstance', 'categories', 'id'];

    function ListEditController($scope, $modalInstance, cat, id) {
        $scope.title = '';
        $scope.cat = {};

        $scope.ok = ok;
        $scope.cancel = cancel;


        init();

        function init() {
            if (id !== 0) {
                cat.get({ id: id }, function(data) {
                    $scope.cat = data;
                });
            }

            $scope.title = id === 0 ? 'Create new category' : 'Edit category';
        }


        function ok() {
            if (id === 0) {
                cat.save($scope.cat, success);
            } else {
                cat.update({ id: id }, $scope.cat, success);
            }
        }

        function success(data) {
            $modalInstance.close('ok');
        }

        function cancel() {
            $modalInstance.dismiss('cancel');
        }
    }
});