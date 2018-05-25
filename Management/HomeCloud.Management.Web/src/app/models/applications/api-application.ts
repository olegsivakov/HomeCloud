import { Application } from "./application";
import { ApiApplicationRelation } from "./api-application-relation";

export class ApiApplication
    extends Application {

    constructor() {
        super(ApiApplicationRelation);
    }
}