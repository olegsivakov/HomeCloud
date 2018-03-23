import { Resource } from "./http/resource";
import { StorageRelation } from "./storage-relation";

export class Storage extends Resource<StorageRelation> {
    public id: string = "";
    public name: string = "";
    public size: number = 0;
    public quota: number = null;

    public hasCatalogs(): boolean {
        return this._links.catalogs != null;
    }

}