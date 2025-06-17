import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-register',
  imports: [FormsModule, CommonModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  confirmPassword!: string;
  password!: string;
  username!: string;

  onRegister() {
    if (this.password !== this.confirmPassword) {
      alert('Passwords do not match!');
      return;
    }

    // Here you would typically send the registration data to your backend
    console.log('Registration successful for:', this.username);
    // Reset form fields
    this.username = '';
    this.password = '';
    this.confirmPassword = '';
  }
}
