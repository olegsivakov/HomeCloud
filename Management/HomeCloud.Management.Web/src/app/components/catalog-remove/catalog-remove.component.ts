import { Component, OnInit, OnDestroy } from '@angular/core';
import { ISubscription } from 'rxjs/Subscription';
import { Catalog } from '../../models/catalog';
import { CatalogService } from '../catalog/catalog.service';
import { NotificationService } from '../shared/notification/notification.service';

@Component({
  selector: 'app-catalog-remove',
  templateUrl: './catalog-remove.component.html',
  styleUrls: ['./catalog-remove.component.css']
})
export class CatalogRemoveComponent implements OnInit, OnDestroy {

  private removeRequestedSubscription: ISubscription = null;
  private catalogRemovedSubscription: ISubscription = null;
  private catalogRemoveFailedSubscription: ISubscription = null;

  public catalog: Catalog = null;

  constructor(private catalogService: CatalogService, private notificationService: NotificationService) { }

  ngOnInit() {
    this.removeRequestedSubscription = this.catalogService.removeRequested$.subscribe(catalog => {
      this.catalog = catalog;
    });

    this.catalogRemovedSubscription = this.catalogService.catalogRemoved$.subscribe(catalog => {
      this.notificationService.warning("Catalog removed", "Catalog" + catalog.Name + " has been removed successfully");

      this.catalog = null;
    });

    this.catalogRemoveFailedSubscription = this.catalogService.catalogRemovedFailed$.subscribe(catalog => {
      this.catalog = catalog;
    });
  }

  ngOnDestroy(): void {
    if (this.removeRequestedSubscription) {
      this.removeRequestedSubscription.unsubscribe();
      this.removeRequestedSubscription = null;
    }

    if (this.catalogRemovedSubscription) {
      this.catalogRemovedSubscription.unsubscribe();
      this.catalogRemovedSubscription = null;
    }

    if (this.catalogRemoveFailedSubscription) {
      this.catalogRemoveFailedSubscription.unsubscribe();
      this.catalogRemoveFailedSubscription = null;
    }

    this.catalog = null;
  }

  public confirm() {
    this.catalogService.remove(this.catalog);

  }

  public cancel() {
    this.catalog = null;
  }
}
