///<reference path="../typings/browser.d.ts"/>
import 'angular';
import 'angular-animate';
import 'angular-resource';
import 'angular-sanitize';
import 'angular-bootstrap';
import 'angular-ui-router';

import { appState } from './config/state';
import {IAsyncState} from "./models/state";

export interface IAsyncModule extends angular.IModule {

    registerService(name: string, constructor: Function): angular.IServiceProvider;
    registerFactory(name: string, serviceFactoryFunction: Function): angular.IServiceProvider;
    registerFilter(name: string | {}): angular.IServiceProvider;
    registerDirective(name: string, directiveFactory: Function) : angular.ICompileProvider;
    registerController(name: string, controllerConstructor: Function): void;
    registerComponent(name: string, options: angular.IComponentOptions): angular.ICompileProvider;

}

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
    app.registerController = $controllerProvider.register;
    app.registerDirective = $compileProvider.directive;
    app.registerComponent = $compileProvider.component;
    app.registerFilter = $filterProvider.register;
    app.registerFactory = $provide.factory;
    app.registerService = $provide.service;
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
