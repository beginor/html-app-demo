define('app/list', ['app'], function(app) {
    'use strict';

    app.factory('categories', categories);
    categories.$inject = ['$resource'];

    app.controller('ListController', ListController);
    ListController.$inject = ['$scope', '$modal', 'categories'];

    app.controller('DeleteConfirmController', DeleteConfirmController);
    DeleteConfirmController.$inject = ['$scope', '$modalInstance'];

    function categories($resource) {
        return $resource('/api/categories/:id', null, {});
    }

    function ListController($scope, $modal, cat) {
        $scope.greeting = 'Category list';
        $scope.data = [];

        $scope.loadData = loadData;
        $scope.editData = editData;
        $scope.deleteData = deleteData;

        $scope.$on('$destroy', onDestroy);

        loadData();

        function loadData() {
            $scope.data = [];
            cat.query(function (data) {
                $scope.data = data;
            });
        }

        function editData(id) {
            console.log(id);
        }

        function deleteData(id) {
            var modalInstance = $modal.open({
                templateUrl: 'delete-confirm.html',
                controller: 'DeleteConfirmController',
                size: 'sm'
            });
            modalInstance.result.then(
                function() {
                    cat.delete({ id: id }, function() {
                        loadData();
                    });
                }
            );
        }

        function onDestroy(evt) {
            console.log('list controller destroy.');
        }
    }

    function DeleteConfirmController($scope, $modalInstance) {
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