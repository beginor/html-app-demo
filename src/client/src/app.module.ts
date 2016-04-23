/**
 * Created by zhang on 4/23/16.
 */
import 'angular';
import 'angular-animate';
import 'angular-resource';
import 'angular-sanitize';
import 'angular-bootstrap';
import 'angular-ui-router';

import {IAsyncState} from "./models/app";
import {appState} from "./config/state";

angular.module('app', ['ngResource', 'ngAnimate', 'ngSanitize', 'ui.bootstrap', 'ui.router']);

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

angular.module('app').config(configState);
