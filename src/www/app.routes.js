define([], function () {
    return {
        defaultRoute: '/welcome',
        routes: {
            '/welcome': {
                templateUrl: 'components/welcome/welcomeView.html',
                controller: 'WelcomeController',
                dependencies: ['components/welcome/welcomeController'],
                allowAnonymous: true
            },
            '/dialogs': {
                templateUrl: 'components/dialogs/dialogsView.html',
                controller: 'DialogsController',
                dependencies: ['components/dialogs/dialogsController'],
                allowAnonymous: true
            },
            '/list': {
                templateUrl: 'components/list/listView.html',
                controller: 'ListController',
                dependencies: ['components/list/listController'],
                allowAnonymous: false
            },
            '/user': {
                templateUrl: 'components/user/userView.html',
                controller: 'UserController',
                dependencies: ['components/user/userController'],
                allowAnonymous: true
            },
            '/login': {
                templateUrl: 'components/user/loginView.html',
                controller: 'LoginController',
                dependencies: ['components/user/loginController'],
                allowAnonymous: true
            },
            '/register': {
                templateUrl: 'components/user/registerView.html',
                controller: 'RegisterController',
                dependencies: ['components/user/registerController'],
                allowAnonymous: true
            },
            '/help': {
                templateUrl: 'components/help/helpView.html',
                controller: 'HelpController',
                dependencies: ['components/help/helpController'],
                allowAnonymous: true
            }
        }
    };
});