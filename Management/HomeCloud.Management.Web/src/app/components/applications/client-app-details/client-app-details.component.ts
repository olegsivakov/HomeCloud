import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { ClientApplication } from '../../../models/applications/client-application';

@Component({
  selector: 'app-client-app-details',
  templateUrl: './client-app-details.component.html',
  styleUrls: ['./client-app-details.component.css']
})
export class ClientAppDetailsComponent implements OnInit {

  @Input('application')
  public application: ClientApplication = null;

  @Output('close')
  closeEmiiter = new EventEmitter<ClientApplication>();

  constructor() { }

  ngOnInit() {
  }

  public close() {
    this.closeEmiiter.emit(this.application);
    this.application = null;
  }
}
