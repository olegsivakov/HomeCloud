import { Relation } from "./relation";
import { HttpMethod } from "./http-method";

export class Link {
    public rel: Relation = Relation.unknown;
    public href: string = "";
    public method: HttpMethod = HttpMethod.unknown;
}