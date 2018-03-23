import { HttpClient } from "@angular/common/http";
import { RelationArray } from "./relation-array";

export interface IResource<T extends RelationArray> {
    _links: T;
}

export class Resource<T extends RelationArray> implements IResource<T> {
    public _links: T = null;

    constructor(public relationType: new() => T) {
        this._links = new this.relationType;
    }

    public hasGet(): boolean {
        return this._links.get != null;
    }

    public hasCreate(): boolean {
        return this._links.create != null;
    }

    public hasUpdate(): boolean {
        return this._links.update != null;
    }

    public hasDelete(): boolean {
        return this._links.delete != null;
    }

    public hasExists(): boolean {
        return this._links.exists != null;
    }
}