import {IAsyncModule} from "../../models/app";

class HomeController {
    
    static $inject = [];
    
}

(<IAsyncModule> angular.module('app'))
    .controllerProvider.register('HomeController', HomeController);
