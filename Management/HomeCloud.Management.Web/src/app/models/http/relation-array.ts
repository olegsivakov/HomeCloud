import { Relation } from "./relation";

export class RelationArray {
    public self: Relation = new Relation();

    public get: Relation = new Relation();

    public validate: Relation = new Relation();

    public create: Relation = new Relation();

    public update: Relation = new Relation();

    public delete: Relation = new Relation();

    public list: Relation = new Relation();

    public exists: Relation = new Relation();

    public next: Relation = new Relation();

    public previous: Relation = new Relation();

    public items: Array<Relation> = new Array<Relation>();
}