define(['components/list/listFactory', 'shared/confirm/confirmController', 'components/list/listEditController'], function () {
    'use strict';

    angular.module('app').registerController('ListController', ListController);

    ListController.$inject = ['$scope', '$modal', 'categories'];
    function ListController($scope, $modal, cat) {
        $scope.greeting = 'Category list';
        $scope.data = [];

        $scope.loadData = loadData;
        $scope.editData = editData;
        $scope.deleteData = deleteData;
        $scope.addNew = addNew;

        $scope.$on('$destroy', onDestroy);

        loadData();

        function addNew() {
            var modalInstance = $modal.open({
                templateUrl: 'components/list/listEditView.html',
                controller: 'ListEditController',
                size: 'sm',
                resolve: {
                    id: function () { return 0; }
                }
            });
            modalInstance.result.then(function () {
                loadData();
            });
        }

        function loadData() {
            $scope.data = [];
            cat.query(function (data) {
                $scope.data = data;
            });
        }

        function editData(id) {
            var modalInstance = $modal.open({
                templateUrl: 'components/list/listEditView.html',
                controller: 'ListEditController',
                size: 'sm',
                resolve: {
                    id: function () { return id; }
                }
            });
            modalInstance.result.then(function() {
                loadData();
            });
            //console.log(id);
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