(function(angular) {
    'use strict';

    var list = angular.module('list', ['ngResource', 'ngRoute', 'ui.bootstrap']);

    list.factory('categories', ['$resource',
        function($resource) {
            return $resource('/api/categories/:id', null, {
            });
        }
    ]);

    list.controller('ListController', ['$scope', '$modal', 'categories',
        //
        function ($scope, $modal, categories) {
            $scope.greeting = 'Category list';

            $scope.loadData = function() {
                $scope.data = [];
                categories.query(function (data) {
                    $scope.data = data;
                });
            };

            $scope.loadData();

            $scope.edit = function (id) {
                console.log(id);
            };

            $scope.delete = function(id) {
                var modalInstance = $modal.open({
                    templateUrl: 'delete-confirm.html',
                    controller: 'DeleteConfirmController',
                    size: 'sm'
                });
                modalInstance.result.then(
                    function() {
                        categories.delete({ id: id }, function() {
                            $scope.loadData();
                        });
                    }
                );
            }
            //
            $scope.$on('$destroy', function(evt) {
                console.log('list controller destroy.');
            });
        }
    ]);

    list.controller('DeleteConfirmController', ['$scope', '$modalInstance',
        function($scope, $modalInstance) {
            $scope.ok = function() {
                $modalInstance.close('ok');
            };
            $scope.cancel = function() {
                $modalInstance.dismiss('cancel');
            }
        }
    ]);
}(window.angular));