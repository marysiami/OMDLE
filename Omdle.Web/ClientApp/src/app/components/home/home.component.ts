import { Component } from "@angular/core";
import { AuthService } from "../../services/auth.service";
import { Router } from "@angular/router";



@Component({
  selector: "app-home",
  templateUrl: "./home.component.html",
})
export class HomeComponent {
  admin: boolean;
  student: boolean;
  teacher: boolean;
  id: string;


  constructor(
    public authService: AuthService,
    private router: Router
  ) {}


  ngOnInit() {    
    this.routePage();
    this.id = this.authService.getUserId();
  }
  routePage() {
    if (this.authService.isInRole("Admin")) {
      this.admin = true;
      this.teacher = false;
      this.student = false;
    }
    else if (this.authService.isInRole("Student")) {
      this.admin = false;
      this.teacher = false;
      this.student = true;
    }
    else if (this.authService.isInRole("Teacher")) {
      this.admin = false;
      this.teacher = true;
      this.student = false;
    }
  }
  openAllCourses() {    
    this.router.navigateByUrl(`/courses/${this.id}`);
  }  
  openStudentCourses() {
    this.router.navigateByUrl(`/studentcourses/${this.id}`);  
  }
  openTeacherCourses() {
    this.router.navigateByUrl(`/teachercourses/${this.id}`);
  }
  openUsers() {
    this.router.navigateByUrl(`/users`);
  }
  openStudents() {
    this.router.navigateByUrl(`/students`);
  }
  openTeachers() {
    this.router.navigateByUrl(`/teachers`);
  }

  openForum() {
    this.router.navigateByUrl(`forum`);
  }
  
  
}
