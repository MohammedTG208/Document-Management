import { Component, inject, signal } from '@angular/core';
import {  FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService, ValidationTwoInput } from '../Auth/authService';
import { Router } from '@angular/router';
import { NgIf } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';




@Component({
  selector: 'app-register',
  imports: [ReactiveFormsModule, NgIf, HttpClientModule],
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
  standalone: true,
})
export class RegisterComponent {

  private  authService = inject(AuthService);
  private roter = inject(Router);
  message = signal<string>('');
  errorMessage: boolean = false;

  registerForm = new FormGroup({
    username: new FormControl('', { validators: [Validators.required, Validators.maxLength(20), Validators.minLength(4)] }),
    password: new FormControl('', { validators: [Validators.required, Validators.maxLength(25), Validators.minLength(8)] }),
    confirmPassword: new FormControl('', { validators: [Validators.required, Validators.maxLength(25), Validators.minLength(8),] })
  }, { validators: [ValidationTwoInput("password", 'confirmPassword')] });

  

  onRegister() {
    this.authService.register({ username: this.registerForm.controls.username.value, password: this.registerForm.controls.password!.value }).subscribe({
      next: () => {
        this.errorMessage = false;
        this.message.set('');
      },
      error: (error) => {
          this.errorMessage = true;
          this.message.set(error.error?.error || 'Registration failed try again later');
        
      },
      complete: () => {
        this.roter.navigate(['login']);
      }
    });
  }
}
