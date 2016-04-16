import {IAsyncModule} from '../../models/app';
import {IAccountService} from '../../services/account.service';

class LoginController {
    
    static $inject = ['AccountService'];
    
    constructor(
        private accountService: IAccountService
    ) {
        
    }
    
    login() {
        //this.accountService.login();
    }
}

(<IAsyncModule> angular.module('app'))
    .compileProvider.component('login', {
        templateUrl: 'components/login/login.view.html',
        controller: LoginController,
    });