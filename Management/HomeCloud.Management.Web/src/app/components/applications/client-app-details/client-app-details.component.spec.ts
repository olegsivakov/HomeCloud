import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ClientAppDetailsComponent } from './client-app-details.component';

describe('ClientAppDetailsComponent', () => {
  let component: ClientAppDetailsComponent;
  let fixture: ComponentFixture<ClientAppDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ClientAppDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ClientAppDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
