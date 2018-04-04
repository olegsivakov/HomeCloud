import { RelationArray } from "./http/relation-array";
import { Relation } from "./http/relation";

export class StorageRelation extends RelationArray {
    public catalogs: Relation = new Relation();
}