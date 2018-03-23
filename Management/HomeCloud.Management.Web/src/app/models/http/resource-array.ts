import { Observable } from "rxjs/Observable";

import { IResource } from "./resource";
import { Relation } from "./relation";
import { PagedArray } from "../paged-array";
import { RelationArray } from "./relation-array";

export class ResourceArray<T extends IResource> implements IResource {
    public relations: RelationArray = new RelationArray();
    public items: PagedArray<T> = new PagedArray<T>();

    public hasPrevious(): boolean {
        return this.relations.previous != null;
    }

    public hasNext(): boolean {
        return this.relations.next != null;
    }

    public hasCreate(): boolean {
        return this.relations.create != null;
    }

    public hasItem(index: number): boolean {
        return this.relations.items != null && this.relations.items.length > index && this.relations.items[index] != null;
    }
}