/**
 * Created by zhang on 4/23/16.
 */
configHttp.$inject = ['$httpProvider'];

export function configHttp($httpProvider: angular.IHttpProvider) {
    if (!$httpProvider.defaults.headers.get) {
        $httpProvider.defaults.headers.get = {};
    }
    //disable IE ajax request caching
    $httpProvider.defaults.headers.get['If-Modified-Since'] = 'Mon, 26 Jul 1997 05:00:00 GMT';
    // extra
    $httpProvider.defaults.headers.get['Cache-Control'] = 'no-cache';
    $httpProvider.defaults.headers.get.Pragma = 'no-cache';
    // send cors crendentials;
    $httpProvider.defaults.withCredentials = true;
}
