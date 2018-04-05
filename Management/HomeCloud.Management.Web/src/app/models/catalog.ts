import { StorageData } from "./storage-data";
import { Resource } from "./http/resource";
import { CatalogRelation } from "./catalog-relation";
import { Relation } from "./http/relation";

export class Catalog
    extends StorageData {

  public id: string = "";
  public name: string = "";
  public size: string = "";
  public readonly isCatalog: boolean = true;
  public creationDate: Date = new Date();

  constructor() {
      super(CatalogRelation);
  }

  public hasData(): boolean {
    let relation: Relation = (this._links as CatalogRelation).data;
    return relation != null && !relation.isEmpty();
  }
}
  