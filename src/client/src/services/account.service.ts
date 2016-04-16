import {IAsyncModule} from "../models/app";
/**
 * Created by zhang on 4/10/16.
 */
export interface IAccountService {
    
}

class AccountService implements IAccountService {
    
    static $inject = ['$http', '$q'];
    
    constructor(private $http, private $q) {
        
    }
    
}

(<IAsyncModule> angular.module('app'))
    .provide.service('AccountService', AccountService);
