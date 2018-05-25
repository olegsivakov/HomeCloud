import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ApiAppListComponent } from './api-app-list.component';

describe('ApiAppListComponent', () => {
  let component: ApiAppListComponent;
  let fixture: ComponentFixture<ApiAppListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ApiAppListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ApiAppListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
