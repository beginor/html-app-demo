import {IAsyncModule} from "../../models/app";
/**
 * Created by zhang on 4/10/16.
 */
class LayoutController {

    static $inject = [];

    constructor() {
        //console.log('layout controller constructor');
    }

}

(<IAsyncModule> angular.module('app'))
    .controllerProvider.register('LayoutController', LayoutController);