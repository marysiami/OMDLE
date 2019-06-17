import { HttpClient, HttpParams } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { CourseListing } from "../models/course-listing.model";
import { LessonListing } from "../models/lesson-listing.model";
import { CreateCourseRequest } from "../models/request/create-course-request.model";
import { CreateLessonRequest } from "../models/request/create-lesson-request.model";
import { UpdateCourseRequest } from "../models/request/update-course-request.model";
import { UpdateLessonRequest } from "../models/request/update-lesson-request.model";
import { BaseService } from "./base.service";
import { SigInCourseRequest } from "../models/request/sign-in-course-request.model";
import { Lesson } from "../models/lesson.model";

@Injectable()
export class CourseService extends BaseService {
  constructor(http: HttpClient, @Inject("BASE_URL") baseUrl: string) {
    super(http, baseUrl);
  }

  //------------------------------------------------------------------------------------------------------------------------------------
  //TUTAJ WSZYSTKIE FUNKCJE DO KURSÓW

  getAllCourses(page, postsPerPage = 10) {
    const url = this.baseUrl + "course/GetAllCourses";
    const params = new HttpParams()
      .set("page", page)
      .set("postsPerPage", postsPerPage.toString());

    return this.http.get<CourseListing>(url, { params: params, headers: this.headers });
  }

  getTeacherCourses(teacherId: string, page, postsPerPage=10) {
    const url = this.baseUrl + "course/GetCoursesFromTeacher";
    const params = new HttpParams()
      .set("teacherId", teacherId)
      .set("page", page)
      .set("postsPerPage", postsPerPage.toString());
    
    return this.http.get<CourseListing>(url, { params: params, headers: this.headers });
  }

  getStudentCourses(studentId: string) {
    const url = this.baseUrl + "course/GetCoursesFromStudent";
    const params = new HttpParams()
      .set("studentId", studentId);    

    return this.http.get<CourseListing>(url, { params: params, headers: this.headers });
  }


  createCourse(title: string, owenrId: string) {

    const url = this.baseUrl + "course/CreateCourse";
    const body = new CreateCourseRequest(title,owenrId);

    return this.http.post(url, body, { headers: this.headers });
  }

  updateCourse(courseId: string, title: string) {

    let url = this.baseUrl + "course/UpdateCourse";
    let body = new UpdateCourseRequest(courseId, title);

    return this.http.put(url, body, { headers: this.headers });
  }

  deleteCourse(courseId) {
    let url = this.baseUrl + "course/DeleteCourse";
    const params = new HttpParams()
      .set("id", courseId);

    return this.http.delete(url, { params: params, headers: this.headers });
  }

  signInCourse(studentId: string, courseId: string) {
    let url = this.baseUrl + "course/SignInCourse";
    const body = new SigInCourseRequest(studentId, courseId);

    return this.http.put(url, body, { headers: this.headers });
  }

  checkOutOfCourse(studentId,courseId) {
    let url = this.baseUrl + "course/CheckOutOfCourse";
    const params = new HttpParams()
      .set("studentId", studentId)
      .set("courseId", courseId);

    return this.http.delete(url, { params: params, headers: this.headers });
  }
  //------------------------------------------------------------------------------------------------------------------------------------
  //TUTAJ WSZYSTKIE FUNKCJE DO LEKCJI


  getLessonsFromCourse(courseId: string, page, postsPerPage = 10) {

    const url = this.baseUrl + "course/GetLessonsFromCourse";
    const params = new HttpParams()
      .set("courseId", courseId)
      .set("page", page)
      .set("postsPerPage", postsPerPage.toString());

    return this.http.get<LessonListing>(url, { params: params, headers: this.headers });
  }

  createLesson(title: string, content:string, date: string, courseId: string) {

    const url = this.baseUrl + "course/CreateLesson";
    const body = new CreateLessonRequest(title,content,date,courseId);

    return this.http.post(url, body, { headers: this.headers });
  }

  updateLesson(title: string, content: string, date: string, courseId:string,) {

    let url = this.baseUrl + "course/UpdateLesson";
    let body = new UpdateLessonRequest(title, content, date, courseId);

    return this.http.put(url, body, { headers: this.headers });
  }
  deleteLesson(lessonId:string) {
    let url = this.baseUrl + "course/DeleteLesson";
    const params = new HttpParams()
      .set("id", lessonId);

    return this.http.delete(url, { params: params, headers: this.headers });
  }

  getLesson(lessonId: string) {
    const url = this.baseUrl + "course/GetLesson";
    const params = new HttpParams()
      .set("lessonId", lessonId);
    return this.http.get<Lesson>(url, { params: params, headers: this.headers });
  }

}
