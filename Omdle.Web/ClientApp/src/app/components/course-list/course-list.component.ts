import { Component, ViewChild } from "@angular/core";
import { MatDialog, MatSort } from "@angular/material";
import { MatTableDataSource } from "@angular/material/table";
import { CourseListing } from "../../models/course-listing.model";
import { Course } from "../../models/course.model";
import { CourseService } from "../../services/course.service";
import { AuthService } from "./../../services/auth.service";
import { CreateCourseModalComponent } from "./create-course-modal/create-course-modal.component";
import { EditCourseModalComponent } from "./edit-course-modal/edit-course-modal.component";
import { ActivatedRoute } from "@angular/router";
import UserRoles from "../../models/UserRoles.model";

@Component({
  selector: "app-course-list",
  templateUrl: "./course-list.component.html",
  styleUrls: ["./course-list.component.css"]
})
export class CourseListComponent {
  @ViewChild(MatSort)
  sort: MatSort;
  courseListing = new CourseListing(0, []);
  displayedColumns: string[] = ["name", "address", "repliesCount", "button"];
  pageSize = 10;
  dataSource = new MatTableDataSource<Course>();
  id: string;
  disableButton: boolean;
  isSigned: string;

  constructor(
    public authService: AuthService,
    private courseService: CourseService,
    private dialog: MatDialog,
    private route: ActivatedRoute
  ) {
  }

  ngOnInit() {    
    this.id = this.authService.getUserId();
    this.getCourses();
    this.routePage();
  }


  routePage() {

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
    this.courseService.getAllCourses(pageNumber, postsPerPage)
      .subscribe(result => {
        this.courseListing = result;
        // this.dataSource = new MatTableDataSource(result.hospitals);
        console.log(result);
        this.dataSource.data = result.courses;
      });
  }

  signIn(id) {
    this.courseService.signInCourse(this.id, id).subscribe(result => this.getCourses(0, this.pageSize));
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
