define([], function () {
    return {
        defaultRoute: '/welcome',
        routes: {
            '/welcome': {
                templateUrl: 'components/welcome/welcomeView.html',
                controller: 'WelcomeController',
                dependencies: ['components/welcome/welcomeController']
            },
            '/dialogs': {
                templateUrl: 'components/dialogs/dialogsView.html',
                controller: 'DialogsController',
                dependencies: ['components/dialogs/dialogsController']
            },
            '/list': {
                templateUrl: 'components/list/listView.html',
                controller: 'ListController',
                dependencies: ['components/list/listController']
            },
            '/user': {
                templateUrl: 'components/user/userView.html',
                controller: 'UserController',
                dependencies: ['components/user/userController']
            },
            '/help': {
                templateUrl: 'components/help/helpView.html',
                controller: 'HelpController',
                dependencies: ['components/help/helpController']
            }
        }
    };
});