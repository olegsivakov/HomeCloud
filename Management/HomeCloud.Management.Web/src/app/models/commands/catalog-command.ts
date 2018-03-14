import { Catalog } from "../catalog";

export class CatalogCommand implements ICommand<Catalog> {

    public readonly catalog: Catalog = null;

    constructor(catalog: Catalog) {
        this.catalog = catalog;
    }

    execute(action: (value: Catalog) => void): void {
        action(this.catalog);
    }
    
}