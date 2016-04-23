///<reference path='../typings/browser.d.ts'/>
import 'app.module';

import {configAsyncModule} from './config/async-module';
import {configHttp} from './config/http';
import {runAtstartup} from './config/startup';
import * as constants from './config/constants';

angular.module('app')
    .config(configAsyncModule)
    .config(configHttp)
    .run(runAtstartup);

angular.forEach(constants, (val, key) => {
    angular.module('app').constant(key, val);
});
