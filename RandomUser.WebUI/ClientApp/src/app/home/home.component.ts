import { Component } from '@angular/core';
import { IUserModel } from '../../interfaces/IUserModel';
import { UserService } from '../../services/user.service';
import { faUser, faCalendar, faPhone, faPencilAlt, faSyncAlt } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  userModel: IUserModel;
  nameVisible = true;
  phoneVisible = false;
  dobVisible = false;

  faUser = faUser;
  faCalendar = faCalendar;
  faPhone = faPhone;
  faPencilAlt = faPencilAlt;
  faSyncAlt = faSyncAlt;

  constructor(
    private userService: UserService) {

    this.getRandom();
  }

  onMouseoverName() {
    this.nameVisible = true;
    this.phoneVisible = false;
    this.dobVisible = false;
  };

  onMouseoverPhone() {
    this.nameVisible = false;
    this.phoneVisible = true;
    this.dobVisible = false;
  };

  onMouseoverDob() {
    this.nameVisible = false;
    this.phoneVisible = false;
    this.dobVisible = true;
  };

  getRandom() {
    this.userService
      .getRandom()
      .subscribe(
        result => { this.userModel = result; },
        error => console.error(error));
  };
}
