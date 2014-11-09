define('routes', [], function () {
    return {
        defaultRoute: '/welcome',
        routes: {
            '/welcome': {
                templateUrl: 'views/welcome.html',
                controller: 'WelcomeController',
                dependencies: ['app/welcome']
            },
            '/dialogs': {
                templateUrl: 'views/dialogs.html',
                controller: 'DialogsController',
                dependencies: ['app/dialogs']
            },
            '/list': {
                templateUrl: 'views/list.html',
                controller: 'ListController',
                dependencies: ['app/list']
            },
            '/user': {
                templateUrl: 'views/user.html',
                controller: 'UserController',
                dependencies: ['app/user']
            },
            '/help': {
                templateUrl: 'views/help.html',
                controller: 'HelpController',
                dependencies: ['app/help']
            }
        }
    };
});