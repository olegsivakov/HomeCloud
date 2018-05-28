import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ClientAppDetailsEssentialsComponent } from './client-app-details-essentials.component';

describe('ClientAppDetailsEssentialsComponent', () => {
  let component: ClientAppDetailsEssentialsComponent;
  let fixture: ComponentFixture<ClientAppDetailsEssentialsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ClientAppDetailsEssentialsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ClientAppDetailsEssentialsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
