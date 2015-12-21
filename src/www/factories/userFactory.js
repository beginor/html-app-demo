define([], function () {
    'use strict';

    angular.module('app').registerFactory('UserFactory', UserFactory);
    UserFactory.$inject = ['$http', '$q'];

    function UserFactory($http, $q) {
        var factory = {};
        // GET api/account
        factory.getUser = function () {

        };
        // POST api/account
        factory.register = function () {

        };
        // POST api/account/login
        factory.login = function () {

        };

        return factory;
    }
});