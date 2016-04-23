import {StringIdNameModel} from "./base-models";

export interface UserInfo {
    name: string;
    authorized: boolean;
    roles: { [key:string]: boolean };
    //area: StringIdNameModel;
    loginProvider: string;
}
