import {IAsyncModule} from "../models/app";
/**
 * Created by zhang on 4/10/16.
 */
class AccountService {
    
    static $inject = ['$http', '$q'];
    
    constructor(private $http, private $q) {
        
    }
    
}

(<IAsyncModule> angular.module('app'))
    .registerService('AccountService', AccountService);
