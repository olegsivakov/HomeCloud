import { Injectable } from '@angular/core';
import { HttpService } from '../http/http.service';
import { Grant } from '../../models/grants/grant';
import { ResourceService } from '../resource/resource.service';
import { PagedArray } from '../../models/paged-array';
import { GrantType } from '../../models/grants/grant-type';
import { Relation } from '../../models/http/relation';
import { HttpMethod } from '../../models/http/http-method';
import { Observable } from 'rxjs/Observable';
import { ResourceArray } from '../../models/http/resource-array';

const grantUrl: string = "http://localhost:57713/v1/grants";
const grantTypeRelation: Relation = new Relation();
grantTypeRelation.href = grantUrl + "/types";
grantTypeRelation.method = HttpMethod.get;
grantTypeRelation.type = "application/json";

@Injectable()
export class GrantService extends HttpService<Grant> {

  constructor(
    protected resourceService: ResourceService
  ) {
    super(Grant, resourceService, );

  }

  public listTypes(): Observable<PagedArray<GrantType>> {
    return this.relation<GrantType>(GrantType, grantTypeRelation).map(resource => {
      if (resource instanceof ResourceArray) {
        return resource.items;
      }

      return new PagedArray<GrantType>();
    });
  }
}
