import { Component, OnInit } from '@angular/core';
import { HeaderComponent } from './home/header/header.component';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  imports: [HeaderComponent, CommonModule, RouterOutlet],
  standalone: true
})
export class AppComponent {
  title = 'DocumentManagementUI';
}
