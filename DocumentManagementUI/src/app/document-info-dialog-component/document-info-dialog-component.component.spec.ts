import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DocumentInfoDialogComponentComponent } from './document-info-dialog-component.component';

describe('DocumentInfoDialogComponentComponent', () => {
  let component: DocumentInfoDialogComponentComponent;
  let fixture: ComponentFixture<DocumentInfoDialogComponentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DocumentInfoDialogComponentComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DocumentInfoDialogComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
