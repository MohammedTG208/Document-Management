import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CradComponent } from './crad.component';

describe('CradComponent', () => {
  let component: CradComponent;
  let fixture: ComponentFixture<CradComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CradComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CradComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
