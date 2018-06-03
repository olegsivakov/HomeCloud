import { Component, OnInit, Input } from '@angular/core';
import { ClientApplication } from '../../../models/applications/client-application';
import { GrantType } from '../../../models/grants/grant-type';

@Component({
  selector: 'app-client-app-details-essentials',
  templateUrl: './client-app-details-essentials.component.html',
  styleUrls: ['./client-app-details-essentials.component.css']
})
export class ClientAppDetailsEssentialsComponent implements OnInit {

  @Input('application')
  public application: ClientApplication = null;

  @Input('grantTypes')
  public grantTypes: Array<GrantType> = new Array<GrantType>();

  constructor() { }

  ngOnInit() {
  }
}
