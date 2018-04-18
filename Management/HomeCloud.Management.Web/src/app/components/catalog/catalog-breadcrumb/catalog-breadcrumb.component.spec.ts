import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CatalogBreadcrumbComponent } from './catalog-breadcrumb.component';

describe('CatalogBreadcrumbComponent', () => {
  let component: CatalogBreadcrumbComponent;
  let fixture: ComponentFixture<CatalogBreadcrumbComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CatalogBreadcrumbComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CatalogBreadcrumbComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
