define(['app', 'components/list/listFactory', 'shared/confirm/confirmController'], function (app) {
    'use strict';

    app.registerController('ListController', ListController);
    ListController.$inject = ['$scope', '$modal', 'categories'];

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
                templateUrl: 'shared/confirm/confirmView.html',
                controller: 'ConfirmController',
                size: 'sm',
                resolve: {
                    title: function() { return '警告：'; },
                    message: function () { return '删除操作不可撤销， 确认删除么？'; }
                }
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

});