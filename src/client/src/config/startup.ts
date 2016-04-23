import {AppRootScope} from '../models/app-root-scope';
import {IAsyncState} from '../models/app';
/**
 * Created by zhang on 4/23/16.
 */
runAtstartup.$inject = [];
export function runAtstartup() {
    console.log('app run at startup');
}
startup.$inject = ['$rootScope', '$state'];

function startup($rootScope: AppRootScope, $state: angular.ui.IStateService) {
    $rootScope.$on(
        '$stateChangeStart',
        (event: angular.IAngularEvent, toState: IAsyncState, toParams: angular.ui.IStateParamsService) => {
            if (angular.isUndefined(toState)) {
                return;
            }
            if (angular.isUndefined(toState.allowAnonymous) || toState.allowAnonymous === true) {
                return;
            }
            if (angular.isUndefined($rootScope.userInfo) || !$rootScope.userInfo.authorized) {
                event.preventDefault();
                $rootScope.returnState = toState;
                $rootScope.returnStateParams = toParams;
                $state.go('login');
            }
        }
    );
}