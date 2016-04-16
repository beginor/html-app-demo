import {IAsyncModule} from "../../models/app";

//import '../../app';
/**
 * Created by zhang on 4/12/16.
 */
class AppHeaderComponent {
    
    static $inject = [];
    
    constructor() {
        console.log('app header constructor');
    }
}

(<IAsyncModule> angular.module('app'))
    .compileProvider.component('appHeader', {
        templateUrl: 'components/app-header/app-header.view.html',
        controller: AppHeaderComponent
    });
