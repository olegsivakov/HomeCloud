import { Resource, GUID } from "./http/resource";
import { RelationArray } from "./http/relation-array";
import { Size } from "./size";

export class StorageData extends Resource {
    private _displaySize: Size = new Size();

    public id: string = GUID;
    public name: string = "";
    public isCatalog: boolean = false;
    public size: number = 0;
    public get displaySize(): Size {
        if (this._displaySize.value != this.size) {
            this._displaySize.value = this.size;
        }

        return this._displaySize;
    }

    constructor(relationType?: new() => RelationArray) {
        super(relationType);
    }
}