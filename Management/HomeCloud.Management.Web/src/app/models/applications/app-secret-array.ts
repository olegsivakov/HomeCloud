import { ResourceArray } from "../http/resource-array";
import { AppSecret } from "./app-secret";

export class AppSecretArray extends ResourceArray<AppSecret> {

    constructor() {
        super();
    }
}