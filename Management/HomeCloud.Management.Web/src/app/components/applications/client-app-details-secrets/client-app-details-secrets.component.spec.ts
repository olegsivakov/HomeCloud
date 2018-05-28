import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ClientAppDetailsSecretsComponent } from './client-app-details-secrets.component';

describe('ClientAppDetailsSecretsComponent', () => {
  let component: ClientAppDetailsSecretsComponent;
  let fixture: ComponentFixture<ClientAppDetailsSecretsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ClientAppDetailsSecretsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ClientAppDetailsSecretsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
