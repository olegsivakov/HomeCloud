import { Component, OnInit, Input, Output, OnChanges, EventEmitter, SimpleChanges } from '@angular/core';
import { ClientApplication } from '../../../models/applications/client-application';
import { ClientAppDetailsTab } from '../../../models/applications/client-app-details-tab';
import { GrantType } from '../../../models/grants/grant-type';

@Component({
  selector: 'app-client-app-details',
  templateUrl: './client-app-details.component.html',
  styleUrls: ['./client-app-details.component.css']
})
export class ClientAppDetailsComponent implements OnInit, OnChanges {

  public get isEsentialsTabActive(): boolean {
    return this.currentTab == ClientAppDetailsTab.essentials;
  }
  public set isEsentialsTabActive(value: boolean) {
    this.changeTab(ClientAppDetailsTab.essentials);
  }

  public get isOriginsTabActive(): boolean {
    return this.currentTab == ClientAppDetailsTab.origins;
  }
  public set isOriginsTabActive(value: boolean) {
    this.changeTab(ClientAppDetailsTab.origins);
  }

  public get isSecretsTabActive(): boolean {
    return this.currentTab == ClientAppDetailsTab.secrets;
  }
  public set isSecretsTabActive(value: boolean) {
    this.changeTab(ClientAppDetailsTab.secrets);
  }

  public get isScopesTabActive(): boolean {
    return this.currentTab == ClientAppDetailsTab.scopes;
  }
  public set isScopesTabActive(value: boolean) {
    this.changeTab(ClientAppDetailsTab.scopes);
  }
  
  public isEditable: boolean = false;  
  public currentTab: ClientAppDetailsTab = null;

  @Input('application')
  public application: ClientApplication = null;
  
  @Input('grantTypes')
  public grantTypes: Array<GrantType> = new Array<GrantType>();

  @Output('save')
  saveEmitter = new EventEmitter();

  @Output('close')
  closeEmitter = new EventEmitter();

  @Output('tabChanged')
  tabChangedEmitter = new EventEmitter<ClientAppDetailsTab>();

  constructor() { }

  ngOnInit() {
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (!changes.application.previousValue || changes.application.previousValue.id != changes.application.currentValue.id) {
      this.changeTab(ClientAppDetailsTab.essentials);
    }
  }

  public changeTab(value: ClientAppDetailsTab) {
    this.currentTab = value;
    this.tabChangedEmitter.emit(this.currentTab);
  }

  public onEdit() {
    this.isEditable = true;
  }

  public onSave(application: ClientApplication) {
    this.saveEmitter.emit(application);
    this.onCancel();
  }

  public onCancel() {
    this.isEditable = false;
  }

  public onClose() {
    this.closeEmitter.emit();
    this.application = null;
  }
}
