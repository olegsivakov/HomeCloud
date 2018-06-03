import { Application } from "./application";
import { ClientApplicationRelation } from "./client-application-relation";
import { AppScopeArray } from "./app-scope-array";
import { Relation } from "../http/relation";
import { AppOriginArray } from "./app-origin-array";
import { AppSecretArray } from "./app-secret-array";

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

    public scopes: AppScopeArray = new AppScopeArray();
    public origins: AppOriginArray = new AppOriginArray();
    public secrets: AppSecretArray = new AppSecretArray();

    public hasScopes(): boolean {
        let relation: Relation = (this._links as ClientApplicationRelation).scopes;
        return relation != null && !relation.isEmpty();
    }

    public hasOrigins(): boolean {
        let relation: Relation = (this._links as ClientApplicationRelation).origins;
        return relation != null && !relation.isEmpty();
    }

    public hasSecrets(): boolean {
        let relation: Relation = (this._links as ClientApplicationRelation).secrets;
        return relation != null && !relation.isEmpty();
    }
}