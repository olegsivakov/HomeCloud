import { Component, OnInit, Input } from '@angular/core';
import { AppSecretArray } from '../../../models/applications/app-secret-array';

@Component({
  selector: 'app-client-app-details-secrets',
  templateUrl: './client-app-details-secrets.component.html',
  styleUrls: ['./client-app-details-secrets.component.css']
})
export class ClientAppDetailsSecretsComponent implements OnInit {

  @Input('secrets')
  public secrets: AppSecretArray = new AppSecretArray();

  constructor() { }

  ngOnInit() {
  }

}
