import { Component } from '@angular/core';
import { IUserModel } from '../../interfaces/IUserModel';
import { ActivatedRoute } from '@angular/router';
import { UserService } from '../../services/user.service'
import { Location } from '@angular/common';
import { faCheck, faTimes, faArrowLeft } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-user-detail',
  templateUrl: './user-detail.component.html',
})
export class UserDetailComponent {
  public userModel: IUserModel;
  public userId: string;
  faCheck = faCheck;
  faTimes = faTimes;
  faArrowLeft = faArrowLeft;

  constructor(
    private userService: UserService,
    private route: ActivatedRoute,
    private location: Location) {

    route.paramMap.subscribe(params => {
      this.userId = params.get("id")
    });

    this.userService
      .get(this.userId)
      .subscribe(
        result => { this.userModel = result; },
        error => console.error(error));
  }

  gotoBack() {
    this.location.back();
  }

  save() {
    this.userService
      .update(this.userModel)
      .subscribe(
        result => { this.gotoBack() },
        error => console.error(error));     
  }

  delete() {
    if (confirm('Are you sure?')) {
      this.userService
        .delete(this.userId)
        .subscribe(
          result => { this.gotoBack() },
          error => console.error(error));
    }
  }
}
