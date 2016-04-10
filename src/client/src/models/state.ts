/**
 * Created by zhang on 4/10/16.
 */
export interface IAsyncModule extends angular.IModule {

    registerService(name: string, constructor: Function): angular.IServiceProvider;
    registerFactory(name: string, serviceFactoryFunction: Function): angular.IServiceProvider;
    registerFilter(name: string | {}): angular.IServiceProvider;
    registerDirective(name: string, directiveFactory: Function) : angular.ICompileProvider;
    registerController(name: string, controllerConstructor: Function): void;
    registerComponent(name: string, options: angular.IComponentOptions): angular.ICompileProvider;

}

export interface IAppStateConfig {

    defaultUrl: string;

    states: { [name: string]: IAsyncState }

}

export interface IAsyncState extends angular.ui.IState {

    dependencies?: string[];

    allowAnonymous?: boolean;

}
