import { Injectable } from '@angular/core';

import { PagedArray } from '../../models/paged-array';
import { Resource } from '../../models/http/resource';
import { ResourceArray } from '../../models/http/resource-array';
import { RelationArray } from '../../models/http/relation-array';
import { Relation } from '../../models/http/relation';
import { StorageRelation } from '../../models/storage-relation';
import { CatalogRelation } from '../../models/catalog-relation';

const KnownTypes: Array<{ new(): any }> = [Resource, ResourceArray, RelationArray, Relation, StorageRelation, CatalogRelation];

@Injectable()
export class CloneableService {

  constructor() { }

  public clone<T>(initializer: new () => T, source: any): T {
    let result: T = new initializer();

    return this.cloneProperty(source, result);
  }

  public cloneArray<T, TArray extends Array<T>>(arrayInitializer: new () => TArray, typeInitializer: new () => T, source: Array<any>): TArray {
      let result: TArray = new arrayInitializer();
      for (let item of source) {
          let resultItem: T = this.clone<T>(typeInitializer, item);
          result.push(resultItem);
      }

      return result;
  }

  public isKnownType(typeName: string): boolean {
    let knownType = KnownTypes.find(knownType => knownType.name.toLowerCase() == typeName.toLowerCase());
    if (knownType) {
        return true;
    }

    return false;
  }

  private cloneProperty(source: any, destination: any): any {
      let properties: Array<string> = Object.keys(source);
      for (let property of properties) {
              if (Object.prototype.hasOwnProperty.call(destination, property)) {
                  if (destination[property] != null && this.isKnownType(destination[property].constructor.name)) {
                      destination[property] = this.cloneProperty(source[property], destination[property]);
                  }
                  else if (source[property] instanceof Array && destination[property] instanceof Array) {
                      source[property].forEach(item => {
                          let destinationItem: any = this.cloneProperty(item, Object.assign({}, item));
                          destination[property].push(destinationItem);
                      });
                  }
                  else {
                      destination[property] = source[property];
                  } 
              }
          }

      return destination;
  }
}
