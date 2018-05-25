import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Application } from '../../../models/applications/application';
import { GrantType } from '../../../models/grants/grant-type';

@Component({
  selector: 'app-application-create',
  templateUrl: './application-create.component.html',
  styleUrls: ['./application-create.component.css']
})
export class ApplicationCreateComponent implements OnInit {

  public application: Application = null;
  private _isVisible: boolean = false;
  

  @Input('header')
  public header: string = "";

  @Input('grantTypes')
  public grantTypes: Array<GrantType> = new Array<GrantType>();

  @Input('errors')
  public errors: Array<string> = new Array<string>();

  @Input('visible')
  public set isVisible(value: boolean) {
    this._isVisible = value;
  }
  public get isVisible(): boolean {
    return this._isVisible && this.application != null;
  }

  @Output('save')
  saveEmitter = new EventEmitter<Application>();

  @Output('change')
  changeEmitter = new EventEmitter<Application>();

  @Output('cancel')
  cancelEmitter = new EventEmitter();

  constructor() { }

  public onSave() {
    this.saveEmitter.emit(this.application);
    this.onCancel();
  }

  public onChange() {
    this.changeEmitter.emit(this.application);
  }

  public onCancel() {
    this.cancelEmitter.emit();
    this.application = null;
  }

  ngOnInit() {
    this.application = new Application();
  }
}
