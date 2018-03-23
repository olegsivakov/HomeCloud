import { HttpClient } from "@angular/common/http";
import { RelationArray } from "./relation-array";

export interface IResource {
    _links: RelationArray;
}

export class Resource implements IResource {
    public _links: RelationArray = new RelationArray();

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