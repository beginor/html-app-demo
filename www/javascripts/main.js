'use strict';

requirejs.config({
    baseUrl: 'javascripts',
    paths: {
        'jquery': 'libs/jquery/2.1.1/jquery-2.1.1',
        'bootstrap': 'libs/bootstrap/3.2.0/js/bootstrap',
        'angular': 'libs/angularjs/1.3.0/angular',
        'angular-animate': 'libs/angularjs/1.3.0/angular-animate',
        'angular-route': 'libs/angularjs/1.3.0/angular-route',
        'angular-resource': 'libs/angularjs/1.3.0/angular-resource',
        'angular-ui-bootstrap': 'libs/angularjs-ui-bootstrap/0.11.2/ui-bootstrap-tpls'
    },
    shim: {
        'bootstrap': { deps: ['jquery'] },
        'angular': { deps: ['jquery'], exports: 'angular' },
        'angular-animate': { deps: ['jquery', 'angular'] },
        'angular-route': { deps: ['jquery', 'angular'] },
        'angular-resource': { deps: ['jquery', 'angular'] },
        'angular-ui-bootstrap': { deps: [ 'jquery', 'bootstrap', 'angular' ] }
    }
});

require(['app'],
    function(app) {
        angular.bootstrap(document, ['app']);
    }
);