import { Component, OnInit } from '@angular/core';
import { ApplicationType } from '../../../models/applications/application-type';

@Component({
  selector: 'app-application-container',
  templateUrl: './application-container.component.html',
  styleUrls: ['./application-container.component.css']
})
export class ApplicationContainerComponent implements OnInit {

  public defaultType: ApplicationType = ApplicationType.ClientApp;

  constructor() { }

  ngOnInit() {
  }

  public onAppTypeChanged(type: ApplicationType) {

  }

}
