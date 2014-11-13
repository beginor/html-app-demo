define('app/dialogs', ['app'], function(app) {
    'use strict';

    app.registerController('DialogsController', DialogsController);

    DialogsController.$inject = ['$scope', '$modal', '$log'];

    app.registerController('ModalController', ModalController);

    ModalController.$inject = ['$scope', '$modalInstance', 'items'];

    function DialogsController($scope, $modal, $log) {
        $scope.greeting = 'Dialog Demo';
        $scope.items = ['item1', 'item2', 'item3'];

        $scope.showDialog = showDialog;

        $scope.$on('$destroy', onDestroy);

        function showDialog() {
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
        }

        function onDestroy(evt, next, current) {
            console.log('dialog controller destroy.');
        }
    }

    function ModalController($scope, $modalInstance, items) {
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
});