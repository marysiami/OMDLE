import { Inject, Injectable } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";
import "rxjs/add/operator/map"

import { BaseService } from "./base.service";
import { User } from "../models/user.model";


@Injectable()
export class ProfileService extends BaseService {

  constructor(http: HttpClient, @Inject("BASE_URL") baseUrl: string) {
    super(http, baseUrl);
  }

  getUserInfo(id) {
    const params = new HttpParams()
      .set("userId", id);

    const url = this.baseUrl + "user/GetUserProfile";

    return this.http.get<User>(url, { params: params, headers: this.headers });
  }
}
