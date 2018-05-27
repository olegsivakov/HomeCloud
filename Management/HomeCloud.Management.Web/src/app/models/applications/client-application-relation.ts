import { RelationArray } from "../http/relation-array";
import { Relation } from "../http/relation";

export class ClientApplicationRelation extends RelationArray {
    public grantTypes: Relation = new Relation();
}