import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-contact',
  imports: [FormsModule],
  templateUrl: './contact.component.html',
  styleUrl: './contact.component.css'
})
export class ContactComponent {
  onSubmit(name: string, email: string, message: string) {
    if (name !== '' && email !== '' && message !== '') {
      // Here you would typically handle the contact form submission logic, e.g., call a service
      alert(`Thank you for your message, ${name}! We will get back to you at ${email}.`);
      // Reset the form fields after submission
      (document.getElementById('contactForm') as HTMLFormElement).reset();
    } else {
      alert('Please fill in all fields.');
    }
  }
}
