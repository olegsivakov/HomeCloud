import { Component, Input, OnInit, OnDestroy } from '@angular/core';
import { ISubscription } from 'rxjs/Subscription';

import { Catalog } from '../../models/catalog';
import { CatalogCommand } from '../../models/commands/catalog-command';
import { CatalogService } from '../../services/catalog/catalog.service';

@Component({
  selector: 'app-catalog-edit',
  templateUrl: './catalog-edit.component.html',
  styleUrls: ['./catalog-edit.component.css']
})
export class CatalogEditComponent implements OnInit, OnDestroy {

  private savingSubscription: ISubscription = null;

  private command: CatalogCommand = null;
  
  constructor(private catalogService: CatalogService) { }

  public get isVisible(): boolean {
    return this.catalog != null;
  }

  public get catalog(): Catalog {
    return this.command != null ? this.command.catalog : null;
  }

  public confirm() {
    this.catalogService.executeSaveCommand(this.command);

    this.cancel();
  }

  public cancel() {  
    this.command = null;
  }

  ngOnInit() {
    this.savingSubscription = this.catalogService.saving$.subscribe(command => {
      this.command = command;
    });
  }

  ngOnDestroy(): void {
    if (this.savingSubscription) {
      this.savingSubscription.unsubscribe();
      this.savingSubscription = null;
    }

    this.cancel();
  }
}
