import { BaseService } from "./base.service";
import { HttpClient, HttpParams } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { UserListing } from "../models/user-listing.model";
import { CreateTeacherRequest } from "../models/request/create-teacher-request.model";

@Injectable()
export class AdminService extends BaseService
{
  constructor(http: HttpClient, @Inject("BASE_URL") baseUrl: string)
  {
    super(http, baseUrl);
  }

  getUsers(page, postsPerPage = 10) {
    const url = this.baseUrl + "admin/GetUsers";
    const params = new HttpParams()
      .set("page", page)
      .set("postsPerPage", postsPerPage.toString());

    return this.http.get<UserListing>(url, { params: params, headers: this.headers });
  }
  getTeachers(page, postsPerPage = 10) {
    const url = this.baseUrl + "admin/GetTeachers";
    const params = new HttpParams()
      .set("page", page)
      .set("postsPerPage", postsPerPage.toString());

    return this.http.get<UserListing>(url, { params: params, headers: this.headers });
  }
  getStudents(page, postsPerPage = 10) {
    const url = this.baseUrl + "admin/GetStudents";
    const params = new HttpParams()
      .set("page", page)
      .set("postsPerPage", postsPerPage.toString());

    return this.http.get<UserListing>(url, { params: params, headers: this.headers });
  }
  createTeacher(id: string) {

    let url = this.baseUrl + "admin/CreateTeacher";
    let body = new CreateTeacherRequest(id);

    return this.http.post(url, body, { headers: this.headers });
  }
}
