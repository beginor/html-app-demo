define(['app'], function (app) {
    'use strict';

    app.registerFactory('categories', categories);
    categories.$inject = ['$resource'];

    function categories($resource) {
        return $resource('api/categories/:id', null, {
            update: {
                method: 'PUT'
            }
        });
    }

});