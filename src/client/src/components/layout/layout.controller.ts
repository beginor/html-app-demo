import {IAsyncModule} from "../../models/app";
/**
 * Created by zhang on 4/10/16.
 */
class LayoutController {

    static $inject = [];

    constructor() {

    }

}

(<IAsyncModule> angular.module('app'))
    .registerController('LayoutController', LayoutController);