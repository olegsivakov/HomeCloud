import { GUID } from "./resource";

export class HttpError {
    public id: string = GUID;
    public messages: Array<string> = new Array<string>();
    public statusCode: number = 500;
}