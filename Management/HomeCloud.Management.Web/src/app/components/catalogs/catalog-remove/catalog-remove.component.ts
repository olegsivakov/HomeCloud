import { Component, OnInit, OnDestroy, Input, Output, EventEmitter } from '@angular/core';
import { ISubscription } from 'rxjs/Subscription';

import { Catalog } from '../../../models/catalog';
import { CatalogState } from '../../../models/catalog-state';
import { CatalogService } from '../../../services/catalog/catalog.service';

@Component({
  selector: 'app-catalog-remove',
  templateUrl: './catalog-remove.component.html',
  styleUrls: ['./catalog-remove.component.css']
})
export class CatalogRemoveComponent implements OnInit, OnDestroy {

  private catalog: Catalog = null;  
  private stateChangedSubscription: ISubscription = null;

  @Output('remove')
  removeEmitter = new EventEmitter<Catalog>();

  @Output('cancel')
  cancelEmitter = new EventEmitter<Catalog>();

  constructor(private catalogService: CatalogService) {    
    this.stateChangedSubscription = this.catalogService.stateChanged$.subscribe(args => {
      if (args.state == CatalogState.remove) {
        this.catalog = args.catalog;
      }
      else {
        this.catalog = null;
      }
    });
  }

  private onRemove() {
    this.removeEmitter.emit(this.catalog);

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
