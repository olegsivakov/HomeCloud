import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ClientAppListComponent } from './client-app-list.component';

describe('ClientAppListComponent', () => {
  let component: ClientAppListComponent;
  let fixture: ComponentFixture<ClientAppListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ClientAppListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ClientAppListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
