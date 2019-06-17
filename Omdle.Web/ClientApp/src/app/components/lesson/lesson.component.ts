import { Component } from "@angular/core";
import { AuthService } from "angularx-social-login";
import { CourseService } from "../../services/course.service";
import { MatDialog } from "@angular/material";
import { ActivatedRoute } from "@angular/router";
import { Lesson } from "../../models/lesson.model";

@Component({
  selector: "app-lesson",
  templateUrl: "./lesson.component.html"
})
export class LessonComponent {
  element: Lesson = null;
  id: string;
  name: string;
  constructor(
    public authService: AuthService,
    private courseService: CourseService,
    private route: ActivatedRoute
  ) { }

  ngOnInit() {
    this.id = this.route.snapshot.paramMap.get("lessonId");
    this.courseService.getLesson(this.id)
      .subscribe(result => {
        this.element = result[0];
        //this.element = result;
        console.log(this.element);
      });
  
  }

 }
