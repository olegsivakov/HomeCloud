import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CatalogEditComponent } from './catalog-edit.component';

describe('CatalogEditComponent', () => {
  let component: CatalogEditComponent;
  let fixture: ComponentFixture<CatalogEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CatalogEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CatalogEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
