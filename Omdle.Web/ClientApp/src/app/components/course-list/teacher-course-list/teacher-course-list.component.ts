import { Component, ViewChild } from "@angular/core";
import { MatDialog, MatSort } from "@angular/material";
import { MatTableDataSource } from "@angular/material/table";
import { ActivatedRoute } from "@angular/router";
import { Course } from "../../../models/course.model";
import { CourseListing } from "../../../models/course-listing.model";
import { CourseService } from "../../../services/course.service";
import { AuthService } from "../../../services/auth.service";
import { CreateCourseModalComponent } from "../create-course-modal/create-course-modal.component";
import { EditCourseModalComponent } from "../edit-course-modal/edit-course-modal.component";

@Component({
  selector: "app-teacher-course-list",
  templateUrl: "./teacher-course-list.component.html"
})
export class TeacherCourseListComponent {
  @ViewChild(MatSort)
  sort: MatSort;
  courseListing = new CourseListing(0, []);
  isLoggedIn: boolean;
  displayedColumns: string[] = ["title", "owner", "lessons", "button"];
  pageSize = 10;
  dataSource = new MatTableDataSource<Course>();
  id: string;
    admin: boolean;
    teacher: boolean;
    student: boolean;

  constructor(
    private authService: AuthService,
    private courseService: CourseService,
    private dialog: MatDialog,
    private route: ActivatedRoute
  ) {
  }

  ngOnInit() {
    this.id = this.authService.getUserId();
    this.isLoggedIn = this.authService.isLoggedIn();   
    this.routePage();

    this.getCourses();
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
  ngAfterViewInit() {
    this.dataSource.sort = this.sort;
  }
  openCreateCourseModal() {
    const dialogRef =
      this.dialog
        .open(CreateCourseModalComponent,
          {
            height: "auto",
            width: "400px",
            data: this.id
          })
        .afterClosed()
        .subscribe(result => this.getCourses(0, this.pageSize));
  }

  getCourses(pageNumber = 0, postsPerPage = 10) {
    this.courseService.getTeacherCourses(this.id, pageNumber, postsPerPage)
      .subscribe(result => {
        this.courseListing = result;        
        this.dataSource.data = result.courses;
      });
  }

  pageChanged(pageEvent) {
    this.getCourses(pageEvent.pageIndex, this.pageSize);
  }
  deleteCourse(id) {
    this.courseService.deleteCourse(id)
      .subscribe(result => this.getCourses(0, this.pageSize));
  }

  editCourse(id) {
    const dialogRef =
      this.dialog
        .open(EditCourseModalComponent,
          {
            height: "auto",
            width: "400px",
            data: id
          })
        .afterClosed()
        .subscribe(result => {
          if (result === "ok") {
            this.getCourses(0, this.pageSize);
          }
        });
  }
}
