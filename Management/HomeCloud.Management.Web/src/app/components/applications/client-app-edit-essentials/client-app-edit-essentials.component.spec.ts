import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ClientAppEditEssentialsComponent } from './client-app-edit-essentials.component';

describe('ClientAppEditEssentialsComponent', () => {
  let component: ClientAppEditEssentialsComponent;
  let fixture: ComponentFixture<ClientAppEditEssentialsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ClientAppEditEssentialsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ClientAppEditEssentialsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
