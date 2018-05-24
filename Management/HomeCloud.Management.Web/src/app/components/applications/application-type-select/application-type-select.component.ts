import { Component, OnInit, EventEmitter, Output, Input } from '@angular/core';
import { ApplicationType } from '../../../models/applications/application-type';

@Component({
  selector: 'app-application-type-select',
  templateUrl: './application-type-select.component.html',
  styleUrls: ['./application-type-select.component.css']
})
export class ApplicationTypeSelectComponent implements OnInit {

  public selected: ApplicationType = null;
  public types: Array<any> = new Array<any>();

  @Input('default')
  public default: ApplicationType = null;

  @Output('selected')
  changed = new EventEmitter<ApplicationType>();

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

    this.select(this.default);
  }

  public select(type: ApplicationType) {
    if (this.selected != type) {
      this.selected = type;

      this.changed.emit(this.selected);
    }
  }
}
