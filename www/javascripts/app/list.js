define('app/list', ['angular'],
    function(angular) {
        'use strict';

        var list = angular.module('list', ['ngResource', 'ngRoute']);

        list.factory('Category', ['$resource',
            function($resource) {
                return $resource('/api/categories/:id', null, {
                });
            }
        ]);

        list.controller('ListController', ['$scope', 'Category',
            //
            function ($scope, Category) {
                $scope.greeting = 'Category list';

                Category.query(function (data) {
                    $scope.data = data;
                });

                $scope.edit = function (id) {
                    console.log(id);
                };
                //
                $scope.$on('$destroy', function(evt) {
                    console.log('list controller destroy.');
                });
            }
        ]);
    }
);