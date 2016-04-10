import {IAsyncModule} from "../../models/app";

class HomeController {
    
    static $inject = [];
    
}

(<IAsyncModule> angular.module('app'))
    .registerController('HomeController', HomeController);
