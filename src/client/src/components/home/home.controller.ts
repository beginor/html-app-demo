import {IAsyncModule} from "../../models/state";

class HomeController {
    
    static $inject = [];
    
}

(<IAsyncModule> angular.module('app'))
    .registerController('HomeController', HomeController);
