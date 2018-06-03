import { Component, OnInit, Input } from '@angular/core';
import { AppOriginArray } from '../../../models/applications/app-origin-array';

@Component({
  selector: 'app-client-app-details-origins',
  templateUrl: './client-app-details-origins.component.html',
  styleUrls: ['./client-app-details-origins.component.css']
})
export class ClientAppDetailsOriginsComponent implements OnInit {

  @Input('origins')
  public origins: AppOriginArray = new AppOriginArray();

  constructor() { }

  ngOnInit() {
  }

}
