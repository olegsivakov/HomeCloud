import { StorageData } from "./storage-data";
import { Resource } from "./http/resource";
import { RelationArray } from "./http/relation-array";
import { Relation } from "./http/relation";

export class Catalog
    extends Resource
    implements StorageData {

  public ID: string = "";
  public Name: string = "";
  public Size: string = "";
  public readonly IsCatalog: boolean = true;
  public CreationDate: Date = new Date();

  public _links: RelationArray = null;
  constructor() {
      super(RelationArray);
  }
}
  