/**
 * Created by zhang on 4/10/16.
 */
export interface IAsyncModule extends angular.IModule {

    controllerProvider: ng.IControllerProvider;
    compileProvider: ng.ICompileProvider,
    filterProvider: angular.IFilterProvider;
    provide: angular.auto.IProvideService;
    
}

export interface IAppStateConfig {

    defaultUrl: string;

    states: { [name: string]: IAsyncState }

}

export interface IAsyncState extends angular.ui.IState {

    dependencies?: string[];

    allowAnonymous?: boolean;

}
