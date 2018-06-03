import { Observable } from "rxjs/Observable";

import { IResource } from "./resource";
import { PagedArray } from "../paged-array";
import { RelationArray } from "./relation-array";

export class ResourceArray<T> implements IResource {
    public items: PagedArray<T> = new PagedArray<T>();
    public _links: RelationArray = new RelationArray();

    constructor() {
    }

    public hasSelf(): boolean {
        return this._links.self != null && !this._links.self.isEmpty();
    }

    public hasPrevious(): boolean {
        return this._links.previous != null && !this._links.previous.isEmpty();
    }

    public hasNext(): boolean {
        return this._links.next != null && !this._links.next.isEmpty();
    }

    public hasCreate(): boolean {
        return this._links.create != null && !this._links.create.isEmpty();
    }

    public hasSave(): boolean {
        return this._links.save != null && !this._links.save.isEmpty();
    }

    public hasItem(index: number): boolean {
        return this._links.items != null && this._links.items.length > index && this._links.items[index] != null && !this._links.items[index].isEmpty();
    }
}