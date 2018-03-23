import { Observable } from "rxjs/Observable";

import { IResource } from "./resource";
import { PagedArray } from "../paged-array";
import { RelationArray } from "./relation-array";

export class ResourceArray<T> implements IResource<RelationArray> {
    public _links: RelationArray = new RelationArray();
    public items: PagedArray<T> = new PagedArray<T>();

    public hasPrevious(): boolean {
        return this._links.previous != null;
    }

    public hasNext(): boolean {
        return this._links.next != null;
    }

    public hasCreate(): boolean {
        return this._links.create != null;
    }

    public hasItem(index: number): boolean {
        return this._links.items != null && this._links.items.length > index && this._links.items[index] != null;
    }
}