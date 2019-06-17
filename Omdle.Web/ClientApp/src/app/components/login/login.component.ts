import { Component } from "@angular/core";
import { AuthService } from "./../../services/auth.service";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { AuthService as SocialAuthService, FacebookLoginProvider, SocialUser } from "angularx-social-login";

@Component({
  selector: "app-login",
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.css"]
})

export class LoginComponent {
  loginForm: FormGroup;
  message: string;
  private user: SocialUser;
  private loggedIn: boolean;

  constructor(private authService: AuthService,
    private socialAuthService: SocialAuthService,
    private formBuilder: FormBuilder,
    private router: Router) {
    this.loginForm = this.formBuilder.group({
      username: [
        "",
        [Validators.required]
      ],
      password: [
        "",
        [Validators.required]
      ]
    });

  }

  ngOnInit() {
    this.socialAuthService.authState.subscribe((user) => {
      this.user = user;
    });
  }

  submit() {
    this.authService
      .login(this.loginForm.controls.username.value, this.loginForm.controls.password.value)
      .subscribe(result => {
        localStorage.setItem("token", result);
        this.message = "";
        this.authService.isLoggedIn();
        this.router.navigate(["/"]);
      },
        error => this.message = "Username lub hasło jest niepoprawne.");
  }

  signInWithFB() {
    this.socialAuthService.signIn(FacebookLoginProvider.PROVIDER_ID)
      .then(() => {
        this.authService.socialLogin(this.user.email, this.user.firstName, this.user.lastName, this.user.email)
          .subscribe(result => {
            localStorage.setItem("token", result);
            this.message = "";
            this.authService.isLoggedIn();
            this.router.navigate(["/"]);
            },
            error => this.message = "Blad logowania!");
      });
  }
}
