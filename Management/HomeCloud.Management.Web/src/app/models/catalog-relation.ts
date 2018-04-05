import { RelationArray } from "./http/relation-array";
import { Relation } from "./http/relation";

export class CatalogRelation extends RelationArray {
    public data: Relation = new Relation();
}