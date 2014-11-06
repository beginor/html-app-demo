define('app/dialogs', ['angular'], function(angular) {
    'use strict';

    var dialogs = angular.module('dialogs', ['ui.bootstrap']);

    dialogs.controller('DialogsController', ['$scope', '$modal', '$log',
        //
        function ($scope, $modal, $log) {
            $scope.greeting = 'Dialog Demo';
            $scope.items = ['item1', 'item2', 'item3'];

            $scope.showDialog = function() {
                var modalInstance = $modal.open({
                    templateUrl: 'myModalContent.html',
                    controller: 'ModalController',
                    //size: 'lg',
                    resolve: {
                        items: function() {
                            return $scope.items;
                        }
                    }
                });

                modalInstance.result.then(
                    function(selectedItem) {
                        $scope.selected = selectedItem;
                    }, function() {
                        $log.info('Modal dismissed at: ' + new Date());
                    }
                );
            };

            //
            $scope.$on('$destroy', function(evt, next, current) {
                console.log('dialog controller destroy.');
            });
        }
    ]);

    dialogs.controller('ModalController', ['$scope', '$modalInstance', 'items',
        function($scope, $modalInstance, items) {
            $scope.items = items;
            $scope.selected = {
                item: $scope.items[0]
            };

            $scope.ok = function() {
                $modalInstance.close($scope.selected.item);
            };

            $scope.cancel = function() {
                $modalInstance.dismiss('cancel');
            };
        }
    ]);

});