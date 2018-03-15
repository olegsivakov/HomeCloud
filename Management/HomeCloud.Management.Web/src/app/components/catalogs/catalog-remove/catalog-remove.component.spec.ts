import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CatalogRemoveComponent } from './catalog-remove.component';

describe('CatalogRemoveComponent', () => {
  let component: CatalogRemoveComponent;
  let fixture: ComponentFixture<CatalogRemoveComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CatalogRemoveComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CatalogRemoveComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
