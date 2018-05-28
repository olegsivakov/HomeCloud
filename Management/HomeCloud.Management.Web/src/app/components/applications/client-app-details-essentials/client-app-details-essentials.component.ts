import { Component, OnInit, Input } from '@angular/core';
import { ClientApplication } from '../../../models/applications/client-application';

@Component({
  selector: 'app-client-app-details-essentials',
  templateUrl: './client-app-details-essentials.component.html',
  styleUrls: ['./client-app-details-essentials.component.css']
})
export class ClientAppDetailsEssentialsComponent implements OnInit {

@Input('application')
public application: ClientApplication = null;

  constructor() { }

  ngOnInit() {
  }

}
