import { Component, Input, OnInit } from '@angular/core';

import { Catalog } from '../../../models/catalog';
import { RightPanelService } from '../../../services/shared/right-panel/right-panel.service';

@Component({  
  selector: 'app-catalog',
  templateUrl: './catalog.component.html',
  styleUrls: ['./catalog.component.css']
})
export class CatalogComponent implements OnInit {

  @Input()
  public catalog: Catalog = null;

  private isDetailed: boolean = false;
  private isEditable: boolean = false;
  private isRemovable: boolean = false;

  constructor(
    private rightPanelService: RightPanelService) { }

  ngOnInit() {
  }

  private setDetailed(catalog: Catalog) {
    this.cancel();
    this.isDetailed = true;
    this.rightPanelService.show();
  }

  private setEditable(catalog: Catalog) {
    this.cancel();
    this.isEditable = true;
  }

  private setRemovable(catalog: Catalog) {
    this.cancel();
    this.isRemovable = true;
  }

  private save(catalog: Catalog) {
    this.catalog = catalog;
    this.cancel();

  }

  private cancel() {
    this.isEditable = false;
    this.isRemovable = false;
    this.isDetailed = false;
  }
}
