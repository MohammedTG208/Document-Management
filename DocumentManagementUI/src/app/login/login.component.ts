import { CommonModule } from '@angular/common';
import { Component, ElementRef, ViewChild } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-login',
  imports: [FormsModule, CommonModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  @ViewChild('form') form?: ElementRef<HTMLFormElement>;
  onSubmit(email: string, password: string) {
    if (email !=='' || password!=='') {
      // Here you would typically handle the login logic, e.g., call an authentication service
      alert('Login successful for:' + email);

      // Reset the form after submission
      this.form?.nativeElement.reset();
    }
  }
}
