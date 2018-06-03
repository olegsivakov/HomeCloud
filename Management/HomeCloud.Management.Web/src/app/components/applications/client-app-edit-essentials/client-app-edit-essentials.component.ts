import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { ClientApplication } from '../../../models/applications/client-application';
import { GrantType } from '../../../models/grants/grant-type';

@Component({
  selector: 'app-client-app-edit-essentials',
  templateUrl: './client-app-edit-essentials.component.html',
  styleUrls: ['./client-app-edit-essentials.component.css']
})
export class ClientAppEditEssentialsComponent implements OnInit {

  @Input('application')
  public application: ClientApplication = null;

  @Input('grantTypes')
  public grantTypes: Array<GrantType> = new Array<GrantType>();

  @Output('save')
  public saveEmitter = new EventEmitter();

  @Output('cancel')
  public cancelEmitter = new EventEmitter();

  constructor() { }

  ngOnInit() {
  }

  public onCancel() {
    this.cancelEmitter.emit();
  }

  public onSave() {
    this.saveEmitter.emit(this.application);
  }
}
