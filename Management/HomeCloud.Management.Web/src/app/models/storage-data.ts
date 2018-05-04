import { Resource, GUID } from "./http/resource";
import { RelationArray } from "./http/relation-array";
import { Size } from "./size";

export class StorageData extends Resource {

    public id: string = GUID;
    public name: string = "";
    public isCatalog: boolean = false;
    public exists: boolean = false;
    public size: Size = new Size();

    constructor(relationType?: new() => RelationArray) {
        super(relationType);
    }
}