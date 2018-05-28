import { RelationArray } from "../http/relation-array";
import { Resource } from "../http/resource";

export class Grant  extends Resource {
    public id: string = "";
    public name: string = "";

    constructor(relationType?: new() => RelationArray) {
        super(relationType);
    }
}