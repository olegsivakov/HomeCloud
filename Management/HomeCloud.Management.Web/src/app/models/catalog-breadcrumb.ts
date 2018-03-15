import { Breadcrumb } from "./breadcrumb";
import { Catalog } from "./catalog";

export class CatalogBreadcrumb extends Breadcrumb {

    constructor(public catalog: Catalog) {
        super();
    }

    public get id(): string {
        return this.catalog ? this.catalog.ID : "";
    }

    public set id(value: string) {
        if (this.catalog) {
            this.catalog.ID = value;
        }
    }

    public get text(): string {
        return this.catalog ? this.catalog.Name : "";
    }

    public set text(value: string) {
        if (this.catalog) {
            this.catalog.Name = value;
        }
    }
}