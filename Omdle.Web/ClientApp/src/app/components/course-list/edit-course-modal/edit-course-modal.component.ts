import { Component, Inject, Optional } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material";
import { ActivatedRoute } from "@angular/router";
import { CourseService } from "../../../services/course.service";

@Component({
  selector: "app-edit-course-modal",
  templateUrl: "./edit-course-modal.component.html"
})
export class EditCourseModalComponent {
  newPostForm: FormGroup;
  message: string;
  
  constructor(
    @Optional() @Inject(MAT_DIALOG_DATA) public data: string,
    private route: ActivatedRoute,
    public dialogRef: MatDialogRef<EditCourseModalComponent>,
    private courseService: CourseService,
    private formBuilder: FormBuilder) {
    this.newPostForm = this.formBuilder.group({
      title: [
        "",
        [Validators.required, Validators.pattern(/^[a-zA-Z0-9_.-]*$/), Validators.minLength(1)]
      ]     
    });
  }
  
  submit() {
    this.courseService
      .updateCourse(this.data, this.newPostForm.controls.title.value)
      .subscribe(result => {
          this.dialogRef.close("ok");
        },
        error => this.message = "Wystąpił błąd");
  }
}
