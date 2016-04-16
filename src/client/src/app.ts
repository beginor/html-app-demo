///<reference path="../typings/browser.d.ts"/>
import 'angular';
import 'angular-animate';
import 'angular-resource';
import 'angular-sanitize';
import 'angular-bootstrap';
import 'angular-ui-router';

import { appState } from './config/state';
import {IAsyncState, IAsyncModule} from "./models/app";

var app = <IAsyncModule> angular.module('app', ['ngResource', 'ngAnimate', 'ngSanitize', 'ui.bootstrap', 'ui.router']);

configAsyncModule.$inject = [
    '$controllerProvider',
    '$compileProvider',
    '$filterProvider',
    '$provide'
];
function configAsyncModule(
    $controllerProvider: angular.IControllerProvider,
    $compileProvider: angular.ICompileProvider,
    $filterProvider: angular.IFilterProvider,
    $provide: angular.auto.IProvideService
) {
    app.controllerProvider = $controllerProvider;
    app.compileProvider = $compileProvider;
    app.filterProvider = $filterProvider;
    app.provide = $provide;
}

app.config(configAsyncModule);

configState.$inject = ['$stateProvider', '$urlRouterProvider'];
function configState(
    $stateProvider: angular.ui.IStateProvider,
    $urlRouterProvider: angular.ui.IUrlRouterProvider
) {
    angular.forEach(appState.states, (state: IAsyncState, name: string) => {
        if (!state.resolve) {
            state.resolve = {};
        }
        if (state.dependencies) {
            state.resolve = {
                dependencies: ['$q', '$rootScope', ($q: angular.IQService, $rootScope: angular.IRootScopeService) => {
                    var deferred = $q.defer();
                    require(state.dependencies, () => {
                       $rootScope.$apply(() => {
                           deferred.resolve();
                       });
                    });
                    return deferred.promise;
                }]
            }
        }
        $stateProvider.state(name, state);
    });
    $urlRouterProvider.otherwise(appState.defaultUrl);
}

app.config(configState);
