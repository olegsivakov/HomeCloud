import { StorageData } from "./storage-data";
import { Resource } from "./http/resource";
import { CatalogRelation } from "./catalog-relation";

export class Catalog
    extends Resource<CatalogRelation>
    implements StorageData {

  public id: string = "";
  public name: string = "";
  public size: string = "";
  public readonly isCatalog: boolean = true;
  public creationDate: Date = new Date();

  constructor() {
      super(CatalogRelation);
  }

  public hasCatalogs(): boolean {
    return this._links.catalogs != null;
  }
}
  