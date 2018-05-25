import { Resource, GUID } from "../http/resource";
import { RelationArray } from "../http/relation-array";

export class Application extends Resource {
    public id: string = GUID;
    public name: string = "";

    constructor(relationType?: new() => RelationArray) {
        super(relationType);
    }
}