import '../app.module';
import {IAsyncModule} from "../models/app";
/**
 * Created by zhang on 4/23/16.
 */
configAsyncModule.$inject = [
    '$controllerProvider',
    '$compileProvider',
    '$filterProvider',
    '$provide'
];
export function configAsyncModule(
    $controllerProvider: angular.IControllerProvider,
    $compileProvider: angular.ICompileProvider,
    $filterProvider: angular.IFilterProvider,
    $provide: angular.auto.IProvideService
) {
    var app = <IAsyncModule> angular.module('app');
    app.controllerProvider = $controllerProvider;
    app.compileProvider = $compileProvider;
    app.filterProvider = $filterProvider;
    app.provide = $provide;
}