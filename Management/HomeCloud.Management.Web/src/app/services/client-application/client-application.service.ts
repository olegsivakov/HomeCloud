import { Injectable } from '@angular/core';
import { ResourceService } from '../resource/resource.service';
import { HttpService } from '../http/http.service';

import { Observable } from 'rxjs/Observable';

import { ClientApplication } from '../../models/applications/client-application';
import { Relation } from '../../models/http/relation';
import { PagedArray } from '../../models/paged-array';

const clientApplicationUrl: string = "http://localhost:57713/v1/clients";

@Injectable()
export class ClientApplicationService extends HttpService<ClientApplication> {

  constructor(
    protected resourceService: ResourceService) {
    super(ClientApplication, resourceService, clientApplicationUrl);
  }

  public hasValidate() {
    return this.resources._links.validate != null && !this.resources._links.validate.isEmpty();
  }

  public validate(entity: ClientApplication): Observable<ClientApplication> {
    entity._links.validate = this.resources._links.validate;

    return super.validate(entity);
  }
}
