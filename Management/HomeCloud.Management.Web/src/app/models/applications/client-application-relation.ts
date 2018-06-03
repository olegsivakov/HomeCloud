import { RelationArray } from "../http/relation-array";
import { Relation } from "../http/relation";

export class ClientApplicationRelation extends RelationArray {
    public scopes: Relation = new Relation();
    public origins: Relation = new Relation();
    public secrets: Relation = new Relation();
}