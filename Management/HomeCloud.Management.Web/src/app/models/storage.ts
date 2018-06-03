import { Resource, GUID } from "./http/resource";
import { StorageRelation } from "./storage-relation";
import { Relation } from "./http/relation";

export class Storage extends Resource {
    public id: string = GUID;
    public name: string = "";
    public size: number = 0;
    public quota: number = null;

    constructor() {
        super(StorageRelation);
    }

    public hasCatalog(): boolean {
        let relation: Relation = (this._links as StorageRelation).catalog;
        return relation != null && !relation.isEmpty();
    }
}