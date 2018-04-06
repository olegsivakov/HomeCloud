import { HttpClient } from "@angular/common/http";
import { RelationArray } from "./relation-array";
import { Relation } from "./relation";
import { CatalogRelation } from "../catalog-relation";

export interface IResource {
    _links: RelationArray;
}

export class Resource implements IResource {
    public _links: RelationArray = null;

    constructor(public relationType?: new() => RelationArray) {
        this._links = this.relationType ? new this.relationType() : new RelationArray();
    }

    public getRelations<T extends RelationArray>(): T {
        return this._links as T;
    }

    public hasSelf(): boolean {
        return this._links.self != null && !this._links.self.isEmpty();
    }

    public hasGet(): boolean {
        return this._links.get != null && !this._links.get.isEmpty();
    }

    public hasCreate(): boolean {
        return this._links.create != null && !this._links.create.isEmpty();
    }

    public hasUpdate(): boolean {
        return this._links.update != null && !this._links.update.isEmpty();
    }

    public hasDelete(): boolean {
        return this._links.delete != null && !this._links.delete.isEmpty();
    }

    public hasExists(): boolean {
        return this._links.exists != null && !this._links.exists.isEmpty();
    }
}