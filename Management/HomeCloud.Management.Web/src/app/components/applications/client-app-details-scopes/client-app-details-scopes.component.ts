import { Component, OnInit, Input } from '@angular/core';
import { AppScopeArray } from '../../../models/applications/app-scope-array';

@Component({
  selector: 'app-client-app-details-scopes',
  templateUrl: './client-app-details-scopes.component.html',
  styleUrls: ['./client-app-details-scopes.component.css']
})
export class ClientAppDetailsScopesComponent implements OnInit {

  @Input('scopes')
  public scopes: AppScopeArray = new AppScopeArray();

  constructor() { }

  ngOnInit() {
  }

}
