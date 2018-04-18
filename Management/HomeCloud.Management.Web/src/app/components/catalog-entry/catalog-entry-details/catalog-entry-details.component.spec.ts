import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CatalogEntryDetailsComponent } from './catalog-entry-details.component';

describe('CatalogEntryDetailsComponent', () => {
  let component: CatalogEntryDetailsComponent;
  let fixture: ComponentFixture<CatalogEntryDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CatalogEntryDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CatalogEntryDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
