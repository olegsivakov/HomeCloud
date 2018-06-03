import { Pipe, PipeTransform } from '@angular/core';
import { GrantType } from '../../models/grants/grant-type';

@Pipe({
  name: 'grantTypeExact'
})
export class GrantTypeExactPipe implements PipeTransform {

  transform(value: number, types: Array<GrantType>): string {
    let type: GrantType = types.find(item => item.id == value);
    if (type) {
      return type.name;
    }

    return "";
  }
}
