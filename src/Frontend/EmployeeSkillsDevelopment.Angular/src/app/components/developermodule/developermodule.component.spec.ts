import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DevelopermoduleComponent } from './developermodule.component';

describe('DevelopermoduleComponent', () => {
  let component: DevelopermoduleComponent;
  let fixture: ComponentFixture<DevelopermoduleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DevelopermoduleComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DevelopermoduleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
