define('routes', [], function () {
    return {
        defaultRoutePath: '/welcome',
        routes: {
            '/welcome': {
                templateUrl: 'views/welcome.html',
                dependencies: ['app/welcome']
            },
            '/dialogs': {
                templateUrl: 'views/dialogs.html',
                dependencies: ['app/dialogs']
            },
            '/list': {
                templateUrl: 'views/list.html',
                dependencies: ['app/list']
            },
            '/user': {
                templateUrl: 'views/user.html',
                dependencies: ['app/user']
            },
            '/help': {
                templateUrl: 'views/help.html',
                dependencies: ['app/help']
            }
        }
    };
});