import { StorageData } from "./storage-data";

export class Catalog implements StorageData {

    public ID: string = "";
    public Name: string = "";
    public Size: string = "";
    public readonly IsCatalog: boolean = true;
    public CreationDate: Date = new Date();

    constructor() {        
    }
  }
  