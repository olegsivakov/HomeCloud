import { Link } from "./link";
import { HttpClient } from "@angular/common/http";
import { Relation } from "./relation";

export interface IResource {
    _links: Array<Link>;
}

export class Resource implements IResource {
    public _links: Array<Link> = Array<Link>();

    public get get(): Link {
        return this._links.find(link => link.rel == Relation.get);
    }

    public hasGet(): boolean {
        return this.get != null;
    }

    public get create(): Link {
        return this._links.find(link => link.rel == Relation.create);
    }

    public hasCreate(): boolean {
        return this.create != null;
    }

    public get update(): Link {
        return this._links.find(link => link.rel == Relation.update);
    }

    public hasUpdate(): boolean {
        return this.update != null;
    }

    public get delete(): Link {
        return this._links.find(link => link.rel == Relation.delete);
    }

    public hasDelete(): boolean {
        return this.delete != null;
    }

    public get exist(): Link {
        return this._links.find(link => link.rel == Relation.exist);
    }

    public hasExist(): boolean {
        return this.exist != null;
    }
}