import { StorageData } from "./storage-data";
import { Resource } from "./http/resource";

export class Catalog
    extends Resource
    implements StorageData {

    public ID: string = "";
    public Name: string = "";
    public Size: string = "";
    public readonly IsCatalog: boolean = true;
    public CreationDate: Date = new Date();

    constructor() {
      super();
    }
  }
  