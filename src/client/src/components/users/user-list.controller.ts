import {IAsyncModule} from "../../models/state";
/**
 * Created by zhang on 4/10/16.
 */
class UserListController {
    
    static $inject = [];
    
    constructor() {
        
    }
    
}

(<IAsyncModule> angular.module('app'))
    .registerController('UserListController', UserListController);
