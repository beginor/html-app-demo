/**
 * Created by zhang on 4/10/16.
 */
export interface IAppStateConfig {

    defaultUrl: string;

    states: { [name: string]: IAsyncState }

}

export interface IAsyncState extends angular.ui.IState {

    dependencies?: string[];

    allowAnonymous?: boolean;

}
