import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ClientAppDetailsOriginsComponent } from './client-app-details-origins.component';

describe('ClientAppDetailsOriginsComponent', () => {
  let component: ClientAppDetailsOriginsComponent;
  let fixture: ComponentFixture<ClientAppDetailsOriginsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ClientAppDetailsOriginsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ClientAppDetailsOriginsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
