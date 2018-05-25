import { Application } from "./application";
import { ClientApplicationRelation } from "./client-application-relation";

export class ClientApplication
    extends Application {

    constructor() {
        super(ClientApplicationRelation);
    }

    public grantType: number = 0;
    public redirectUrl: string = null;
    public postLogoutRedirectUrl: string = null;

    public identityTokenLifetime: number = null;
    public accessTokenLifetime: number = null;
    public absoluteRefreshTokenLifetime: number = null;
    public slidingRefreshTokenLifetime: number = null;
}