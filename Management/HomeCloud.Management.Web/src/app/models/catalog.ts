import { StorageData } from "./storage-data";
import { GUID } from "./http/resource";
import { CatalogRelation } from "./catalog-relation";
import { Relation } from "./http/relation";

export class Catalog
    extends StorageData {

  public id: string = GUID;
  public name: string = "";
  public size: number = 0;
  public readonly isCatalog: boolean = true;
  public creationDate: Date = new Date();

  public count: number = 0;

  constructor() {
      super(CatalogRelation);
  }

  public hasData(): boolean {
    let relation: Relation = (this._links as CatalogRelation).data;
    return relation != null && !relation.isEmpty();
  }
}
  