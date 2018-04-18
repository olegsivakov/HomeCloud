import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CatalogEntryCardComponent } from './catalog-entry-card.component';

describe('CatalogEntryCardComponent', () => {
  let component: CatalogEntryCardComponent;
  let fixture: ComponentFixture<CatalogEntryCardComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CatalogEntryCardComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CatalogEntryCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
