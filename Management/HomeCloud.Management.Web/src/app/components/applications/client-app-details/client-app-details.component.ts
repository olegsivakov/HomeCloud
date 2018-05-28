import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { ClientApplication } from '../../../models/applications/client-application';
import { ClientAppDetailsTab } from '../../../models/applications/client-app-details-tab';

@Component({
  selector: 'app-client-app-details',
  templateUrl: './client-app-details.component.html',
  styleUrls: ['./client-app-details.component.css']
})
export class ClientAppDetailsComponent implements OnInit {

  public currentTab: ClientAppDetailsTab = null;

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

  @Input('application')
  public application: ClientApplication = null;

  @Output('close')
  closeEmiiter = new EventEmitter();

  @Output('tabChanged')
  tabChangedEmitter = new EventEmitter<ClientAppDetailsTab>();

  constructor() { }

  ngOnInit() {
    this.changeTab(ClientAppDetailsTab.essentials);
  }

  public changeTab(value: ClientAppDetailsTab) {
    this.currentTab = value;
    this.tabChangedEmitter.emit(this.currentTab);
  }

  public close() {
    this.closeEmiiter.emit();
    this.application = null;
  }
}
