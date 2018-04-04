import { Resource } from "./http/resource";
import { StorageRelation } from "./storage-relation";

export class Storage extends Resource {
    public id: string = "";
    public name: string = "";
    public size: number = 0;
    public quota: number = null;

    constructor() {
        super(StorageRelation);
    }

    public hasCatalogs(): boolean {
        return (this._links as StorageRelation).catalogs != null;
    }

}