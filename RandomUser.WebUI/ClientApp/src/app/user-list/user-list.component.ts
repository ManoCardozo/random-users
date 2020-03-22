import { Component } from '@angular/core';
import { IUserModel } from '../../interfaces/IUserModel';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html'
})
export class UserListComponent {
  public users: IUserModel[];

  constructor(userService: UserService) {
    userService
      .getList()
      .subscribe(
        result => { this.users = result; },
        error => console.error(error));
  }
}


