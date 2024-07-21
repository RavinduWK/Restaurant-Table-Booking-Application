import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-register-restaurant',
  templateUrl: './register-restaurant.component.html',
  styleUrls: ['./register-restaurant.component.css'],
})
export class RegisterRestaurantComponent implements OnInit {
  currentStep: number = 0;
  steps!: HTMLElement[];
  formSteps!: HTMLElement[];

  ngOnInit() {
    this.steps = Array.from(
      document.querySelectorAll('.step')
    ) as HTMLElement[];
    this.formSteps = Array.from(
      document.querySelectorAll('.form-step')
    ) as HTMLElement[];
    this.updateStepIndicator();
  }

  updateStepIndicator() {
    this.steps.forEach((step, index) => {
      step.classList.toggle('active', index === this.currentStep);
    });
    this.formSteps.forEach((formStep, index) => {
      formStep.classList.toggle('active', index === this.currentStep);
    });
  }

  nextStep() {
    this.currentStep = Math.min(this.currentStep + 1, this.steps.length - 1);
    this.updateStepIndicator();
  }

  prevStep() {
    this.currentStep = Math.max(this.currentStep - 1, 0);
    this.updateStepIndicator();
  }
}
