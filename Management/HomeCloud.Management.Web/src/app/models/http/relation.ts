import { HttpMethod } from "./http-method";

export class Relation {
    public href: string = "";
    public method: HttpMethod = HttpMethod.unknown;
    public type: string = "";

    public isEmpty(): boolean {
        return (this.href == null || this.href == "")
                || (this.type == null || this.type == "")
                || (this.method == null || (this.method as HttpMethod) == HttpMethod.unknown);
    }
}