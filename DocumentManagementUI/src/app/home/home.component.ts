import { Component, inject, OnInit } from '@angular/core';
import { CradComponent } from '../crad/crad.component';
import { CommonModule } from '@angular/common';
import { ContactComponent } from './contact/contact.component';
import { DUMMY_DATA_CARD } from '../../../Dummy_Data/Dummy_Data';
import { CardService } from '../card/card.service';

@Component({
  selector: 'app-home',
  imports: [CradComponent, CommonModule, ContactComponent],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css', '../app.component.css']
})
export class HomeComponent implements OnInit {
 folders: any[any] = [];
 private cardService=inject(CardService);

  ngOnInit() {
    this.loadFolders();
  }

  loadFolders() {
    this.cardService.getThreeFolders().subscribe((data: any[]) => {
      this.folders = data;
    }, error => {
      console.error('Error fetching folders:', error);
    });
  }
}
