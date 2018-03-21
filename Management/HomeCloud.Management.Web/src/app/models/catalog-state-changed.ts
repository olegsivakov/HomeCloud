import { CatalogState } from "./catalog-state";
import { Catalog } from "./catalog";

export class CatalogStateChanged {
    constructor(public catalog: Catalog, public state: CatalogState) { }
}