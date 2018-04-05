import { Resource } from "./http/resource";
import { RelationArray } from "./http/relation-array";

export class StorageData extends Resource {
    public id: string = "";
    public name: string = "";
    public isCatalog: boolean = false;

    constructor(relationType?: new() => RelationArray) {
        super(relationType);
    }
}