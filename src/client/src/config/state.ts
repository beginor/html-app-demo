/**
 * Created by zhang on 4/10/16.
 */
import {IAppStateConfig} from "../models/app";

var appState: IAppStateConfig = {
    defaultUrl: '/home',
    states: {
        'app': {
            url: '',
            templateUrl: 'components/layout/app-layout.view.html',
            controller: 'LayoutController',
            controllerAs: 'vm',
            dependencies: [
                './components/layout/layout.controller'
            ],
            allowAnonymous: true
        },
        'app.home': {
            url: '/home',
            views: {
                'mainView': {
                    templateUrl: 'components/home/home.view.html',
                    controller: 'HomeController',
                    controllerAs: 'vm'
                }
            },
            dependencies: [
                './components/home/home.controller'
            ],
            allowAnonymous: true
        },
        'admin': {
            url: '/admin',
            templateUrl: 'components/layout/admin-layout.view.html',
            controller: 'LayoutController',
            controllerAs: 'vm',
            dependencies: [
                './components/layout/layout.controller'
            ],
            allowAnonymous: false
        },
        'admin.users': {
            url: '/users',
            views: {
                'mainView': {
                    templateUrl: 'components/users/user-list.view.html',
                    controller: 'UserListController',
                    controllerAs: 'vm'
                }
            },
            dependencies: [
                'components/users/user-list.controller'
            ],
            allowAnonymous: false
        }
    }
};

export { appState };
