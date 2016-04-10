import {IAsyncModule} from "../../app";

class HomeController {
    
    static $inject = [];
    
}

(<IAsyncModule> angular.module('app'))
    .registerController('HomeController', HomeController);
