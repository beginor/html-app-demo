export interface BaseModel<T> {

    id: T;

}

export interface NumberModel extends BaseModel<number> {

}

export interface StringModel extends BaseModel<string> {

}

export interface BaseIdNameModel<TKey, TValue> extends BaseModel<TKey> {

    name: TValue;

}

export interface NumberIdNameModel extends BaseIdNameModel<number, string> {

}

export interface StringIdNameModel extends BaseIdNameModel<string, string> {
    
}