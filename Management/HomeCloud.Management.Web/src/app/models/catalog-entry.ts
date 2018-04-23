import { StorageData } from "./storage-data";
import { GUID } from "./http/resource";
import { RelationArray } from "./http/relation-array";

export class CatalogEntry
    extends StorageData {

    private _file: File = null;

    public id: string = GUID;  
    public name: string = "";
    public type: string = "";
    public readonly isCatalog: boolean = false;
    public creationDate: Date = new Date();
    public count: number = 0;
    public set file(value: File) {
        this._file = value;
        this.name = this._file.name;
        this.size = this._file.size;
    }

    constructor(file?: File) {
        super(RelationArray);
        if (file) {
            this.file = file;
        }
    }

    public toFormData(): FormData {
        let result: FormData = new FormData();
        result.append(this._file.name, this._file, this._file.name);

        return result;
    }
}