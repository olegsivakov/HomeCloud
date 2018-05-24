import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ApiAppDetailsComponent } from './api-app-details.component';

describe('ApiAppDetailsComponent', () => {
  let component: ApiAppDetailsComponent;
  let fixture: ComponentFixture<ApiAppDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ApiAppDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ApiAppDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
