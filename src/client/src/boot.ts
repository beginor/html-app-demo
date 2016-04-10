///<reference path="../typings/browser.d.ts"/>

require.config({
    paths: {
        'angular': '../bower_components/angular/angular',
        'angular-animate': '../bower_components/angular-animate/angular-animate',
        'angular-resource': '../bower_components/angular-resource/angular-resource',
        'angular-sanitize': '../bower_components/angular-sanitize/angular-sanitize',
        'angular-bootstrap': '../bower_components/angular-bootstrap/ui-bootstrap-tpls',
        'angular-ui-router': '../bower_components/angular-ui-router/release/angular-ui-router'
    },
    shim: {
        'angular': { exports: 'angular' },
        'angular-animate': { deps: ['angular'] },
        'angular-resource': { deps: ['angular'] },
        'angular-sanitize': { deps: ['angular'] },
        'angular-bootstrap': { deps: ['angular'] },
        'angular-ui-router': { deps: ['angular']}
    }
});

require(['angular', 'app'], () => {
    angular.bootstrap(document, ['app']);
});
