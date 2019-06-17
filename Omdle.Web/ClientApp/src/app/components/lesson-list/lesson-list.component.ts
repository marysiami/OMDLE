import { Component, ViewChild } from "@angular/core";
import { MatDialog, MatSort } from "@angular/material";
import { MatTableDataSource } from "@angular/material/table";
import { CourseListing } from "../../models/course-listing.model";
import { Course } from "../../models/course.model";
import { CourseService } from "../../services/course.service";
import { AuthService } from "./../../services/auth.service";
import { ActivatedRoute } from "@angular/router";
import UserRoles from "../../models/UserRoles.model";
import { Lesson } from "../../models/lesson.model";
import { LessonListing } from "../../models/lesson-listing.model";
import { CreateLessonModalComponent } from "./create-lesson-modal/create-lesson-modal.component";

@Component({
  selector: "app-lesson-list",
  templateUrl: "./lesson-list.component.html",
  styleUrls: ["./lesson-list.component.css"]
})
export class LessonListComponent {
  @ViewChild(MatSort)
  sort: MatSort;
  lessonListing = new LessonListing(0, [],"");
  displayedColumns: string[] = ["name"];
  pageSize = 10;
  dataSource = new MatTableDataSource<Lesson>();
  disableButton: boolean;
  isSigned: string;
  courseId: string;


  constructor(
    public authService: AuthService,
    private courseService: CourseService,
    private dialog: MatDialog,
    private route: ActivatedRoute
  ) {
  }

  ngOnInit() {
    this.courseId = this.route.snapshot.paramMap.get("courseId");
    this.getLessons();
  }

  routePage() {

  }


  ngAfterViewInit() {
    this.dataSource.sort = this.sort;
  }

  openCreateLessonModal() {
    const dialogRef =
      this.dialog
        .open(CreateLessonModalComponent,
          {
            height: "auto",
            width: "700px",
            data: this.courseId
          })
        .afterClosed()
        .subscribe(result => this.getLessons(0, this.pageSize));
  }

  getLessons(pageNumber = 0, postsPerPage = 10) {
    this.courseService.getLessonsFromCourse(this.courseId,pageNumber, postsPerPage)
      .subscribe(result => {
        this.lessonListing = result;
        // this.dataSource = new MatTableDataSource(result.hospitals);
        this.dataSource.data = result.lessons;
      });
  }

  
  pageChanged(pageEvent) {
    this.getLessons(pageEvent.pageIndex, this.pageSize);
  }
  //deleteCourse(id) {
  //  this.courseService.deleteCourse(id)
  //    .subscribe(result => this.getLessons(0, this.pageSize));
  //}

  //editCourse(id) {
  //  const dialogRef =
  //    this.dialog
  //      .open(EditCourseModalComponent,
  //        {
  //          height: "auto",
  //          width: "400px",
  //          data: id
  //        })
  //      .afterClosed()
  //      .subscribe(result => {
  //        if (result === "ok") {
  //          this.getLessons(0, this.pageSize);
  //        }
  //      });
  //}

 
}
