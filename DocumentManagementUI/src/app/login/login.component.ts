import { Component, inject, signal } from '@angular/core';
import {  FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService, storeToken } from '../Auth/authService';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-login',
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  errorMessage: boolean = false;
  message = signal<string>('');
  private router = inject(Router);
  loginForm = new FormGroup({
    userName: new FormControl('', { validators: [Validators.required, Validators.email] }),
    password: new FormControl('', { validators: [Validators.required, Validators.maxLength(25), Validators.minLength(8)] })
  });
  private authService = inject(AuthService);

  onSubmit() {
    this.authService.login({
      userName: this.loginForm.controls.userName.value,
      password: this.loginForm.controls.password.value
    }).subscribe({
      next: (response) => {
        this.errorMessage = false;
        this.message.set('');
        storeToken(response);

        this.authService.setLoginStatus(true);  
        this.router.navigate(['']);              
      },
      error: (error) => {
        this.errorMessage = true;
        this.message.set(error.error?.error || 'Login failed try again later');
      }
    });
  }
}
