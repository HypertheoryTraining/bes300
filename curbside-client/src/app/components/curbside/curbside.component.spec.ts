import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CurbsideComponent } from './curbside.component';

describe('CurbsideComponent', () => {
  let component: CurbsideComponent;
  let fixture: ComponentFixture<CurbsideComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CurbsideComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CurbsideComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
