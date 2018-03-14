import { Injectable } from '@angular/core';
import { Subject } from 'rxjs/Subject';

@Injectable()
export class ProgressService {

  private progressChangedSource: Subject<boolean> = new Subject<boolean>();

  progressChanged$ = this.progressChangedSource.asObservable();

  constructor() { }

  public show(): void {
    this.progressChangedSource.next(true);
  }

  public hide(): void {
    this.progressChangedSource.next(false);
  }
}
