import { Relation } from "./relation";

export class RelationArray {
    public self: Relation = null;

    public get: Relation = null;

    public create: Relation = null;

    public update: Relation = null;

    public delete: Relation = null;

    public list: Relation = null;

    public exists: Relation = null;

    public next: Relation = null;

    public previous: Relation = null;

    public items: Array<Relation> = null;
}