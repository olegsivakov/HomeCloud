import { HttpClient } from "@angular/common/http";
import { RelationArray } from "./relation-array";

export interface IResource {
    relations: RelationArray;
}

export class Resource implements IResource {
    public relations: RelationArray = new RelationArray();

    constructor(public relationType: new() => RelationArray) {
        this.relations = new this.relationType;
    }

    public hasGet(): boolean {
        return this.relations.get != null;
    }

    public hasCreate(): boolean {
        return this.relations.create != null;
    }

    public hasUpdate(): boolean {
        return this.relations.update != null;
    }

    public hasDelete(): boolean {
        return this.relations.delete != null;
    }

    public hasExists(): boolean {
        return this.relations.exists != null;
    }
}