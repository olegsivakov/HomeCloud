import { Component, OnInit, OnDestroy } from '@angular/core';
import { ISubscription } from 'rxjs/Subscription';

import { Catalog } from '../../models/catalog';
import { CatalogCommand } from '../../models/commands/catalog-command';
import { CatalogService } from '../../services/catalog/catalog.service';

@Component({
  selector: 'app-catalog-remove',
  templateUrl: './catalog-remove.component.html',
  styleUrls: ['./catalog-remove.component.css']
})
export class CatalogRemoveComponent implements OnInit, OnDestroy {

  private removingSubscription: ISubscription = null;
  private command: CatalogCommand = null;

  constructor(private catalogService: CatalogService) { }

    public get isVisible(): boolean {
      return this.catalog != null;
    }
  
    public get catalog(): Catalog {
      return this.command != null ? this.command.catalog : null;
    }

  ngOnInit() {
    this.removingSubscription = this.catalogService.removing$.subscribe(command => {
      this.command = command;
    });
  }

  ngOnDestroy(): void {
    if (this.removingSubscription) {
      this.removingSubscription.unsubscribe();
      this.removingSubscription = null;
    }

    this.cancel();
  }

  public confirm() {
    this.catalogService.executeRemoveCommand(this.command);

    this.cancel();
  }

  public cancel() {
    this.command = null;
  }
}
