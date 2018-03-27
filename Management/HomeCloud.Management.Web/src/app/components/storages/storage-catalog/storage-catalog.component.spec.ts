import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { StorageCatalogComponent } from './storage-catalog.component';

describe('StorageCatalogComponent', () => {
  let component: StorageCatalogComponent;
  let fixture: ComponentFixture<StorageCatalogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StorageCatalogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StorageCatalogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
