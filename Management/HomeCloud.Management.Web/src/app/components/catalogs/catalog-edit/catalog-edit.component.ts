import { Component, OnInit, OnDestroy, EventEmitter, Output, Input } from '@angular/core';
import { ISubscription } from 'rxjs/Subscription';

import { Catalog } from '../../../models/catalog';
import { CatalogState } from '../../../models/catalog-state';
import { CatalogDataService } from '../../../services/catalog/catalog-data.service';

@Component({
  selector: 'app-catalog-edit',
  templateUrl: './catalog-edit.component.html',
  styleUrls: ['./catalog-edit.component.css']
})
export class CatalogEditComponent implements OnInit, OnDestroy {

  private catalog: Catalog = null;  
  private stateChangedSubscription: ISubscription = null;

  @Output('save')
  saveEmitter = new EventEmitter<Catalog>();

  @Output('cancel')
  cancelEmitter = new EventEmitter<Catalog>();

  constructor(private catalogService: CatalogDataService) {
    this.stateChangedSubscription = this.catalogService.stateChanged$.subscribe(args => {
      if (args.state == CatalogState.edit) {
        this.catalog = Object.create(args.catalog);
      }
      else {
        this.catalog = null;
      }
    });
  }

  private onSave() {
    this.saveEmitter.emit(this.catalog);

    this.onCancel();
  }

  private onCancel() {
    this.cancelEmitter.emit(this.catalog);
    this.catalog = null;
  }

  ngOnInit() {
  }

  ngOnDestroy(): void {
    if (this.stateChangedSubscription) {
      this.stateChangedSubscription.unsubscribe();
      this.stateChangedSubscription = null;
    }

    this.catalog = null;
  }
}
