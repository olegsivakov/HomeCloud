import { Component, OnInit, Input } from '@angular/core';
import { Application } from '../../../models/applications/application';

@Component({
  selector: 'app-application-card',
  templateUrl: './application-card.component.html',
  styleUrls: ['./application-card.component.css']
})
export class ApplicationCardComponent implements OnInit {

  @Input('application')
  public application: Application = null;

  @Input('selected')
  public isSelected: boolean = false;

  constructor() { }

  ngOnInit() {
  }

  public get canRemove(): boolean {
    return this.application.hasDelete && this.application.hasDelete();
  }
  public onRemove(): void {
    if (this.canRemove) {
      //remove goes here
    }
  }
}
