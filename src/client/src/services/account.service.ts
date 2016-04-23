import {IAsyncModule} from "../models/app";
/**
 * Created by zhang on 4/10/16.
 */
import {UserInfo} from "../models/user-info";
import {LoginModel} from "../models/login-model";

export interface IAccountService {
    login(model: LoginModel): angular.IPromise<any>;
    getUserInfo(): angular.IPromise<UserInfo>;
    logout(): angular.IPromise<any>;
}

class AccountService implements IAccountService {
    
    static $inject = ['$http', '$q', 'apiRoot'];
    
    constructor(
        private $http: angular.IHttpService,
        private $q: angular.IQService,
        private apiRoot: string
    ) { }
    
    login(model: LoginModel): angular.IPromise<any> {
        var deferred = this.$q.defer<any>();
        var url = this.apiRoot + 'account/login';
        this.$http.post(url, model).then(
            response => deferred.resolve({}),
            error => deferred.reject(error.data)
        );
        return deferred.promise;
    }
    
    getUserInfo(): angular.IPromise<UserInfo> {
        var deferred = this.$q.defer<UserInfo>();
        var url = this.apiRoot + 'account/userinfo';
        this.$http.get<UserInfo>(url, { cache: false }).then(
            response => {
                deferred.resolve(response.data);
            },
            error => {
                deferred.reject(error.data);
            }
        );
        return deferred.promise;
    }
    
    logout(): angular.IPromise<any> {
        var deferred = this.$q.defer<any>();
        var url = this.apiRoot + 'account/logout';
        this.$http.post(url, {}).then(
            response => deferred.resolve({}),
            error => deferred.reject(error.data)
        );
        return deferred.promise;
    }
}

(<IAsyncModule> angular.module('app'))
    .provide.service('AccountService', AccountService);
