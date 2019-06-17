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
  selector: "app-student-course-list",
  templateUrl: "./student-course-list.component.html"
})
export class StudentCourseListComponent {
  @ViewChild(MatSort)
  sort: MatSort;
  courseListing = new CourseListing(0, []);
  isLoggedIn: boolean;
  displayedColumns: string[] = ["title", "button"];
  pageSize = 10;
  dataSource = new MatTableDataSource<Course>();
  id: string;

  constructor(
    private authService: AuthService,
    private courseService: CourseService,
    private dialog: MatDialog,
    private route: ActivatedRoute
  ) {
  }

  ngOnInit() {
    this.isLoggedIn = this.authService.isLoggedIn();
    this.id = this.authService.getUserId();
    this.getCourses();
   
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
        .subscribe(result => this.getCourses());
  }

  getCourses() {
    this.courseService.getStudentCourses(this.id)
      .subscribe(result => {
        this.courseListing = result;
        this.dataSource.data = result.courses;
      });
  }
  deleteCourse(courseid) {
    this.courseService.checkOutOfCourse(this.id, courseid)
      .subscribe();
    this.getCourses();    
  }
    
}
