import { Injectable } from '@angular/core';
import { HttpService } from '../http/http.service';
import { HttpClient } from '@angular/common/http';
import { StorageRelation } from '../../models/storage-relation';
import { Storage } from '../../models/storage';
import { Observable } from 'rxjs/Observable';
import { PagedArray } from '../../models/paged-array';

const storageUrl: string = "http://localhost:54832/v1/storages";

@Injectable()
export class StorageService extends HttpService<StorageRelation, Storage> {

  constructor(protected httpClient: HttpClient) {
    super(httpClient, storageUrl);
   }
}
