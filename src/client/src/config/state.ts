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
                'components/layout/layout.controller',
                'components/app-header/app-header.component'
            ],
            allowAnonymous: true
        },
        'app.home': {
            url: '/home',
            views: {
                'main': {
                    templateUrl: 'components/home/home.view.html',
                    controller: 'HomeController',
                    controllerAs: 'vm'
                }
            },
            dependencies: [
                'components/home/home.controller'
            ],
            allowAnonymous: true
        },
        'app.login': {
            url: '/login',
            views: {
                'main': {
                    template: '<login></login>'
                }
            },
            dependencies: [
                'components/login/login.controller'
            ],
            allowAnonymous: true
        },
        'admin': {
            url: '/admin',
            templateUrl: 'components/layout/admin-layout.view.html',
            controller: 'LayoutController',
            controllerAs: 'vm',
            dependencies: [
                'components/layout/layout.controller',
                'components/app-header/app-header.component'
            ],
            allowAnonymous: false
        },
        'admin.users': {
            url: '/users',
            views: {
                'main': {
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
