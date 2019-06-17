import { Inject, Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import "rxjs/add/operator/map"

import { JwtUtil } from "../utils/jwt.util";

import { LoginRequest } from "../models/request/login-request.model";
import { SocialLoginRequest } from "../models/request/social-login-request.model";
import { RegisterRequest } from "../models/request/register-request.model";
import { BaseService } from "./base.service";
import UserRoles from "../models/UserRoles.model";
import { log } from "util";


@Injectable()
export class AuthService extends BaseService {
  public isLogged: boolean = false;
  public loggedName = "";
  public loggedRole: UserRoles = null;

  constructor(http: HttpClient, @Inject("BASE_URL") baseUrl: string, private jwtUtil: JwtUtil) {
    super(http, baseUrl);
    this.isLoggedIn();
  }

  login(username, password) {
    const loginModel = new LoginRequest(username, password);
    const url = this.baseUrl + "Auth/Login";

    return this.http.post<string>(url, loginModel);
  }

  socialLogin(username, firstname, lastname, email) {
    const loginModel = new SocialLoginRequest(username, firstname, lastname, email);
    const url = this.baseUrl + "Auth/SocialLogin";

    return this.http.post<string>(url, loginModel);
  }

  async logout() {
    this.isLogged = false;
    this.loggedName = "";
    this.loggedRole = null;
    localStorage.removeItem("token");

    return true;
  }

  register(username, firstname, lastname, password, email) {
    const registerModel = new RegisterRequest(username, firstname, lastname, password, email);
    const url = this.baseUrl + "Auth/Register";

    return this.http.post<string>(url, registerModel);
  }

  isLoggedIn() {
    const token = localStorage.getItem("token");

    if (token != null) {
      const decodedToken = this.jwtUtil.decode(localStorage.getItem("token"));
      const currentUnixTimestamp = Math.round((new Date()).getTime() / 1000);

      if (decodedToken.exp > currentUnixTimestamp) {
        this.isLogged = true;
        this.loggedName = decodedToken["given_name"] || "";
        this.loggedRole = decodedToken["role"] || "";
        return true;
      }
    }

    this.isLogged = false;
    return false;
  }

  checkRole(roleName: string) {
    return UserRoles[this.loggedRole.toString().toLowerCase()] >= UserRoles[roleName.toLowerCase()];
  }

  isInRole(roleName) {
    if (localStorage.getItem("token") != null) {
      let decodedToken = this.jwtUtil.decode(localStorage.getItem('token'));
      return decodedToken.role.toLowerCase() === roleName.toLowerCase();
    }
    else {
      return false;
    }
  }

  getUserId() {
    var x = localStorage.getItem("token");
    if (localStorage.getItem("token") == null) {
      return false;
    }
    else {
      let decodedToken = this.jwtUtil.decode(localStorage.getItem('token'));
      return decodedToken.jti;
    }
  }
}
