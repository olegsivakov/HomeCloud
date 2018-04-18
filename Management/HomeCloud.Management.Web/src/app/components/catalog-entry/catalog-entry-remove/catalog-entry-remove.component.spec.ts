import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CatalogEntryRemoveComponent } from './catalog-entry-remove.component';

describe('CatalogEntryRemoveComponent', () => {
  let component: CatalogEntryRemoveComponent;
  let fixture: ComponentFixture<CatalogEntryRemoveComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CatalogEntryRemoveComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CatalogEntryRemoveComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
