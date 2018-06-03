import { Injectable } from '@angular/core';
import { ResourceService } from '../resource/resource.service';
import { HttpService } from '../http/http.service';

import { Observable } from 'rxjs/Observable';

import { ClientApplication } from '../../models/applications/client-application';
import { Relation } from '../../models/http/relation';
import { PagedArray } from '../../models/paged-array';
import { environment } from '../../../environments/environment';
import { AppScopeArray } from '../../models/applications/app-scope-array';
import { ClientApplicationRelation } from '../../models/applications/client-application-relation';
import { AppOriginArray } from '../../models/applications/app-origin-array';
import { AppSecretArray } from '../../models/applications/app-secret-array';

@Injectable()
export class ClientApplicationService extends HttpService<ClientApplication> {

  constructor(
    protected resourceService: ResourceService) {
    super(ClientApplication, resourceService, environment.identityServiceUrl + "/clients");
  }

  public hasValidate() {
    return this.resources._links.validate != null && !this.resources._links.validate.isEmpty();
  }

  public validate(entity: ClientApplication): Observable<ClientApplication> {
    entity._links.validate = this.resources._links.validate;

    return super.validate(entity);
  }

  public scopes(entity: ClientApplication): Observable<AppScopeArray> {
    return this.relation(AppScopeArray, (entity._links as ClientApplicationRelation).scopes).map(item => item as AppScopeArray);
  }

  public origins(entity: ClientApplication): Observable<AppOriginArray> {
    return this.relation(AppOriginArray, (entity._links as ClientApplicationRelation).origins).map(item => item as AppOriginArray);
  }

  public secrets(entity: ClientApplication): Observable<AppSecretArray> {
    return this.relation(AppSecretArray, (entity._links as ClientApplicationRelation).secrets).map(item => item as AppSecretArray);
  }
}
