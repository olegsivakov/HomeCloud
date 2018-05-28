import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ClientAppDetailsScopesComponent } from './client-app-details-scopes.component';

describe('ClientAppDetailsScopesComponent', () => {
  let component: ClientAppDetailsScopesComponent;
  let fixture: ComponentFixture<ClientAppDetailsScopesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ClientAppDetailsScopesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ClientAppDetailsScopesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
