import { Injectable } from '@angular/core';
import { Subject } from 'rxjs/Subject';

@Injectable()
export class RightPanelService {

  private visibilityChangedSource = new Subject<boolean>();

  visibilityChanged$ = this.visibilityChangedSource.asObservable();

  constructor() { }

  public show() {
    this.visibilityChangedSource.next(true);
  }

  public hide() {
    this.visibilityChangedSource.next(false);
  }

}
