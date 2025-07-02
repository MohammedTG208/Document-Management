import { Component, OnInit } from '@angular/core';
import { HeaderComponent } from './home/header/header.component';
import { CradComponent } from './crad/crad.component';
import { DUMMY_DATA_CARD } from '../../Dummy_Data/Dummy_Data';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
  imports: [HeaderComponent, CommonModule, RouterOutlet]
})
export class AppComponent {
  title = 'DocumentManagementUI';
}
