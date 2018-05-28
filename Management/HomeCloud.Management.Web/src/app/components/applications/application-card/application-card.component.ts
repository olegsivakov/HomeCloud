import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Application } from '../../../models/applications/application';
import { Event } from '_debugger';

@Component({
  selector: 'app-application-card',
  templateUrl: './application-card.component.html',
  styleUrls: ['./application-card.component.css']
})
export class ApplicationCardComponent implements OnInit {

  private isLoaded: boolean = false;

  @Input('application')
  public application: Application = null;

  @Input('selected')
  public isSelected: boolean = false;

  @Output('load')
  load = new EventEmitter<Application>();

  @Output('select')
  select = new EventEmitter<Application>();

  @Output('remove')
  remove = new EventEmitter<Application>();

  constructor() {
  }

  ngOnInit() {
  }

  public get canLoad(): boolean {
    return this.application.hasGet && this.application.hasGet();
  }
  public onLoad() {
    if (!this.isLoaded && this.canLoad) {
      this.load.emit(this.application);
    }
  }
  public get canSelect(): boolean {
    return true;
  }
  public onSelect() {
    this.select.emit(this.application);
  }

  public get canRemove(): boolean {
    return this.application.hasDelete && this.application.hasDelete();
  }
  public onRemove(): void {
    if (this.canRemove) {
      this.remove.emit(this.application);
    }
  }
}
