import { HttpClient } from "@angular/common/http";
import { RelationArray } from "./relation-array";

export interface IResource<T extends RelationArray> {
    relations: T;
}

export class Resource<T extends RelationArray> implements IResource<T> {
    public relations: T = null;

    constructor(public relationType: new() => T) {
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