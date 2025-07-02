import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { Role } from '../../../../Directive/role.drective';

@Component({
  selector: 'app-header',
  standalone: true,
  templateUrl: './header.component.html',
  styleUrl: './header.component.css',
  imports: [RouterLink, Role]
})
export class HeaderComponent {
}
