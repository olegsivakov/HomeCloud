import { StorageData } from "./storage-data";
import { Resource } from "./http/resource";
import { RelationArray } from "./http/relation-array";

export class Catalog
    extends Resource<RelationArray>
    implements StorageData {

  public id: string = "";
  public name: string = "";
  public size: string = "";
  public readonly isCatalog: boolean = true;
  public creationDate: Date = new Date();

  constructor() {
      super(RelationArray);
  }
}
  