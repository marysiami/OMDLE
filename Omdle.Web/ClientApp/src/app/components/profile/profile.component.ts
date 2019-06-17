import { Component } from "@angular/core";
import { AuthService } from "../../services/auth.service";
import { Router } from "@angular/router";
import { User } from "../../models/user.model";
import { ProfileService } from '../../services/profile.service';

@Component({
  selector: "app-profile",
  templateUrl: "./profile.component.html",
  styleUrls: ["./profile.component.css"]
})
export class ProfileComponent {
  info = {};

  isLoggedIn: boolean;

  constructor(
    private authService: AuthService,
    private profileService: ProfileService
  ) {
  }

  ngOnInit() {
    this.isLoggedIn = this.authService.isLoggedIn();
    this.profileService.getUserInfo(this.authService.getUserId())
      .subscribe(result => this.info = result);
  }

}
