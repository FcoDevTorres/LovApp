import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';

@Component({
  selector: 'app-nav',
  imports: [FormsModule, BsDropdownModule],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css'
})
export class NavComponent {
  login: any = {};
  isLogged = false;
  accountService = inject(AccountService);

  loginSubmit() {
    console.log("login", this.login);
    this.accountService.loginService({ username: this.login.user, password: this.login.pass }).subscribe({
      next: (response) => {
        console.log("login response", response);
        this.isLogged = true;
      },
      error: (err) => {
        console.error("error", err);
      }
    })
  }

  logout() {
    this.isLogged = false;
  }
}
