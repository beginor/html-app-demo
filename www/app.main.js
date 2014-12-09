requirejs.config({
    paths: {
        'angular': 'resources/bower_components/angular/angular.min',
        'angular-animate': 'resources/bower_components/angular-animate/angular-animate.min',
        'angular-route': 'resources/bower_components/angular-route/angular-route.min',
        'angular-resource': 'resources/bower_components/angular-resource/angular-resource.min',
        'angular-bootstrap': 'resources/bower_components/angular-bootstrap/ui-bootstrap-tpls.min'
    },
    shim: {
        'angular': { exports: 'angular' },
        'angular-animate': { deps: ['angular'] },
        'angular-route': { deps: ['angular'] },
        'angular-resource': { deps: ['angular'] },
        'angular-bootstrap': { deps: [ 'angular' ] }
    }
});

require(['app'],
    function(app) {
        angular.bootstrap(document, ['app']);
    }
);