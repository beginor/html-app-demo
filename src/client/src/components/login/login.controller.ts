import {IAsyncModule} from '../../models/app';
import {IAccountService} from '../../services/account.service';
import {LoginModel} from "../../models/login-model";
import {AppRootScope} from "../../models/app-root-scope";
import {UserInfo} from "../../models/user-info";

import '../../services/account.service';

class LoginController {

    model: LoginModel = {
        userName: 'admin',
        password: '1a2b3c4D',
        rememberMe: true
    };

    error: any;

    static $inject = ['$rootScope', '$scope', '$state', 'AccountService'];

    constructor(
        private $rootScope: AppRootScope,
        private $scope: angular.IScope,
        private $state: angular.ui.IStateService,
        private accountService: IAccountService
    ) {
    }

    login() {
        this.accountService.login(this.model).then(() => {
            return this.accountService.getUserInfo();
        }).then((userInfo: UserInfo) => {
            this.$rootScope.userInfo = userInfo;
            this.$rootScope.$broadcast('accountChange');
            if (angular.isDefined(this.$rootScope.returnState)) {
                this.$state.go(this.$rootScope.returnState, this.$rootScope.returnStateParams);
            }
            else {
                this.$state.go('app.home');
            }
        }).catch(error => {
            this.error = error;
        })
    }

}

(<IAsyncModule> angular.module('app'))
    .compileProvider.component('login', {
        templateUrl: 'components/login/login.view.html',
        controller: LoginController,
    });