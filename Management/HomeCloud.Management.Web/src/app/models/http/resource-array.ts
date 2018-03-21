import { Observable } from "rxjs/Observable";

import { IResource } from "./resource";
import { Relation } from "./relation";
import { Link } from "./link";
import { PagedArray } from "../paged-array";

export class ResourceArray<T extends IResource> implements IResource {
    public _links: Array<Link> = new Array<Link>();
    public items: PagedArray<T> = new PagedArray<T>();

    public get previous(): Link {
        return this._links.find(link => link.rel == Relation.previous);
    }

    public hasPrevious(): boolean {
        return this.previous != null;
    }

    public get next(): Link {
        return this._links.find(link => link.rel == Relation.next);
    }

    public hasNext(): boolean {
        return this.next != null;
    }

    public get create(): Link {
        return this._links.find(link => link.rel == Relation.create);
    }

    public hasCreate(): boolean {
        return this.create != null;
    }

    public hasRelation(relation: string) {
        return this.relation(relation) != null;
    }

    public relation(relation: string) {
        return this._links.find(link => link.rel == relation);
    }
}