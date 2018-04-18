import { Breadcrumb } from "./breadcrumb";
import { Catalog } from "../catalog";
import { GUID } from "../http/resource";

export class CatalogBreadcrumb extends Breadcrumb {

    constructor(public catalog: Catalog) {
        super();
    }

    public get id(): string {
        return this.catalog ? this.catalog.id : GUID;
    }

    public set id(value: string) {
        if (this.catalog) {
            this.catalog.id = value;
        }
    }

    public get text(): string {
        return this.catalog ? this.catalog.name : "";
    }

    public set text(value: string) {
        if (this.catalog) {
            this.catalog.name = value;
        }
    }

    public get count(): number {
        return this.catalog ? this.catalog.count : 0;
    }
}