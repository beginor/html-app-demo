/**
 * Created by zhang on 4/7/16.
 */
import {UserInfo} from "./user-info";

export interface AppRootScope extends angular.IRootScopeService {
    
    userInfo: UserInfo;
    
    returnState: angular.ui.IState;
    
    returnStateParams: angular.ui.IStateParamsService;
    
}
