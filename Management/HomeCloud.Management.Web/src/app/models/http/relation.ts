import { HttpMethod } from "./http-method";

export class Relation {
    public href: string = "";
    public method: HttpMethod = HttpMethod.unknown;
    public type: string = "";
}