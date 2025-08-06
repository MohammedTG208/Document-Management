import { Component, OnInit } from '@angular/core';
import { CradComponent } from '../crad/crad.component';
import { CommonModule } from '@angular/common';
import { ContactComponent } from './contact/contact.component';
import { DUMMY_DATA_CARD } from '../../../Dummy_Data/Dummy_Data';

@Component({
  selector: 'app-home',
  imports: [CradComponent, CommonModule, ContactComponent],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css', '../app.component.css']
})
export class HomeComponent implements OnInit {
  docInfo = DUMMY_DATA_CARD;
  islogin = false;
  username = 'User';

  ngOnInit(): void {
    this.checkLocalStorege();
  }
  //any name onCardClick077(id: number) will work.
  selectCard(id: number) {
    console.log("This card id " + id);
    // You can add more logic here if needed, like navigating to a detail page.
  }

  checkLocalStorege() {
    let message = localStorage.getItem('message');
    if (message != null) {
      this.islogin = true;
      this.username = JSON.parse(message).username;
    }
  }
}
