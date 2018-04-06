import { RelationArray } from "./http/relation-array";
import { Relation } from "./http/relation";

export class CatalogRelation extends RelationArray {
    public data: Relation = new Relation();
    public createCatalog: Relation = new Relation();
    public createFile: Relation = new Relation();
}