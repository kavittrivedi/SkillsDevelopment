import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TestermoduleComponent } from './testermodule.component';

describe('TestermoduleComponent', () => {
  let component: TestermoduleComponent;
  let fixture: ComponentFixture<TestermoduleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [TestermoduleComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TestermoduleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
