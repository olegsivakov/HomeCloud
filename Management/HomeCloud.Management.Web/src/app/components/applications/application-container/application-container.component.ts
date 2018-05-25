import { Component, OnInit, OnDestroy } from '@angular/core';

import { ApplicationType } from '../../../models/applications/application-type';

@Component({
  selector: 'app-application-container',
  templateUrl: './application-container.component.html',
  styleUrls: ['./application-container.component.css']
})
export class ApplicationContainerComponent implements OnInit, OnDestroy {
  public selected: ApplicationType = null;
  public types: Array<any> = new Array<any>();

  constructor() { }

  ngOnInit() {
    this.types.push({
      value: ApplicationType.ClientApp,
      shape: "world"
    });

    this.types.push({
      value: ApplicationType.ApiApp,
      shape: "application"
    });

    this.select(ApplicationType.ClientApp);
  }

  ngOnDestroy(): void {
    this.types.slice(0);
  }

  public select(type: ApplicationType) {
    if (this.selected != type) {
      this.selected = type;
    }
  }

  public get isClientAppSelected(): boolean {
    return this.selected == ApplicationType.ClientApp;
  }

  public get isApiAppSelected(): boolean {
    return this.selected == ApplicationType.ApiApp;
  }
}
