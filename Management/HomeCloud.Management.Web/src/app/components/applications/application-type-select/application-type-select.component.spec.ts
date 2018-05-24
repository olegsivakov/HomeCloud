import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ApplicationTypeSelectComponent } from './application-type-select.component';

describe('ApplicationTypeSelectComponent', () => {
  let component: ApplicationTypeSelectComponent;
  let fixture: ComponentFixture<ApplicationTypeSelectComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ApplicationTypeSelectComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ApplicationTypeSelectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
